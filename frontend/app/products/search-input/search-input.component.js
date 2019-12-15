(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('searchInput', {
        templateUrl: 'products/search-input/search-input.component.html',
        controller: searchInputController
    });

    searchInputController.$inject = ['productsService', '$location'];

    function searchInputController(productsService, $location) {
        var $ctrl = this;
        $ctrl.searchInput = "";
        $ctrl.hints = [];
        $ctrl.search = search;
        $ctrl.submit = submit;

        function submit() {
            $location.path(/product-search/ + $ctrl.searchInput);
        }

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