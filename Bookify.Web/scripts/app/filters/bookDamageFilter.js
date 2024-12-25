angular.module('myApp').filter('convertBookDamage', function () {
	return function(damageLevel) {

        var damageStatuses = ['Loše', 'Dobro', 'Izvrsno'];

        return damageStatuses[damageLevel - 1];
	};
});