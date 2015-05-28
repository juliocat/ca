using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections;

namespace ca.CoreApp
{
    class actividad
    {
        public DataSet TraerIdUnidad(int IdUnidad)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_menu_traeractividad");
            db.AddInParameter(cmd, "IdUnidad", DbType.Int32, IdUnidad);
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "actividad.TraerIdUnidad");
            }
            return ds;
        }

        public void EnviarAdmin(ArrayList nt)
        {
            foreach(string tempo in nt)
            {
                Logger.Write(tempo, "NotificarErrorAdministrador");
            }
        }
    }
}
