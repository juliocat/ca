using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class ban
    {
        public string TraerPermisos(string ip)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_ban_TraerPermisos");
            db.AddInParameter(cmd, "ipMch", DbType.String , ip);
            string r = "";

            try
            {
                r = (string) db.ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "app_ban_TraerPermisos");
            }
            return r;
        }
    }
}
