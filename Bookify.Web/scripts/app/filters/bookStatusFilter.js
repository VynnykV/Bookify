angular.module('myApp').filter('convertBookStatusPartOne', function () {
	return function(returnDeadline) {

		if (!returnDeadline)
			return 'dostupno odmah';
		else
			return 'dostupno od';

	};
});

// the use of two filters allows for different appeareances of the words and the date
angular.module('myApp').filter('convertBookStatusPartTwo', function ($filter) {
	return function (returnDeadline) {

		if (!returnDeadline)
			return '';
		else
			return ' ' + $filter('convertDateShort')(returnDeadline);

	};
});