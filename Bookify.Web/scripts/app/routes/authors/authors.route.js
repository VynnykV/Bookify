angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('authors', {
			url: '/authors',
			controller: 'authorsController',
			templateUrl: 'scripts/app/routes/authors/authors.template.html',
			parent: 'userLoggedIn'
		});
});