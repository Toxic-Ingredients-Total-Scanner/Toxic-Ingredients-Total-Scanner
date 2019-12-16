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
    $ctrl.toggleIngredient = toggleIngredient;
    $ctrl.getLegalIcon = getLegalIcon;
    $ctrl.getProductImage = getProductImage;
    $ctrl.getProductsByEan = getProductsByEan;
    $ctrl.warning = openWarning();

    function openWarning() {
      alert("This is just application for test purposes. Some products contains random ingredients list!")
    }

    function getProductsByEan() {
      productsService.getFullProductInfoByEan($ctrl.ean).then(
        function(response) {
          $ctrl.product = response.data;
        }
      )
    }

    function getLegalIcon() {
      return $ctrl.product.isLegal ? 'fa-check-square' : 'fa-times';
    }

    function getProductImage() {
      try{ return $ctrl.product.productImage || $ctrl.product.base64Image ||'../img/no_image_placeholder.png'; }
      catch (e) {return '../img/no_image_placeholder.png';}
    }

    function toggleIngredient(ingredient){
      ingredient.isOpen = ingredient.isOpen || false;
      ingredient.isOpen = !ingredient.isOpen;
    }
  }
})(window.angular);