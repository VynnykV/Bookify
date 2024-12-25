angular.module('myApp').service('countryService', ['$http', function ($http) {
    var countries = [];

    this.loadCountries = function () {
        if (countries.length > 0) {
            return Promise.resolve(countries);
        } else {
            return $http.get('jsons/countries.json')
                .then(function (response) {
                    countries = response.data.map(function (country) {
                        return country.country;
                    });
                    return countries;
                })
                .catch(function (error) {
                    console.error("Error loading countries: ", error);
                    return [];
                });
        }
    };
}]);
