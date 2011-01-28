using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ContractDetailTimelineRepository : BaseRepository
    {
        public ContractDetailTimeline Add(int contractDetailID, int timeCount, string timeUnit)
        {
            timeCount = timeCount < 1 ? 1 : timeCount;
            ContractDetailRepository contractDetailRepo = new ContractDetailRepository() { DB = DB };
            var contractDetail = contractDetailRepo.Get(contractDetailID);

            var lastTimeline = contractDetail.ContractDetailTimelines.OrderByDescending(r => r.Order).FirstOrDefault();

            var newTimeline = new ContractDetailTimeline();

            if (lastTimeline == null)
            {
                newTimeline.Order = 1;
                newTimeline.FromDate = contractDetail.EffectiveDate.HasValue ? contractDetail.EffectiveDate : DateTime.Now.Date;
            }
            else
            {
                newTimeline.Order = lastTimeline.Order + 1;
                newTimeline.FromDate = lastTimeline.ToDate.Value.AddDays(1);
            }

            if (timeUnit.ToLower() == "day")
            {
                newTimeline.ToDate = newTimeline.FromDate.Value.AddDays(timeCount).AddDays(-1);
            }
            else if (timeUnit.ToLower() == "month")
            {
                newTimeline.ToDate = newTimeline.FromDate.Value.AddMonths(timeCount).AddDays(-1);
            }

            newTimeline.ContractDetailID = contractDetailID;

            DB.AddToContractDetailTimelines(newTimeline);

            Save();

            return newTimeline;
        }

        public ContractDetailTimeline Get(int id)
        {
            return DB.ContractDetailTimelines.Single(r => r.ID == id);
        }

        public ContractDetailTimeline Get(int? contractDetailID, int? order)
        {
            return DB.ContractDetailTimelines.SingleOrDefault(r => r.ContractDetailID == contractDetailID && r.Order == order);
        }

        public ContractDetail DeleteTheLastTimeline(int contractDetailID)
        {
            ContractDetailRepository contractDetailRepo = new ContractDetailRepository() { DB = DB };
            var r = contractDetailRepo.Get(contractDetailID);
            var lastTimeline = r.ContractDetailTimelines.OrderByDescending(r1 => r1.Order).FirstOrDefault();
            if (lastTimeline != null)
            {
                if (r.SiteMonitorings.Where(r1 => r1.Order == lastTimeline.Order).Count() == 0)
                {
                    DB.DeleteObject(lastTimeline);
                    Save();
                }
            }
            return r;
        }

        public ContractDetailTimeline Update(ContractDetailTimeline e)
        {
            //var r = Get(e.ID);
            //if (r != null)
            //{
            //    r.FromDate = e.FromDate;
            //    r.ToDate = e.ToDate;

            //    if (ValidConflictTimeline(r))
            //    {
            //        Save();
            //        e = r;
            //    }
            //}
            //return e;

            DB.ContractDetailTimelines.Attach(e);
            if (ValidConflictTimeline(e))
            {
                DB.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
                Save();
            }

            return e;
        }

        public bool ValidConflictTimeline(ContractDetailTimeline e)
        {
            bool isValid = true;

            ContractDetailTimeline preTimeline = Get(e.ContractDetailID, e.Order - 1);
            ContractDetailTimeline nextTimeline = Get(e.ContractDetailID, e.Order + 1);

            if (preTimeline != null)
            {
                if (preTimeline.IsJoin(e))
                    isValid = false;
            }

            if (nextTimeline != null)
            {
                if (nextTimeline.IsJoin(e))
                    isValid = false;
            }

            return isValid;
        }


    }
}