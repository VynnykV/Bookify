angular.module('myApp').config(function ($httpProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $httpProvider.interceptors.push('authInterceptorService');
});