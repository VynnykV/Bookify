angular.module('myApp').filter('convertDateShort', function () {
	return function(date) {

		date = new Date(date);

		var day = date.getDate();
		// getMonth is zero indexed
		var month = date.getMonth()+1;

		return day + '.' + month + '.';

	};
});