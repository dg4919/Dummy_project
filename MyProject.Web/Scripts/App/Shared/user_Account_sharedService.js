// contains all ajax related to user A/c > like regiter,login, forget/reset passowrd etc

(function () {

    var fn = function (globalConfig, localStorageService, $filter, $rsc, httpSvc) {

        // define multiple variables at a time
        var apiUrl = globalConfig.apiEndPoint + globalConfig.version.Site,
            fac = {};

        fac.Register = function (_model) {
            return httpSvc.post(
               apiUrl + '/Home/register_User',
               _model);
        }

        fac.Login = function (_model) {
            return httpSvc
                  .post(apiUrl + '/Home/Login', _model)
                  .then(function (d) {
                      if (d.result === 'ok')
                          fac.saveUserInfo(d.user, d.token);

                      return {
                          msg: d.result,
                      };                                                  // only will pass status of login
                  });
        }

        fac.Logout = function () {

            localStorageService.remove('authorizeData');

            $rsc.user.currentUser = '';
            $rsc.user.is_login = false;
        }

        fac.saveUserInfo = function (user, token) {
            var entity = {
                userInfo: user,
                tokenInfo: token
            }

            // local storage store user info in browser & when ever user clear history then it will also removed
            localStorageService.set('authorizeData', entity);

            $rsc.user.currentUser = user;
            $rsc.user.is_login = true;
        }


        //*********  Forget Login ******************

        fac.forgetPassword = function (_model) {
            return httpSvc
                .get('templates/EmailTemplates/forget_password.html')
                .then(function (response) {
                    angular.extend(_model, {
                        html_template: response
                    });

                    return httpSvc.post(
                                 apiUrl + '/Home/forget_password',
                                 _model);
                });
        }

        fac.verify_forgetLogin_OTP = function (_model) {
            return httpSvc.post(
              apiUrl + '/Home/verify_forgetLogin_OTP',
              _model);
        }

        fac.save_newPassword = function (_model) {
            return httpSvc.post(
             apiUrl + '/Home/saved_newforget_password',
             _model);
        }


        return fac;
    }

    angular.module('Silverzone_service.Shared')
        .factory('user_Account_Service', ['globalConfig', 'localStorageService', '$filter', '$rootScope', 'httpService', fn]);

})();

