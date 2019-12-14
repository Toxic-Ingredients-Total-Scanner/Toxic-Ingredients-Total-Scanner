(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner')
    .component('homePage', {
      templateUrl: 'home-page/home-page.component.html',
      controller: homePageController
    });

  homePageController.$inject = [];

  function homePageController() {
    var $ctrl = this;
  }
})(window.angular);