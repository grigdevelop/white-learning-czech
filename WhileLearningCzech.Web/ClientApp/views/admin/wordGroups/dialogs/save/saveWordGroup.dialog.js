(function () {
    function saveWordGroupDialog($scope, dialogService, wordGroupService) {
        // subscribe to show event
        dialogService.subscribe("saveWordGroupDialog", onDialogShow);

        $scope.wordGroup = {name: ""};        

        function onDialogShow(data) {
            $scope.wordGroup = data;

            dialogService.showDialog("saveWordGroup-dialog");
        }

        function saveWordGroup() {
            console.log($scope.wordGroup);
            if ($scope.wordGroup.id) {
                wordGroupService.updateWordGroup($scope.wordGroup)
                    .then(function (wg) {
                        dialogService.hideDialog("saveWordGroup-dialog");
                    })
                    .catch(function (error) {
                        dialogService.dialogs.showMessageDialog("Error", error.message);
                    });
            } else {
                wordGroupService.createWordGroup($scope.wordGroup)
                    .then(function (wg) {
                        dialogService.hideDialog("saveWordGroup-dialog");
                    })
                    .catch(function(error) {
                        dialogService.dialogs.showMessageDialog("Error", error.message);
                    });
            }
        }

        $scope.saveWordGroup = saveWordGroup;
    }

    angular.module("learningApp.comp")
        .component("saveWordGroupDialog",
            {
                templateUrl: "views/admin/wordGroups/dialogs/save/saveWordGroup.dialog.html",
                controller: ["$scope", "dialogService","wordGroupService", saveWordGroupDialog]
            });
})();