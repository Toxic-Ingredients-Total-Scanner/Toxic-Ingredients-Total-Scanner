(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('productsSearchResult', {
        templateUrl: 'products/products-search-result/products-search-result.component.html',
        controller: productsSearchResultController
    });

    productsSearchResultController.$inject = ['productsService', '$routeParams', '$location'];

    function productsSearchResultController(productsService, $routeParams, $location) {
        var $ctrl = this;
        $ctrl.ean = $routeParams.ean;
        $ctrl.product = getProductByEan();

        function getProductByEan() {
            productsService.getProductByEan($ctrl.ean).then(
                function(response) {
                    if(response) {
                        $location.path(/product/ + response.data.gtin);
                    }
                }
            )
        }
    }
})(window.angular);