(function () {


    function dialogService() {

        var service = {
            dialogs: {} // show dialog concrete implementation
        };
        var subscribers = {};

        function showDialog(id) {
            $("#" + id).modal("show");
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

        function dialogConreteInit() {
            service.dialogs.showMessageDialog = function (title, message) {
                publish("ngMessageDialog", { title: title, message: message });
            };
        }

        service.subscribe = subscribe;
        service.publish = publish;
        service.showDialog = showDialog;

        dialogConreteInit();
        return service;
    }

    angular.module("learningApp.services")
        .factory("dialogService", [dialogService]);
})();