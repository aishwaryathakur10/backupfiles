/// <reference path="ordersController.js" />
'use strict';
app.controller('ordersController', ['$scope', 'ordersService' , 'dataService', function ($scope, ordersService ,dataService) {

    $scope.orders = [];




    $scope.isDisabled = false;
    $scope.disablefunc = function () {
        $scope.isDisabled = true;
    }


    dataService.usertype().then(function (userrole) {
        if (userrole.data[0] == "staff") {
            $scope.disablefunc();
        }
    });


    $scope.delrecord = function (order) {

        var obj = $scope.car.indexOf(order);
        $scope.car.splice(obj, 1);
    }

 
    ordersService.getOrders().then(function (results) {

        $scope.orders = results.data;
        $scope.car = [
        {
            id: 1,
            name: "SUV",
            model: "2017 Lexus NX"
            
        },
            {
                id: 2,
                name: "Sedan",
                model: "2017 Volvo V60"
            },
            {
                id: 3,
                name: "Wagon",
                model: "2017 Hyundai Veloster"
                
            },
            {
                id: 4,
                name: "Coupe",
                model: "2018 Mercedes-Benz SLC"
            },
            {
                id: 5,
                name: "Crossover",
                model: "2017 Mercedes-Benz C-Classr"
            }

        ];

    }, function (error) {
        //alert(error.data.message);
    });

}]);