using MyProject.Entities.Models;

namespace MyProject.Data
{
    public interface IUserRepository : IRepository<User>
    {
        bool is_userExist(string emailId);
        User get_userInfo(string emailId);

        bool VerifyPassword(string password, string passwordHash);
        string GetPasswordHash(string password);
    }
}
