var myApp = angular.module("mySocialNetworks", []);
myApp.controller("SocialNetworksController", function ($scope, $http) {


    $http.get("/Admin/SocialNetworkGet/").then(function (result) {
        $scope.platformList = result.data;
        $scope.platformID = 0;
        $scope.active = true;
    });

    $scope.deleteSocialNetwork = function (ID) {
        $http.post("/Admin/SocialNetworkDelete/" + ID).then(function (result) {
            $scope.platformList = result.data;
        });
        swal("İşlem Başarılı", "", "success")
    }

    $scope.editSocialNetwork = function (ID) {
        $http.post("/Admin/SocialNetworkEdit/" + ID).then(function (result) {
            $scope.platformID = result.data.ID;
            $scope.platform = result.data.Platform;
            $scope.adress = result.data.Address;
            $scope.active = result.data.Active;
            $scope.image = result.data.Image;
        });
    }

    $scope.saveSocialNetwork = function (platformID, platform, adress, platformIcon, active) {

        var data = [{ platformID: platformID, platform: platform, adress: adress, image: platformIcon, active: active }];

        $http.post("/Admin/SaveEditSocialNetwork/", data[0]).then(function (result) {
            $scope.platformID = 0;
            $scope.platform = "";
            $scope.adress = "";
            $scope.active = true;
            $scope.platformList = result.data;

        });
    }


});

myApp.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}]);