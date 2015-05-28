using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace ca.CoreApp
{
    class cron
    {
        public DataSet TraerCalendar(int IdSemana)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_cron_TraerCalendar");
            db.AddInParameter(cmd, "diasemana", DbType.Int32, IdSemana);
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);

            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "app_cron_TraerCalendar");
            }
            return ds;
        }
        public DateTime TraerHoraServidor()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("app_cron_TraerHora");
            DateTime tiempoServidor = DateTime.Now;

            try
            {
                tiempoServidor = (DateTime) db.ExecuteScalar(cmd);
                return tiempoServidor;
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "app_cron_TraerHora");
            }
            return tiempoServidor;
        }
    }
}
