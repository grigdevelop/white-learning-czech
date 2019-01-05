(function () {


    function wordService(requestService, notifyService) {
        var service = {};

        function getWords(params) {
            if (!params) params = {};

            return requestService.post("word/search", params);
        }

        function createWord(word) {
            return requestService.post("word/create", word)
                .then(function (result) {
                    notifyService.notify("update-words");
                    return result;
                });
        }

        function updateWord(word) {
            return requestService.post("word/update", word)
                .then(function (result) {
                    notifyService.notify("update-words");
                    return result;
                });
        }

        function deleteWord(word) {
            return requestService.post("word/delete", word)
                .then(function (result) {
                    notifyService.notify("update-words");
                    return result;
                });
        }

        service.getWords = getWords;
        service.createWord = createWord;
        service.updateWord = updateWord;
        service.deleteWord = deleteWord;
        return service;
    }

    angular.module("learningApp.services")
        .factory("wordService", ["requestService", "notifyService", wordService]);
})();