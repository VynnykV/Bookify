angular.module('myApp').controller('addNewBookController', function (
    $scope,
    $stateParams,
    $state,
    bookSearchService,
    languageService,
    ngDialog,
    $rootScope,
    authService,
    serviceBase,
    Upload,
    $q // Inject $q for promises
) {
    $scope.book = {};
    $scope.isEditMode = false;

    // Initialize genres and selected genres model
    $scope.genres = [];
    $scope.genresModel = [];

    // Load authors
    bookSearchService.getAllAuthors().then(function (response) {
        $scope.authors = response.data.map(function (author) {
            return {
                id: author.id,
                fullName: author.firstName + ' ' + author.lastName,
            };
        });
    });

    // Load categories
    bookSearchService.getAllCategories().then(function (response) {
        $scope.categories = response.data;
    });

    // Load languages
    languageService.loadLanguages().then(function (languages) {
        $scope.languages = languages;
    });

    // Function to load genres
    var loadGenres = function () {
        return bookSearchService.getAllGenres().then(function (response) {
            $scope.genres = response.data;
        });
    };

    // Function to load book data
    var loadBookData = function () {
        return bookSearchService.getBookById($stateParams.bookId).then(
            function (response) {
                $scope.book = response.data;

                // Set the URLs for the existing cover image and PDF file
                $scope.coverImageUrl = serviceBase + $scope.book.coverImagePath; // Adjust if necessary
                $scope.pdfFileUrl = serviceBase + $scope.book.filePath; // Adjust if necessary
            },
            function (error) {
                alert('Failed to load book data.');
            }
        );
    };

    // Check if in edit mode
    if ($stateParams.bookId) {
        $scope.isEditMode = true;

        // Wait for both genres and book data to load
        $q.all([loadGenres(), loadBookData()]).then(function () {
            // Map genres for the dropdown
            $scope.genresModel = [];
            angular.forEach($scope.book.genres, function (bookGenre) {
                var matchedGenre = $scope.genres.find(function (genre) {
                    return genre.id === bookGenre.id;
                });
                if (matchedGenre) {
                    $scope.genresModel.push(matchedGenre);
                }
            });

            // Pre-select author, category, language, and page count
            // These should already be set if book data matches $scope.book properties
        });
    } else {
        // Not in edit mode, just load genres
        loadGenres();
    }

    $scope.genressettings = {
        template: '{{option.name}}',
        checkBoxes: true,
        selectedToTop: true,
        smartButtonMaxItems: 3,
        smartButtonTextConverter: function (itemText, originalItem) {
            return originalItem.name;
        },
        showCheckAll: false,
        scrollableHeight: '200px',
        scrollable: true,
        enableSearch: true,
    };

    $scope.getGenreIds = function () {
        return $scope.genresModel.map(function (genre) {
            return genre.id;
        });
    };

    $scope.removeExistingPdf = function () {
        $scope.pdfFile = null; // Скидаємо вибраний файл
        $scope.pdfFileUrl = null; // Скидаємо існуючий файл
    };

    $scope.removeExistingCoverImage = function () {
        $scope.picFile = null; // Скидаємо вибраний файл
        $scope.coverImageUrl = null; // Скидаємо існуючий файл
    };

    var addUrl = serviceBase + 'api/search/add';
    var editUrl = serviceBase + 'api/search/edit/' + $stateParams.bookId;

    $scope.uploadBook = function () {
        if ($scope.bookForm.$valid) {
            var genreIds = $scope.getGenreIds();
            var url = $scope.isEditMode ? editUrl : addUrl;

            // Prepare data
            var data = {
                title: $scope.book.title,
                author: $scope.book.authorId,
                language: $scope.book.language,
                category: $scope.book.categoryId,
                numOfPages: $scope.book.pageCount,
                description: $scope.book.description,
                genres: angular.toJson(genreIds),
            };

            // Include files (either new or existing)
            if ($scope.pdfFile) {
                data.pdf = $scope.pdfFile;
            }
            if ($scope.picFile) {
                data.file = $scope.picFile;
            }

            // Adjust HTTP method based on mode
            var method = $scope.isEditMode ? 'PUT' : 'POST';

            Upload.upload({
                url: url,
                data: data,
                method: method, // Specify PUT for edit mode
            }).then(
                function (resp) {
                    console.log('Success', resp.data);
                    $state.go('bookList'); // Navigate to the book list
                },
                function (resp) {
                    console.error('Error status:', resp.status);
                },
                function (evt) {
                    var progressPercentage = parseInt((100.0 * evt.loaded) / evt.total);
                    console.log('Progress:', progressPercentage + '%');
                }
            );
        } else {
            alert('The form is filled out incorrectly!');
        }
    };


});
