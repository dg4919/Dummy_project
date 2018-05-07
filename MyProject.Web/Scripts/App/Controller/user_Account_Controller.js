/// <reference path="../angular-1.5.8.js" />

// User Account related functionality
(function (app) {

    var user_loginControllerfn = function ($sc, $rsc, $state, acount_svc) {
        $sc.userInfo = {};

        $sc.user_SignIn = function (form) {
            if (form.validate())
                loginUser();
        }

        // validating rules for registration mmodel
        $sc.validationOptions = {
            rules: {
                userName: {            // use with name attribute in control
                    required: true,
                    validateEmailMobile: true
                },
                password: {            // use with name attribute in control
                    required: true
                }
            }
        }

        function loginUser() {
            acount_svc.Login($sc.userInfo).then(function (result) {

                if (result.msg === 'ok') {
                    $rsc.notify_fx('You have successfully login !', 'success');
                    $state.go('employee_info');
                }
                else if (result.msg === 'invalid') {
                    $rsc.notify_fx('username or password is incorrect', 'warning');
                }
                else if (result.msg === 'notfound') {
                    $rsc.notify_fx('You are not registered with us. Please sign up.', 'error');
                }

            }, function () {
                $rsc.user.currentUser = '';
                $rsc.user.is_login = false;
                console.log('in error');
            });
        }
    }

    var user_signUpControllerfn = function ($sc, $rsc, acount_svc) {
        $sc.userInfo = {};

        $sc.register_User = function (form) {

            if (form.validate()) {
                acount_svc.Register($sc.userInfo).then(function (d) {
                    if (d.result === 'ok') {
                        $rsc.notify_fx('You have successfully registered with us !', 'success');

                        var _userInfo = {
                            userName: $sc.userInfo.emailId,
                            Password: $sc.userInfo.password
                        };
                        acount_svc.Login(_userInfo);
                    }
                    else if (d.result === 'exist') {
                        $rsc.notify_fx('User Email Id already exist, Try another :(', 'warning');
                    }
                });
            }
        }

        // validating rules for registration model
        $sc.validationOptions = {
            rules: {
                userName: {
                    required: true,
                },
                mobileNo:{
                    required: true,
                    minlength: 10
                },
                emailId: {            // use with name attribute in control
                    required: true,
                    email: true
                },
                pasword: {
                    required: true,
                },
                confirm_pasword: {
                    required: true,
                    equalTo: "#password"
                },
            },
            messages: {
                mobileNo: {            // use with name attribute in control
                    required: "Mobile number is required !",
                    minlength: "Please enter a valid Mobile number"
                },
                emailId: {            // use with name attribute in control
                    required: "Email is required !",
                    email: "Please enter a valid email id !"
                },
            }
        }
    }

    var employeeFn = function ($sc, $rsc, svc) {
        $sc.empInfo = {};
        $sc.show_empList = true;

        function getEmpInfo() {
            svc.get_empInfo()
                .then(function (d) {
                    $sc.empList = d.result;
                    $sc.genderInfo = d.genderList;
            });
        }

        getEmpInfo();

        $sc.create_employ = function (form) {

            if (form.validate()) {
                svc.createEmploy($sc.empInfo)
                    .then(function (d) {
                        if (d.result === 'ok')
                            $rsc.notify_fx('You have successfully registered with us !', 'success');
                        else if (d.result === 'exist')
                            $rsc.notify_fx('Employee Email Id or Mobile Number already exist, Try another :(', 'warning');
                    });
            }
        }

        $sc.validationOptions = {
            rules: {
                userName: {
                    required: true,
                },
                mobileNo: {
                    required: true,
                    minlength: 10
                },
                emailId: {            // use with name attribute in control
                    required: true,
                    email: true
                },
                gender: {
                    required: true,
                }
            },
            messages: {
                mobileNo: {            // use with name attribute in control
                    required: "Mobile number is required !",
                    minlength: "Please enter a valid Mobile number"
                },
                emailId: {            // use with name attribute in control
                    required: "Email is required !",
                    email: "Please enter a valid email id !"
                },
            }
        }

        $sc.showInfo = function (val) {
            if (val === 1)
                $sc.show_empList = false;
            else {
                $sc.show_empList = true;
                getEmpInfo();
            }
        }

    }

    // In new ui-bootstrap-tpls-2.3.0  =>  ($modal changed to > $uibModal)  &&  ('$ModalInstance' changed to > $uibModalInstance)
    app.controller('user_loginController', ['$scope', '$rootScope', '$state', 'user_Account_Service', user_loginControllerfn])
       .controller('user_signUpController', ['$scope', '$rootScope', 'user_Account_Service', user_signUpControllerfn])
       .controller('employeeController', ['$scope', '$rootScope', 'siteMasterService', employeeFn]);

    ;

})(angular.module('Silverzone_app'));