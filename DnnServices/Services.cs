﻿using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Log.EventLog;
using DnnServicesObjects;

namespace DnnServices
{

    public class Services : DotNetNuke.Entities.Modules.PortalModuleBase
    {

        public void Log(ServicesAction action)
        {
            EventLogController eventLog = new EventLogController();

            DotNetNuke.Services.Log.EventLog.LogInfo logInfo = default(DotNetNuke.Services.Log.EventLog.LogInfo);
            logInfo = new LogInfo();
            logInfo.LogUserName = action.Username;
            logInfo.LogPortalID = PortalId;
            logInfo.LogTypeKey = action.LogTypeKey;
            logInfo.LogServerName = action.LogServerName;
            logInfo.AddProperty("Requested By", action.AppName);
            //logInfo.AddProperty("PropertyName2", propertyValue2);

            eventLog.AddLog(logInfo);
        }

        public ServicesUser GetUserByName(string username)
        {
            ServicesUser user = new ServicesUser();
            UserInfo userInfo = UserController.GetUserByName(username);
            user.AffiliateID = userInfo.AffiliateID;
            user.DisplayName = userInfo.DisplayName;
            user.Email = userInfo.Email;
            user.FirstName = userInfo.FirstName;
            user.IsSuperUser = userInfo.IsSuperUser;
            user.LastIPAddress = userInfo.LastIPAddress;
            user.LastName = userInfo.LastName;
            user.PortalID = userInfo.PortalID;
            user.Roles = userInfo.Roles;
            user.UserID = userInfo.UserID;
            user.Username = userInfo.Username;
            return user;
        }

        public int GetUserID(string username)
        {
            int userID = 0;
            UserInfo userInfo = UserController.GetUserByName(username);
            userID = userInfo.UserID;
            return userID;
        }



    }

}
