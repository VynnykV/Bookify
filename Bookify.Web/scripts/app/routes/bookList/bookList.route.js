angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('bookList', {
			url: '/book-list',
			controller: 'bookListController',
            templateUrl: 'scripts/app/routes/bookList/bookList.template.html',
			parent: 'userLoggedIn'
		});
});