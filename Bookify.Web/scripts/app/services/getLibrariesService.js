app.factory('getLibrariesService', function ($http, serviceBase) {

    function getLibraries() {

        return $http.get(serviceBase + 'api/libraries/get').then(function (results) {
            return results;
        });
    };

    function getLibraryById(libraryId) {

        return $http.get(serviceBase + 'api/libraries/get-byid', { params: { libraryId: libraryId } }).then(function (results) {
            return results;
        });
    };

    function getLibraryByIdForMembership(libraryId) {

        return $http.get(serviceBase + 'api/libraries/get-byid-membership', { params: { libraryId: libraryId } }).then(function (results) {
            return results;
        });
    };

    function getAllLibraries() {

        return $http.get(serviceBase + 'api/libraries/get-all').then(function (results) {
            return results;
        });
    };

    function submitBarcode(libraryId, barcodeNumber) {

        return $http.get(serviceBase + 'api/libraries/enroll-with-barcode', { params: {libraryId: libraryId, barcodeNumber: barcodeNumber}}).then(function (results) {
            return results;
        });
	};

    function getLibraryMember(memberBarcode) {

        return $http.get(serviceBase + 'api/libraries/get-member-by-barcode', { params: { memberBarcode: memberBarcode } }).then(function (results) {
			return results;
		});
    }

    function lendAndReturnScanned(bookBarcodes, memberBarcode) {
        return $http.get(serviceBase + 'api/libraries/lend-return', { params: { bookBarcodes: bookBarcodes, memberBarcode: memberBarcode } }).then(function (results) {
            return results;
        });
    }

    function getBookList(sortOption, descending, searchQuery, pageNumber) {

        if (!searchQuery)
            searchQuery = "";

        return $http.get(serviceBase + 'api/libraries/get-book-list', { params: { sortOption: sortOption, descending: descending, searchQuery: searchQuery, pageNumber: pageNumber } }).then(function (results) {
            return results;
        });
    }

    function getMemberList(sortOption, descending, searchQuery, pageNumber) {

        if (!searchQuery)
            searchQuery = "";

        return $http.get(serviceBase + 'api/libraries/get-member-list', { params: { sortOption: sortOption, descending: descending, searchQuery: searchQuery, pageNumber: pageNumber } }).then(function (results) {
            return results;
        });
    }
    
	var getLibrariesServiceFactory = {};
	getLibrariesServiceFactory.getLibraries = getLibraries;
	getLibrariesServiceFactory.getLibraryById = getLibraryById;
	getLibrariesServiceFactory.getLibraryByIdForMembership = getLibraryByIdForMembership;
    getLibrariesServiceFactory.getAllLibraries = getAllLibraries;
    getLibrariesServiceFactory.submitBarcode = submitBarcode;
    getLibrariesServiceFactory.getLibraryMember = getLibraryMember;
    getLibrariesServiceFactory.lendAndReturnScanned = lendAndReturnScanned;
    getLibrariesServiceFactory.getBookList = getBookList;
    getLibrariesServiceFactory.getMemberList = getMemberList;

	return getLibrariesServiceFactory;

});