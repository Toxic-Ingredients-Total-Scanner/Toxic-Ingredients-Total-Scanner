(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').config(productsConfig);

  productsConfig.$inject = ['$locationProvider', '$routeProvider'];

  function productsConfig($locationProvider, $routeProvider) {
      $locationProvider.html5Mode(true);
      $locationProvider.hashPrefix('');

      $routeProvider
          .when("/", {
              template: "<home-page></home-page>"
          })
          .when("/product/:ean", {
              template: "<product-view></product-view>"
          })
          .when("/product-search/:ean", {
              template: "<products-search-result></products-search-result>"
          })
          .when("/edit-product/:ean", {
              template: "<edit-product></edit-product>"
          })
          .when("/add-new-product/", {
              template: "<edit-product></edit-product>"
          });
      $routeProvider.otherwise({redirectTo:'/'});

  }
})(window.angular);