(function () {
    function richViewerCtrl($scope) {

    }

    angular.module("learningApp.dirs")
        .controller("RichViewerCtrl", ["$scope", richViewerCtrl])
        .directive("richViewer", function () {

            function link(scope, element, attrs, controller) {
                var container = element[0].firstChild;

                var editor = new Quill(container,
                    {
                        readOnly: true
                    });

                scope.$watch("content", function(oldValue, newValue) {
                    editor.container.firstChild.innerHTML = newValue;
                });

            }


            return {
                restrict: "E",
                link: link,
                scope: {
                    content: "="
                },                
                template: "<div>loading content ...</div>"
            };
        });
})();