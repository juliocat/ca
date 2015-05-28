using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class unidad
    {
        public DataSet TraerIdCurso(int IdCurso)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_menu_traerunidad");
            db.AddInParameter(cmd, "IdCurso", DbType.Int32, IdCurso);
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "unidad.TraerIdCurso");
            }
            return ds;
        }
    }
}
