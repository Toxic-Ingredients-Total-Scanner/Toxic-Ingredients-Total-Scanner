(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').component('productView', {
    templateUrl: 'products/product-view/product-view.component.html',
    controller: productViewController
  });

  productViewController.$inject = ['productsService', '$routeParams'];

  function productViewController(productsService, $routeParams) {
    var $ctrl = this;

    $ctrl.ean = $routeParams.ean;
    $ctrl.product = getProductsByEan();

    $ctrl.getProductsByEan = getProductsByEan;

    function getProductsByEan() {
      productsService.getFullProductInfoByEan($ctrl.ean).then(
        function(response) {
          $ctrl.product = response.data;
        }
      )
    }
  }
})(window.angular);