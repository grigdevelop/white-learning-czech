(function () {
    function controller($scope, articleService) {

        $scope.articles = [];

        function loadArticles() {
            articleService.search()
                .then(function(articles) {
                    $scope.articles = articles;
                });
        }

        loadArticles();
    }

    angular.module("learningApp.comp")
        .component("articlesList",
            {
                templateUrl: "components/articlesList/articles.list.html",
                controller: ["$scope", "articleService", controller]
            });
})();