var myApp = angular.module("myLogs", []);
myApp.controller("LogsController", function ($scope, $http) {

    $http.get("/Admin/LogsGet/").then(function (result) {
        $scope.logsList = result.data;
    });
});