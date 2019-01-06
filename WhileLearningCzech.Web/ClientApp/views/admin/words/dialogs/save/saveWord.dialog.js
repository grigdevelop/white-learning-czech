(function () {
    function saveWordDialog($scope, dialogService, wordService) {
        // subscribe to show event
        dialogService.subscribe("saveWordDialog", onDialogShow);

        $scope.word = {
            english: "",
            czech: "",
            description: "",
            wordGroupId: null
        };

        function onDialogShow(data) {
            $scope.word = data;

            dialogService.showDialog("saveWord-dialog");
        }

        function saveWord() {
            console.log($scope.word);
            if ($scope.word.id) {
                wordService.updateWord($scope.word)
                    .then(function (wg) {
                        dialogService.hideDialog("saveWord-dialog");
                    })
                    .catch(function (error) {
                        dialogService.dialogs.showMessageDialog("Error", error.message);
                    });
            } else {
                wordService.createWord($scope.word)
                    .then(function (wg) {
                        dialogService.hideDialog("saveWord-dialog");
                    })
                    .catch(function (error) {
                        dialogService.dialogs.showMessageDialog("Error", error.message);
                    });
            }
        }

        function groupChanged(groupId) {
            $scope.word.wordGroupId = groupId;
        }

        $scope.groupChanged = groupChanged;
        $scope.saveWord = saveWord;
    }

    angular.module("learningApp.comp")
        .component("saveWordDialog",
            {
                templateUrl: "views/admin/words/dialogs/save/saveWord.dialog.html",
                controller: ["$scope", "dialogService", "wordService", saveWordDialog]
            });
})();