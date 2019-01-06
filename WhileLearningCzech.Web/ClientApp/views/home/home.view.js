(function () {
    function homeCtrl($scope, wordGroupService) {
        $scope.wordGroups = [];

        function loadWordGroups() {
            wordGroupService.getWordGroups().then(function(result) {
                $scope.wordGroups = result;
            });
        }


        loadWordGroups();

    }

    angular.module("learningApp.ctrl")
        .controller("HomeCtrl", ["$scope","wordGroupService", homeCtrl]);
})();