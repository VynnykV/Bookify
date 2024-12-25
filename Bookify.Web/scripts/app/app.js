var app = angular.module('myApp', ['ui.router', 'ngDialog', 'ngMessages', 'cfp.hotkeys', 'angular-hidScanner', 'angularjs-dropdown-multiselect', 'ngFileUpload', 'chart.js']);

app.run(function (authService) {
    authService.fillAuthData();
});