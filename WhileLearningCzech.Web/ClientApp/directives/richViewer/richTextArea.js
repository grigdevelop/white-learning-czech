(function () {
    function richTextAreaCtrl($scope) {


    }

    angular.module("learningApp.dirs")
        .controller("RichTextAreaCtrl", ["$scope", richTextAreaCtrl])
        .directive("richTextArea", function () {

            function link(scope, element, attrs, controller) {
                var container = element[0].firstChild;

                // configuring editor
                var toolbarOptions = [["bold", "italic"], ["link", "image", "code"]];
                var options = {
                    theme: "snow",
                    modules: {
                        toolbar: toolbarOptions,
                        imageResize: {
                            displaySize: true
                        }
                    }
                };
                //Quill.register('modules/imageResize', ImageResize);
                var editor = new Quill(container, options);

                // detect changes from current control
                editor.on("text-change",
                    function (delta, oldData, source) {
                        scope.ngModel = editor.container.firstChild.innerHTML;
                    });

                // detect changes from client side
                scope.$watch("ngModel", function (oldValue, newValue) {
                    editor.container.firstChild.innerHTML = scope.ngModel;
                });

                scope.$on("$destroy", function () {
                    console.log("destroying element");
                });
            }


            return {
                restrict: "E",
                link: link,
                scope: {
                    ngModel: "="
                },
                template: "<div></div>"
            };
        });
})();