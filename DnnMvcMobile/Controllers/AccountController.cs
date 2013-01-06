using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using DnnServicesObjects;

namespace DnnMvcMobile.Controllers
{
    using Data;
    using Models;

    [Authorize]
    public class AccountController : Controller
    {
 
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Authentication credentials)
        {
            //login
            credentials.Cookies = new CookieContainer();
            string url = DnnServices.GetUrl(credentials.DnnHttpAlias, "Services", "Auth", "Login", false);

            string errorMsg = null;
            HttpStatusCode statusCode;
            CookieContainer cookies = credentials.Cookies;
            ServicesAction action = new ServicesAction();
            action.AppName = "DnnMvcMobile";
            action.LogServerName = System.Environment.MachineName;
            action.LogTypeKey = "LOGIN_FAILURE"; //default for this action
            action.Username = credentials.Username;
            string body = JsonConvert.SerializeObject(action);

            string response = DnnServices.PostRequest(url, credentials.Username, credentials.Password, body, out statusCode, out errorMsg, ref cookies);
            if (statusCode == HttpStatusCode.OK)
            {
                FormsAuthentication.SetAuthCookie(credentials.Username, false);
                //deserialize response
                ServicesUser servicesUser = new ServicesUser();
                servicesUser = JsonConvert.DeserializeObject<ServicesUser>(response);
                //TODO servicesUser data handling, at least UserID
                return RedirectToAction("Index", "Account");
            }
            else
            {
                switch (statusCode)
                {
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        ViewBag.Message = errorMsg;
                        //post LOGIN_FAILURE to event log
                        url = DnnServices.GetUrl(credentials.DnnHttpAlias, "Services", "Log", "LogAnonymous", false);
                        DnnServices.PostRequest(url, credentials.Username, credentials.Password, body, out statusCode, out errorMsg, ref cookies);
                        ViewBag.Message += " " + errorMsg;
                        break;
                    default:
                        //something else entirely!
                        ViewBag.Message = "<li>Please contact the system administrator for http://" + credentials.DnnHttpAlias + "</li><li>Http Status: " + statusCode.ToString() + "</li><li>Error Message: " + errorMsg + "</li>";
                        break;
                }
                return View();
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }


}