(function () {


    function authService(requestService, notifyService) {

        var service = {};

        function login(data) {
            return requestService.post("auth/login", data)
                .then(function (result) {
                    requestService.setToken(result.token);
                    return result;
                });
        }

        function validateToken() {
            return requestService.post("auth/validateToken", {})
                .then(function (result) {
                    result.isAuthorized = true;
                    console.log("result: ", result);
                    notifyService.notify("user-state-changed", result);
                })
                .catch(function (error) {
                    notifyService.notify("user-state-changed", { isAuthorized: false });
                    throw error;
                });
        }

        service.login = login;
        service.validateToken = validateToken;
        return service;
    }

    angular.module("learningApp.services")
        .factory("authService", ["requestService", "notifyService", authService]);
})();