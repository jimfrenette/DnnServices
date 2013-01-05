using System;
using System.Collections.Generic;


namespace DnnServicesObjects
{

    public class ServicesAction
    {
        public string AppName { get; set; }
        public string LogContent { get; set; }
        public string LogServerName { get; set; }
        public string LogTypeKey { get; set; }
        public string Username { get; set; }
    }

    public class ServicesUser
    {
        public int AffiliateID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public bool IsSuperUser { get; set; }
        public string LastIPAddress { get; set; }
        public string LastName { get; set; }
        public int PortalID { get; set; }
        public string[] Roles { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
    }

}
