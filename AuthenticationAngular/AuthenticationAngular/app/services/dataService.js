'use strict';
app.factory('dataService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = 'http://localhost:49570/';

    var dataServiceFactory = {};

    var _usertype = function () {

        return $http.get(serviceBase + 'api/data/role').then(function (userrole) {
          
            return userrole;
        });
    };

    dataServiceFactory.usertype = _usertype;

    return dataServiceFactory;

}]);