/* eslint-disable */
// Unobtrusive Ajax support library for jQuery
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// @version v3.2.6
// 
// Microsoft grants you the right to use these script files for the sole
// purpose of either: (i) interacting through your browser with the Microsoft
// website or online service, subject to the applicable licensing or use
// terms; or (ii) using the files as included with a Microsoft product subject
// to that product's license terms. Microsoft reserves all other rights to the
// files not expressly granted by Microsoft, whether by implication, estoppel
// or otherwise. Insofar as a script file is dual licensed under GPL,
// Microsoft neither took the code under GPL nor distributes it thereunder but
// under the terms set out in this paragraph. All notices and licenses
// below are for informational purposes only.

/*jslint white: true, browser: true, onevar: true, undef: true, nomen: true, eqeqeq: true, plusplus: true, bitwise: true, regexp: true, newcap: true, immed: true, strict: false */
/*global window: false, jQuery: false */


$(document).ready(function () {
    var data_click = "unobtrusiveAjaxClick",
        data_target = "unobtrusiveAjaxClickTarget",
        data_validation = "unobtrusiveValidation";

    function getFunction(code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }
        if (typeof fn === "function") {
            return fn;
        }
        argNames.push(code);
        return Function.constructor.apply(null, argNames);
    }

    function isMethodProxySafe(method) {
        return method === "GET" || method === "POST";
    }

    function asyncOnBeforeSend(xhr, method) {
        if (!isMethodProxySafe(method)) {
            xhr.setRequestHeader("X-HTTP-Method-Override", method);
        }
    }

    function asyncOnSuccess(element, data, contentType) {
        var mode;

        if (contentType.indexOf("application/x-javascript") !== -1) {
            return;
        }

        mode = (element.getAttribute("data-ajax-mode") || "").toUpperCase();
        $(element.getAttribute("data-ajax-update")).each(function (i, update) {
            switch (mode) {
                case "BEFORE":
                    $(update).prepend(data);
                    break;
                case "AFTER":
                    $(update).append(data);
                    break;
                case "REPLACE-WITH":
                    $(update).replaceWith(data);
                    break;
                default:
                    $(update).html(data);
                    break;
            }
        });

        // Remove data
        $('form').removeData(data_click);
        $('form').removeData(data_target);
    }

    function asyncRequest(element, options) {
        var confirm, loading, method, duration, dirtyignore;
        dirtyignore = element.getAttribute("data-dirty-ignore");
        confirm = element.getAttribute("data-ajax-confirm");
        if (confirm && !window.confirm(confirm)) {
            return;
        }

        loading = $(element.getAttribute("data-ajax-loading"));
        duration = parseInt(element.getAttribute("data-ajax-loading-duration"), 10) || 0;

        $.extend(options, {
            type: element.getAttribute("data-ajax-method") || undefined,
            url: element.getAttribute("data-ajax-url") || undefined,
            cache: (element.getAttribute("data-ajax-cache") || "").toLowerCase() === "true",
            beforeSend: function (xhr) {
                var result;
                asyncOnBeforeSend(xhr, method);
                result = getFunction(element.getAttribute("data-ajax-begin"), ["xhr"]).apply(element, arguments);
                if (result !== false) {
                    loading.removeClass('d-none');
                }
                return result;
            },
            complete: function () {
                loading.addClass('d-none');
                getFunction(element.getAttribute("data-ajax-complete"), ["xhr", "status"]).apply(element, arguments);
            },
            success: function (data, status, xhr) {
                asyncOnSuccess(element, data, xhr.getResponseHeader("Content-Type") || "text/html");
                getFunction(element.getAttribute("data-ajax-success"), ["data", "status", "xhr"]).apply(element, arguments);
                ajaxCompleted(dirtyignore); // QVS
                loading.addClass('d-none');
            },
            error: function () {
                getFunction(element.getAttribute("data-ajax-failure"), ["xhr", "status", "error"]).apply(element, arguments);
            }
        });

        options.data.push({ name: "X-Requested-With", value: "XMLHttpRequest" });

        method = options.type.toUpperCase();
        if (!isMethodProxySafe(method)) {
            options.type = "POST";
            options.data.push({ name: "X-HTTP-Method-Override", value: method });
        }

        // change here:
        // Check for a Form POST with enctype=multipart/form-data
        // add the input file that were not previously included in the serializeArray()
        // set processData and contentType to false
        var $element = $(element);
        if ($element.is("form") && $element.attr("enctype") === "multipart/form-data") {
            var formdata = new FormData();
            $.each(options.data, function (i, v) {
                formdata.append(v.name, v.value);
            });
            $("input[type=file]", $element).each(function () {
                var file = this;
                $.each(file.files, function (n, v) {
                    formdata.append(file.name, v);
                });
            });
            $.extend(options, {
                processData: false,
                contentType: false,
                data: formdata
            });
        }
        // end change

        $.ajax(options);
    }

    function validate(form) {
        var validationInfo = $(form).data(data_validation);
        return !validationInfo || !validationInfo.validate || validationInfo.validate();
    }

    $(document).on("click", "a[data-ajax=true]", function (evt) {
        evt.preventDefault();
        asyncRequest(this, {
            url: this.href,
            type: "GET",
            data: []
        });
    });

    $(document).on("click", "form[data-ajax=true] input[type=image]", function (evt) {
        var name = evt.target.name,
            target = $(evt.target),
            form = $(target.parents("form")[0]),
            offset = target.offset();

        form.data(data_click, [
            { name: name + ".x", value: Math.round(evt.pageX - offset.left) },
            { name: name + ".y", value: Math.round(evt.pageY - offset.top) }
        ]);

        setTimeout(function () {
            form.removeData(data_click);
        }, 0);
    });

    //new code from QVS
    $(document).on("click", "button[data-ajax=true], input[data-ajax=true]", function (evt) {
        var name = evt.currentTarget.name,
            target = $(evt.currentTarget),
            form = $(target.parents("form")[0]);

        $(form).attr('data-ajax-update', $(this).attr('data-ajax-update'));
        $(form).attr('data-ajax', 'true');
        $(form).attr('data-dirty-ignore', $(this).hasClass('dirty-ignore'));
        $(form).attr('data-ajax-loading', $(this).attr('data-ajax-loading'));
        $(form).attr('data-ajax-loading-duration', $(this).attr('data-ajax-loading-duration'));

        form.data(data_click, name ? [{ name: name, value: evt.currentTarget.value }] : []);
        form.data(data_target, target);
    });
    //end new code

    $(document).on("submit", "form[data-ajax=true]", function (evt) {
        var clickInfo = $(this).data(data_click) || [],
            clickTarget = $(this).data(data_target),
            isCancel = clickTarget && (clickTarget.hasClass("cancel") || clickTarget.attr('formnovalidate') !== undefined);
        evt.preventDefault();
        $(this).attr('data-ajax', 'false');
        if (!isCancel && !validate(this)) {
            return;
        }
        //new code from https://github.com/aspnet/jquery-ajax-unobtrusive/issues/37
        if (clickTarget !== null) {
            var action = this.action;
            if (clickTarget[0].formAction) {
                action = clickTarget[0].formAction;
            }
            //end new code
            asyncRequest(this, {
                url: action, // replaced this.action by action
                type: this.method || "GET",
                data: clickInfo.concat($(this).serializeArray())
            });
        }
    });
});