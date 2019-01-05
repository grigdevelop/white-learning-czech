(function () {
    function wordGroupsDropdown($scope, wordGroupService) {
        var ctrl = this;
        $scope.wordGroups = [];
        $scope.selectedWordGroup = {};

        function loadWordGroups() {
            wordGroupService.getWordGroups()
                .then(function(wordGroups) {
                    $scope.wordGroups = wordGroups;
                    $scope.wordGroups.unshift({ name: "[[no group]]", id: null });
                    $scope.selectedWordGroup = null;
                });
        }

        function wordGroupChanged() {
            ctrl.onChange({ groupId: $scope.selectedWordGroup});
        }

        $scope.wordGroupChanged = wordGroupChanged;

        loadWordGroups();


    }

    angular.module("learningApp.comp")
        .component("wordGroupsDropdown",
            {
                templateUrl: "views/admin/wordGroups/dropdown/wordGroups.dropdown.html",
                controller: ["$scope", "wordGroupService", wordGroupsDropdown],
                bindings: {
                    onChange: "&"
                }
            });
})();