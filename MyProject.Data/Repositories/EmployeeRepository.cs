using MyProject.Entities.Models;
using MyProject.Context;
using System.Linq;

namespace MyProject.Data
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(dbContext dbcontext) : base(dbcontext) { }

        //***********************  Functions *************************

        public bool is_emailIdExist(string emailId)
        {
            return _dbset.Any(x => x.EmailId == emailId);
                                //&& x.UserId == uId);
        }

        public bool is_mobilNoExist(string mobileNo)
        {
            return _dbset.Any(x => x.MobileNumber == mobileNo);
        }

    }
}
