using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class ejercicio
    {
        public DataSet TraerIdActividad(int IdActividad)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_menu_traerejercicio");
            db.AddInParameter(cmd, "IdActividad", DbType.Int32, IdActividad);
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "ejercicio.TraerIdActividad");
            }
            return ds;
        }
    }
}
