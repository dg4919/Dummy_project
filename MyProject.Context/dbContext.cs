using System;
using System.Linq;
using System.Data.Entity;
using MyProject.Entities.Models.Common;
using System.Threading;
using System.Data.Entity.Validation;
using MyProject.Entities.Models;

namespace MyProject.Context
{
    // connection string name & context class name should be same
    public class dbContext : DbContext
    {
        public dbContext() :
            base("myProjectContext")
        {
            Database.SetInitializer<dbContext>(null);
        }

        #region  ******************  Properties  ******************

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }

        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentEntry>()
            //  .HasOptional(x => x.QuestionPaperLot)
            //  .WithRequired(y => y.StudentEntry);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.Entity is IAuditableEntity && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));
            foreach (var entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identity = Thread.CurrentPrincipal.Identity.Name;
                    long identityName = string.IsNullOrEmpty(identity) ? 0 : long.Parse(identity);
                    //long identityName = 1;

                    TimeZone zone = TimeZone.CurrentTimeZone;
                    DateTime now = zone.ToUniversalTime(DateTime.Now);

                    //DateTime now = DateTime.UtcNow;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreationDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreationDate).IsModified = false;
                    }
                    entity.UpdatedBy = identityName;
                    entity.UpdationDate = now;
                }
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                // var newException = new FormatException(e);  // format the exception messgae
                throw e;
            }
        }

    }
}
