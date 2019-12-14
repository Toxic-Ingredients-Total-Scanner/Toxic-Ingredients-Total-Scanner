(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').config(productsConfig);

  productsConfig.$inject = ['$locationProvider', '$routeProvider'];

  function productsConfig($locationProvider, $routeProvider) {
      $locationProvider.html5Mode(true);
      $locationProvider.hashPrefix('');

      $routeProvider
          .when("/product/:ean", {
              template: "<product-view></product-view>"
          })
          .when("/", {
            template: "<home-page></home-page>"
          });
      $routeProvider.otherwise({redirectTo:'/'});

  }
})(window.angular);