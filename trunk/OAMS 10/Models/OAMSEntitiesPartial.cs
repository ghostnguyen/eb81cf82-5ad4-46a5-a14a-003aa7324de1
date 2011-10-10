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

                if (entry.State == EntityState.Modified)
                {
                    if (e is Site)
                    {
                        int count = 0;


                        for (int i = 0; i < entry.CurrentValues.FieldCount; i++)
                        {
                            var o = entry.OriginalValues.GetValue(i);
                            var c = entry.CurrentValues.GetValue(i);

                            if (o is DBNull && c is DBNull)
                            { }
                            else if (o is DBNull || c is DBNull)
                            {
                                count++;
                            }
                            else
                            {
                                if (c is int)
                                {
                                    if ((int)c != (int)o)
                                    {
                                        count++;
                                    }
                                }

                                if (c is Guid)
                                {
                                    if ((Guid)c != (Guid)o)
                                    {
                                        count++;
                                    }
                                }

                                if (c is double)
                                {
                                    if ((double)c != (double)o)
                                    {
                                        count++;
                                    }
                                }

                                if (c is string)
                                {
                                    if ((string)c != (string)o)
                                    {
                                        count++;
                                    }
                                }

                                if (c is bool)
                                {
                                    if ((bool)c != (bool)o)
                                    {
                                        count++;
                                    }
                                }
                            }

                        }

                        if (count > AppSetting.PropertiesCount)
                        {
                            throw new Exception("Site is modified with many properties.");
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
                }


                // Validate the objects in the Added and Modified state
                // if the validation fails throw an exeption.
            }
            return base.SaveChanges(options);
        }
    }
}