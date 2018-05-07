/// <reference path="../../Lib/angular-1.5.8.js" />

(function () {

    var configFn = function ($stateProvider, $urlRouterProvider, $httpProvider, ScrollBarsProvider) {

        // scroll config
        ScrollBarsProvider.defaults = {
            axis: 'y', // enable 2 axis scrollbars by default
            theme: 'dark',
            autoHideScrollbar: true
        };

        // interceptor call before start & end for each ajax call > a centralised mechanism to handle reqst & response
        $httpProvider.interceptors.push('httpInterceptor_Service');

     
        // Default Url, if any URL not match, redirect to it
        $urlRouterProvider.otherwise('/Login');

        $stateProvider
            .state("project_login", {
                url: '/Login',
                templateUrl: 'templates/views/login.html',
                controller: 'user_loginController',
            })

        .state("user_register", {
            url: '/Register',
            templateUrl: 'templates/views/Registration.html',
            controller: 'user_signUpController',
        })

         .state("employee_info", {
             url: '/Employee',
             templateUrl: 'templates/views/employee.html',
             controller: 'employeeController',
             data: {
                 requireLogin: true
             }
         })

        ;
    }

    // it contains global setting like > global variable used across diffrent ctrlers in this application
    var runFn = function ($rsc, $state, $injector, webUrl) {
        webUrl.module = 'Site';         // value provider

        // it will call every time when ever we redirect to other URL/page
        // toState contain state name > means URL to redirect 
        // toParams contains paramerter with that URL 
        $rsc.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
            // to go up on eaach state change

            if ((toState.data !== undefined && toState.data.requireLogin)
                && $rsc.user.currentUser === '') {         // when user in not logged in & login is required !
                event.preventDefault();

                // server side clear > formauthetiation cookie
                $injector.get('user_Account_Service').Logout();     // calling directly service method
                $state.go('project_login');
            }
        });

    }


    var moduleDependencies =
        [
            'ngScrollbars',               // https://github.com/iominh/ng-scrollbars,
            'Silverzone_app.Common',//,     // load dependency from a common js file
        ];

    angular.module('Silverzone_app', moduleDependencies)
    .config(['$stateProvider', '$urlRouterProvider', '$httpProvider', 'ScrollBarsProvider', configFn])
    .run(['$rootScope', '$state', '$injector', 'webUrl', runFn])
    ;

   

})();