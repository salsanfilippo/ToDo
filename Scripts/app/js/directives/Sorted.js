'use strict';

todoApp.directive('sorted', function () {
    return {
        restrict: 'A',
        scope: true,
        transclude: true,
        template: '<a ng-click="doSort()" ng-transclude></a>' +
                  '&nbsp;' +
                  '<span ng-show="doShow(true)"><i class="glyphicon glyphicon-collapse-up"></i></span>' +
                  '<span ng-show="doShow(false)"><i class="glyphicon glyphicon-collapse-down"></i></span>',
        controller: function ($scope, $element, $attrs) {
            $scope.sort = $attrs.sorted;

            $scope.doSort = function () {
                $scope.sortBy($scope.sort);
            };

            $scope.doShow = function(asc) {
                return (asc != $scope.sortDesc) && ($scope.sortOrder == $scope.sort);
            };
        }
    }
});