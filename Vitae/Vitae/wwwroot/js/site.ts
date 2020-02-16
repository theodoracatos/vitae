declare var jQuery;
declare var $;
declare var Resources;

(function ($) {
    addJQueryValidators();
    startRating();
})(jQuery);

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

$(document).ready(function () {
    loadingProcedure();
    setupSbAdmin();
});

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

function ajaxCompleted() {
    loadingProcedure();
    startRating();
    resetFormValidator('form');
}

function loadingProcedure() {
    setupDatepicker();
    configureConfirmationModal();
    setRequiredLabel();
    loadCropper();
    loadImageComponent();
    loadFilerUpload();
    initializeTooltips();
    initializeTagsinput();
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
        var hasCancel = $(this).hasClass("cancel");
        var formmethod = $('form').attr('method');
        $.confirm({
            title: Resources.SharedResource.PleaseConfirm,
            theme: "bootstrap",
            buttons: {
                Ok: {
                    keys: ['enter'],
                    text: Resources.SharedResource.Ok,
                    action: function () {
                        if (formmethod == 'post') {
                            if (hasCancel) {
                                $('form').data("validator").cancelSubmit = true;
                                var validator = $("form").validate();
                                validator.resetForm();
                            }
                            $('form').attr('action', formaction).submit();

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
            },
            onRemove: function () {
                $('.uploadedFile').show();
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

function startRating() {
    $(".rating").rating({
        filled: 'fas fa-star fa-2x',
        empty: 'far fa-2x fa-star',
        stop: 4,
        extendSymbol: function (symbolNr) {
            $(this).on('rating.rateclick', function (e, currentRate) {
               
            })

            $(this).tooltip({
                container: 'body',
                placement: 'bottom',
                title: function () {
                    var text = '';
                    var ratingNode = $(this).parent().parent().find('.rating');
                    var isReadonly = $(ratingNode).attr("readonly") != null;
                    var finalRate = $(ratingNode).val();
                    symbolNr = isReadonly ? Math.round(finalRate) : symbolNr;

                    switch (symbolNr) {
                        case 1: { text = Resources.SharedResource.KnowledgeBasic; break; }
                        case 2: { text = Resources.SharedResource.KnowledgeBusinessFluent; break; }
                        case 3: { text = Resources.SharedResource.KnowledgeFluent; break; }
                        case 4: { text = Resources.SharedResource.KnowledgeNative; break; }
                    }

                    if (isReadonly) {
                        text += ' (' + Math.round(finalRate * 100) / 100 + ')'
                    }

                    return text;
                }
            });
        }
    });
}

function initializeTagsinput() {
    $('.tagsinput').tagsinput(
        {
            trimValue: true,
            maxChars: 50
        }
    );
}