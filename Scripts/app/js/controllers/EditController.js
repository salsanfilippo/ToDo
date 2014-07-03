'use strict';

todoApp.controller('EditController', function EditController($scope, $location, $routeParams, todoData) {
    if (angular.isUndefined($routeParams.todoId)) {
        $scope.item = { todo: 'Todo 1002', priority: '1', dueDate: '11/05/2012' };
    } else {
        todoData.getToDo($routeParams.todoId, function (item) {
            var dateComponents = item.dueDate.split('T')[0].split('-');

            $scope.item = item;
            $scope.item.dueDate = dateComponents[1] + '/' + dateComponents[2] + '/' + dateComponents[0];
        });
    }

    $scope.save = function () {
        todoData.save($scope.item, function (newToDo, headers) {
            if (angular.isUndefined($scope.item.id)) {
                $scope.item.id = newToDo.id;
                console.log('ToDo created, Id: ' + $scope.item.id);
            } else {
                console.log('ToDo updated, Id: ' + $scope.item.id);
            }
        });
    }

    $scope.cancel = function () {
        $location.url("/");
    }
});