using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Services.Log.EventLog;
using DotNetNuke.Web.Api;
using DnnServicesObjects;

namespace DnnServices
{
    public class LogController : DnnApiController
    {
        /// <summary>
        /// WebAPI deserializes json post into DnnServicesObjects.ServicesAction
        /// </summary>
        /// <param name="ServicesAction">json object</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage LogAnonymous(ServicesAction action)
        {
            Services services = new Services();
            services.Log(action);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
