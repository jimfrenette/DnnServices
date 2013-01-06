using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Web.Api;
using DnnServicesObjects;


namespace DnnServices
{

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
        /// WebAPI deserializes json post into DnnServicesObjects.ServicesAction
        /// </summary>
        /// <param name="ServicesAction">json object</param>
        /// <returns></returns>
        [DnnAuthorize()]
        [HttpPost]
        public HttpResponseMessage Login(ServicesAction action)
        {
            Services services = new Services();
            ServicesUser servicesUser = services.GetUserByName(action.Username);
            if (servicesUser.IsSuperUser)
            {
                return Host(action);
            }
            else
            {
                action.LogTypeKey = "LOGIN_SUCCESS";
                services.Log(action);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        //[RequireHost] //default auth filter - removing this will still require host unless another auth filter is designated here
        [HttpPost]
        public HttpResponseMessage Host(ServicesAction action)
        {
            action.LogTypeKey = "LOGIN_SUPERUSER";
            Services services = new Services();
            services.Log(action);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }

}
