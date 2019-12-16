(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').service('productsService', productsServiceController);

  productsServiceController.$inject = ['$http'];

  function productsServiceController($http) {
    var self = this;

    self.searchFor = searchFor;
    self.getFullProductInfoByEan = getFullProductInfoByEan;
    self.getProductByEan = getProductByEan;
    self.updateProduct = updateProduct;
    self.addProduct = addProduct;

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

    function updateProduct(product) {
      return $http.put('/api/Products/fullRequest', product)
        .then(function(response) {
          return response;
        }, function errorCallback(response) {
          return response;
        });
    }

    function addProduct(product) {
      return $http.post('/api/Products/fullRequest', product)
        .then(function(response) {
          return response;
        }, function errorCallback(response) {
          return response;
        });
    }

    function getProductByEan(eanCode) {
      return $http.get('/api/Products?ean=' + eanCode)
        .then(function(response) {
          return response;
        }, function errorCallback(response) {
          return null;
        });
    }

  }
})(window.angular);