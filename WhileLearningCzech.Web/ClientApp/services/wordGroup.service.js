(function () {


    function wordGroupService(requestService, notifyService) {
        var service = {};

        function getWordGroups() {
            return requestService.post("wordGroup/search", {});
        }

        function createWordGroup(wordGroup) {
            return requestService.post("wordGroup/create", wordGroup)
                .then(function (result) {
                    notifyService.notify("update-wordGroups");
                    return result;
                });
        }

        function updateWordGroup(wordGroup) {
            return requestService.post("wordGroup/update", wordGroup)
                .then(function (result) {
                    notifyService.notify("update-wordGroups");
                    return result;
                });
        }

        function deleteWordGroup(wordGroup) {
            return requestService.post("wordGroup/delete", wordGroup)
                .then(function (result) {
                    notifyService.notify("update-wordGroups");
                    return result;
                });
        }

        service.getWordGroups = getWordGroups;
        service.createWordGroup = createWordGroup;
        service.updateWordGroup = updateWordGroup;
        service.deleteWordGroup = deleteWordGroup;
        return service;
    }

    angular.module("learningApp.services")
        .factory("wordGroupService", ["requestService","notifyService", wordGroupService]);
})();