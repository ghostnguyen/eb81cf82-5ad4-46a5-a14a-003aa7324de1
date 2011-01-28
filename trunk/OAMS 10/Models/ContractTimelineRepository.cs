using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;

namespace OAMS.Models
{
    public class ContractTimelineRepository : BaseRepository
    {
        public ContractTimeline Add(int contractID, int timeCount, string timeUnit)
        {
            timeCount = timeCount < 1 ? 1 : timeCount;
            ContractRepository contractRepo = new ContractRepository() { DB = DB };
            Contract contract = contractRepo.Get(contractID);

            ContractTimeline lastContractTimeline = contract.ContractTimelines.OrderByDescending(r => r.Order).FirstOrDefault();

            ContractTimeline newContractTimeline = new ContractTimeline();

            if (lastContractTimeline == null)
            {
                newContractTimeline.Order = 1;
                newContractTimeline.FromDate = contract.SignedDate.HasValue ? contract.SignedDate : DateTime.Now.Date;
            }
            else
            {
                newContractTimeline.Order = lastContractTimeline.Order + 1;
                newContractTimeline.FromDate = lastContractTimeline.ToDate.Value.AddDays(1);
            }

            if (timeUnit.ToLower() == "day")
            {
                newContractTimeline.ToDate = newContractTimeline.FromDate.Value.AddDays(timeCount).AddDays(-1);
            }
            else if (timeUnit.ToLower() == "month")
            {
                newContractTimeline.ToDate = newContractTimeline.FromDate.Value.AddMonths(timeCount).AddDays(-1);
            }

            newContractTimeline.ContractID = contractID;

            DB.AddToContractTimelines(newContractTimeline);

            Save();

            return newContractTimeline;
        }

        public ContractTimeline Get(int id)
        {
            return DB.ContractTimelines.SingleOrDefault(r => r.ID == id);
        }

        public ContractTimeline Get(int? contractID, int? order)
        {
            return DB.ContractTimelines.SingleOrDefault(r => r.ContractID == contractID && r.Order == order);
        }

        public Contract DeleteTheLastTimeline(int contractID)
        {
            ContractRepository contractRepo = new ContractRepository() { DB = DB };
            var r = contractRepo.Get(contractID);
            var lastTimeline = r.ContractTimelines.OrderByDescending(r1 => r1.Order).FirstOrDefault();
            if (lastTimeline != null)
            {
                DB.DeleteObject(lastTimeline);
                Save();
            }
            return r;
        }

        public ContractTimeline Update(ContractTimeline e)
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

            DB.ContractTimelines.Attach(e);
            if (ValidConflictTimeline(e))
            {
                DB.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
                Save();
            }

            return e;
        }

        public bool ValidConflictTimeline(ContractTimeline e)
        {
            bool isValid = true;

            ContractTimeline preTimeline = Get(e.ContractID, e.Order - 1);
            ContractTimeline nextTimeline = Get(e.ContractID, e.Order + 1);

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
