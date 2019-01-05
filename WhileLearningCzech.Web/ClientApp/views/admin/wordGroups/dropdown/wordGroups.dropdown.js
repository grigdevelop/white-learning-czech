(function () {
    function wordGroupsDropdown($scope, wordGroupService) {
        console.log("entering  dropdown");
        var ctrl = this;
        $scope.wordGroups = [];
        $scope.selectedWordGroupId = null;

        function loadWordGroups() {
            wordGroupService.getWordGroups()
                .then(function (wordGroups) {
                    $scope.wordGroups = wordGroups;                    
                });
        }

        function wordGroupChanged() {
            console.log("calling custom dropdown change");
            ctrl.onChange({ groupId: $scope.selectedWordGroupId });
        }

        ctrl.$onChanges = function (changes) {

            if (changes.selectedWordGroupId) {
                $scope.selectedWordGroupId = changes.selectedWordGroupId.currentValue;

            }
        };

        $scope.wordGroupChanged = wordGroupChanged;

        loadWordGroups();


    }

    angular.module("learningApp.comp")
        .component("wordGroupsDropdown",
            {
                templateUrl: "views/admin/wordGroups/dropdown/wordGroups.dropdown.html",
                controller: ["$scope", "wordGroupService", wordGroupsDropdown],
                bindings: {
                    onChange: "&",
                    selectedWordGroupId: "<"
                }
            });
})();