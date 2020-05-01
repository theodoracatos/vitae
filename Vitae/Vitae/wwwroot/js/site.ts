/* eslint-disable */

declare let Chart;
declare let autosize;

$(document).ready(function () {
    addJQueryValidators();
    loadingProcedure();
    setupSbAdmin();
    displayScrollButton();
    setupDirtyForms(false);
});

function ajaxCompleted(dirtyignore) {
    addJQueryValidators();
    loadingProcedure();
    startRating();
    showDynamicContent();
    resetFormValidator('form');
    if (dirtyignore === 'false') {
        setupDirtyForms(true);
    }
}

function setupDirtyForms(asyncCall) {
    if (asyncCall) {
        setDirty();
    }
    else {
        $('form:not(.nodirty)').dirtyForms({
            ignoreSelector: '.dirty-ignore',
            helpers:
                [
                    {
                        isDirty: function ($node, index) {
                            if ($node.is('form')) {
                                return $node.hasClass('mydirty');
                            }
                        }
                    }
                ]
        });
    }
    $('form').on('dirty.dirtyforms clean.dirtyforms', function (ev) {  
        let $submitResetButtons = $('form').find('[type="submit"]');
        let $languageSelect = $('form').find('#languageSelect select');
        let $hasUnsafedChanges = $('form').find('#HasUnsafedChanges');

        if (ev.type === 'dirty') {
            $submitResetButtons.removeAttr('disabled');
            $languageSelect.attr('disabled', 'disabled');
        }
        else if ($hasUnsafedChanges.val() != "True") {
            $submitResetButtons.attr('disabled', 'disabled');
            $languageSelect.removeAttr('disabled');
        }
    });

    $(document).bind('dirty.dirtyforms', function (event) {
        // Access the form that triggered the event
        var $form = $(event.target);
    });
}

function showDynamicContent() {
    $('.footer-content').fadeIn(2000);
    $('.footer-content').removeClass('d-none');
}

function displayScrollButton() {
    // Smooth scrolling using jQuery easing
    $('a.js-scroll-trigger').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html, body').animate({
                    scrollTop: target.offset().top
                }, 1000, "easeInOutExpo");
                return false;
            }
        }
    });

    // Scroll to top button appear
    $(document).scroll(function () {
        var scrollDistance = $(this).scrollTop();
        if (scrollDistance > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });
}

function setHiddenLanguageSelect() {
    let $languageSelect = $('form').find('#languageSelect select');
    let $languageSelectHidden = $('form').find('#languageSelect input[type="hidden"]');

    $languageSelectHidden.val($languageSelect.val());
}

function setDirty() {
    let $submitResetButtons = $('form').find('[type="submit"]');
    let $languageSelect = $('form').find('#languageSelect select');

    $('form').addClass('mydirty');
    $submitResetButtons.removeAttr('disabled');
    $languageSelect.attr('disabled', 'disabled');
}

function drawRadar(chartId, title, xLabels, yValues) {
    let canvas: any = document.getElementById(chartId);
    let ctx = canvas.getContext("2d");
    let bgColor = getColor(0);

    var data = {
        labels: xLabels,
        datasets: [{
            backgroundColor: [
                bgColor
            ],
            data: yValues
        }]
    };

    let options = {
        legend: {
            position: 'top',
            display: false
        },
        title: {
            display: true,
            text: title
        },
        scale: {
            ticks: {
                beginAtZero: true,
                stepSize: 1
            }
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'radar',
    }); 
}

