using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Objects;

namespace OAMS.Models
{
    public partial class OAMSEntities
    {
        public string Username { get; set; }

        public override int SaveChanges(System.Data.Objects.SaveOptions options)
        {
            foreach (ObjectStateEntry entry in ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                dynamic e = entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    try
                    {
                        e.CreatedDate = DateTime.Now;
                        e.CreatedBy = OAMSSetting.Username;
                    }
                    catch (Exception)
                    {
                    }
                }

                try
                {
                    e.LastUpdatedDate = DateTime.Now;
                    e.LastUpdatedBy = OAMSSetting.Username;
                }
                catch (Exception)
                {
                }



                // Validate the objects in the Added and Modified state
                // if the validation fails throw an exeption.
            }
            return base.SaveChanges(options);
        }
    }
}