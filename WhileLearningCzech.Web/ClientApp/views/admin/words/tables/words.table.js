(function () {
    function wordsTableCtrl($scope, wordService, notifyService, dialogService) {
        var ctrl = this;
        $scope.words = [];
        $scope.groupId = null;

        ctrl.$onChanges = function (changes) {

            if (changes.groupId) {
                if (!changes.groupId.currentValue) {
                    changes.groupId.currentValue = null;
                }

                $scope.groupId = changes.groupId.currentValue;
                loadWords();
            }
        };

        notifyService.subscribe("update-words",
            function () {
                loadWords();
            });

        function loadWords() {
            wordService.getWords({ groupId: $scope.groupId })
                .then(words => {
                    $scope.words = words;
                });
        }

        function updateWord(word) {
            dialogService.dialogs.showSaveWordDialog(word);
        }

        function deleteWord(word){
            dialogService.dialogs.confirmDialog("Are you sure?",
                "Do you confirm to delete " + word.czech,
                function (confirm, dialogClose) {
                    if (confirm) {
                        wordService.deleteWord(word)
                            .then(function () {
                                dialogClose();
                            });
                    } else {
                        dialogClose();
                    }
                });
        }

        $scope.updateWord = updateWord;
        $scope.deleteWord = deleteWord;

    }

    angular.module("learningApp.comp")
        .component("wordsTable",
            {
                templateUrl: "views/admin/words/tables/words.table.html",
                controller: ["$scope", "wordService", "notifyService","dialogService", wordsTableCtrl],
                bindings: {
                    groupId: "<"
                }
            });
})();