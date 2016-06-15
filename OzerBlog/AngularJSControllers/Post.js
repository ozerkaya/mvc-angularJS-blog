var myApp = angular.module('myPosts', ['ngSanitize', 'textAngular']);
myApp.controller("PostController", function ($scope, $http) {
    $scope.labels = [];

    ////Take Post List
    $http.get("/Admin/PostsGet/").then(function (result) {
        $scope.postList = result.data.Posts;
        $scope.enumList = result.data.Enums;
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
        $http.post("/Admin/SaveEditPost/", data).then(function (result) {
            $scope.postList = result.data;
            $scope.postTitle = "";
            $scope.postContet = "";
            $scope.postID = 0;
        });
    }

    $scope.labelClick = function (ID) {
        ID = ID + ",";
        if ($scope.labels.indexOf(ID) == -1) {
            $scope.labels.push(ID);
        }
        else {
            $scope.labels.splice($scope.labels.indexOf(ID), 1);
        }

        $scope.labelsTxt = "";
        for (var i = 0; i < $scope.labels.length; i++) {
            $scope.labelsTxt += $scope.labels[i];
        }
    }



});