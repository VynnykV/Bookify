angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('login', {
            url: '/login',
            controller: 'loginController',
            templateUrl: 'scripts/app/routes/login/login.template.html',
            parent: 'userNotLoggedIn'
        });
});