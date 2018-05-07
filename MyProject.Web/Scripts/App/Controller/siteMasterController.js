/// <reference path="../angular-1.5.8.js" />

// Use to work with Website Layout functionality
(function (app) {

    // when redirect to other URL > then there respective ctrler is called & 'site_MasterController' called once
    // children controller inside this ctrler > can access scope variable of this ctrl
    var site_MasterControllerfn = function ($sc, $rsc, $modal, $state, acount_svc, localStorageService, siteSvc, $window) {

        $rsc.user_logOut = function () {
            acount_svc.Logout();
            $state.go('book_list');
        }

        $sc.factorial = function (num) {
            if (!num)
                return 0;

            var res = 0;
            for (i = 1; i <= num; i++) {
                res *= i;
            }

            return res;
        }

    }

    app
    .controller('site_MasterController', ['$scope', '$rootScope', '$uibModal', '$state', 'user_Account_Service', 'localStorageService', 'siteMasterService', '$window', site_MasterControllerfn])
    
    ;

})(angular.module('Silverzone_app'));