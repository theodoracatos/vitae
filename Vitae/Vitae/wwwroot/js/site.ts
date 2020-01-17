declare var jQuery;
declare var $;

$(document).ready(function () {
    $('.datetimepicker').datepicker(
        {
            format: 'dd.mm.yyyy',
            autoHide: true,
            endDate: new Date()
        }
    );

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
});