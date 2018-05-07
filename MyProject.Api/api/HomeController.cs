using MyProject.Data;
using MyProject.Entities;
using MyProject.Entities.Models;
using MyProject.Entities.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Http;

namespace MyProject.Api.api.Site
{
    public class HomeController : ApiController
    {
        IUserRepository userRepository;
        IEmployeeRepository employeeRepository { get; set; }

        [HttpPost]
        public IHttpActionResult register_User(RegisterViewModel model)
        {
            if (userRepository.is_userExist(model.emailId))
                return Ok(new { result = "exist" });

            model.password = userRepository.GetPasswordHash(model.password);
            userRepository.Create(RegisterViewModel.parse(model));

            return Ok(new { result = "ok" });
        }

        [HttpPost]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!userRepository.is_userExist(model.userName))
                    return Ok(new { result = "notfound" });

                var _user = userRepository.get_userInfo(model.userName);
                if (userRepository.VerifyPassword(model.Password, _user.Password))
                    return Ok(new
                    {
                        result = "ok",
                        user = LoginViewModel.parse(_user),
                        token = getToken(model)
                    });
                else
                    return Ok(new { result = "invalid" });        // password is not match
            }

            return Ok(new { result = "error" });
        }

        [Authorize, HttpPost]
        public IHttpActionResult create_employee(Employee model)
        {
            if (employeeRepository.is_emailIdExist(model.EmailId) ||
                employeeRepository.is_mobilNoExist(model.MobileNumber))
                return Ok(new { result = "exist" });

            model.UserId = Convert.ToInt64(User.Identity.Name);
            employeeRepository.Create(model);
            return Ok(new { result = "ok" });
        }

        [Authorize, HttpGet]
        public IHttpActionResult get_employee()
        {
            long userId = Convert.ToInt64(User.Identity.Name);

            var _genders = Enum.GetValues(typeof(genderType))
                        .Cast<genderType>()
                        .Select(v => new
                        {
                            Id = v.GetHashCode(),
                            Name = v.ToString()
                        }).ToList();

            return Ok(new
            {
                result = LoginViewModel.parse(userRepository.FindById(userId).Employees),
                genderList = _genders
            });
        }

        private AccessTokenViewModel getToken(LoginViewModel model)
        {
            var url = Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority;
            WebRequest myWebRequest = WebRequest.Create(url + "/token");
            myWebRequest.ContentType = "application/x-www-form-urlencoded";
            myWebRequest.Method = "POST";
            var request = string.Format("grant_type=password&userName={0}&Password={1}",
                            HttpUtility.UrlEncode(model.userName),
                            HttpUtility.UrlEncode(model.Password));

            byte[] bytes = Encoding.ASCII.GetBytes(request);
            myWebRequest.ContentLength = bytes.Length;
            using (Stream outputStream = myWebRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }

            using (WebResponse webResponse = myWebRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AccessTokenViewModel));

                //Get deserialized object from JSON stream
                AccessTokenViewModel token = (AccessTokenViewModel)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }


        // *****************  Constructor  ********************************

        public HomeController(
            IUserRepository _userRepository,
            IEmployeeRepository _employeeRepository)
        {
            userRepository = _userRepository;
            employeeRepository = _employeeRepository;
        }


    }
}
