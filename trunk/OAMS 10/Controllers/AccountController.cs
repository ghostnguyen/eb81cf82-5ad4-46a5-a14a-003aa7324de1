using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OAMS.Models;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using System.Web.Profile;

namespace OAMS.Controllers
{
    [HandleError]
    
    public class AccountController : Controller
    {
        AccountRepository repo = new AccountRepository();

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

       

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }


        [CustomAuthorize]
        public ActionResult Index()
        {
            return View(repo.GetAll());
        }

        public ActionResult Guest()
        {
            return View();
        }

        
        public ActionResult NoRight()
        {
            return View();
        }
        
        [CustomAuthorize]
        public ActionResult Edit(string id)
        {
            return View(new UserModel() { Username = id });
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult Edit(string id, string[] RoleList)
        {
            RoleRepository roleRepo = new RoleRepository();
            roleRepo.SetRoles(id, RoleList);

            return View(new UserModel() { Username = id });
        }

        [CustomAuthorize]
        public ActionResult EditRoleAuthentication(string roleName)
        {
            return View(repo.GetRole(roleName));
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult EditRoleAuthentication(string roleName, int?[] ControllerActionIDList)
        {
            MVCAuthorizationRepository _MVCAuthorizationRepository = new MVCAuthorizationRepository();

            List<int?> list = ControllerActionIDList == null ? new List<int?>() : ControllerActionIDList.ToList();

            _MVCAuthorizationRepository.SetRoleAuthorization(roleName, list);

            return View(repo.GetRole(roleName));
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult OpenIdLogOn(string returnUrl)
        {
            if (OAMSSetting.ByPassLogin && !Request.IsAuthenticated)
            {
                string username = repo.Create_ByPassLogin();
                this.IssueAuthTicket(username, true);

                if (string.IsNullOrEmpty(returnUrl))
                    returnUrl = "~/";

                return Redirect(returnUrl);
            }
            else
            {
                var openid = new OpenIdRelyingParty();
                var response = openid.GetResponse();
                if (response == null)  // Initial operation
                {
                    // Step 1 - Send the request to the OpenId provider server
                    string openid_identifier = "https://www.google.com/accounts/o8/id";
                    //Identifier id;


                    //if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                    //{
                    //    try
                    //    {
                    //        var req = openid.CreateRequest(Request.Form["openid_identifier"]);
                    //        return req.RedirectingResponse.AsActionResult();
                    //    }
                    //    catch (ProtocolException ex)
                    //    {
                    //        // display error by showing original LogOn view
                    //        //this.ErrorDisplay.ShowError("Unable to authenticate: " + ex.Message);
                    //        return View("Logon");
                    //    }
                    //}
                    //else
                    //{
                    //    // display error by showing original LogOn view
                    //    //this.ErrorDisplay.ShowError("Invalid identifier");
                    //    //return View("LogOn", this.ViewModel);
                    //    return View("LogOn");
                    //}


                    try
                    {
                        var req = openid.CreateRequest(openid_identifier);

                        var fetch = new FetchRequest();
                        fetch.Attributes.AddRequired(WellKnownAttributes.Contact.Email);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.First);
                        fetch.Attributes.AddRequired(WellKnownAttributes.Name.Last);

                        req.AddExtension(fetch);


                        return req.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException)
                    {
                        // display error by showing original LogOn view
                        //this.ErrorDisplay.ShowError("Unable to authenticate: " + ex.Message);
                        return View("Logon");
                    }

                }
                else  // OpenId redirection callback
                {
                    // Step 2: OpenID Provider sending assertion response
                    switch (response.Status)
                    {
                        case AuthenticationStatus.Authenticated:
                            string identifier = response.ClaimedIdentifier;

                            var fetch = response.GetExtension<FetchResponse>();
                            string email = string.Empty;
                            string fullname = string.Empty;
                            if (fetch != null)
                            {
                                email = fetch.GetAttributeValue(WellKnownAttributes.Contact.Email);
                                fullname = fetch.GetAttributeValue(WellKnownAttributes.Name.FullName);
                            }

                            if (repo.Exist(email, identifier))
                            {

                            }
                            else
                            {
                                repo.Create(email, identifier);
                            }

                            // OpenId lookup fails - Id doesn't exist for login - login first
                            //if (busUser.ValidateUserOpenIdAndLoad(identifier) == null)
                            //{
                            //    //this.ErrorDisplay.HtmlEncodeMessage = false;
                            //    //this.ErrorDisplay.ShowError(busUser.ErrorMessage +
                            //    //        "Please <a href='" + WebUtils.ResolveUrl("~/Account/Register") +
                            //    //        "'>register</a> to create a new account or <a href='" +
                            //    //        WebUtils.ResolveUrl("~/Account/Register") +
                            //    //        "'>associate</a> an existing account with your OpenId");

                            //    //return View("LogOn", this.ViewModel);
                            //    return View("LogOn");
                            //}

                            // Capture user information for AuthTicket
                            // and issue Forms Auth token
                            //UserState userState = new UserState()
                            //{
                            //    Email = busUser.Entity.Email,
                            //    Name = busUser.Entity.Name,
                            //    UserId = busUser.Entity.Id,
                            //    IsAdmin = busUser.Entity.IsAdmin
                            //};
                            //this.IssueAuthTicket(userState, true);

                            this.IssueAuthTicket(email, true);

                            RoleRepository roleRepo = new RoleRepository();
                            if (roleRepo.GetRolesList(email).Count() == 0)
                            {
                                returnUrl = "~/Account/Guest";
                            }

                            if (string.IsNullOrEmpty(returnUrl))
                                returnUrl = "~/";

                            return Redirect(returnUrl);

                        case AuthenticationStatus.Canceled:
                            //this.ErrorDisplay.ShowMessage("Canceled at provider");
                            //return View("LogOn", this.ViewModel);
                            return View("LogOn");
                        case AuthenticationStatus.Failed:
                            //this.ErrorDisplay.ShowError(response.Exception.Message);
                            //return View("LogOn", this.ViewModel);
                            return View("LogOn");
                    }
                }
            }
            return new EmptyResult();
        }

        //private void IssueAuthTicket(UserState userState, bool rememberMe)
        private void IssueAuthTicket(string userId, bool rememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId,
                                                                 DateTime.Now, DateTime.Now.AddDays(10),
                                                                 rememberMe, userId);

            string ticketString = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            if (rememberMe)
                cookie.Expires = DateTime.Now.AddDays(10);

            HttpContext.Response.Cookies.Add(cookie);
        }

        [CustomAuthorize]
        public ActionResult UpdateControllerAction()
        {
            RoleRepository repo = new RoleRepository();
            repo.InitRole();

            ControllerActionRepository actionAuthorizationRepo = new ControllerActionRepository();
            actionAuthorizationRepo.UpdateActionList();

            return RedirectToAction("Index", "Home");
        }

        [CustomAuthorize]
        public ActionResult GetAllRoles()
        {
            return View(Roles.GetAllRoles().ToList());
        }

        [HttpGet]
        [CustomAuthorize]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult CreateRole(string roleName)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
                return RedirectToAction("GetAllRoles");
            }
            else
                return View();
        }

        [CustomAuthorize]
        public ActionResult DeleteRole(string id)
        {
            if (id == "Admin" || id == "Account")
            { }
            else
            {
                Roles.DeleteRole(id);
            }

            return RedirectToAction("GetAllRoles");
        }

        [CustomAuthorize]
        public ActionResult EditAccountInRole(string rolename)
        {
            object o = rolename;
            return View(o);
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult EditAccountInRole(string rolename, string[] UserList)
        {
            RoleRepository roleRepo = new RoleRepository();
            roleRepo.SetUsersToRole(rolename, UserList);

            return View((object)rolename);
        }
    }
}
