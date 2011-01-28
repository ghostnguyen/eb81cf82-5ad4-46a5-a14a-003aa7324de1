using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;

namespace OAMS.Models
{
    public class ClientContactRepository : BaseRepository
    {
        public IQueryable<ClientContact> GetAll()
        {
            return DB.ClientContacts;
        }

        public ClientContact Add(int clientID)
        {
            var ClientRepo = new ClientRepository() { DB = DB };
            var Client = ClientRepo.Get(clientID);

            var ClientContact = new ClientContact();
            Client.ClientContacts.Add(ClientContact);
            Save();
            return ClientContact;
        }

        public ClientContact Get(int id)
        {
            return DB.ClientContacts.SingleOrDefault(r => r.ID == id);
        }

        public ClientContact Update(ClientContact e)
        {
            DB.ClientContacts.Attach(e);
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
