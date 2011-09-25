using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class SiteRepository : BaseRepository<SiteRepository>
    {
        public IQueryable<Site> GetAll()
        {
            return DB.Sites;
        }

        public Site Get(int id)
        {
            return DB.Sites.Where(r => r.ID == id).SingleOrDefault();

        }

        public Site Add(Action<Site> updateMethod, IEnumerable<HttpPostedFileBase> files, string[] noteList)
        {
            Site e = new Site();
            updateMethod(e);

            UpdateFrontBackLit(e);
            UpdateGeo(e);
            //UpdateCategory(e);

            DB.Sites.AddObject(e);

            Save();

            PicasaRepository picasaRepository = new PicasaRepository();
            picasaRepository.DB = DB;

            picasaRepository.UploadPhoto(e, files, noteList);

            Save();

            return e;
        }

        public void Update(int ID, Action<Site> updateMethod, IEnumerable<HttpPostedFileBase> files, List<int> DeletePhotoList, string[] noteList, List<SDP> siteDetailFiles, List<int> DeleteSiteDetailPhotoList, List<MoveSP> moveL)
        {
            Site e = Get(ID);

            updateMethod(e);

            UpdateGeo(e);

            UpdateFrontBackLit(e);

            Save();

            PicasaRepository picasaRepository = new PicasaRepository();
            picasaRepository.DB = DB;

            picasaRepository.UploadPhoto(e, files, noteList);
            Save();

            DeletePhoto(DeletePhotoList);

            picasaRepository.UploadPhoto(siteDetailFiles);
            Save();

            DeleteSiteDetailPhoto(DeleteSiteDetailPhotoList);
            Save();

            MovePhoto(moveL);
            Save();
        }

        public void UpdateContractor(Site e)
        {

        }

        public void UpdateGeo(Site e)
        {
            GeoRepository geoRepository = new GeoRepository();
            geoRepository.Set3LevelByFullname(e.NewGeoFullName, e.UpdateGeo);
        }



        public void UpdateFrontBackLit(Site e)
        {
            if (!e.FrontlitNumerOfLamps.HasValue
                           || e.FrontlitNumerOfLamps <= 0)
            {
                e.FontLightArmsStraight = null;
                e.FontlitArmsPlacement = null;
                e.FontlitIlluminationDistribution = null;
                e.FrontlitSideLighting = null;
                e.FrontlitTopBottom = null;
            }
            else
            {
                e.BacklitFormat = null;
                e.BacklitIlluninationSpread = null;
                e.BacklitLightBoxLeakage = null;
                e.BacklitLightingBlocks = null;
                e.BacklitVisualLegibility = null;
            }
        }

        public Site InitWithDefaultValue()
        {
            Site e = new Site();
            e.CloseToAirport = false;
            e.CloseToFactory = false;
            e.CloseToGasStation = false;
            e.CloseToHopistal = false;
            e.CloseToMarket = false;
            e.CloseToOffice = false;
            e.CloseToParking = false; ;
            e.CloseToResident = false;
            e.CloseToSchool = false;
            e.CloseToShopping = false;
            e.CloseToStadium = false;
            e.CloseToStation = false;
            e.CloseToTownCenter = false;
            e.CloseToUniversity = false;

            e.VisibilityBridgeWalkway = 5;
            e.VisibilityBuilding = 5;
            e.VisibilityHight = 5;
            e.VisibilityElectricityPolesOther = 5;
            e.VisibilityTrees = 5;

            e.DirectionalTrafficPublicTransport = 5;
            e.ShopSignsBillboards = 5;
            e.FlagsTemporaryBannersPromotionalItems = 5;
            e.CompetitiveProductSigns = 5;

            Site lastSite = DB.Sites.OrderByDescending(r => r.ID).FirstOrDefault();
            if (lastSite != null)
            {
                e.Lat = lastSite.Lat.TrimDouble();
                e.Lng = lastSite.Lng.TrimDouble();
            }

            return e;
        }



        public void Delete(int ID)
        {
            Site s = Get(ID);

            List<SitePhoto> l = s.SitePhotoes.ToList();

            foreach (var item in l)
            {
                DB.SitePhotoes.DeleteObject(item);
            }

            DB.Sites.DeleteObject(s);
            Save();
        }

        public void DeletePhoto(List<int> IDList)
        {
            if (IDList != null)
            {
                List<SitePhoto> l = DB.SitePhotoes.Where(r => IDList.Contains(r.ID)).ToList();
                PicasaRepository picasaRepository = new PicasaRepository();
                foreach (var item in l)
                {
                    picasaRepository.DeletePhoto(item);
                    DB.DeleteObject(item);
                }

                Save();
            }
        }

        public void MovePhoto(List<MoveSP> moveL)
        {
            if (moveL != null)
            {
                foreach (var item in moveL)
                {
                    var photo = DB.SitePhotoes.FirstOrDefault(r => r.ID == item.SitePhotoID);
                    var sd = DB.SiteDetails.FirstOrDefault(r => r.ID == item.SiteDetailID);

                    if (photo != null && sd != null && photo.SiteID == sd.SiteID)
                    {
                        MovePhoto(photo, sd);
                    }
                }
            }
        }

        public void MovePhoto(SitePhoto photo, SiteDetail sd)
        {
            PicasaRepository picasaRepository = new PicasaRepository();

            if (string.IsNullOrEmpty(sd.AlbumUrl))
            {
                sd.AlbumUrl = picasaRepository.CreateAlbum("SD_" + sd.ID.ToString());
            }

            var createdEntry = picasaRepository.MovingPhoto(photo.Url, photo.AtomUrl, sd.AlbumUrl, photo.Note);

            if (createdEntry != null)
            {
                var p = new SiteDetailPhoto();

                p.Url = createdEntry.Media.Content.Url;
                p.AtomUrl = createdEntry.EditUri.Content;
                p.TakenDate = photo.TakenDate;
                p.Lng = photo.Lng;
                p.Lat = photo.Lat;
                p.Note = photo.Note;
                sd.SiteDetailPhotoes.Add(p);

                DB.DeleteObject(photo);
            }

            Save();
        }

        public void DeleteSiteDetailPhoto(List<int> IDList)
        {
            if (IDList != null)
            {
                var l = DB.SiteDetailPhotoes.Where(r => IDList.Contains(r.ID)).ToList();
                PicasaRepository picasaRepository = new PicasaRepository();
                foreach (var item in l)
                {
                    picasaRepository.DeletePhoto(item);
                    DB.DeleteObject(item);
                }

                Save();
            }
        }

        public string getAlbumID(string atom)
        {
            return atom.Split('/')[9].Split('?')[0];
        }
        public void MovePhoto(int from,int to)
        {
            //var s = Get(id).SitePhotoes;

            var s = DB.Sites.Where(r => r.ID >= from && r.ID < to).SelectMany(r => r.SitePhotoes).ToList();
            //
            string genAlbum = getAlbumID(AppSetting.AlbumAtomUrl);
            int count = s.Where(r => getAlbumID(r.AtomUrl) == genAlbum).Count();
            
            var s2 = s.Where(r => getAlbumID(r.AtomUrl) != genAlbum).OrderBy(r => r.SiteID).ThenBy(r => r.ID);
            
            int count2 = s2.Count();

            //string albumid = AppSetting.AlbumAtomUrl.Split('/')[9].Split('?')[0];

            foreach (var item in s2)
            {
                try
                {
                    PicasaRepository.I.MovePhoto2GenericAlbum(item);
                    Save();
                }
                catch (Exception)
                {

                }
            }

            Save();
        }
    }
}