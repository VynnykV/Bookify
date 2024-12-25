angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('userLoggedIn', {
            abstract: true,
            template: '<ui-view/>',
            onEnter: function (authService, $location) {
                if (!authService.authentication.isAuth)
                    $location.path('login');
            }
        });
});