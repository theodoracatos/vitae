declare var jQuery;
declare var $;
declare var Resources;

$(document).ready(function () {
    loadingProcedure();
});

function ajaxCompleted() {
    loadingProcedure();
}

function loadingProcedure() {
    setupDatepicker();
    configureConfirmationModal();
    setRequiredLabel();
    loadCropper();
    loadImageComponent();
    loadFilerUpload();
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
    $('input[type=text], input[type=hidden], input[type=radio], input[type=file], select, textarea').each(function () {
        var req = $(this).attr('data-val-required');
        if (undefined != req) {
            var label = $('label[for="' + $(this).attr('id') + '"]');
            var text = label.text();
            if (text.length > 0 && text.indexOf('*') < 0) {
                label.append('<span style="color:red"> *</span>');
            }
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
    $('#btnRemoveFile').click(function (e) {
        $('.uploadedFile').addClass("d-none");
        $('#About_Vfile_FileName').val('');
    });

    $(':not(.jFiler) > .filer').filer(
        {
            showThumbs: true,
            limit: 1,
            fileMaxSize: 10,
            extensions: ['application/pdf'],
            onSelect: function () {
                $('.uploadedFile').hide();
            },
            onRemove: function () {
                $('.uploadedFile').show();
            },
            captions: {
                button: Resources.SharedResource.Browse,
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