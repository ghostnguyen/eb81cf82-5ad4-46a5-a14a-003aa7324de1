using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;

namespace OAMS.Models
{
    public class ContractorContactRepository : BaseRepository
    {
        public IQueryable<ContractorContact> GetAll()
        {
            return DB.ContractorContacts;
        }

        public ContractorContact Add(int contractorID)
        {
            var contractorRepo = new ContractorRepository() { DB = DB };
            var contractor = contractorRepo.Get(contractorID);

            var contractorContact = new ContractorContact();
            contractor.ContractorContacts.Add(contractorContact);
            Save();
            return contractorContact;
        }

        public ContractorContact Get(int id)
        {
            return DB.ContractorContacts.SingleOrDefault(r => r.ID == id);
        }

        public ContractorContact Update(ContractorContact e)
        {
            DB.ContractorContacts.Attach(e);
            DB.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
            Save();
            return e;
        }

        public void Delete(int id)
        {
            var v = Get(id);
            DB.DeleteObject(v);
            Save();
        }
    }
}
