using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAMS.Models
{
    public partial class ControllerAction
    {
        public string Desc
        {
            get { return string.Format("{0}.{1}.[{2}].[{3}]", Controller, Action, Parameters, HttpVerb); }
        }
    }
}