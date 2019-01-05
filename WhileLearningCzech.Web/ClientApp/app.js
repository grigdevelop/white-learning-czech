(function () {
    angular.module("learningApp.ctrl", []);
    angular.module("learningApp.comp", []);
    angular.module("learningApp.services", []);
    var app = angular.module("learningApp",
        ["ui.router", "learningApp.ctrl", "learningApp.comp", "learningApp.services"]);
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
            routes.login = {
                url: "/",
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

            addAuth(routes.admin);

            for (var routeName in routes) {
                if (routes.hasOwnProperty(routeName)) {
                    $stateProvider.state(routes[routeName]);
                }
            }


        }]);
    app.controller("AppCtrl", ["$state", "$scope",appCtrl]);

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