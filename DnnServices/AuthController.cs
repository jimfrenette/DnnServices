using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Web.Api;

namespace DnnServices
{
    using Models;

    public class AuthController : DnnApiController
    {

        [DnnAuthorize()]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Web API Services (DnnAuthorize) 01.00.00");
        }

        //[RequireHost] //default auth filter - removing this will still require host unless another auth filter is designated here
        [HttpGet]
        public HttpResponseMessage PingHost()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Web API Services (Host) 01.00.00");
        }

        /// <summary>
        /// WebAPI deserializes json post into Model.AuthorizeAction
        /// so long as the json maps to the model 
        /// </summary>
        /// <param name="AuthorizeAction">json object</param>
        /// <returns></returns>
        [DnnAuthorize()]
        [HttpPost]
        public HttpResponseMessage Login(AuthorizeAction auth)
        {
            if (auth.UserName == "host")
            {
                return Host(auth);
            }
            else
            {
                auth.LogTypeKey = "LOGIN_SUCCESS";
                //UserInfo userInfo = UserController.GetUserByName(auth.UserName);
                Services services = new Services();
                services.Log(auth);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        //[RequireHost] //default auth filter - removing this will still require host unless another auth filter is designated here
        [HttpPost]
        public HttpResponseMessage Host(AuthorizeAction auth)
        {
            auth.LogTypeKey = "LOGIN_SUPERUSER";
            //UserInfo userInfo = UserController.GetUserByName(auth.UserName);
            Services services = new Services();
            services.Log(auth);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }

}
