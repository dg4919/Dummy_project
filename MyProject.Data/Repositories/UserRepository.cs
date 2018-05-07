using MyProject.Entities.Models;
using MyProject.Context;
using System.Linq;

namespace MyProject.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(dbContext dbcontext) : base(dbcontext) { }

        //***********************  Functions *************************

        public bool is_userExist(string emailId)
        {
            return _dbset.Any(x => x.EmailId == emailId);
        }

        public User get_userInfo(string emailId)
        {
            return _dbset.SingleOrDefault(x => x.EmailId == emailId);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;

            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public string GetPasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


    }
}
