angular.module('myApp').controller('bookListController',
    function ($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, getLibrariesService) {

        var descending = false;
        var lastPickedOption = null;
        var pageNumber = 0;
        $scope.disableMoreResults = false;

        $scope.setSortOption = function (sortName) {
            $scope.disableMoreResults = false;
            if (lastPickedOption !== sortName)
                descending = false;
            else
                descending = !descending;

            lastPickedOption = sortName;
            pageNumber = 0;
            getLibrariesService.getBookList(sortName, descending, $scope.searchQuery, pageNumber).then(function (response) {
                $scope.books = response.data;
            });

        }

        $scope.search = function () {

            var sortOption = lastPickedOption || 'name';
            pageNumber = 0;
            getLibrariesService.getBookList(sortOption, descending, $scope.searchQuery, pageNumber).then(function (response) {
                $scope.books = response.data;
            });

            $scope.disableMoreResults = false;

        }

        $scope.setSortOption('name');

        $scope.loadMoreBooks = function () {
            var sortOption = lastPickedOption || 'name';

            getLibrariesService.getBookList(sortOption, descending, $scope.searchQuery, ++pageNumber).then(function (response) {
                $scope.books = $scope.books.concat(response.data);
                if (response.data.length === 0)
                    $scope.disableMoreResults = true;

            });
        }

        $scope.redirectToEdit = function (bookId) {
            $state.go('editBook', { bookId: bookId });
        };
    });