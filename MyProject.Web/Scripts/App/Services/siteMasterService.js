
(function (app) {

    var fn = function (globalConfig, httpSvc) {
        var apiUrl = globalConfig.apiEndPoint + globalConfig.version.Site,
            fac = {};

        fac.get_empInfo = function (model) {
            return httpSvc.get(apiUrl + '/Home/get_employee');
        };

        fac.createEmploy = function (_model) {
            return httpSvc.post(
             apiUrl + '/Home/create_employee',
             _model);
        }
        
        
        return fac;
    }

    app.factory('siteMasterService', ['globalConfig', 'httpService', fn]);

})(angular.module('Silverzone_app'));

