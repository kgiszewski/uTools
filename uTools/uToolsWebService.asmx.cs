using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using umbraco.interfaces;
using umbraco.DataLayer;


using umbraco.BusinessLogic;

namespace uTools
{
    /// <summary>
    /// Summary description for CampaignMonitor
    /// </summary>
    [WebService(Namespace = "http://franklinfueling.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    [System.Web.Script.Services.ScriptService]
    public class uToolsWebService : System.Web.Services.WebService
    {

        private JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private Dictionary<string, string> returnValue = new Dictionary<string, string>();
        private Dictionary<string, string> cmRequest = new Dictionary<string, string>();
        private enum status { SUCCESS, ERROR };

        [WebMethod]
        public Dictionary<string, string> GetLogs(string top, string userID, string logTypeID)
        {
            Authorize();

            List<LogItem> dbLogs = new List<LogItem>();
            try
            {
                string sql = "";
                string where = " WHERE 1=1 ";

                string userIDsql = "";
                if (userID != "")
                {
                    userIDsql = " AND userId=" + Convert.ToInt32(userID);
                }

                string logTypeSql = "";
                if (logTypeID != "")
                {
                    string logType = System.Enum.GetName(typeof(LogTypes), Convert.ToInt32(logTypeID));
                    logTypeSql = " AND logHeader='" + logType + "'";
                }

                sql = "SELECT TOP "+Convert.ToInt32(top)+" * FROM umbracoLog "+ where + userIDsql + logTypeSql + " ORDER BY id DESC";
                //umbraco.BusinessLogic.Log.Add(LogTypes.Debug, 0, sql);

                IRecordsReader reader = SqlHelper.ExecuteReader(sql);
                dbLogs = LogItem.ConvertIRecordsReader(reader);

                SqlHelper.Dispose();

            }
            catch (Exception e)
            {
                returnValue.Add("status", jsonSerializer.Serialize(status.ERROR.ToString()));
                returnValue.Add("error", jsonSerializer.Serialize(e.Message));
            }

            List<uToolsLogItem> finalLogs = new List<uToolsLogItem>();
            foreach(LogItem thisItem in dbLogs){
                finalLogs.Add(new uToolsLogItem() { userID = thisItem.UserId, nodeID = thisItem.NodeId, name = new User(thisItem.UserId).Name, logType = thisItem.LogType.ToString(), comment = thisItem.Comment, dateTime = thisItem.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff")});
            }

            returnValue.Add("status", status.SUCCESS.ToString());
            returnValue.Add("logs", jsonSerializer.Serialize(finalLogs));

            return returnValue;
        }

        [WebMethod]
        public Dictionary<string, string> GetRecentDocuments()
        {
            //not yet used anywhere, incomplete
            Authorize();

            string sql = "SELECT TOP 100 * FROM cmsDocument ORDER BY updateDate DESC";

            IRecordsReader reader = SqlHelper.ExecuteReader(sql);

            while(reader.Read())
            {
                HttpContext.Current.Response.Write(reader.Get<int>("nodeId").ToString());
            }

            SqlHelper.Dispose();

            returnValue.Add("status", status.SUCCESS.ToString());
            return returnValue;
        }


        public static ISqlHelper SqlHelper
        {
            get
            {
                return umbraco.BusinessLogic.Application.SqlHelper;
            }
        }

        internal static void Authorize()
        {
            if (!umbraco.BasePages.BasePage.ValidateUserContextID(umbraco.BasePages.BasePage.umbracoUserContextID))
            {
                throw new Exception("Client authorization failed. User is not logged in");
            }

        }
    }

    public class uToolsLogItem
    {
        public string name="";
        public int userID;
        public int nodeID;
        public string dateTime = "";
        public string logType = "";
        public string comment = "";

    }
}
