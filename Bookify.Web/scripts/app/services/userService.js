app.factory('userService', function ($http, serviceBase) {

	function getUser() {

		return $http.get(serviceBase + 'api/account/get').then(function (results) {
			return results;
		});
	};

	function editUserInfo(user) {
		return $http.post(serviceBase + 'api/account/edit-info', user);
	}

	function editUserPassword(user) {
		return $http.post(serviceBase + 'api/account/edit-password', user);
	}

	var userServiceFactory = {};
	userServiceFactory.getUser = getUser;
	userServiceFactory.editUserInfo = editUserInfo;
	userServiceFactory.editUserPassword = editUserPassword;

	return userServiceFactory;

});