angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('createAuthor', {
            url: '/create-author',
            controller: 'authorController',
            templateUrl: 'scripts/app/routes/author/author.template.html',
            parent: 'userLoggedIn'
        })
        .state('editAuthor', {
            url: '/edit-author/:authorId',
            controller: 'authorController',
            templateUrl: 'scripts/app/routes/author/author.template.html',
            parent: 'userLoggedIn'
        });
});
