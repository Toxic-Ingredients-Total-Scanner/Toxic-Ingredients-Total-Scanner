(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('searchHints', {
        templateUrl: 'products/search-hints/search-hints.component.html',
        controller: searchHintsController,
        bindings: {
            hints: '=',
        }
    });

    searchHintsController.$inject = [];

    function searchHintsController() {
        var $ctrl = this;
    }
})(window.angular);