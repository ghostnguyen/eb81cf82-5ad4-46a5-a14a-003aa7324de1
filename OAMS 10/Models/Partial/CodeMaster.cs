using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public partial class CodeMaster
    {
        static public string GetIconUrl(string code)
        {
            string url = VirtualPathUtility.ToAbsolute("~/Content/Image/");

            switch (code)
            {
                case "WMB":
                    url += "wallmountedbannee.png";
                    break;
                case "BRL":
                    url += "britelite.png";
                    break;
                case "BSH":
                    url += "busshelter.png";
                    break;
                case "CMR":
                    url += "covermarket.png";
                    break;
                case "ELV":
                    url += "elevator.png";
                    break;
                case "ITK":
                    url += "itkiosk.png";
                    break;
                case "Billboard":
                    url += "billboard.png";
                    break;
                case "BillboardPole":
                    url += "billboardpole.png";
                    break;
                case "Other":
                    url += "other.png";
                    break;
            }

            return url;
        }
    }
}