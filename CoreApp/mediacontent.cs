using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class mediacontent
    {
        public DataSet GetPListEjercicio(int Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_pl_ejercicio");
            db.AddInParameter(cmd, "IdEjercicio", DbType.Int32, Id);
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "mediacontent.GetPListEjercicio");
            }
            return ds;
        }

        public DataSet GetPListUnidad(int Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_pl_unidad");
            db.AddInParameter(cmd, "IdUnidad", DbType.Int32, Id);
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "mediacontent.GetPListUnidad");
            }
            return ds;
        }
    }
}
