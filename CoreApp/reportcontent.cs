using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class reportcontent
    {
        public bool SendReport(string loc, string ip, string razon)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("atec_Reportar");
            db.AddInParameter(cmd, "Localizacion", DbType.String, loc);
            db.AddInParameter(cmd, "ipequipo", DbType.String, ip);
            db.AddInParameter(cmd, "razon", DbType.String, razon);

            try
            {
                db.ExecuteScalar(cmd);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "atec_Reportar");
                return false;
            }
        }
    }
}
