using MyProject.Entities.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyProject.Entities.ViewModel
{
    public class LoginViewModel
    {
        [Required, MaxLength(100)]
        public string userName { get; set; }

        [Required, MaxLength(100)]
        public string Password { get; set; }

        public static dynamic parse(User _user)
        {
            return new
            {
                _user.Name,
                _user.EmailId,
            };
        }

        public static dynamic parse(ICollection<Employee> empList)
        {
            return empList.Select(x => new
            {
                x.Name,
                x.EmailId,
                x.MobileNumber,
                Gender = x.GenderType.ToString()
            });
        }
    }

    public class AccessTokenViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }

    public class RegisterViewModel
    {
        [Required, MaxLength(100)]
        public string userName { get; set; }

        [Required, MaxLength(100)]
        public string emailId { get; set; }

        [Required, MaxLength(100)]
        public string password { get; set; }

        public static User parse(RegisterViewModel vm)
        {
            return new User()
            {
                Name = vm.userName,
                EmailId = vm.emailId,
                Password = vm.password,
                Status = true
            };
        }
    }


}