using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.GData.Photos;
using Google.GData.Client;

namespace OAMS.Models
{
    public class SiteDetailRepository : BaseRepository<SiteDetailRepository>
    {
        public IQueryable<SiteDetail> GetAll()
        {
            return DB.SiteDetails;
        }

        //public void UpdateCategory(SiteDetail e)
        //{
        //    CategoryRepository catRepository = new CategoryRepository();
        //    catRepository.Set3LevelByFullname(e.NewCategoryFullName, e.UpdateCategory);
        //}



        public SiteDetail Add(int siteID)
        {
            var siteRepo = new SiteRepository() { DB = DB };
            var site = siteRepo.Get(siteID);

            var siteDetail = new SiteDetail();
            site.SiteDetails.Add(siteDetail);

            Save();

            return siteDetail;
        }

        public SiteDetail Get(int id)
        {
            var v = DB.SiteDetails.SingleOrDefault(r => r.ID == id);
            return v;
        }

        public SiteDetail Update(SiteDetail e)
        {
            DB.SiteDetails.Attach(e);
            //UpdateCategory(e);
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

        public void UploadPhoto(List<SDP> siteDetailFiles)
        {
            if (siteDetailFiles == null)
            {
                return;
            }

            var v = siteDetailFiles.Where(r => r.File != null).ToList().GroupBy(r => r.SiteDetailID);

            if (v.Count() == 0)
            {
                return;
            }

            foreach (var item in v)
            {
                if (item.Count() == 0) continue;

                var siteDetail = Get(item.Key);
                var files = item.Select(r => r.File);
                var notes = item.Select(r => r.Note).ToArray();
                var l = PicasaRepository.I.UploadPhoto2(files, notes);
                if (l != null)
                {
                    for (int i = 0; i < l.Count(); i++)
                    {
                        var entry = l[i];
                        if (entry != null)
                        {
                            SiteDetailPhoto photo = new SiteDetailPhoto();

                            Helper.UpdateIPhoto(files.ElementAt(i), notes[i], entry, photo);

                            siteDetail.SiteDetailPhotoes.Add(photo);

                            Save();

                            string note = string.Format("SDP_{0}_SD_{1}_S_{2}", photo.ID.ToString(), siteDetail.ID.ToString(), siteDetail.SiteID.ToString());
                            entry.UpdateSummary(note);
                        }
                    }
                }
            }
        }



        public void DeleteSiteDetailPhoto(List<int> IDList)
        {
            if (IDList != null)
            {
                var l = DB.SiteDetailPhotoes.Where(r => IDList.Contains(r.ID)).ToList();
                //PicasaRepository picasaRepository = new PicasaRepository();
                foreach (var item in l)
                {
                    //picasaRepository.DeletePhoto(item);
                    DB.DeleteObject(item);
                }

                Save();
            }
        }
    }
}