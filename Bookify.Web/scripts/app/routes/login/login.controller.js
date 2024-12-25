angular.module('myApp').controller('loginController', ['$scope', '$state', '$location', 'authService', function ($scope, $state, $location, authService) {

    $scope.login = function () {
		authService.login($scope.loginData, $scope.isUserRemembered);
	};

}]);