"use strict";

angular.module('umbraco').controller('cDoc.DocumentationController', ['$scope', 'notificationsService', '$location', function ($scope, notificationsService, $location) {

    function getHtmlCode(url) {
        let successResult = "";
        const filePath = getFilePath();
        const urlWithQuery = url + "?filePath=" + filePath;
        $.ajax({
            url: urlWithQuery,
            dataType: "JSON",
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

    function getFilePath() {
        var path = $location.path();
        return path.split("edit/")[1];
    }

    function addEventToTreeElements() {
        const mainRoot = $("li[data-element=tree-item-documentation]");

    }

    function init() {
        var htmlCode = getHtmlCode("/umbraco/backoffice/api/CustomDocumentation/GetHtmlForRoute");
        setHtmlCode(htmlCode);
    }

    init();
    $scope.setEventHandlers = function () {
        addEventToTreeElements();
    };
}]);