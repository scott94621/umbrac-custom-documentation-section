"use strict";

angular.module('umbraco').controller('cDoc.DocumentationController', ['$scope', 'notificationsService', function ($scope, notificationsService) {

    function getHtmlCode(url) {
        let successResult = "";
        $.ajax({
            url: url,
            dataType: "html",
            async: false,
            success: function (data) {
                successResult = data;
            },
            error: function (jqXHR, textStatus) {
                notificationsService.error("Something went wrong while getting the html code. " + textStatus);
            }
        });
        return successResult;
    }

    function setHtmlCode(htmlCode) {
        $("#markdownDiv").html(htmlCode);
    }

    function init() {
        var htmlCode = getHtmlCode("/umbraco/backoffice/api/CustomDocumentation/GetHtmlForRoute");
        setHtmlCode(htmlCode);

    }

    init();
}]);