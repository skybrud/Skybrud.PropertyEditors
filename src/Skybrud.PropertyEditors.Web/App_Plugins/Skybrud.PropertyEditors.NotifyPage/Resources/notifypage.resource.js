angular.module("umbraco.resources")
	.factory("skybrudNotifyPage", function ($http) {
	    return {
	        getById: function (id) {
	            return $http.get(Umbraco.Sys.ServerVariables.DCIntranet.NotifyPageServiceUrl + 'GetSubscribersByPageId?id=' + id);
	        },
            sendMessages: function(id) {
                return $http.get(Umbraco.Sys.ServerVariables.DCIntranet.NotifyPageServiceUrl + 'SendMessages?id=' + id);
            }
	    };
	});