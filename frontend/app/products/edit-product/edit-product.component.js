(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('editProduct', {
        templateUrl: 'products/edit-product/edit-product.component.html',
        controller: editProductController
    });

    editProductController.$inject = ['productsService', '$routeParams', '$location'];

    function editProductController(productsService, $routeParams, $location) {
        var $ctrl = this;
        $ctrl.ean = $routeParams.ean;
        $ctrl.product = getProductByEan();

        function getProductByEan() {
            productsService.getFullProductInfoByEan($ctrl.ean).then(
                function(response) {
                    if(response) {
                        return response.data;
                    }
                    else {
                        return {};
                    }
                }
            )
        }
    }
})(window.angular);