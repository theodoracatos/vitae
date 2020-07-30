/* eslint-disable */

declare var jQuery;
declare var $;
declare var Resources;

$(document).ready(function () {
    startTooltips();
    startRating();
    initializeTagsinput();
});

function startTooltips() {
    /* Tooltips */
    $("body").tooltip({ selector: '[data-toggle=tooltip]', container: 'body' });
}

function startRating() {

    $('.ratingstar').on('change', function () {
        setDirty();
    });

        $(".ratingstar").rating({
            filled: 'fas fa-star fa-2x',
            empty: 'far fa-2x fa-star', 
            stop: 4,
            extendSymbol: function (symbolNr) {
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
     $('.tagsinput, .mytags').tagsinput(
         {
             trimValue: true,
             maxChars: 20
         }
     );
} 

function enableSubmit() {
    $('#submitCaptcha').removeAttr('disabled');
}