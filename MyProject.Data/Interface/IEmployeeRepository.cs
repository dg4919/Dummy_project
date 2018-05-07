using MyProject.Entities.Models;

namespace MyProject.Data
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        bool is_emailIdExist(string emailId);
        bool is_mobilNoExist(string mobileNo);
    }
}
