using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace OAMS.Models
{
    public class CodeMasterRepository : BaseRepository
    {
        private OAMSEntities db = new OAMSEntities();

        public IQueryable<CodeMaster> Get(Expression expression)
        {
            string type = PropertyName.GetMemberName(expression);
            return db.CodeMasters.Where(r => r.Type == type).OrderBy(r => r.Order);
        }

        public IQueryable<CodeMaster> Get(string type)
        {
            return db.CodeMasters.Where(r => r.Type == type);
        }

        public string GetNote(string type, string code)
        {
            CodeMaster e = db.CodeMasters.Where(r => r.Type == type && r.Code == code).FirstOrDefault();

            return e == null ? "" : e.Note;
        }

    }

    public class CodeMasterType
    {
        public string Type = "Type";
        public string Material { get; set; }
        public string CBDViewed { get; set; }
        public string Grade { get; set; }
        public string RoadType { get; set; }
        public string TrafficSpeed { get; set; }
        public string Illumination { get; set; }
        public string DistanceFromRoadside { get; set; }
        public string AboveStreet { get; set; }
        public string DurationOfView { get; set; }
        public string AngleOfVision { get; set; }
        public string VisualClutter { get; set; }

    }
}