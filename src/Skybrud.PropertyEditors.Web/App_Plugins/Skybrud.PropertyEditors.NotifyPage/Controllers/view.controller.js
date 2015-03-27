angular.module("umbraco").controller("Skybrud.NotifyPage.Controller", function ($scope, $routeParams, skybrudNotifyPage) {

    skybrudNotifyPage.getById($routeParams.id).then(function (response) {
        $scope.emails = response.data.data;
    });

    $scope.sendMessages = function () {

        var conf = confirm('Are you sure?');

        if (conf) {

            skybrudNotifyPage.sendMessages($routeParams.id).then(function(response) {
                alert('Mails are send!');
            });

        } 

        //send
    }

});