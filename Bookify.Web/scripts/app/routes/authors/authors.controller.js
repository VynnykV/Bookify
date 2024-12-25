angular.module('myApp').controller('authorsController', function ($scope, $stateParams, $state, authorsService, ngDialog, $rootScope) {
    $scope.members = [];

    $scope.loadMembers = function () {
        authorsService.getAllAuthors().then(function (response) {
            $scope.members = response.data;
        });
    };

    $scope.loadMembers();

    $scope.search = function () {
        authorsService.getAllAuthors().then(function (response) {
            $scope.members = filterMembers(response.data, $scope.searchQuery);
        });
    };

    $scope.redirectToEdit = function (authorId) {
        $state.go('editAuthor', { authorId: authorId });
    };

    function filterMembers(members, query) {
        if (!query) return members;
        return members.filter(function (member) {
            return Object.values(member).some(function (value) {
                return value && value.toString().toLowerCase().includes(query.toLowerCase());
            });
        });
    }
});
