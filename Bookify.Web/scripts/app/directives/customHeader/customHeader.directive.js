angular.module('myApp').directive('customheader', function () {
    return {
        controller: 'headerController',
		templateUrl: 'scripts/app/directives/customHeader/customHeader.template.html'
	};
});