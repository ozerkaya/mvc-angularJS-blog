var myApp = angular.module("myIndexBlog", []);
myApp.controller("IndexBlogController", function ($scope) {

    $scope.GotoPost = function (id) {
        alert(id);
    };
});