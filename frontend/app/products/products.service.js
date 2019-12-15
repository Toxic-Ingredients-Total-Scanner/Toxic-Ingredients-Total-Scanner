(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').service('productsService', productsServiceController);

  productsServiceController.$inject = ['$http'];

  function productsServiceController($http) {
    var self = this;

    self.searchFor = searchFor;
    self.getFullProductInfoByEan = getFullProductInfoByEan;


    function searchFor(searching) {
      return $http.get('/api/Products/names?phrase=' + searching)
        .then(function(response) {
          return response;
      }, function errorCallback(response) {
      });
    }

    function getFullProductInfoByEan(eanCode) {
      return $http.get('/api/Products/fullRequest?ean=' + eanCode)
        .then(function(response) {
          return response;
        }, function errorCallback(response) {
          return null;
        });
    }
  }
})(window.angular);