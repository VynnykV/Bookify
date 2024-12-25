var app = angular.module('myApp').controller("homeController", function ($scope, $state, authService) {

	$scope.search = function () {
        $state.go('search', { searchQuery: $scope.searchQuery });
    };

    $scope.isLoggedIn = authService.authentication.isAuth;
    $scope.role = authService.authentication.role;

    $scope.logOut = function () {
        authService.logOut();
        $scope.isLoggedIn = false;
    }
});