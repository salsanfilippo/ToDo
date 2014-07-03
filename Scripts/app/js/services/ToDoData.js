'use strict';

todoApp.factory('todoData', function ($resource) {
    var resource = $resource('/api/todo/:id', { id: '@id' }, { update: { method: 'PUT' } });
    return {
        getToDo: function (todoId, callback) {
            return resource.get({ id: todoId }, function (todo) {
                if (callback)
                    callback(todo);
            });
        },

        getAllToDos: function (query, callback) {
            return resource.query(query, callback);
        },

        delete: function(id, callback) {
            resource.delete({ id: id }, callback);
        },

        save: function (todo, callback) {
            if (angular.isUndefined(todo.id)) { // Create new ToDo
                resource.save(todo, callback);
            } else { // Update existing ToDo
                resource.update({ id: todo.id }, todo, callback);
            }
        }
    }
});