'use strict';

var todoApp = angular.module('todoApp', ['ui.bootstrap', 'ngRoute', 'ngResource'])
    .config(function ($routeProvider, $locationProvider) {
        console.log('.config()');
        $routeProvider.
            when('/', { controller: 'ListController', templateUrl: '/scripts/app/views/list.html' }).
            when('/new', { controller: 'EditController', templateUrl: '/scripts/app/views/detail.html' }).
            when('/edit/:todoId', { controller: 'EditController', templateUrl: '/scripts/app/views/detail.html' }).
            otherwise({ redirectTo: '/' });
            $locationProvider.html5Mode(true);
    });
