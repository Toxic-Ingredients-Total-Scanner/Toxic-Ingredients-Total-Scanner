(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('productsSearchResult', {
        templateUrl: 'products/products-search-result/products-search-result.component.html',
        controller: productsSearchResultController
    });

    productsSearchResultController.$inject = ['productsService', '$routeParams'];

    function productsSearchResultController(productsService, $routeParams) {
        var $ctrl = this;

        $ctrl.ean = $routeParams.ean;
    }
})(window.angular);