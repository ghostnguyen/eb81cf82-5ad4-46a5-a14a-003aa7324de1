using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ContractorRepository : BaseRepository
    {
        public IQueryable<Contractor> GetAll()
        {
            return DB.Contractors;
        }

        public Contractor Get(int ID)
        {
            return DB.Contractors.Where(r => r.ID == ID).SingleOrDefault();
        }

        public Contractor Add(Contractor e)
        {
            DB.Contractors.AddObject(e);
            Save();
            return e;
        }

        public Contractor Update(Contractor e)
        {
            Save();
            return e;
        }

        public void Delete(Contractor e)
        {
            DB.Contractors.DeleteObject(e);
            Save();
        }

        public bool Replace(int id, int replaceID)
        {
            Contractor e = Get(id);
            Contractor replaceContractor = Get(replaceID);

            if (e == null || replaceContractor == null)
            {
                return false;
            }
            else
            {
                IQueryable<Site> sL = DB.Sites.Where(r => r.ContractorID == replaceID);
                foreach (var item in sL)
                {
                    item.ContractorID = id;
                }

                IQueryable<Contract> cL = DB.Contracts.Where(r => r.ContractorID == replaceID);
                foreach (var item in cL)
                {
                    item.ContractorID = id;
                }

                Save();

                var contactL = replaceContractor.ContractorContacts.ToList();
                foreach (var item in contactL)
                {
                    var contactDetailL = item.ContractorContactDetails.ToList();
                    foreach (var itemDetail in contactDetailL)
                    {
                        DB.ContractorContactDetails.DeleteObject(itemDetail);                        
                    }
                    DB.ContractorContacts.DeleteObject(item);
                }

                DB.Contractors.DeleteObject(replaceContractor);
                Save();

                return true;
            }
        }
    }
}