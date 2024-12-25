angular.module('myApp').controller('userProfileController',
	[
        '$scope', '$state', '$location', 'authService', 'userService', 'countryService', '$timeout',
        function ($scope, $state, $location, authService, userService, countryService, $timeout) {
            $scope.countries = [];
            countryService.loadCountries().then(function (countries) {
                $scope.countries = countries;
            });

			userService.getUser().then(function(response) {
				$scope.user = response.data;
            });

            $scope.user = [];

            $scope.date = new Date().setHours(0, 0, 0, 0);

            $scope.save_info = function () {
                userService.editUserInfo($scope.user)
                    .then(function (result) {
                        $scope.message = result.data ? "The data has been changed successfully." : "The data has not been changed. Try refreshing the page.";
                    });

                $timeout(function () {
                    $scope.message = null;
                }, 3000);
            };

            $scope.save_password = function () {
                userService.editUserPassword($scope.user)
                    .then(function (result) {
                        $scope.message = result.data ? "The data has been changed successfully." : "The data has not been changed. Try refreshing the page.";
                    });

                $timeout(function () {
                    $scope.message = null;
                }, 3000);
            };

            $scope.tabName = 'profile';
            $scope.profileTab = "activeTab";

            $scope.switchTab = function (tabToSwitch) {

                $scope.tabName = tabToSwitch;

                $scope.profileTab = null;
                $scope.borrowedTab = null;
                $scope.reservedTab = null;

                if ($scope.tabName === 'profile')
                    $scope.profileTab = "activeTab";
                else if ($scope.tabName === 'borrowedBooks')
                    $scope.borrowedTab = "activeTab";
                else if ($scope.tabName === 'reservedBooks')
                    $scope.reservedTab = "activeTab";

            };

		}
	]);