angular.module('myApp').controller('bookDetailsController',
	function($scope, $stateParams, $state, serviceBase, bookSearchService, authService) {

	    $scope.isLoggedIn = authService.authentication.isAuth;

        var loadBookData = function () {
            return bookSearchService.getBookById($stateParams.bookId).then(
                function (response) {
                    $scope.book = response.data;

                    // Set the URLs for the existing cover image and PDF file
                    $scope.book.coverImageUrl = serviceBase + $scope.book.coverImagePath; // Adjust if necessary
                    $scope.book.pdfFileUrl = serviceBase + $scope.book.filePath; // Adjust if necessary
                    $scope.book.author.fullName = $scope.book.author.firstName + ' ' + $scope.book.author.lastName;
                    $scope.book.genresString = $scope.book.genres.map(function (genre) {
                        return genre.name;
                    }).join(", ");
                },
                function (error) {
                    alert('Failed to load book data.');
                }
            );
        };

        $scope.search = function () {
            $state.go('search', { searchQuery: $scope.searchQuery });
        };

        loadBookData();

        $scope.isLoggedIn = authService.authentication.isAuth;

        $scope.handleReadBook = function () {
            if ($scope.isLoggedIn) {
                window.location.href = $scope.book.pdfFileUrl;
            } else {
                window.location.href = '/login';
            }
        };
	});