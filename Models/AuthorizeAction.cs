using System;

namespace DnnServices.Models
{
    public class AuthorizeAction
    {
        public string AppName { get; set; }
        public string LogContent { get; set; }
        public string LogServerName { get; set; }
        public string LogTypeKey { get; set; }
        public string UserName { get; set; }
    }
}
