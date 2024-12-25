angular.module('myApp').controller('signupController', ['$scope', '$http', '$location', '$timeout', 'countryService', 'authService', function ($scope, $http, $location, $timeout, countryService, authService) {
    $scope.savedSuccessfully = false;
    $scope.countries = [];

    // Load countries using the service
    countryService.loadCountries().then(function (countries) {
        $scope.countries = countries;
    });

    $scope.signUp = function () {
        authService.saveRegistration($scope.registration).then(function (response) {
            $scope.savedSuccessfully = true;
        },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
            });
    };
}]);
