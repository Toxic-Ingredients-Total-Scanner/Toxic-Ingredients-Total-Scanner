(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').service('productsService', productsServiceController);

  productsServiceController.$inject = ['$http'];

  function productsServiceController($http) {
    var self = this;

    self.searchFor = searchFor;

    //TODO handle returned promise object
    function searchFor(searching) {
      return $http.get('/api/Products/names?phrase=' + searching)
        .then(function(response) {
          return response;
      }, function errorCallback(response) {
      });
    }
  }
})(window.angular);