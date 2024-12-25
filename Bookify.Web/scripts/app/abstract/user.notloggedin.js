angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('userNotLoggedIn', {
            abstract: true,
            template: '<ui-view/>',
            onEnter: function (authService) {
                if (authService.authentication.isAuth) {
                    sessionStorage.removeItem('authorizationData');
                    localStorage.removeItem('authorizationData');

                    authService.logOut();
                }
            }
        });
});