(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner')
        .component('navigationBar', {
        templateUrl: 'navigation-bar/navigation-bar.html',
        controller: navigationBarController
    });

    navigationBarController.$inject = [];

    function navigationBarController() {
        var $ctrl = this;

        $ctrl.clickMe = clickMe;

        function clickMe() {
            alert("to implement");
        }
    }
})(window.angular);