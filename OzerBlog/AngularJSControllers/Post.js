var myApp = angular.module('myPosts', ['ngSanitize', 'textAngular']);
myApp.controller("PostController", function ($scope, $http, $location, $anchorScroll) {
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
            $scope.postTitle = result.data.Post.title;
            $scope.postContent = result.data.Post.content;
            $scope.postID = result.data.Post.ID;

            $location.hash('top');
            $anchorScroll();
            $scope.labelsTxt = "";
            for (var i = 0; i < $scope.enumList.length; i++) {

                for (var j = 0; j < result.data.Labels.length; j++) {

                    if ($scope.enumList[i].value == result.data.Labels[j].value) {
                        $scope.enumList[i].check = true;
                        $scope.labelsTxt = $scope.labelsTxt + result.data.Labels[j].value + ",";
                        $scope.labels.push(result.data.Labels[j].value);
                        break;
                    }
                    else {
                        $scope.enumList[i].check = false;
                    }


                }
            }

        });
    }

    ////Delete Selected Post
    $scope.deletePost = function (ID) {
        $http.post("/Admin/PostDelete/" + ID).then(function (result) {
            $scope.postList = result.data;
        });
    }

    ////Save and Edit 
    $scope.saveEditPost = function (ID, Title, Content, Labels) {
        var data = { ID: ID, Title: Title, Content: Content, Labels: Labels }
        $http.post("/Admin/SaveEditPost/", data).then(function (result) {
            $scope.postList = result.data;
            $scope.postTitle = "";
            $scope.postContent = "";
            $scope.postID = 0;
            $scope.labelsTxt = "";
        });
    }

    $scope.labelClick = function (ID, checkValue) {
        if (checkValue) {
            $scope.labels.push(ID);
        }
        else {
            $scope.labels.splice($scope.labels.indexOf(ID), 1);
        }

        $scope.labelsTxt = "";
        for (var i = 0; i < $scope.labels.length; i++) {
            $scope.labelsTxt = $scope.labelsTxt + $scope.labels[i] + ",";
        }
    }



});