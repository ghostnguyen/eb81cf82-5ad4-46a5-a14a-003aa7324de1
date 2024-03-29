﻿using System;
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
            if (AppSetting.Offline)
            { }
            else
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

                        if (e is Site)
                        {
                            var site = e as Site;
                            site.UpdateScore();
                        }
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        int count = entry.CountPropertiesChanged();

                        if (e is Site)
                        {
                            if (count > AppSetting.PropertiesCount)
                            {
                                throw new Exception("Site is modified with many properties.");
                            }

                            var site = e as Site;
                            site.UpdateScore();
                        }

                        if (count == 0)
                        {
                            this.ObjectStateManager.ChangeObjectState(e, EntityState.Unchanged);
                        }
                        else
                        {
                            try
                            {
                                e.LastUpdatedDate = DateTime.Now;
                                e.LastUpdatedBy = OAMSSetting.Username;
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    // Validate the objects in the Added and Modified state
                    // if the validation fails throw an exeption.
                }
            }
            return base.SaveChanges(options);
        }
    }
}