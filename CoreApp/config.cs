using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;

namespace ca.CoreApp
{
    class config
    {
        public string UrlBase;
        public int PerfilConfig;
        public string AliasLibro;
        public int TiempoParaTema;
        public int idTema;

        public config()
        {
            this.PerfilConfig = Convert.ToInt32(ConfigurationSettings.AppSettings["PerfilConfig"].ToString());
            this.UrlBase = this.GetSettingValue(this.PerfilConfig, "URLBASE");
            this.TiempoParaTema = System.Convert.ToInt32((this.GetSettingValue(this.PerfilConfig, "TIEMPOTEMA")));
            this.idTema = System.Convert.ToInt32(this.GetSettingValue(this.PerfilConfig, "TEMA"));

            //Validacion
            if(this.TiempoParaTema<1) this.TiempoParaTema = 1;
        }

        public string GetSettingValue(int idPerfil, string Clave)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DbCommand cmd = db.GetStoredProcCommand("app_Config_TraerValorConfig");
            db.AddInParameter(cmd, "IdPerfil", DbType.Int32, idPerfil);
            db.AddInParameter(cmd, "Clave", DbType.String, Clave);
            string val = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
                val = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "GetSettingValue.Configuracion");
            }
            return val;
        }

        public string GetAliasLibro(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DbCommand cmd = db.GetStoredProcCommand("app_Config_TraerAliasLibro");
            db.AddInParameter(cmd, "IdUnidad", DbType.Int32, id);
            string val = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
                val = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "Configuracion.GetAliasLibro");
            }
            return val;
        }

        public string GetAliasLibroxActividad(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DbCommand cmd = db.GetStoredProcCommand("app_Config_TraerAliasLibroxAct");
            db.AddInParameter(cmd, "IdActividad", DbType.Int32, id);
            string val = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
                val = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "Configuracion.GetAliasLibroxActividad");
            }
            return val;
        }

        public string GetAliasLibroxEjercicio(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DataSet ds;
            DbCommand cmd = db.GetStoredProcCommand("app_Config_TraerAliasxEjer");
            db.AddInParameter(cmd, "IdEjercicio", DbType.Int32, id);
            string val = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
                val = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "Configuracion.GetAliasLibroxEjercicio");
            }
            return val;
        }

        public string BuildURL(string urlBase, string AliasLibro, string MediaType, string NombreArchivo)
        {
            string path = null;

            try
            {
                path = System.IO.Path.Combine(urlBase, AliasLibro);
                path = System.IO.Path.Combine(path, MediaType);
                path = System.IO.Path.Combine(path, NombreArchivo);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "config.BuildPath");
            }

            return path;
        }
    }
}
