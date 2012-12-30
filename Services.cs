using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Log.EventLog;

namespace DnnServices
{
    using Models;

    public class Services : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public void Log(AuthorizeAction auth)
        {
            EventLogController eventLog = new EventLogController();

            DotNetNuke.Services.Log.EventLog.LogInfo logInfo = default(DotNetNuke.Services.Log.EventLog.LogInfo);
            logInfo = new LogInfo();
            logInfo.LogUserName = auth.UserName;
            logInfo.LogPortalID = PortalSettings.PortalId;
            logInfo.LogTypeKey = auth.LogTypeKey;
            logInfo.AddProperty("Requested By", auth.AppName);
            //logInfo.AddProperty("PropertyName2", propertyValue2);

            eventLog.AddLog(logInfo);
        }
    }
}
