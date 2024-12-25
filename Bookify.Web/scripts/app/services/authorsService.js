app.factory('authorsService', function ($http, serviceBase) {
    // Функція для отримання всіх авторів
    function getAllAuthors() {
        return $http.get(serviceBase + 'api/Authors').then(function (results) {
            return results;
        });
    }

    // Функція для отримання автора за ID
    function getAuthorById(authorId) {
        return $http.get(serviceBase + 'api/Authors/' + authorId).then(function (results) {
            return results;
        });
    }

    // Функція для додавання нового автора
    function addAuthor(author) {
        return $http.post(serviceBase + 'api/Authors', author).then(function (results) {
            return results;
        });
    }

    // Функція для оновлення інформації про автора
    function updateAuthor(authorId, author) {
        return $http.put(serviceBase + 'api/Authors/' + authorId, author).then(function (results) {
            return results;
        });
    }

    // Функція для видалення автора за ID
    function deleteAuthor(authorId) {
        return $http.delete(serviceBase + 'api/Authors/' + authorId).then(function (results) {
            return results;
        });
    }

    // Об'єкт, який містить всі методи сервісу
    var authorsServiceFactory = {};
    authorsServiceFactory.getAllAuthors = getAllAuthors;
    authorsServiceFactory.getAuthorById = getAuthorById;
    authorsServiceFactory.addAuthor = addAuthor;
    authorsServiceFactory.updateAuthor = updateAuthor;
    authorsServiceFactory.deleteAuthor = deleteAuthor;

    return authorsServiceFactory;
});
