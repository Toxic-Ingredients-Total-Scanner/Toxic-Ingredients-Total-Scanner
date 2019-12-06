(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('searchInput', {
        templateUrl: 'products/search-input/search-input.component.html',
        controller: searchInputController
    });

    searchInputController.$inject = ['productsService'];

    function searchInputController(productsService) {
        var $ctrl = this;
        $ctrl.search
        $ctrl.hints;

        $ctrl.searchFor = searchFor;

        function searchFor(searching) {
            productsService.searchFor(searching).then(
              function(response) {
                  $ctrl.hints = response;
              }
            )
        }
    }
})(window.angular);