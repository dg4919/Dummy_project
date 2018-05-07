using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Entities.Models.Common
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public DateTime CreationDate { get; set; }
       
        public long CreatedBy { get; set; }

        public DateTime UpdationDate { get; set; }

        public long UpdatedBy { get; set; }
    }
}
