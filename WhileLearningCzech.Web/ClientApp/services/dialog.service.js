(function () {


    function dialogService() {

        var service = {
            dialogs: {} // show dialog concrete implementation
        };
        var subscribers = {};

        function showDialog(id) {
            $("#" + id).modal("show");
        }

        function hideDialog(id) {
            $("#" + id).modal("hide");
        }

        function subscribe(name, callback) {
            // every dialog has only one subsriber
            subscribers[name] = callback;
        }

        function publish(name, data) {
            if (subscribers[name]) {
                subscribers[name](data);
            }
        }

        function dialogConcreteInit() {
            service.dialogs.showMessageDialog = function (title, message) {
                publish("ngMessageDialog", { title: title, message: message });
            };
            service.dialogs.confirmDialog = function(title, message, callback) {
                publish("ngConfirmDialog", { title: title, message: message, callback });
            };

            service.dialogs.showSaveWordGroupDialog = function(wordGroup) {
                publish("saveWordGroupDialog", JSON.parse(JSON.stringify(wordGroup)));
            };
            service.dialogs.showSaveWordDialog = function (word) {
                publish("saveWordDialog", JSON.parse(JSON.stringify(word)));
            };
            service.dialogs.showArticleDialog = function (article) {
                publish("articleDialog", JSON.parse(JSON.stringify(article)));
            };
        }

        service.subscribe = subscribe;
        service.publish = publish;
        service.showDialog = showDialog;
        service.hideDialog = hideDialog;

        dialogConcreteInit();
        return service;
    }

    angular.module("learningApp.services")
        .factory("dialogService", [dialogService]);
})();