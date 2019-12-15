(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('searchInput', {
        templateUrl: 'products/search-input/search-input.component.html',
        controller: searchInputController
    });

    searchInputController.$inject = ['productsService'];

    function searchInputController(productsService) {
        var $ctrl = this;
        $ctrl.searchInput = "";
        $ctrl.hints = [];
        $ctrl.search = search;

        function search() {
            if($ctrl.searchInput.length < 1) $ctrl.hints = [];
            if($ctrl.searchInput.length < 3) return;
            var eanRegex = /^[0-9]*$/;
            if(!$ctrl.searchInput.match(eanRegex)){
                searchFor();
            }
        }

        function searchFor() {
            productsService.searchFor($ctrl.searchInput).then(
              function(response) {
                  $ctrl.hints = response.data;
              }
            )
        }
    }
})(window.angular);