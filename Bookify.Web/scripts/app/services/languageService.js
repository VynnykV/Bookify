angular.module('myApp').service('languageService', ['$http', function ($http) {
    var languages = [];

    this.loadLanguages = function () {
        if (languages.length > 0) {
            return Promise.resolve(languages);
        } else {
            return $http.get('jsons/languages.json')
                .then(function (response) {
                    languages = response.data.map(function (language) {
                        return language.name;
                    });
                    return languages;
                })
                .catch(function (error) {
                    console.error("Error loading languages: ", error);
                    return [];
                });
        }
    };
}]);