function drawChart(chartId, title, xLabels, yValues, yLabels) {
    let canvas: any = document.getElementById(chartId);
    let ctx = canvas.getContext("2d");

    var datasets = [];
    $(yValues).each(function (index) {
        let bgColor = getColor(index);
        let d = {
            label: yLabels[index],
            fill: true,
            lineTension: 0.1,
            backgroundColor: [
                bgColor
            ],
            borderColor: bgColor,
            borderWidth: 1,
            data: $(this)
        }
        datasets.push(d);
    });

    let data = {
        labels: xLabels,
        datasets: datasets
    };

    let options = {
        responsive: true,
        maintainAspectRatio: false,
        legend: {
            display: true
        },
        title: {
            display: true,
            text: title
        },
        hover: {
            mode: 'nearest',
            intersect: true
        },
        scales: {
            yAxes: [{
                ticks: {
                    min: 0,
                    beginAtZero: true,
                    stepSize: 1
                },
                gridLines: {
                    display: true,
                    lineWidth: 1,
                    //color: 'rgba(208, 187, 149, 0.75)'
                }
            }],
            xAxes: [{
                ticks: {
                    min: 0,
                    beginAtZero: true
                }, 
                gridLines: {
                    display: true,
                    //color: 'rgba(208, 187, 149, 0.15)'
                }
            }]
        }
    };

    new Chart(ctx, {
        options: options,
        data: data,
        type: 'line',
    });  
}

function getColor(index) {
    switch (index % 10) {
        case 0: default: {
            return 'rgba(63, 81, 181, 0.5)';
        }
        case 1: {
            return 'rgba(76, 175, 80, 0.5)';
        }
        case 2: {
            return 'rgba(255, 152, 0, 0.5)';
        }
        case 3: {
            return 'rgba(244, 67, 54, 0.5)';
        }
        case 4: {
            return 'rgba(255, 235, 59, 0.5)';
        }
        case 5: {
            return 'rgba(103, 58, 183, 0.5)';
        }
        case 6: {
            return 'rgba(233, 30, 99, 0.5)';
        }
        case 7: {
            return 'rgba(0, 188, 212,0.5)';
        }
        case 8: {
            return 'rgba(96, 125, 139,0.5)';
        }
        case 9: {
            return 'rgba(121, 85, 72,0.5)';
        }
    }
}

function addJQueryValidators() {
    $.validator.unobtrusive.adapters.add('dategreaterthan', [], function (options) {
        options.rules['dategreaterthan'] = {};
        options.messages['dategreaterthan'] = options.message;
    });

    $.validator.addMethod('dategreaterthan', function (value, element, params) {
        var baseName = $(element).attr('name').substr(0, $(element).attr('name').indexOf('.'));

        var startYear = baseName + "." + $(element).data('val-dategreaterthan-startyear');
        var startMonth = baseName + "." + $(element).data('val-dategreaterthan-startmonth')
        var startDay = baseName + "." + $(element).data('val-dategreaterthan-startday')
        var endMonth = baseName + "." + $(element).data('val-dategreaterthan-endmonth')
        var endDay = baseName + "." + $(element).data('val-dategreaterthan-endday')

        var startDate = new Date($("select[name='" + startYear + "']").val(), $("select[name='" + startMonth + "']").val(), $("select[name='" + startDay + "']").val() || 1);
        var endDate = new Date($(element).val(), $("select[name='" + endMonth + "']").val(), $("select[name='" + endDay + "']").val() || 1);

        // Compare dates
        return startDate <= endDate;
    });

    var $form = $("form");
    $form.unbind();
    $form.data("validator", null);
    $.validator.unobtrusive.parse($form);

    if ($form.data("unobtrusiveValidation") != null) {
        $form.validate($form.data("unobtrusiveValidation").options);
    }
}

function setupSbAdmin() {
    // Add active state to sidbar nav links
    var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
    $("#layoutSidenav_nav .sb-sidenav a.nav-link").each(function () {
        if (this.href === path) {
            $(this).addClass("active");
        }
    });

    // Toggle the side navigation
    $("#sidebarToggle").on("click", function (e) {
        e.preventDefault();
        $("body").toggleClass("sb-sidenav-toggled");
    });

    $('#dataTable').DataTable();
}

function initSelectPicker() {
    $('.selectpicker').selectpicker(
        {
            liveSearch: true,
            container: '.card-body',
            maxOptions: 7,
            noneResultsText: Resources.SharedResource.NoResults
        }
    );
    $('.dropdown-toggle').attr('title', '');
}

