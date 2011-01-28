using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;

namespace OAMS.Models
{
    public class ContractorContactDetailRepository : BaseRepository
    {
        public IQueryable<ContractorContactDetail> GetAll()
        {
            return DB.ContractorContactDetails;
        }

        public ContractorContactDetail Add(int contractorContactID)
        {
            var contractorContactRepo = new ContractorContactRepository() { DB = DB };
            var contractorContact = contractorContactRepo.Get(contractorContactID);

            var contractorContactDetail = new ContractorContactDetail();
            contractorContact.ContractorContactDetails.Add(contractorContactDetail);
            Save();
            return contractorContactDetail;
        }

        public ContractorContactDetail Get(int id)
        {
            return DB.ContractorContactDetails.SingleOrDefault(r => r.ID == id);
        }

        public ContractorContactDetail Update(ContractorContactDetail e)
        {
            DB.ContractorContactDetails.Attach(e);
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
