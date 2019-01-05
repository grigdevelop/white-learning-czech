/**
 * Request service
 * @typedef {Object} requestService
 * @name get
 * @function
 * @param {string} url url
 * @name post
 * @function
 * @param {string} url url
 * @param {any} data data
 */

(function () {

    /**
     * Request service
     * @alias requestService
     * @param {any} $http $http
     * @param {any} $q $q
     * @returns { requestService } requestService
     */
    function requestService($http, $q) {

        /** @const { requestService } */       
        var service = {};
        service.baseUrl = "api/";
        service.headers = {};

        /**
         * Do HttpGet request
         * @param {string} url Request url
         * @returns {Promise<any>} Returns a instance of promise.
         */
        function get(url) {
            /** @const {any} */
            var deferred = $q.defer();

            $http.get(service.baseUrl + url)
                .then(function (result) {
                    deferred.resolve(result);
                })
                .catch(function (error) {
                    deferred.reject(error);
                });

            return deferred.promise;
        }

        /**
         * Do HttpPost request
         * @param {string} url Request url
         * @param {any} data Request data
         * @returns {Promise<any>} Returns a instance of promise.
         */
        function post(url, data) {
            /** @const {any} */
            var deferred = $q.defer();

            // http options
            var options = {
                headers: service.headers
            };

            $http.post(service.baseUrl + url, data, options)
                .then(function (result) {
                    deferred.resolve(result.data);
                })
                .catch(function (error) {
                    if (error.status === 401) {
                        error.data = { message: "Unauthorized request" };
                    }
                    deferred.reject(error.data);
                });

            return deferred.promise;
        }

        function setToken(token) {
            service.headers["Authorization"] = `Bearer ${token}`;
        }

        service.get = get;
        service.post = post;
        service.setToken = setToken;
        return service;
    }

    angular.module("learningApp.services")
        .factory("requestService", ["$http", "$q", requestService]);
})();