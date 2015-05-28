using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

using System.Drawing;
using System.IO;

namespace ca.CoreApp
{
    class tema
    {
        public Image TraerFooter(int Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("tem_Tema_TraerFooter");
            db.AddInParameter(cmd, "IdTema", DbType.Int32, Id);
            byte[] rec;
            Image img=null;

            try
            {
                rec = (byte[])db.ExecuteScalar(cmd);
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                /*FileStream fs = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                fs.Write(rec, 0, rec.Length);
                fs.Flush();
                fs.Close();*/
                MemoryStream ms = new MemoryStream(rec, 0, rec.Length);
                ms.Write(rec, 0, rec.Length);
                img = Image.FromStream(ms, true);

                //img = Image.FromFile(strfn);                
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "tema.TraerFooter");
            }
            return img;
        }
        public Image TraerHeader(int Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("tem_Tema_TraerHeader");
            db.AddInParameter(cmd, "IdTema", DbType.Int32, Id);
            byte[] rec;
            Image img = null;


            try
            {
                rec = (byte[])db.ExecuteScalar(cmd);
                string strfn = Convert.ToString(DateTime.Now.ToFileTime());
                /*FileStream fs = new FileStream(strfn, FileMode.CreateNew, FileAccess.Write);
                fs.Write(rec, 0, rec.Length);
                fs.Flush();
                fs.Close();*/
                MemoryStream ms = new MemoryStream(rec, 0, rec.Length);
                ms.Write(rec, 0, rec.Length);
                img = Image.FromStream(ms, true);

                //img = Image.FromFile(strfn);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "tema.TraerHeader");
            }
            return img;
        }
        public string TraerColor(int Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("tem_Tema_TraerColor");
            db.AddInParameter(cmd, "IdTema", DbType.Int32, Id);
            string rec=null;

            try
            {
                rec = (string)db.ExecuteScalar(cmd);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "tema.TraerHeader");
            }
            return rec;
        }

        
        public DataSet TraerLista()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("tem_Tema_TraerLista");
            DataSet ds = null;

            try
            {
                ds = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message, "tema.TraerLista");
            }
            return ds;
        }
    }
}
