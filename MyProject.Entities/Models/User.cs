using MyProject.Entities.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Entities.Models
{
    public class User: Entity<long>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string EmailId { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
