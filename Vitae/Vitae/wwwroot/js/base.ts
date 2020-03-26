declare var jQuery;
declare var $;
declare var Resources;

$(document).ready(function () {
    startTooltips();
    startRating();
    initializeTagsinput();
});

function startTooltips() {
    $('[data-toggle="tooltip"]').tooltip();
}

function startRating() {
        $(".ratingstar").rating({
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