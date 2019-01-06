(function () {
    angular.module("learningApp.ctrl", []);
    angular.module("learningApp.comp", []);
    angular.module("learningApp.services", []);
    angular.module("learningApp.dirs", []);
    var app = angular.module("learningApp",
        ["ui.router","ngAnimate", "learningApp.ctrl", "learningApp.comp", "learningApp.services", "learningApp.dirs"]);
    app.config(["$stateProvider", "$urlRouterProvider", "$locationProvider",
        function ($stateProvider, $urlRouterProvider, $locationProvider) {
            //$locationProvider.hashPrefix("!");
            $locationProvider.html5Mode(true);
            $urlRouterProvider.otherwise("/");

            //$stateProvider.when("/login", {
            //    templateUrl: "views/login/login.view.html",
            //    controller: "LoginCtrl"
            //});
            var routes = {};
            routes.home = {
                url: "/",
                name: "home",
                templateUrl: "views/home/home.view.html",
                controller: "HomeCtrl"
            };
            routes.login = {
                url: "/login",
                name: "login",
                templateUrl: "views/login/login.view.html",
                controller: "LoginCtrl"
            };
            routes.admin = {
                name: "admin",
                url: "/admin",
                templateUrl: "views/admin/admin.view.html",
                controller: "AdminCtrl"
            };
            routes.adminWords = {
                name: "admin.words",
                url: "/",
                templateUrl: "views/admin/words/words.view.html",
                controller: "WordsCtrl"
            };
            routes.adminWordGroups = {
                name: "admin.wordGroups",
                url: "/wordGroups",
                templateUrl: "views/admin/wordGroups/wordGroups.view.html",
                controller: "WordGroupsCtrl"
            };
            routes.adminArticles = {
                name: "admin.articles",
                url: "/articles",
                templateUrl: "views/admin/articles/articles.view.html",
                controller: "ArticlesCtrl"
            };


            addAuth(routes.admin);

            for (var routeName in routes) {
                if (routes.hasOwnProperty(routeName)) {
                    $stateProvider.state(routes[routeName]);
                }
            }


        }]);
    app.controller("AppCtrl", ["$state", "$scope", appCtrl]);

    app.filter("trust", ["$sce", function ($sce) {
        return function (htmlCode) {
            return $sce.trustAsHtml(htmlCode);
        }
    }]);

    function appCtrl($state, $scope) {

        $state.defaultErrorHandler(function (error) {
            console.log("unauthorized request");
        });

    }

    function addAuth(state) {
        if (!state.resolve) {
            state.resolve = {};
        }

        state.resolve.auth = ["$q", "$state", "authService", authorize];

        function authorize($q, $state, authService) {
            var deferred = $q.defer();

            authService.validateToken()
                .then(function () {
                    deferred.resolve();
                })
                .catch(function () {
                    $state.go("login");
                    deferred.reject();
                });

            return deferred.promise;
        }
    }
})();