(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('editProduct', {
        templateUrl: 'products/edit-product/edit-product.component.html',
        controller: editProductController
    });

    editProductController.$inject = ['productsService', '$routeParams', '$location'];

    function editProductController(productsService, $routeParams, $location) {
        var $ctrl = this;
        $ctrl.adding = $location.url() === '/add-new-product/';
        $ctrl.ean = $routeParams.ean;
        $ctrl.product = getProductByEan();
        $ctrl.imageFile = {};
        $ctrl.updateProduct = updateProduct;
        $ctrl.addNewIngredient = addNewIngredient;
        $ctrl.getBase64 = getBase64;
        $ctrl.removeIngredient = removeIngredient;

        $('input[type=file]').change(function () {
            $ctrl.imageFile = this.files[0];
            getBase64();
        });

        function updateProduct() {
            console.log($ctrl.product.base64Image);
            if(!$ctrl.adding){
                productsService.updateProduct($ctrl.product).then(
                  function() {
                      $location.path("/product/" + $ctrl.product.gtin);
                      return null;
                  }
                )
            } else if($ctrl.adding) {
                productsService.addProduct($ctrl.product).then(
                  function() {
                      $location.path("/product/" + $ctrl.product.gtin);
                      return null;
                  }
                )
            }
        }

        function getBase64() {
            var reader = new FileReader();
            reader.readAsDataURL($ctrl.imageFile);
            reader.onload = function () {
                console.log(reader);
                $ctrl.product.base64Image = reader.result;
                console.log($ctrl.product.base64Image);
            };
            reader.onerror = function (error) {
                console.log('Error: ', error);
            };
        }

        function addNewIngredient(){
            $ctrl.product.ingredients = $ctrl.product.ingredients || [];
            $ctrl.product.ingredients.push({});
        }

        function removeIngredient(ingredient) {
            $ctrl.product.ingredients.splice($ctrl.product.ingredients.indexOf(ingredient), 1 );
        }

        function getProductByEan() {
            productsService.getFullProductInfoByEan($ctrl.ean).then(
                function(response) {
                    if(response) {
                        $ctrl.product = response.data;
                    }
                    else {
                        $ctrl.product = {};
                        $ctrl.product.ingredients = [];
                        return {};
                    }
                }
            )
        }
    }
})(window.angular);