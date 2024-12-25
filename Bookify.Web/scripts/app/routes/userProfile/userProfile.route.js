angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
		.state('userProfile', {
            url: '/profile',
			controller: 'userProfileController',
            templateUrl: 'scripts/app/routes/userProfile/userProfile.template.html',
            parent: 'userLoggedIn',
            onEnter: function ($rootScope) {
                $rootScope.isPageWithValidation = true;
            },
            onExit: function ($rootScope) {
                $rootScope.isPageWithValidation = false;
            }
        });
});