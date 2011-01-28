using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;

namespace OAMS.Models
{
    public class ClientContactDetailRepository : BaseRepository
    {
        public IQueryable<ClientContactDetail> GetAll()
        {
            return DB.ClientContactDetails;
        }

        public ClientContactDetail Add(int clientContactID)
        {
            var ClientContactRepo = new ClientContactRepository() { DB = DB };
            var ClientContact = ClientContactRepo.Get(clientContactID);

            var ClientContactDetail = new ClientContactDetail();
            ClientContact.ClientContactDetails.Add(ClientContactDetail);
            Save();
            return ClientContactDetail;
        }

        public ClientContactDetail Get(int id)
        {
            return DB.ClientContactDetails.SingleOrDefault(r => r.ID == id);
        }

        public ClientContactDetail Update(ClientContactDetail e)
        {
            DB.ClientContactDetails.Attach(e);
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
