using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class asistencia
    {
        public DataSet TraerDescripProblemas()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("atec_Problema_TraerTodo");
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "asistencia.TraerDescripProblemas");
            }
            return ds;
        }
    }
}
