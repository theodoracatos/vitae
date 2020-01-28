declare var jQuery;
declare var $;

$(document).ready(function () {
    loadingProcedure();
});

function ajaxCompleted() {
    loadingProcedure();
}


function loadingProcedure() {
    /* Datetime */
    $('.datetimepicker').datepicker(
        {
            format: 'dd.mm.yyyy',
            autoHide: true,
            endDate: new Date()
        }
    );

    /* Confirmation */
    $('.confirmation').on('click', function () {
        var formaction = $(this).attr('formaction');
        var hasCancel = $(this).hasClass("cancel");
        var formmethod = $('form').attr('method');
        $.confirm({
            title: "Please confirm",
            theme: "bootstrap",
            buttons: {
                Ok: {
                    keys: ['Enter'],
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
                    keys: ['Esc'],
                    action: function () {

                    }
                }
            }
        });
        return false;
    });

    /* Required (*) */
    $('input[type=text], select, input[type=radio]').each(function () {
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