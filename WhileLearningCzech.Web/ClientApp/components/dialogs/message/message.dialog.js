(function () {
    function messageDialogController($scope, dialogService) {
        // subscribe to show event
        dialogService.subscribe("ngMessageDialog",onDialogShow);

        $scope.data = {
            title: "",
            message: ""
        };
        function onDialogShow(data) {
            $scope.data.message = data.message;
            $scope.data.title = data.title;
            console.log(data);

            dialogService.showDialog("message-dialog");
        }

    }

    angular.module("learningApp.comp")
        .component("ngMessageDialog",
            {
                templateUrl: "components/dialogs/message/message.dialog.html",
                controller: ["$scope", "dialogService", messageDialogController]
            });
})();