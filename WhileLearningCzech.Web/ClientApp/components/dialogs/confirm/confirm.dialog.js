(function () {
    function confirmDialogController($scope, dialogService) {
        // subscribe to show event
        dialogService.subscribe("ngConfirmDialog", onDialogShow);

        $scope.data = {
            title: "",
            message: "",
            callback: null
        };
        function onDialogShow(data) {
            $scope.data.message = data.message;
            $scope.data.title = data.title;
            $scope.data.callback = data.callback;

            dialogService.showDialog("confirm-dialog");
        }

        function closeDialog() {
            dialogService.hideDialog("confirm-dialog");
        }

        function sendResponse(yesOrTrue) {
            $scope.data.callback(yesOrTrue, closeDialog);
        }

        $scope.sendResponse = sendResponse;

    }

    angular.module("learningApp.comp")
        .component("ngConfirmDialog",
            {
                templateUrl: "components/dialogs/confirm/confirm.dialog.html",
                controller: ["$scope", "dialogService", confirmDialogController]
            });
})();