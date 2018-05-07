using MyProject.Entities.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Entities.Models
{
    public class Employee: Entity<long>
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string EmailId { get; set; }

        [MaxLength(10)]
        public string MobileNumber { get; set; }

        public genderType GenderType { get; set; }

        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
