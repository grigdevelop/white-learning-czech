(function () {


    function notifyService($rootScope) {
        var service = {};

        function subscribe(name, callback) {
            $rootScope.$on(name, callback);
        }

        function notify(name, data) {
            $rootScope.$emit(name, data);
        }

        service.subscribe = subscribe;
        service.notify = notify;
        return service;
    }

    angular.module("learningApp.services")
        .factory("notifyService", ["$rootScope", notifyService]);
})();