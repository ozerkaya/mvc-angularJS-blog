var myApp = angular.module('myPosts', ['ngSanitize', 'textAngular']);
myApp.controller("PostController", function ($scope, $http) {

    ////Take Post List
    $http.get("/Admin/PostsGet/").then(function (result) {
        $scope.postList = result.data;
        $scope.postID = 0;
    });

    ////Take Selected Post Datas
    $scope.editPostTake = function (ID) {
        $http.post("/Admin/PostsEdit/" + ID).then(function (result) {
            $scope.postTitle = result.data.title;
            $scope.postContet = result.data.content;
            $scope.postID = result.data.ID;
        });
    }

    ////Delete Selected Post
    $scope.deletePost = function (ID) {
        $http.post("/Admin/PostDelete/" + ID).then(function (result) {
            $scope.postList = result.data;
        });
    }

    ////Save and Edit 
    $scope.saveEditPost = function (ID, Title, Content) {
        var data = { ID: ID, Title: Title, Content: Content }
        $http.post("/Admin/SaveEdit/", data).then(function (result) {
            $scope.postList = result.data;
            $scope.postTitle = "";
            $scope.postContet = "";
            $scope.postID = 0;
        });
    }



});