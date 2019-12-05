(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('searchHints', {
        templateUrl: 'products/search-hints/search-hints.component.html',
        controller: searchHintsController,
        bindings: {
            hints: '=',
        }
    });

    searchHintsController.$inject = ['productsService'];

    function searchHintsController(productsService) {
        var $ctrl = this;

        $ctrl.hintOnClick = function (hint) {
            alert("Not implemented yet." + hint)
            productsService.randomFunction("hej");
        }
    }
})(window.angular);