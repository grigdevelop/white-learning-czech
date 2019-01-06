(function () {
    function articleDialog($scope, $timeout, dialogService, articleService) {
        // subscribe to show event
        dialogService.subscribe("articleDialog", onDialogShow);

        $scope.article = null;
        $scope.saveWithoutClosing = false;
        $scope.message = false;

        function onDialogShow(data) {
            $scope.article = data;
            dialogService.showDialog("article-dialog");
        }

        function onSaveCompleted() {
            if (!$scope.saveWithoutClosing) {
                dialogService.hideDialog("article-dialog");
            } else {
                $scope.message = "Successfully saved";
                $timeout(function () {
                    $scope.message = false;
                }, 3000);
            }
        }

        $scope.onClose = function () {
            $scope.article = null;
            dialogService.hideDialog("article-dialog");
        };

        $scope.onSubmit = function () {

            if ($scope.article.id) {
                articleService.update($scope.article)
                    .then(function (wg) {
                        onSaveCompleted();
                    })
                    .catch(function (error) {
                        dialogService.dialogs.showMessageDialog("Error", error.message);
                    });
            } else {
                articleService.create($scope.article)
                    .then(function (wg) {
                        onSaveCompleted();
                    })
                    .catch(function (error) {
                        dialogService.dialogs.showMessageDialog("Error", error.message);
                    });
            }
        };

        $scope.contentChanged = function (content) {
            $scope.article.content = content;
        };

    }

    angular.module("learningApp.comp")
        .component("articleDialog",
            {
                templateUrl: "components/dialogs/article/article.dialog.html",
                controller: ["$scope","$timeout", "dialogService", "articleService", articleDialog]
            });
})();