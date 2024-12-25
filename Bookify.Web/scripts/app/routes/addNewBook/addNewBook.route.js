angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('addNewBook', {
			url: '/new-book',
            controller: 'addNewBookController',
            templateUrl: 'scripts/app/routes/addNewBook/addNewBook.template.html',
			parent: 'userLoggedIn'
		})
		.state('editBook', {
			url: '/edit-book/:bookId',
			controller: 'addNewBookController',
			templateUrl: 'scripts/app/routes/addNewBook/addNewBook.template.html',
			parent: 'userLoggedIn'
		});
});