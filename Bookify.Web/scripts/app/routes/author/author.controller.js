angular.module('myApp').controller('authorController', ['$scope', '$http', '$location', '$timeout', '$stateParams', 'countryService', 'authorsService',
    function ($scope, $http, $location, $timeout, $stateParams, countryService, authorsService) {

        $scope.countries = [];
        $scope.member = {};
        $scope.isEditMode = false;

        countryService.loadCountries().then(function (countries) {
            $scope.countries = countries;
        });

        if ($stateParams.authorId) {
            $scope.isEditMode = true;
            authorsService.getAuthorById($stateParams.authorId).then(function (response) {
                $scope.member = response.data;
                $scope.member.birthDate = response.data.birthDate ? new Date(response.data.birthDate) : undefined;
                $scope.member.deathDate = response.data.deathDate ? new Date(response.data.deathDate) : undefined;
            }, function (error) {
                alert('Failed to load author data.');
            });
        }

        $scope.save = function () {
            if ($scope.isEditMode) {
                authorsService.updateAuthor($stateParams.authorId, $scope.member).then(function (response) {
                    $location.path('/authors');
                }, function (error) {
                    alert('Failed to update author. Please try again.');
                });
            } else {
                authorsService.addAuthor($scope.member).then(function (response) {
                    $location.path('/authors');
                }, function (error) {
                    alert('Failed to add author. Please try again.');
                });
            }
        };
    }]);
