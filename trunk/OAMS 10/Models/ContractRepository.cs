using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class ContractRepository : BaseRepository
    {
        public IQueryable<Contract> GetAll()
        {
            return DB.Contracts;
        }

        public Contract Get(int ID)
        {
            return DB.Contracts.Where(r => r.ID == ID).SingleOrDefault();
        }

        public Contract Add(Contract e)
        {
            DB.Contracts.AddObject(e);

            return e;
        }

        public void Delete(Contract e)
        {
            DB.Contracts.DeleteObject(e);
        }

        public void AddSite(int contractID, int siteDetailID)
        {
            var siteDetailRepo = new SiteDetailRepository() { DB = DB };
            var siteDetail = siteDetailRepo.Get(siteDetailID);

            ContractDetail e = new ContractDetail();
            e.ContractID = contractID;
            e.SiteID = siteDetail.SiteID;

            e.Format = siteDetail.Format;
            e.SiteDetailName = siteDetail.Name;

            DB.ContractDetails.AddObject(e);

            Save();

            ContractDetailRepository contractDetailRepository = new ContractDetailRepository();
            contractDetailRepository.CopyTimeline(e.ID);
        }

        public void OverwriteTimelineForDetail(int ID)
        {
            var r = Get(ID);
            if (r != null
                && r.ContractTimelines.Count > 0)
            {
                foreach (var item in r.ContractDetails)
                {
                    var detailList = item.ContractDetailTimelines.ToList();

                    foreach (var tl in detailList)
                    {
                        DB.DeleteObject(tl);
                    }

                    foreach (var tl in r.ContractTimelines)
                    {
                        ContractDetailTimeline contractDetailTimeline = new ContractDetailTimeline();
                        contractDetailTimeline.FromDate = tl.FromDate;
                        contractDetailTimeline.ToDate = tl.ToDate;
                        contractDetailTimeline.Order = tl.Order;

                        item.ContractDetailTimelines.Add(contractDetailTimeline);
                    }
                }
                DB.SaveChanges();
            }
        }

        public Rpt01_Model_SiteMonitoring Report(int contractID, DateTime? from, DateTime? to)
        {
            SiteMonitoringRepository siteMonitoringRepository = new SiteMonitoringRepository();
            Rpt01_Model_SiteMonitoring rpt = new Rpt01_Model_SiteMonitoring();
            rpt.From = from;
            rpt.To = to;
            rpt.Details = new List<Rpt01_Model_SiteMonitoringDetail>();

            var contract = Get(contractID);
            if (contract != null)
            {
                int index = 1;

                foreach (OAMS.Models.ContractDetail cd in contract.ContractDetails.OrderBy(r => r.Site.Type))
                {
                    Rpt01_Model_SiteMonitoringDetail detail = new Rpt01_Model_SiteMonitoringDetail();

                    DateTime? lastDate = null;

                    SiteMonitoring sm = siteMonitoringRepository.Find(cd, from, to);
                    sm = sm ?? new SiteMonitoring();

                    lastDate = sm.SiteMonitoringPhotoes
                        .OrderByDescending(r => r.TakenDate)
                        .Select(r => r.TakenDate)
                        .FirstOrDefault();

                    detail.ID = index;
                    detail.SiteCode = cd.Site.Code;
                    detail.Supplier = contract.Contractor != null ? contract.ContractorName : cd.Site.ContractorName;
                    detail.Province = cd.Site.Geo1 != null ? cd.Site.Geo1.FullName : "";
                    detail.District = cd.Site.Geo2 != null ? cd.Site.Geo2.Name : "";
                    detail.Street = string.Format("{0} {1} {2}", cd.Site.AddressLine1, cd.Site.AddressLine2, cd.Site.Geo3 == null ? "" : cd.Site.Geo3.Name);
                    detail.Size = string.Format("{0}m x {1}m", cd.Site.Width.ToString(), cd.Site.Height.ToString());
                    detail.Product = sm.Product == null ? "" : sm.Product.Name;
                    detail.Client = sm.Product == null ? "" : sm.Product.NewClientName;
                    detail.SiteType = cd.Site.Type;
                    detail.SiteFormat = cd.Format;
                    detail.LastestPhotoDate = lastDate;
                    detail.BackFront = cd.Site.BacklitFormat != null ? "Backlit" : "Frontlit";
                    detail.Working = sm.Working;
                    detail.NumOfBulbs = sm.NoOfBullbs;
                    detail.BulbsWorking = sm.BullsWorking;
                    detail.Issue = sm.Issues;
                    detail.Clean = sm.Clean;
                    detail.CreativeGoodConditon = sm.CreativeGoodConditon;
                    detail.ExternalInterference = sm.ExternalInterference;
                    detail.Vandalism = sm.Vandalism;
                    detail.Comments = sm.Comments;
                    detail.RequiredFollowUp = sm.RequiredFollowUp;
                    detail.Action = sm.Action;
                    detail.DateOfProblem = sm.DateOfProblem;
                    detail.NowFixed = sm.NowFixed;

                    index++;
                    if (sm.Working.HasValue && sm.Working.Value) rpt.TotalWorking++;
                    if (sm.Issues != null) rpt.TotalIssues++;
                    if (sm.Clean.HasValue && sm.Clean.Value) rpt.TotalClean++;
                    if (sm.CreativeGoodConditon.HasValue && sm.CreativeGoodConditon.Value) rpt.TotalCreativeGoodCondition++;
                    if (sm.ExternalInterference.HasValue && sm.ExternalInterference.Value) rpt.TotalExternnalInterference++;
                    if (sm.Vandalism.HasValue && sm.Vandalism.Value) rpt.TotalVandalism++;
                    if (sm.RequiredFollowUp.HasValue && sm.RequiredFollowUp.Value) rpt.TotalRequiredFollowUp++;
                    if (sm.NowFixed.HasValue && sm.NowFixed.Value) rpt.TotalNowFixed++;

                    rpt.Details.Add(detail);
                }
            }

            return rpt;
        }

        public List<SiteMonitoring> ReportDetails(int contractID, DateTime? from, DateTime? to)
        {
            SiteMonitoringRepository siteMonitoringRepository = new SiteMonitoringRepository();
            List<SiteMonitoring> details = new List<SiteMonitoring>();

            var contract = Get(contractID);
            if (contract != null)
            {
                foreach (OAMS.Models.ContractDetail cd in contract.ContractDetails.OrderBy(r => r.Site.Type))
                {
                    SiteMonitoring sm = siteMonitoringRepository.Find(cd, from, to);

                    if (sm != null)
                        details.Add(sm);
                }
            }

            return details;
        }
    }
}