function loadingProcedure() {
    setupDatepicker();
    setupAutosize();
    configureConfirmationModal();
    setRequiredLabel();
    loadCropper();
    loadImageComponent();
    loadFilerUpload();
    initializeTooltips();
    initializeTagsinput();
    loadDisabledLinkMessage();
    assignCollapser();
    loadDynamicContent();
}

function setupAutosize() {
    autosize($('textarea'));
}

function loadDynamicContent() {
    let lastScrollTop = 0;
    $(window).scroll(function (event) {
        let st = $(this).scrollTop();

        if (st > lastScrollTop) {
            // downscroll code
            $(".partial").each(function (index) {
                if (index == 0 && !$(this).hasClass('clicked')) {
                    if ($(this).visible()) {
                        $(this).trigger('click');
                        $(this).addClass('clicked');
                    }
                }
            });

        } else {
            // upscroll code
        }
        lastScrollTop = st;
    });
}

function assignCollapser() {
    $('a[data-toggle="collapse"]').click(function () {
        $(this).find('input[type="hidden"]').val($(this).find('input[type="hidden"]').val().toLowerCase() === "true" ? "false" : "true");
    });
}

function loadDisabledLinkMessage() {
    $('a.nav-link.disabled').on('click', function () {
        $.alert({
            title: Resources.SharedResource.AccessDenied,
            content: Resources.SharedResource.AccessDeniedMessage,
        });
    });
}

function resetFormValidator(formId) {
    $(formId).removeData('validator');
    $(formId).removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(formId);
}

function initializeTooltips() {
    /* Tooltips */
    $("body").tooltip({ selector: '[data-toggle=tooltip]', container: 'body' });
}

function setupDatepicker() {
    $('.datetimepicker').datepicker(
        {
            format: 'dd.mm.yyyy',
            autoHide: true,
            endDate: new Date()
        }
    );
}

function setRequiredLabel() {
    $('input[type=text], input[type=password], input[type=email], input[type=hidden], input[type=radio], input[type=file], select, textarea').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            $('label[for="' + $(this).attr('id') + '"]').each(function () {
                var text = $(this).text();
                if (text.length > 0 && text.indexOf('*') < 0) {
                    $(this).append('<span style="color:red"> *</span>');
                }
            });
        }
    });
}

function configureConfirmationModal() {
    $('.confirmation').on('click', function () {
        var formaction = $(this).attr('formaction');
        var content = $(this).data('confirm-text');
        var hasCancel = $(this).hasClass("cancel");
        var formmethod = $('form').attr('method');
        var sender = $(this);
        $.confirm({
            content: content,
            title: Resources.SharedResource.PleaseConfirm,
            theme: "bootstrap",
            buttons: {
                Ok: {
                    keys: ['enter'],
                    text: Resources.SharedResource.Ok,
                    action: function () {
                        if (formmethod === 'post') {
                            if (hasCancel) {
                                $('form').data("validator").cancelSubmit = true;
                                var validator = $("form").validate();
                                validator.resetForm();
                            }
  
                            $(sender).unbind('click');
                            $(sender).click();

                            // Close modal dialog
                            if (($('.modal').data('bs.modal') || {})._isShown) {
                                $('.modal').modal('toggle');
                            }
                        }
                        else {
                            window.location.href = formaction;
                        }
                    }
                },
                Cancel: {
                    keys: ['esc'],
                    text: Resources.SharedResource.Cancel,
                    action: function () {

                    }
                }
            }
        });
        return false;
    });

}

