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

            ServicesAction auth = new ServicesAction();
            auth.AppName = "DnnMvcMobile";
            auth.LogServerName = System.Environment.MachineName;
            auth.Username = credentials.Username;
            string body = JsonConvert.SerializeObject(auth);

            string response = DnnServices.PostRequest(url, credentials.Username, credentials.Password, body, out statusCode, out errorMsg, ref cookies);
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    ViewBag.Message = errorMsg;
                    break;
                default:
                    //something else entirely!
                    ViewBag.Message = "<li>Please contact the system administrator for http://" + credentials.DnnHttpAlias + "</li><li>Http Status: " + statusCode.ToString() + "</li><li>Error Message: " + errorMsg + "</li>";
                    break;
            }

            if (statusCode == HttpStatusCode.OK)
            {
                FormsAuthentication.SetAuthCookie(credentials.Username, false);
                
                //deserialize response
                ServicesUser servicesUser = new ServicesUser();
                servicesUser = JsonConvert.DeserializeObject<ServicesUser>(response);
                //TODO pass servicesUser as a model to Account View
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }


}