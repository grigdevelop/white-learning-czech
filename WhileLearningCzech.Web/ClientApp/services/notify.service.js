(function () {


    function notifyService($rootScope) {
        var service = {};

        function subscribe(name, callback) {
            return $rootScope.$on(name, callback);
        }

        function notify(name, data) {
            $rootScope.$emit(name, data);
        }

        function unSubscribe(name, callback) {

        };

        service.subscribe = subscribe;
        service.notify = notify;
        return service;
    }

    angular.module("learningApp.services")
        .factory("notifyService", ["$rootScope", notifyService]);
})();