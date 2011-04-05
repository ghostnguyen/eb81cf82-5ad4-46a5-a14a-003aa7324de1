using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public class SiteRepository : BaseRepository
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

        public void Update(int ID, Action<Site> updateMethod, IEnumerable<HttpPostedFileBase> files, List<int> DeletePhotoList, string[] noteList, List<SDP> siteDetailFiles, List<int> DeleteSiteDetailPhotoList)
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
    }
}