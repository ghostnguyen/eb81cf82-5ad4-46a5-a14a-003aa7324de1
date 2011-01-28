using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;

namespace OAMS.Models
{
    public class AccountProfile : ProfileBase
    {
        static public AccountProfile GetByUsername(string username)
        {
            return (AccountProfile)
                   (ProfileBase.Create(username));
        }

        static public AccountProfile CurrentUser
        {
            get
            {
                return (AccountProfile)
                       (ProfileBase.Create(Membership.GetUser().UserName));
            }
        }

        public string OpenId
        {
            get { return ((string)(base["OpenId"])); }
            set { base["OpenId"] = value; Save(); }
        }

        // add additional properties here
    }
}