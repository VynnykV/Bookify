'use strict';
app.factory('authService', function ($http, $q, $state, serviceBase) {

    var authentication = {
        isAuth: false,
        userName: ""
    };

    function register(registration) {
        logOut();
        return $http.post(serviceBase + 'api/account/register', registration).then(function successful(response) {

            var loginData = {
                userName: registration.username,
                password: registration.password
            };
            login(loginData, true);

            return response;
        }, function error(result) {
            parseErrors(result);
        });
    };

    function login(loginData, isUserRemembered) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        return $http.post(serviceBase + 'token',
            data,
            { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function successful(response) {

                var storageItem = JSON.stringify({ token: response.data.access_token, userName: loginData.userName, role: JSON.parse(response.data.roles)[0] });

                // session storage is temporary; isUserRemembered is user's preference
                if (isUserRemembered)
                    localStorage.setItem('authorizationData', storageItem)
                else
                    sessionStorage.setItem('authorizationData', storageItem)

                authentication.isAuth = true;
                authentication.userName = loginData.userName;
                authentication.role = JSON.parse(response.data.roles)[0];

                $state.go('home');
            },
            function error(result) {
                alert(result.data.error_description);
            });
    };

    function logOut() {

        localStorage.removeItem('authorizationData');
        sessionStorage.removeItem('authorizationData');

        authentication.isAuth = false;
        authentication.userName = "";
        authentication.role = null;
    };

    function parseErrors(result) {
        let errors = [];

        // Перевірка, чи є дані про помилки
        if (result && result.data && result.data.modelState) {
            // Проходження по кожній властивості modelState
            for (let key in result.data.modelState) {
                if (result.data.modelState.hasOwnProperty(key)) {
                    errors = errors.concat(result.data.modelState[key]);
                }
            }
        } else if (result && result.data && result.data.message) {
            // Якщо є загальне повідомлення про помилку
            errors.push(result.data.message);
        }

        // Виведення або обробка помилок
        if (errors.length > 0) {
            alert(errors.join("\n"));
        } else {
            alert("An unknown error occurred.");
        }
    }

    function fillAuthData() {

        var authData = JSON.parse(localStorage.getItem('authorizationData')) || JSON.parse(sessionStorage.getItem('authorizationData'));

        if (authData) {
            authentication.isAuth = true;
            authentication.userName = authData.userName;
            authentication.role = authData.role;
        }
    }


    var authServiceFactory = {};
    authServiceFactory.saveRegistration = register;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
});