function loadCropper() {
    const WIDTH = 300;
    const HEIGHT = 300;

    $("#imgPreview").cropper('destroy');
    $("#imgPreview").cropper({
        aspectRatio: WIDTH / HEIGHT,
        initialAspectRatio: WIDTH / HEIGHT,
        autoCropArea: 1,
        viewMode: 1,
        restore: false,
        guides: true,
        scalable: false,
        movable: false,
        zoomable: true,
        dragMode: 'move',
        minContainerWidth: WIDTH,
        minContainerHeight: WIDTH,
        cropBoxMovable: true,
        cropBoxResizable: false,
        imageSmoothingQuality: 'high',
        imageSmoothingEnabled: false,
        ready: function () {
            $('.imgPhoto').val($("#imgPreview").cropper('getCroppedCanvas',
                {
                    width: WIDTH, // resize the cropped area
                    height: HEIGHT,
                    imageSmoothingQuality: 'high',
                    imageSmoothingEnabled: false,
                }
            ).toDataURL());
        },
        cropmove: function () {
            $('.imgPhoto').val($("#imgPreview").cropper('getCroppedCanvas',
                {
                    width: WIDTH, // resize the cropped area
                    height: HEIGHT,
                    imageSmoothingQuality: 'high',
                    imageSmoothingEnabled: false,
                }
            ).toDataURL());
        }
    });
}

function loadImageComponent() {
    $("#imgInput").change(function () {
        if (this.files && this.files[0]) {
            var file = this.files[0];
            if (file.name.toLowerCase().match(".jpg$|.jpeg$|.png$")) {
                $('#imgFormFileName').val(file.name.toLowerCase());
                readImage(file);
            }
        }
    });

    // Images
    $("#btnRemoveImage").click(function (e) {
        $('#divPreview').addClass("d-none");
        $('#divPreview').removeClass("d-flex");
        $('#divSelector').removeClass("d-none");
        $("#imgInput").val('');
        $("#imgPreview").attr('src', '');
        $(".imgPhoto").val('');
        $("#imgFormFileName").val('');

        setDirty();
    });
}

function readImage(file) {
    var reader = new FileReader();
    reader.onload = function (e: any) {
        $('#imgPreview').attr('src', e.target.result);
        $('.imgPhoto').val(e.target.result);

        loadCropper();
    };
    reader.readAsDataURL(file);
    $('#divPreview').removeClass("d-none");
    $('#divSelector').addClass("d-none");
    setDirty();
}

function loadFilerUpload() {
    $(':not(.jFiler) > .filer').filer(
        {
            showThumbs: true,
            limit: 1,
            fileMaxSize: 10,
            icon: '<i class="fas fa-plus"></i>',
            extensions: ['application/pdf'],
            onSelect: function () {
                $('.uploadedFile').hide();

                setDirty();
            },
            onRemove: function () {
                $('.uploadedFile').show();

                setDirty();
            },
            captions: {
                button: Resources.SharedResource.Browse + "...",
                feedback: Resources.SharedResource.ChooseFile,
                feedback2: Resources.SharedResource.ChosenFile,
                drop: Resources.SharedResource.DropFile,
                removeConfirmation: Resources.SharedResource.RemoveFileConfirmation + "?",
                errors: {
                    filesLimit: Resources.SharedResource.Only + " {{fi-limit}} " + Resources.SharedResource.AllowedFiles,
                    filesType: Resources.SharedResource.OnlyPdf,
                    filesSize: "{{fi-name}} " + Resources.SharedResource.FileTooLarge + " {{fi-fileMaxSize}} MB.",
                    filesSizeAll: Resources.SharedResource.FilesTooLarge + "! " + Resources.SharedResource.PleaseUpload + " {{fi-maxSize}} MB.",
                    folderUpload: Resources.SharedResource.FolderUpload
                }
            },
            templates: {
                removeConfirmation: false,

            },
            dialogs: {
                alert: function (text) {
                    $.alert({
                        title: Resources.SharedResource.Warning,
                        content: text,
                    });
                },
                confirm: function (text, callback) {
                    $.confirm({
                        title: Resources.SharedResource.Confirm,
                        content: text,
                        buttons: {
                            Yes: {
                                keys: ['enter'],
                                text: Resources.SharedResource.Yes,
                                action: function() {
                                    callback()
                                }
                            },
                            No: {
                                keys: ['esc'],
                                text: Resources.SharedResource.No,
                                action: function () {
                                    null
                                }
                            }
                        }
                    });
                }
            },
        });
}