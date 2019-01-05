(function () {
    function wordsCtrl($scope, dialogService) {

        $scope.groupId = null;

        function groupChanged(groupId) {
            $scope.groupId = parseInt(groupId);
        }

        function addWord() {
            dialogService.dialogs.showSaveWordDialog({wordGroupId: $scope.groupId});
        }

        $scope.groupChanged = groupChanged;
        $scope.addWord = addWord;

    }

    angular.module("learningApp.ctrl")
        .controller("WordsCtrl", ["$scope","dialogService", wordsCtrl]);

})();