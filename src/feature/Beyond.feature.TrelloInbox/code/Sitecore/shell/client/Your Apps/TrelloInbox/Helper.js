var SetAsDone = function () {
    $('.setasdone').click(function () {
        var cardId = $(this).attr("cardId");
        jQuery.ajax({
            type: "GET",
            dataType: "json",
            data: { 'cardId': cardId },
            url: "/api/sitecore/Trello/SetCardAsDone",
            cache: false,
            success: function () {
                window.location.reload(false);
            },
            error: function () {
                
            }
        });
    });
}