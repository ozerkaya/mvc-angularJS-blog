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
        swal({
            title: "Emin misin?",
            text: "Kayıt Geri Dönülmeyecek şekilde Silinecek!",
            type: "warning",
            showCancelButton: true,
            cancelButtonText: "İptal",
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, Kaydı Sil!",
            closeOnConfirm: false
        },
      function () {
          $http.post("/Admin/UserDelete/" + ID).then(function (result) {
              $scope.userList = result.data;
              swal("Silindi!", "Kayıt başarıyla Silindi...", "success");
          });
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