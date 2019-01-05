(function () {
    function loginCtrl($scope, $state, authService, dialogService) {

        $scope.loginModel = {
            username: "",
            password: ""
        };

        $scope.login = function () {
            authService.login($scope.loginModel)
                .then(function () {
                    $state.go("admin.words");
                })
                .catch(function (error) {
                    dialogService.dialogs.showMessageDialog("Authentication error", error.message);
                });
        };

        $scope.validateToken = function () {
            authService.validateToken()
                .then(function () {
                    dialogService.dialogs.showMessageDialog("Token validation", "Validation successfully");
                })
                .catch(function (error) {
                    dialogService.dialogs.showMessageDialog("Token validation", "Token not validated");
                });
        };
    }

    angular.module("learningApp.ctrl")
        .controller("LoginCtrl", ["$scope", "$state", "authService", "dialogService", loginCtrl]);
})();