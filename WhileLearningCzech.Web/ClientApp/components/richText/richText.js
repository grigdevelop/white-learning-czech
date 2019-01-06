(function () {
    function richTextCtrl($element) {
        var ctrl = this;
        var container = ($element.find(".rich-text-container")[0]);
        var toolbarOptions = [['bold', 'italic'], ['link', 'image', 'code']];
        var options = {
            theme: "snow",
            modules: { toolbar: toolbarOptions }
        };
        var editor = new Quill(container, options);
        editor.on("text-change",
            function (delta, oldData, source) {
                ctrl.onChange({ content: editor.container.firstChild.innerHTML });
            });

        ctrl.$onChanges = function (changes) {

            if (changes.content) {
                editor.container.firstChild.innerHTML = (changes.content.currentValue);
            }
        };

    }

    angular.module("learningApp.comp")
        .component("richText",
            {
                templateUrl: "components/richText/richText.html",
                controller: ["$element", richTextCtrl],
                bindings: {
                    onChange: "&",
                    content: "<"
                }
            });
})();