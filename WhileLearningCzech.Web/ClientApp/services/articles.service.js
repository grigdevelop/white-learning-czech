(function () {


    function articleService(requestService, notifyService) {
        var service = {};

        function search() {            
            return requestService.post("article/search", {});
        }

        function create(word) {
            return requestService.post("article/create", word)
                .then(function (result) {
                    notifyService.notify("update-articles");
                    return result;
                });
        }

        function update(word) {
            return requestService.post("article/update", word)
                .then(function (result) {
                    notifyService.notify("update-articles");
                    return result;
                });
        }

        function deleteArticle(word) {
            return requestService.post("article/delete", word)
                .then(function (result) {
                    notifyService.notify("update-articles");
                    return result;
                });
        }

        service.search = search;
        service.create = create;
        service.update = update;
        service.deleteArticle = deleteArticle;
        return service;
    }

    angular.module("learningApp.services")
        .factory("articleService", ["requestService", "notifyService", articleService]);
})();