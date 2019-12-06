(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').service('productsService', productsServiceController);

  productsServiceController.$inject = ['$http'];

  function productsServiceController($http) {
    var self = this;

    self.randomFunction = randomFunction;
    self.searchFor = searchFor;

    function randomFunction(param) {
      console.log("productsService " + param);
    }

    //TODO handle returned promise object
    function searchFor(searching) {
      return $http.get('/api/Products/names?phrase=' + searching)
        .then(function successCallback(response) {
        return response.data;
      }, function errorCallback(response) {
      });
    }
  }
})(window.angular);