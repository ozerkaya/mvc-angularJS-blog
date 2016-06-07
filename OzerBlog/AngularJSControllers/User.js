var myApp = angular.module("myUsers", []);
myApp.controller("UserController", function ($scope, $http) {

    ////Take User List
    $http.get("/Admin/UsersGet/").then(function (result) {
        $scope.userList = result.data;
        $scope.userID = 0;
    });

    ////Take Selected User Datas
    $scope.editUserTake = function (ID) {
        $http.post("/Admin/UsersEdit/" + ID).then(function (result) {
            $scope.userUsername = result.data.username;
            $scope.userID = result.data.ID;
        });
    }

    ////Delete Selected User
    $scope.deleteUser = function (ID) {
        $http.post("/Admin/UserDelete/" + ID).then(function (result) {
            $scope.userList = result.data;
        });
    }

    ////Save and Edit 
    $scope.saveEditUser = function (ID, Username, Password) {
        var data = { ID: ID, Username: Username, Password: Password }
        $http.post("/Admin/SaveEditUser/", data).then(function (result) {
            $scope.userList = result.data;
            $scope.userUsername = "";
            $scope.userPassword = "";
            $scope.userID = 0;
        });
    }
});