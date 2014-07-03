'use strict';

todoApp.controller('ListController', function ListController($scope, todoData) {
    $scope.limit = 20;
    $scope.offset = 0;
    $scope.sortOrder = '';
    $scope.desc = false;

    $scope.search = function () {
        console.log('search()');
        todoData.getAllToDos({ q: $scope.query, limit: $scope.limit, offset: $scope.offset, sort: $scope.sortOrder, desc: $scope.sortDesc  },
            function (items) {
                var count = items.length;
                $scope.noMore = (count < 20);
                $scope.items = $scope.items.concat(items);
                console.log("Next " + count + " items... Total shown: " + $scope.items.length);
            })
    };

    $scope.delete = function () {
        var itemId = this.item.id;
        todoData.delete(itemId, function () {
            $("#item_" + itemId).fadeOut();
        });
    }

    $scope.reset = function () {
        $scope.offset = 0;
        $scope.items = [];
        $scope.search();
    };

    $scope.showMore = function () {
        return !$scope.noMore;
    };

    $scope.sortBy = function (order) {
        if ($scope.sortOrder == order) {
            $scope.sortDesc = !$scope.sortDesc;
        }
        else {
            $scope.sortDesc = false;
        }

        $scope.sortOrder = order;

        console.log("sortOrder = " + $scope.sortOrder);
        console.log("sortDesc = " + $scope.sortDesc);

        $scope.reset();
    };

    $scope.doShow = function (ascending, column) {
        return (ascending != $scope.sortDesc) && ($scope.sortOrder == column);
    };

    $scope.reset();
});
