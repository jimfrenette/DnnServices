using System;
using System.IO;
using System.Net;
using System.Text;

namespace DnnMvcMobile.Data
{
    internal class DnnServices
    {
        internal static string GetRequest(string url, string username, string password, out HttpStatusCode statusCode, out string errorMsg)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Credentials = new NetworkCredential(username, password);
            CookieContainer cookies = null;
            string response = DoRequest(webRequest, out statusCode, out errorMsg, ref cookies);
            return response;
        }
        internal static string GetRequest(string url, out HttpStatusCode statusCode, out string errorMsg, ref CookieContainer cookies)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cookies != null)
                webRequest.CookieContainer = cookies;
            string response = DoRequest(webRequest, out statusCode, out errorMsg, ref cookies);
            return response;
        }
        internal static string GetRequest(string url, string username, string password, out HttpStatusCode statusCode, out string errorMsg, ref CookieContainer cookies)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            if (cookies != null)
                webRequest.CookieContainer = cookies;
            webRequest.Credentials = new NetworkCredential(username, password);
            webRequest.Accept = "application/text";
            string response = DoRequest(webRequest, out statusCode, out errorMsg, ref cookies);
            return response;
        }

        private static string DoRequest(HttpWebRequest webRequest, out HttpStatusCode statusCode, out string errorMsg, ref CookieContainer cookies)
        {
            string response = String.Empty;
            errorMsg = null;
            statusCode = HttpStatusCode.NoContent;
            try
            {
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(webResponse.GetResponseStream()))
                    {
                        response = sr.ReadToEnd();
                    }
                    statusCode = ((HttpWebResponse)webResponse).StatusCode;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse errResponse = (HttpWebResponse)ex.Response;
                errorMsg = ex.Message;
                statusCode = errResponse.StatusCode;
                //read the response stream for the error message
                using (System.IO.StreamReader sr = new System.IO.StreamReader(errResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }
            }
            return response;
        }

        internal static string GetUrl(string portalAlias, string module, string controller, string action, bool secure)
        {
            string scheme = Uri.UriSchemeHttp + Uri.SchemeDelimiter;
            if (secure) scheme = Uri.UriSchemeHttps + Uri.SchemeDelimiter;

            string url = string.Format("{0}{1}/DesktopModules/{2}/API/{3}/{4}", scheme, portalAlias, module, controller, action);
            return url;
        }

        internal static string PostRequest(string url, string username, string password, string body, out HttpStatusCode statusCode, out string errorMsg, ref CookieContainer cookies)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Credentials = new NetworkCredential(username, password);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(body);
            webRequest.ContentLength = byteArray.Length;
            using (System.IO.Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            string response = DoRequest(webRequest, out statusCode, out errorMsg, ref cookies);
            return response;
        }


    }
}