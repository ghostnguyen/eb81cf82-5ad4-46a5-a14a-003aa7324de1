using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class CampaignDetailRepository : BaseRepository<CampaignDetailRepository>
    {
        public CampaignDetail Create(int campaignID,int contractDetailID)
        {
            CampaignDetail old = DB.CampaignDetails.Where(r => r.CampaignID == campaignID && r.ContractDetailID == contractDetailID).SingleOrDefault();

            if (old != null) return null;

            CampaignDetail e = new CampaignDetail();
            e.CampaignID = campaignID;
            e.ContractDetailID = contractDetailID;

            DB.CampaignDetails.AddObject(e);

            return e;
        }

        
    }
}