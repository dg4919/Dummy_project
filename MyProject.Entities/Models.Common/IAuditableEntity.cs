using System;

namespace MyProject.Entities.Models.Common
{
    public interface IAuditableEntity
    {
        #region Property

        DateTime CreationDate { get; set; }
        long CreatedBy { get; set; }
        DateTime UpdationDate { get; set; }
        long UpdatedBy { get; set; }
       
        #endregion
    }
}
