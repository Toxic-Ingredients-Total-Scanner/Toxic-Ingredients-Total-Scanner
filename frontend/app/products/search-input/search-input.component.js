(function (angular) {
    'use strict';
    angular.module('toxicIngredientsTotalScanner').component('searchInput', {
        templateUrl: 'products/search-input/search-input.component.html',
        controller: searchInputController
    });

    searchInputController.$inject = ['$scope', '$element', '$attrs', '$http'];

    function searchInputController($scope, $element, $attrs, $http) {
        var $ctrl = this;
        $ctrl.search
        $ctrl.hints;

        $ctrl.searchFor = function() {
            if($ctrl.search.length < 3) return $ctrl.hints = [];
            return $http({
                method: 'GET',
                url: '/api/Products/names?phrase=' + $ctrl.search
            }).then(function successCallback(response) {
                $ctrl.hints = response.data;
                console.log($ctrl.hints);
            }, function errorCallback(response) {
            });
        }


    }
})(window.angular);