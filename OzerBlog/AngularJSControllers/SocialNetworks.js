var myApp = angular.module("mySocialNetworks", []);
myApp.controller("SocialNetworksController", function ($scope, $http) {   
    

    $http.get("/Admin/SocialNetworkGet/").then(function (result) {
        $scope.platformList = result.data;
        $scope.platformID = 0;
        $scope.active = true;
    });

    $scope.savePlatform = function (platformID, platform, adress, platformIcon, active) {

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