(function () {
    function wordsTableCtrl($scope, wordService, notifyService) {
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

    }

    angular.module("learningApp.comp")
        .component("wordsTable",
            {
                templateUrl: "views/admin/words/tables/words.table.html",
                controller: ["$scope", "wordService", "notifyService", wordsTableCtrl],
                bindings: {
                    groupId: "<"
                }
            });
})();