using System;
using System.Data;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ca.CoreApp
{
    class diagnos
    {
        public string informe="";
        const string CLRF = "\r\n";
        const string TAB = "\t";
        const string ISOK = "\t [OK]";
        const string ISFAILDED = "\t [FAILED]";

        const string ALIAS_IMAGENES = "ALIASIMAGENES";
        const string ALIAS_DOCUMENTOS = "ALIASDOCUMENTOS";
        const string ALIAS_VIDEO = "ALIASVIDEO";
        const string ALIAS_AUDIO = "ALIASAUDIO";
        private bool IsDisconected=false;
        private bool IsFileNotFounded=false;
        public bool IsFileNotFoundedContinue = false;

        public void StartDiagnostic()
        {
            this.AppendInforme("Started at: " + DateTime.Now);
            this.AppendInforme("------Step 1 to 2------");
            this.AppendInforme("Test connection (data source)");
            this.AppendInforme(this.ComprobarConexion());
            if (IsDisconected == true) { return;}
            this.AppendInforme("------Step 2 to 2------");
            this.AppendInforme("Test IdPerfil (data source)");
            this.AppendInforme(this.ComprobarIdPerfil());
            if (IsDisconected == true) { return; }
        }


        private void AppendInforme(string message)
        {
            informe += message + CLRF;
        }

        private string ComprobarConexion()
        {
            curso c = new curso();
            DataSet ds;
            Database db = DatabaseFactory.CreateDatabase();

            string result = db.ConnectionString;
            string veredit;

            ds = c.TraerTodos();

            if (ds != null)
            {
                IsDisconected = false;
                veredit = ISOK;
            }
            else
            {
                IsDisconected = true;
                veredit = ISFAILDED;
            }
            return result + veredit;
        }
        private string ComprobarIdPerfil()
        {
            config cf = new config();
            Database db = DatabaseFactory.CreateDatabase();

            string tmp="";
            string result="";
            int id;

            id = Convert.ToInt32(ConfigurationSettings.AppSettings["PerfilConfig"].ToString());

            result = "Perfil activo: " + id.ToString();
            //Audio
            tmp = cf.GetSettingValue(id, ALIAS_AUDIO);
            if (tmp != null)
            {
                result += "\r\n" + ALIAS_AUDIO + ": " + tmp + ISOK;
                IsDisconected = false;
            }
            else
            {
                result += "\r\n" + ALIAS_AUDIO + ": <not found>" + ISFAILDED;
                IsDisconected = true;
            }
            //Video
            tmp = cf.GetSettingValue(id, ALIAS_VIDEO);
            if (tmp != null)
            {
                result += "\r\n" + ALIAS_VIDEO + ": " + tmp + ISOK;
                IsDisconected = false;
            }
            else
            {
                result += "\r\n" + ALIAS_VIDEO + ": <not found>" + ISFAILDED;
                IsDisconected = true;
            }
            //Imgs
            tmp = cf.GetSettingValue(id, ALIAS_IMAGENES);
            if (tmp != null)
            {
                result += "\r\n" + ALIAS_IMAGENES + ": " + tmp + ISOK;
                IsDisconected = false;
            }
            else
            {
                result += "\r\n" + ALIAS_IMAGENES + ": <not found>" + ISFAILDED;
                IsDisconected = true;
            }
            //Docs
            tmp = cf.GetSettingValue(id,ALIAS_DOCUMENTOS);
            if (tmp != null)
            {
                result += "\r\n" + ALIAS_DOCUMENTOS + ": " + tmp + ISOK;
                IsDisconected = false;
            }
            else
            {
                result += "\r\n" + ALIAS_DOCUMENTOS + ": <not found>" + ISFAILDED;
                IsDisconected = true;
            }
          
            return result;
        }
        
        public void ProbarArchivosUnidad(int IdCurso, config cfg)
        {
            DataSet ds, dsm;
            unidad u = new unidad();
            string tmpText;
            string tmpPath;
            string tmpVeredic;
            string tipomedia="0";
            mediacontent m = new mediacontent();
            int id;

            this.AppendInforme("* Test Course->Units \r\n");

            ds = u.TraerIdCurso(IdCurso);
            if (ds != null)
            {
                //Por cada unidad
                this.AppendInforme("------START------");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.AppendInforme( TAB + dr[0].ToString() + ">" + dr[1].ToString() );

                    id = Convert.ToInt32(dr[0].ToString());

                    //Por cada Playlist de esta unidad
                    dsm = m.GetPListUnidad(id);
                    foreach (DataRow drp in dsm.Tables[0].Rows)
                    {
                        tipomedia = drp[1].ToString();
                        cfg.AliasLibro = cfg.GetAliasLibro(Convert.ToInt32(dr[0].ToString()));

                        switch(tipomedia)
                        {
                            case "1":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_AUDIO);
                                break;
                            case "2":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_VIDEO);
                                break;
                            case "3":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_IMAGENES);
                                break;
                            case "4":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_DOCUMENTOS);
                                break;
                        }
                        tmpText = drp[0].ToString() + ">" + tipomedia;

                        tmpPath = cfg.BuildURL(cfg.UrlBase, cfg.AliasLibro, tipomedia, drp[2].ToString());

                        if (this.CheckRuta(tmpPath) == true)
                        {
                            tmpVeredic = ISOK;
                            IsFileNotFounded = false;
                        }
                        else
                        {
                            tmpVeredic = ISFAILDED;
                            IsFileNotFounded = true;
                        }
                        this.AppendInforme(TAB + TAB + tmpText + ">>" + tmpPath + TAB + tmpVeredic);
                        if (IsFileNotFoundedContinue == false && IsFileNotFounded == true)
                        {
                            return;//Fin, no continuar hasta encontrar
                        }
                    }
                }
                this.AppendInforme("------END------");

            }
            else
            {
                this.AppendInforme("<Cannot access to Data Source>" + ISFAILDED);
            }
        }
        private bool CheckRuta(string url)
        {
            bool IsExist;
            try
            {
                IsExist = System.IO.File.Exists(url);
                return IsExist;
            }
            catch
            {
                return false;
            }
            
        }

        public void ProbarArchivosEjercicio(int IdActividad, config cfg)
        {
            DataSet ds, dsm;
            ejercicio e = new ejercicio();
            string tmpText;
            string tmpPath;
            string tmpVeredic;
            string tipomedia = "0";
            mediacontent m = new mediacontent();
            int id;

            this.AppendInforme("* Test Activity->Execise \r\n");

            ds = e.TraerIdActividad(IdActividad);
            if (ds != null)
            {
                //Por cada unidad
                this.AppendInforme("------START------");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    this.AppendInforme(TAB + dr[0].ToString() + ">" + dr[1].ToString());

                    id = Convert.ToInt32(dr[0].ToString());

                    //Por cada Playlist de esta unidad
                    dsm = m.GetPListEjercicio(id);
                    foreach (DataRow drp in dsm.Tables[0].Rows)
                    {
                        tipomedia = drp[1].ToString();
                        cfg.AliasLibro = cfg.GetAliasLibroxEjercicio(id);

                        switch (tipomedia)
                        {
                            case "1":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_AUDIO);
                                break;
                            case "2":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_VIDEO);
                                break;
                            case "3":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_IMAGENES);
                                break;
                            case "4":
                                tipomedia = cfg.GetSettingValue(cfg.PerfilConfig, ALIAS_DOCUMENTOS);
                                break;
                        }
                        tmpText = drp[0].ToString() + ">" + tipomedia;

                        tmpPath = cfg.BuildURL(cfg.UrlBase, cfg.AliasLibro, tipomedia, drp[2].ToString());

                        if (this.CheckRuta(tmpPath) == true)
                        {
                            tmpVeredic = ISOK;
                            IsFileNotFounded = false;
                        }
                        else
                        {
                            tmpVeredic = ISFAILDED;
                            IsFileNotFounded = true;
                        }
                        this.AppendInforme(TAB + TAB + tmpText + ">>" + tmpPath + TAB + tmpVeredic);
                        if (IsFileNotFoundedContinue == false && IsFileNotFounded == true)
                        {
                            return;//Fin, no continuar hasta encontrar
                        }
                    }
                }
                this.AppendInforme("------END------");

            }
            else
            {
                this.AppendInforme("<Cannot access to Data Source>" + ISFAILDED);
            }
        }
    }
}
