(function () {
    function headerCtrl($scope, notifyService) {
        $scope.currentUser = { isAuthorized: false };
        notifyService.subscribe("user-state-changed",
            function (e, user) {
                console.log("user", user);
                $scope.currentUser.isAuthorized = user.isAuthorized;
                $scope.currentUser.username = user.username;
            });
    }

    angular.module("learningApp.comp")
        .component("ngHeader",
            {
                templateUrl: "components/header/header.view.html",
                controller: ["$scope", "notifyService", headerCtrl]
            });
})();