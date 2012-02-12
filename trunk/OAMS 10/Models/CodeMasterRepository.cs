using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace OAMS.Models
{
    public class CodeMasterRepository : BaseRepository<CodeMasterRepository>
    {
        private OAMSEntities db = new OAMSEntities();

        public IQueryable<CodeMaster> Get(Expression expression)
        {
            string type = PropertyName.GetMemberName(expression);

            return Get(type);
        }

        public IQueryable<CodeMaster> Get(string type)
        {
            var l = db.CodeMasters.Where(r => r.Type == type && r.Realm == AppSetting.Realm);

            if (l == null || l.FirstOrDefault() == null)
            {
                l = db.CodeMasters.Where(r => r.Type == type && r.Realm == null);
            }

            return l.OrderBy(r => r.Order);
        }

        public string GetNote(string type, string code)
        {
            var e = Get(type, code);

            return e == null ? "" : e.Note;
        }

        public CodeMaster Get(string type, string code)
        {
            var l = Get(type);

            CodeMaster e = l.Where(r => r.Code == code).FirstOrDefault();

            return e;
        }

    }

    public class CodeMasterType
    {
        public static string Type = "Type";
        public static string InstallationPosition1 = "InstallationPosition1";

        public string Format { get; set; }
        //public string Material { get; set; }
        //public string CBDViewed { get; set; }
        //public string Grade { get; set; }
        //public string RoadType { get; set; }
        //public string TrafficSpeed { get; set; }
        //public string Illumination { get; set; }
        //public string DistanceFromRoadside { get; set; }
        //public string AboveStreet { get; set; }
        //public string DurationOfView { get; set; }
        //public string AngleOfVision { get; set; }
        //public string VisualClutter { get; set; }

    }
}