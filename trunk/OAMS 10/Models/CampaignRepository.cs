using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class CampaignRepository : BaseRepository<CampaignRepository>
    {
        public IQueryable<Campaign> GetAll()
        {
            return DB.Campaigns;
        }

        public Campaign Get(int id)
        {
            return DB.Campaigns.Where(r => r.ID == id).SingleOrDefault();
        }

        public Campaign Add(Campaign e)
        {
            DB.Campaigns.AddObject(e);
            return e;
        }
    }
}