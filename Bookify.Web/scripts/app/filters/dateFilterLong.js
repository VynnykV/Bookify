angular.module('myApp').filter('convertDateLong', function () {
	return function(date) {

        date = new Date(date);

		var monthNames = [
			'siječnja', 'veljače', 'ožujka',
			'travnja', 'svibnja', 'lipnja', 'srpnja',
			'kolovoza', 'rujna', 'listopada',
			'studenog', 'prosinca'
        ];

		var day = date.getDate();
		var monthIndex = date.getMonth();
        var year = date.getFullYear();

		return day + '. ' + monthNames[monthIndex] + ' ' + year + '.';

	};
});