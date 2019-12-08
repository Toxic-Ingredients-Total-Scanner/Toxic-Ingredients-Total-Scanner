(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').config(productsConfig);

  productsConfig.$inject = ['$stateProvider', '$urlRouterProvider'];

  function productsConfig($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider
      .state('productViewState', {
        url: '/product/{ean:[0-9]*}',
        template: '<product-view></product-view>'
      });


  }
})(window.angular);