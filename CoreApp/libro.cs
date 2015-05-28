using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class libro
    {
        public string TraerAliasLibro(int IdUnidad)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_ui_loadportada");
            db.AddInParameter(cmd, "Id", DbType.Int32, IdUnidad);
            DataSet ds = null;
            string val=null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
                val = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "libro.TraerAliasLibro");
            }
            return val;
        }
    }
}
