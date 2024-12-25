angular.module('myApp').factory('authInterceptorService', ['$q', '$location', function ($q, $location) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

		var authData = JSON.parse(localStorage.getItem('authorizationData'));

		if (!authData)
			authData = JSON.parse(sessionStorage.getItem('authorizationData'));

        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }
        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path('/login');
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);