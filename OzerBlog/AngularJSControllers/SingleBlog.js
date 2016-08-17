var myApp = angular.module("mySingleBlog", []);
myApp.controller("SingleBlogController", function ($scope, $http) {

    $scope.loadComments = function (id) {

        $http.get("/SinglePost/listComment/" + id).then(function (result) {
            $scope.commentList = result.data;
        });
    };

    $scope.insertComment = function (name, email, comment, postID) {
        var data = { name: name, email: email, comment: comment, postID: postID };
        if (comment == "" || comment == undefined) {
            swal("Lütfen Yorum Giriniz", "", "error")
        }
        else {
            $http.post("/SinglePost/insertComment/", data).then(function (result) {
                $scope.name = "";
                $scope.email = "";
                $scope.comment = "";
                $scope.commentList = result.data;
                swal("Yorumunuz Eklenmiştir", "", "success")
            });
        }

    };

});