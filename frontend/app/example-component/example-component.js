(function (angular) {
  'use strict';
  angular.module('toxicIngredientsTotalScanner').component('exampleComponent', {
    templateUrl: 'example-component/example-component.html',
    controller: exampleComponentController
  });

  exampleComponentController.$inject = ['$scope', '$element', '$attrs'];

  function exampleComponentController($scope, $element, $attrs) {
    var ctrl = this;
    ctrl.list = [
      {
        name: 'Superman',
        location: ''
      },
      {
        name: 'Batman',
        location: 'Wayne Manor'
      }
    ];
    ctrl.updateHero = function (hero, prop, value) {
      hero[prop] = value;
    };

    ctrl.deleteHero = function (hero) {
      var idx = ctrl.list.indexOf(hero);
      if (idx >= 0) {
        ctrl.list.splice(idx, 1);
      }
    };
  }
})(window.angular);