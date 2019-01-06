(function () {
    function articlesCtrl($scope, dialogService, articleService, notifyService) {

        $scope.articles = [];
        loadArticles();

        notifyService.subscribe("update-articles", function () {
            loadArticles();
        });

        $scope.create = function () {
            dialogService.dialogs.showArticleDialog({content: ""});
        };

        $scope.delete = function (article) {
            dialogService.dialogs.confirmDialog("Are you sure?",
                "Are you sure to delete this article?",
                function (confirm, closeDialog) {
                    if (confirm) {
                        articleService.deleteArticle(article)
                            .then(function () {
                                closeDialog();
                            });
                    } else {
                        closeDialog();
                    }
                });
        };

        $scope.update = function (article) {
            dialogService.dialogs.showArticleDialog(article);
        };

        function loadArticles() {
            articleService.search()
                .then(function (articles) {
                    $scope.articles = articles;
                });
        }
    }

    angular.module("learningApp.ctrl")
        .controller("ArticlesCtrl", ["$scope", "dialogService", "articleService", "notifyService", articlesCtrl]);

})();