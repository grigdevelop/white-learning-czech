(function () {
    function wordGroupsCtrl($scope, wordGroupService, dialogService, notifyService) {

        $scope.wordGroups = [];

        var updateSub = notifyService.subscribe("update-wordGroups", loadWordGroups);

        function loadWordGroups() {
            wordGroupService.getWordGroups()
                .then(function (result) {
                    $scope.wordGroups = result;
                });
        }

        function createWordGroup() {
            dialogService.dialogs.showSaveWordGroupDialog({});
        }

        function updateWordGroup(wg) {
            dialogService.dialogs.showSaveWordGroupDialog(wg);
        }

        function deleteWordGroup(wg) {
            dialogService.dialogs.confirmDialog("Are you sure?",
                "Do you confirm to delete " + wg.name,
                function (confirm, dialogClose) {
                    if (confirm) {
                        wordGroupService.deleteWordGroup(wg)
                            .then(function () {
                                dialogClose();
                            });
                    } else {
                        dialogClose();
                    }
                });
        }

        $scope.createWordGroup = createWordGroup;
        $scope.updateWordGroup = updateWordGroup;
        $scope.deleteWordGroup = deleteWordGroup;


        loadWordGroups();

        $scope.$on("$destroy", function () {
            updateSub();
        });
    }


    angular.module("learningApp.ctrl")
        .controller("WordGroupsCtrl", ["$scope", "wordGroupService", "dialogService", "notifyService", wordGroupsCtrl]);

})();