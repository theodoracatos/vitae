/* eslint-disable */

declare let Chart;
declare let autosize;

$(document).ready(function () {
    addJQueryValidators();
    loadingProcedure();
    setupSbAdmin();
    setupDirtyForms(false);
});

function ajaxCompleted(dirtyignore) {
    loadingProcedure();
    startRating();
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
        $('form').dirtyForms({
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

        if (ev.type === 'dirty') {
            $submitResetButtons.removeAttr('disabled');
            $languageSelect.attr('disabled', 'disabled');
        }
        else {
            $submitResetButtons.attr('disabled', 'disabled');
            $languageSelect.removeAttr('disabled');
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

function drawRadar(chartId, title, labels, dataset, color) {
    var canvas: any = document.getElementById(chartId);
    var ctx = canvas.getContext("2d");
    var bgColor = getColor(color, true);

    var data = {
        labels: labels,
        datasets: [{
            backgroundColor: [
                bgColor
            ],
            data: dataset
        }]
    };

    var options = {
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

function drawChart(chartId, title, xLabels, yValues, color) {
    var canvas: any = document.getElementById(chartId);
    var ctx = canvas.getContext("2d");
    var bgColor = getColor(color, true);
    var data = {
        labels: xLabels,
        datasets: [{
        fill: true,
        lineTension: 0.1,
        backgroundColor: [
            bgColor
        ],
      // borderColor: bgColor,
        borderWidth: 1,
            data: yValues
         }]};

    var options = {
        maintainAspectRatio: false,
        legend: {
            display: false
        },
        title: {
            display: true,
            text: title
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

function getColor(color, isBgColor) {
    if (isBgColor) {
        return color == 'gold' ? 'rgba(183, 158, 103, 0.5)' : 'red' ? 'rgba(239, 110, 126, 0.5)' : '';
    }
    else {
        color == 'gold' ? 'rgba(183, 158, 103, 1)' : 'red' ? 'rgba(239, 110, 126, 1)' : '';
    }
}

function addJQueryValidators() {
    $.validator.addMethod('agelimit', function (value, element, params) {
        var date = new Date();
        var year = params[1];

        return (parseInt(value) < date.getUTCFullYear() - year);
    });

    $.validator.unobtrusive.adapters.add('agelimit', ['year'], function (options) {
        var element = $(options.form).find('select#Person_Birthday_Year')[0];

        options.rules['agelimit'] = [element, parseInt(options.params['year'])];
        options.messages['agelimit'] = options.message;
    });
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
}

function setupAutosize() {
    autosize($('textarea'));
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