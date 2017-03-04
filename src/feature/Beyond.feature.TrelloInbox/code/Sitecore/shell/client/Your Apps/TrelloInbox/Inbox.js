require.config({
    paths: {
        blockUI: '/sitecore/shell/client/Your Apps/TrelloInbox/jquery.blockUI'
    },
    'shim': {
        'blockUI': ['jquery']
    }
});

define(["sitecore", "blockUI"], function (Sitecore, blockUI) {
    var model = Sitecore.Definitions.Models.ControlModel.extend({
        initialize: function (options) {
            this._super();
            var app = this;
            app.doBlockUI();
            app.set("InboxParam", '');
            app.GetTrelloInbox(app, 'updateddate', 'DESC');
            app.initSort(app);
            $.unblockUI();
        }
        ,
        GetTrelloInbox: function (app, sortfield, sortorder) {
            jQuery.ajax({
                type: "GET",
                dataType: "json",
                data: { 'sortfield': sortfield, 'sortorder': sortorder },
                url: "/api/sitecore/Trello/getUserTrelloInbox",
                cache: false,
                success: function (data) {
                    app.set("InboxParam", data);
                },
                error: function () {
                    console.log("There was an error in GetTrelloInbox() function!");
                }
            });
        },
        doBlockUI: function () {
            $.blockUI({
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
        }
        ,
        initSort: function (app) {
            var app = this;
            $(document).ready(function () {
                $('.sortable').click(function () {
                    var sortField = $(this).data("fieldname");
                    var sortOrder = $(this).data("fieldsortorder");
                    $(this).data("fieldsortorder", sortOrder == "ASC" ? "DESC" : "ASC");

                    app.GetTrelloInbox(app, sortField, sortOrder);
                });
            });
        }
    });

    var view = Sitecore.Definitions.Views.ControlView.extend({
        initialize: function (options) {
            this._super();
        }
    });



    Sitecore.Factories.createComponent("TrelloInboxDataList", model, view, ".sc-listcontrol");

});
