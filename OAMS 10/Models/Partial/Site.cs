using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace OAMS.Models
{

    public partial class Site
    {
        public string GeoFullName
        {
            get
            {
                GeoRepository geoRepository = new GeoRepository();
                return geoRepository.GetFullname(Geo1, Geo2, Geo3);
            }
        }

        public string NewGeoFullName
        {
            get;
            set;
        }



        public string NewCategoryFullName
        {
            get;
            set;
        }

        public string ContractorName
        {
            get
            {
                return Contractor != null ? Contractor.Name : "";
            }
        }



        public int UpdateGeo(Guid? geoID1, Guid? geoID2, Guid? geoID3)
        {
            Geo1ID = geoID1;
            Geo2ID = geoID2;
            Geo3ID = geoID3;

            return 0;
        }

        public int GetScore(Expression<Func<Site, object>> expression)
        {
            var propertyName = PropertyName.For(expression);

            var value = this.GetType().GetProperty(propertyName).GetValue(this, null).ToString();

            return (int)CodeMasterRepository.I.Get(propertyName, value).Score.ToInt();            
        }

        public void UpdateScore()
        {
            int? score = 0;

            if (AppSetting.IsPOSTAR)
            {
                score = 100                    
                    - GetScore(r => r.Deflection)
                    - GetScore(r => r.ViewingAngle)
                    - GetScore(r => r.ViewingDistance)
                    - GetScore(r => r.Obstruction)
                    - GetScore(r => r.CompetitiveProductSigns)
                    ;
            }
            else
            {
                //((((((((((((((4)*isnull([InstallationPosition2],(0))+(4)*isnull([RoadType2],(0)))+isnull([ViewingDistance],(0)))+isnull([ViewingSpeed],(0)))+isnull([High],(0)))+isnull([VisibilityBuilding],(0)))+isnull([VisibilityHight],(0)))+isnull([VisibilityTrees],(0)))+isnull([VisibilityBridgeWalkway],(0)))+isnull([VisibilityElectricityPolesOther],(0)))+isnull([DirectionalTrafficPublicTransport],(0)))+isnull([ShopSignsBillboards],(0)))+isnull([FlagsTemporaryBannersPromotionalItems],(0)))+isnull([CompetitiveProductSigns],(0)))
                score = 0
                    + 4 * InstallationPosition2
                    + 4 * RoadType2
                    + int.Parse(ViewingDistance)
                    + ViewingSpeed
                    + High
                    + VisibilityBuilding
                    + VisibilityHight
                    + VisibilityTrees
                    + VisibilityBridgeWalkway
                    + VisibilityElectricityPolesOther
                    + DirectionalTrafficPublicTransport
                    + ShopSignsBillboards
                    + FlagsTemporaryBannersPromotionalItems
                    + int.Parse(CompetitiveProductSigns);
            }

            Score = score;
        }
    }
}