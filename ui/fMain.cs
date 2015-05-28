using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using ca.CoreApp;
using WMPLib;
using System.Runtime.InteropServices;
using System.Diagnostics;
using ca.ui;
using System.IO;

namespace ca.ui
{
    public partial class fMain : Form
    {
        //Tipo de media
        const int TYPE_AUDIO = 1;
        const int TYPE_VIDEO = 2;
        const int TYPE_IMGS = 3;
        const int TYPE_DOCS = 4;
        
        //Claves de Configuración
        const string ALIAS_IMAGENES = "ALIASIMAGENES";
        const string ALIAS_DOCUMENTOS = "ALIASDOCUMENTOS";
        const string ALIAS_VIDEO = "ALIASVIDEO";
        const string ALIAS_AUDIO = "ALIASAUDIO";

        //Mensajes de barra de estado y IconNotify
        const string STS_BUSY = "Obteniendo datos, espere...";
        const string STS_READY = "Listo!";
        const string ERROR_DATASOURCE = "Base de Datos no disponible. Código de servicio DS001";

        //Flags
        private bool IsAutoPlayAudio = false; //Solo para audio
        private bool IsDisconnect = false; //Flag Desconectado (Manejo de errores)
        private int TiempoTrans = 0; //Contador de tiempo para instalar tema

        //Obj configuración (Interface)
        private config confg;
        private bool isAudioActive = true;

        //Extensión playlist
        private ArrayList PlayListVideos = new ArrayList();

        //Localizacion recurso
        private string[] locRecurso = new string[3];

        bool isOpenNextVideo = false;

        //ban
        string banPermisos = "";
        string ipPc="";

        //API
        
        //Cron
        DateTime ServerTime;
        string CalendarF = "";
        bool isnotify = false;
        fNotify fNotif;

        public fMain()
        {
            InitializeComponent();
            
        }

#region EscribirListas

        private void WritelstCourse()
        {
            curso c = new curso();
            DataSet ds = c.TraerTodos();
            if (ds != null)
            {
                this.WriteList(this.lsvCourse, ds, 3);
                this.IsDisconnect = false;
            }
            else
            {
                this.MostrarMensajeError(ERROR_DATASOURCE);
                this.IsDisconnect = true;
                this.MostrarMensajeErrorDesconex();
            }
        }

        private void WritelstUnit(int IdCourse)
        {
            unidad u = new unidad();
            DataSet ds = u.TraerIdCurso(IdCourse);
            if (ds != null)
            {
                //ban policy
                bool bp = this.SimboloBan(1);
                if (bp == true)
                {
                    this.EstadoIsBan();
                    ds = this.BanPolicy(ds);
                }

                //Write!
                this.WriteList(this.lsvUnit, ds, 2);
                this.IsDisconnect = false;
            }
            else
            {
                this.MostrarMensajeError(ERROR_DATASOURCE);
                this.IsDisconnect = true;
                this.MostrarMensajeErrorDesconex();
            }
        }

        private DataSet BanPolicy(DataSet dsd)
        {
            DataTable dt;
            try
            {
                dt = dsd.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[1].ToString().Contains("* Exam") == true)
                    {
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
            }
            catch
            {

            }
            return dsd;
        }

        private void WritelstActivity(int IdUnidad)
        {
            actividad a = new actividad();
            DataSet ds = a.TraerIdUnidad(IdUnidad);
            if (ds != null)
            {
                this.WriteList(this.lsvActivity, ds, 1);
                this.IsDisconnect = false;
            }
            else
            {
                this.MostrarMensajeError(ERROR_DATASOURCE);
                this.IsDisconnect = true;
                this.MostrarMensajeErrorDesconex();
            }
        }

        private void WritelstExercise(int IdActivity)
        {
            ejercicio e = new ejercicio();
            DataSet ds = e.TraerIdActividad(IdActivity);
            if (ds != null)
            {
                this.WriteList(this.lsvExercise, ds, 0);
                this.IsDisconnect = false;
            }
            else
            {
                this.MostrarMensajeError(ERROR_DATASOURCE);
                this.IsDisconnect = true;
                this.MostrarMensajeErrorDesconex();
            }
        }

        private void WriteList(ListView lv, DataSet ds, int IdImg)
        {
            DataTable dt = ds.Tables[0];
            ListViewItem lvi;
            int i=0;

            lv.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                lvi = lv.Items.Add(dr[0].ToString(), IdImg);
                for (i = 1; i < dt.Columns.Count; i++)
                {
                    lvi.SubItems.Add(dr[i].ToString());
                }
            }
        }

#endregion

#region CtrlSeleccionListas

        private void lsvCourse_DoubleClick(object sender, EventArgs e)
        {
            this.SetEstado(STS_BUSY, true);
            this.WaitAnimeOn();

            this.Refresh();
            
            ListViewItem oList;
            int id;
            oList = this.lsvCourse.SelectedItems[0];

            id = Convert.ToInt32(oList.Text);

            this.WritelstUnit(id);

            //Si ocurre un error de conexion, detener el curso.
            if (this.IsDisconnect == true)
            {
                this.SetEstado(STS_READY);
                this.WaitAnimeOff();
                return;
            }

            //Ensamblar la ruta de exploracion en 0
            locRecurso[0] = oList.SubItems[1].Text;
            WriteLocLabel(1);
            //Etiqueta
            this.lblTitle.Text = locRecurso[0];
            //

            this.discoverCtrl.SelectedPane = this.dis1;
            this.dis1.Enabled = true;
            this.dis2.Enabled = false;
            this.dis3.Enabled = false;

            this.WaitAnimeOff();
            //Segun ID 37, comentar sonar:
            //EventoSonar(2);
            this.SetEstado(STS_READY);
        }

        private void lsvUnit_DoubleClick(object sender, EventArgs e)
        {
            this.SetEstado(STS_BUSY, true);
            this.WaitAnimeOn(); //restaurar
            this.Refresh();

            ListViewItem oList;
            int id;
            oList = this.lsvUnit.SelectedItems[0];

            id = Convert.ToInt32(oList.Text);

            this.WritelstActivity(id);

            //Si ocurre un error de conexion, detener el curso.
            if (this.IsDisconnect == true)
            {
                this.SetEstado(STS_READY);
                this.WaitAnimeOff();
                return;
            }

            //añadido, seleccionar portada en UI + CargarAlias
            this.LoadAliasLibro(id);
            this.LoadPortada(id);
            //fin

            //Ensamblar la ruta de exploracion en 1
            locRecurso[1] = oList.SubItems[1].Text;
            WriteLocLabel(2);
            //Etiqueta
            this.lblTitle.Text = locRecurso[1];
            //

            ////Cargar listas!

            mediacontent m = new mediacontent();
            DataSet ds;
            ds = m.GetPListUnidad(id);
            this.LoadPlayList(ds);
            
            //--Cargar listas
            
            
            this.discoverCtrl.SelectedPane = this.dis2;
            this.dis2.Enabled = true;
            this.dis3.Enabled = false;

            this.WaitAnimeOff();
            //Segun ID 37, comentar sonar:
            //EventoSonar(2);
            this.SetEstado(STS_READY);
        }

        private void lsvActivity_DoubleClick(object sender, EventArgs e)
        {
            this.SetEstado(STS_BUSY, true);
            this.WaitAnimeOn();

            ListViewItem oList;
            int id;
            oList = this.lsvActivity.SelectedItems[0];

            id = Convert.ToInt32(oList.Text);

            this.WritelstExercise(id);

            //Si ocurre un error de conexion, detener el curso.
            if (this.IsDisconnect == true)
            {
                this.SetEstado(STS_READY);
                this.WaitAnimeOff();
                return;
            }

            //Ensamblar la ruta de exploracion en 1
            locRecurso[2] = oList.SubItems[1].Text;
            WriteLocLabel(3);
            //Etiqueta
            this.lblTitle.Text = locRecurso[2];
            //

            this.discoverCtrl.SelectedPane = this.dis3;
            this.dis3.Enabled = true;

            this.WaitAnimeOff();
            //Segun ID 37, comentar sonar:
            //EventoSonar(2);
            this.SetEstado(STS_READY);
        }

        private void lsvExercise_DoubleClick(object sender, EventArgs e)
        {
            this.SetEstado(STS_BUSY, true);
            this.WaitAnimeOn();

            this.Refresh();

            ListViewItem oList;
            int id;
            oList = this.lsvExercise.SelectedItems[0];

            id = Convert.ToInt32(oList.Text);

            DataSet ds;
            ds = this.CargarListaAudio(id);

            //Si ocurre un error de conexion, detener el curso.
            if (this.IsDisconnect == true)
            {
                this.MostrarMensajeError(ERROR_DATASOURCE);
                this.MostrarMensajeErrorDesconex();

                this.SetEstado(STS_READY);
                this.WaitAnimeOff();
                return;
            }

            this.LoadPlayList(ds);
            //--Cargar listas

            this.WaitAnimeOff();
            //Segun ID 37, comentar sonar:
            //EventoSonar(2);
            this.SetEstado(STS_READY);
        }

#endregion

#region Playlist

        private DataSet CargarListaAudio(int id)
        {
            ////Cargar listas!
            mediacontent m = new mediacontent();
            DataSet ds;
            ds = m.GetPListEjercicio(id);
            if (ds != null)
            {
                this.IsDisconnect = false;
            }
            else
            {
                this.IsDisconnect = true;
            }
            return ds;
        }

        private void LoadPlayList(DataSet ds)
        {
            this.WaitAnimeOn();
            
            //Reiniciar todos los visores
            this.ResetMediaVw();

            //Cargar segun cada tipo
            this.DistribuirMedia(ds);

            //Check Autoplay
            this.CheckedAutoplay();

            this.WaitAnimeOff();
        }

        private void CheckedAutoplay()
        {
            if (this.IsAutoPlayAudio == true)
            {
                this.wmpAudio.Ctlcontrols.play();
            }
        }

        private void DistribuirMedia(DataSet ds)
        {
            if (ds != null)
            {
                DataTable dt;

                try
                {
                    dt = ds.Tables[0];
                }
                catch
                {
                    Logger.Write("Falta contenido", "Error");
                    return;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    //Estructura:
                    //[0] = IdMedia
                    //[1] = MediaType {1=audio;2=video;3=imagen;4=sonido}
                    //[2] = Resource (Nombre archivo)

                    switch (dr[1].ToString())
                    {
                        case "1":
                            this.LoadPlayListAudio(dr);
                            break;
                        case "2":
                            this.LoadPlayListVideo(dr);
                            break;
                        case "3":
                            this.LoadPlayListImgs(dr);
                            break;
                        case "4":
                            this.LoadPlayListDocs(dr);
                            break;
                    }
                }
            }
            else
            {
                this.SetEstado("Folder is Empty");
            }
        }

        private void ResetMediaVw()
        {
            this.wmpAudio.currentPlaylist.clear();
            this.wmpVideo.currentPlaylist.clear();
            this.picImgs.Image = this.picImgs.InitialImage;
            this.IsAutoPlayAudio = false;
            this.lstDocs.Items.Clear();
            this.PlayListVideos.Clear();
            this.cmbVideoList.Enabled = false;
            this.cmbVideoList.Items.Clear();
            this.cmbVideoList.Text = "<Empty list>";
        }

        private void LoadPlayListAudio(DataRow dr)
        {
            IWMPMedia oMedia;
            string tipomedia, archivo, ruta;

            tipomedia = this.confg.GetSettingValue(this.confg.PerfilConfig,ALIAS_AUDIO);
            archivo = dr[2].ToString();

            ruta = this.confg.BuildURL(this.confg.UrlBase, this.confg.AliasLibro, tipomedia, archivo);

            oMedia = this.wmpAudio.newMedia(ruta);

            this.wmpAudio.currentPlaylist.appendItem(oMedia);

            //Comprobar autoplay, si al menos solo un registro cumple, entonces autoplay
            if (dr[3].ToString() == "True")
            {
                this.IsAutoPlayAudio = true;
            }
        }

        private void LoadPlayListVideo(DataRow dr)
        {
            IWMPMedia oMedia;
            string tipomedia, archivo, ruta;

            tipomedia = this.confg.GetSettingValue(this.confg.PerfilConfig, ALIAS_VIDEO);
            archivo = dr[2].ToString();

            ruta = this.confg.BuildURL(this.confg.UrlBase, this.confg.AliasLibro, tipomedia, archivo);

            oMedia = this.wmpVideo.newMedia(ruta);

            this.wmpVideo.currentPlaylist.appendItem(oMedia);

            //cargar el combo
            //this.cmbVideoList.Items.Add
            ListViewItem lv;

            lv = new ListViewItem(dr[0].ToString());
            lv.SubItems.Add(ruta);
            lv.SubItems.Add(dr[4].ToString());

            this.PlayListVideos.Add(lv);

            this.cmbVideoList.Enabled = true; //Habilitar lista video
            this.cmbVideoList.Text = "<Choose video>";
            this.cmbVideoList.Items.Add(dr[0].ToString() + " | " + dr[4].ToString());
        }

        private void PlayVideo(string Id)
        {
            ListViewItem lv;
            for (int i = 0; i < this.PlayListVideos.Count; i++)
            {
                lv = (ListViewItem)this.PlayListVideos[i];
                //Comparar por ruta
                if (Id.Trim() == lv.Text)
                { 
                    IWMPMedia oMedia;
                    oMedia = this.wmpVideo.currentPlaylist.get_Item(i);
					this.wmpVideo.Ctlcontrols.playItem(oMedia);                    

                    //Issue #16
                    //if (this.wmpVideo.playState == WMPPlayState.wmppsPlaying)                     
                    //{
                    //    this.wmpVideo.fullScreen = true;
                    //}
                }
            }
        }

        private void LoadPlayListDocs(DataRow dr)
        {
            ListViewItem oList;
            int id = this.ReconocerExtension(dr[2].ToString());

            //obtener ruta
            string tipomedia, archivo, ruta;

            tipomedia = this.confg.GetSettingValue(this.confg.PerfilConfig,ALIAS_DOCUMENTOS);
            archivo = dr[2].ToString();

            ruta = this.confg.BuildURL(this.confg.UrlBase, this.confg.AliasLibro, tipomedia, archivo);
            //obtener ruta

            oList = this.lstDocs.Items.Add(dr[0].ToString(),id);
            oList.SubItems.Add(dr[4].ToString());
            oList.SubItems.Add(ruta);
            //oList.ToolTipText = ruta;
        }

        private int ReconocerExtension(string filename)
        {
            int id;
            if (filename.Contains(".doc") == true)
            {
                id = 0;
            }
            else if (filename.Contains(".jpg") == true)
            {
                id = 1;
            }
            else if (filename.Contains(".htm") == true)
            {
                id = 2;
            }
            else if (filename.Contains(".pdf") == true)
            {
                id = 3;
            }
            else if (filename.Contains(".ppt") == true)
            {
                id = 4;
            }
            else //extension desconocida
            {
                id = 5;
            }
            return id;
        }

        private void LoadPlayListImgs(DataRow dr)
        {
            Image oImage;
            string tipomedia, archivo, ruta;

            tipomedia = this.confg.GetSettingValue(this.confg.PerfilConfig, ALIAS_IMAGENES);
            archivo = dr[2].ToString();

            ruta = this.confg.BuildURL(this.confg.UrlBase, this.confg.AliasLibro, tipomedia, archivo);

            try
            {
                oImage = System.Drawing.Bitmap.FromFile(ruta);
                this.picImgs.Image = oImage;
            }
            catch
            {
                this.picImgs.Image = this.picImgs.ErrorImage;
                //TODO: escribir en log
            }
        }

#endregion

        private void GetIpPC()
        {
            //Obtener ip (inicio)
            try
            {
                //Prevenir error de socket u otro de "host"
                this.ipPc = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
            }
            catch
            {
                this.ipPc = "eth0=null";
            }
            //Una manera extraña de comprobar un 2do NIC
            if (this.ipPc == "eth0=null")
            {
                try
                {
                    this.ipPc = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[1].ToString();
                }
                catch
                {
                    this.ipPc = "eth0=null";
                }
            }
        }

        private void fMain_Load(object sender, EventArgs e)
        {

            this.GetIpPC();

            //ban b = new ban();
            //this.banPermisos = b.TraerPermisos(this.ipPc);
            //Comprobar ban
            //bool bp = this.SimboloBan(0);
            //if (bp == true)
            //{
            //    MessageBox.Show("Your IP is banned. (call 'Stop App')", "Problem");
            //    Application.Exit();
            //}

            //Iniciar app
            this.SetEstado(STS_BUSY, true);

            //fAbout f = new fAbout();
            //f.Show();
            fIntro f = new fIntro();
            f.Show();

            this.EventoSonar(1);

            Logger.Write("App Iniciada","General");

            this.wmpAudio.settings.volume = 100;
            this.wmpVideo.settings.volume = 100;

            confg = new config();//iniciar var configuracion
            this.WritelstCourse();

            //Corrige el comportamiento extraño de discoverCtrl
            this.discoverCtrl.SelectedPane = this.dis3;
            this.discoverCtrl.SelectedPane = this.dis2;
            this.discoverCtrl.SelectedPane = this.dis1;
            this.discoverCtrl.SelectedPane = this.dis0;
            //fin correccion

            this.picPortada.Image = this.picPortada.InitialImage;
            this.picImgs.Image = this.picImgs.InitialImage;

            f.Dispose();

            this.SetEstado(STS_READY);
            //iniciar maximizado
            //f.WindowState = FormWindowState.Maximized;
            this.WindowState = FormWindowState.Maximized;
            //this.Visible = true;
			//Despertar Cron
            this.WakeUpCron();

            //Tema
            this.timTheme.Enabled = true;
		}

        private void WakeUpCron()
        {
            //Si variables de cron OK
            cron cnr = new cron();
            try
            {
                //Hora servidor:
                this.ServerTime = cnr.TraerHoraServidor();

                //Calcular dia semana
                DayOfWeek d = this.ServerTime.DayOfWeek;
                int di=0;

                switch (d)
                { 
                    case DayOfWeek.Monday:
                        di = 1;
                        break;
                    case DayOfWeek.Tuesday:
                        di = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        di = 3;
                        break;
                    case DayOfWeek.Thursday:
                        di = 4;
                        break;
                    case DayOfWeek.Friday:
                        di = 5;
                        break;
                    case DayOfWeek.Saturday:
                        di = 6;
                        break;
                    case DayOfWeek.Sunday:
                        di = 7;
                        break;
                }

                //Traer calendario:
                DataSet ds;
                DataTable dt;
                ds = cnr.TraerCalendar(di);
                dt = ds.Tables[0];
                DateTime dp;

                //Guardar cada registro cadena

                foreach(DataRow dr in dt.Rows)
                {
                    dp = Convert.ToDateTime(dr[0].ToString());
                    this.CalendarF += "|" + dp.ToShortTimeString();
                }


                //Despertar a CRON
                this.timCron.Enabled = true;
            }
            catch
            {
                //No se pudo configurar a CRON
                this.timCron.Enabled = false;
            }
        }

        private void CronActivityAlarm()
        { 
            //Verificar ocurrencia de alarma
            string t;
            int r;
            
            t = this.ServerTime.ToShortTimeString();
            r = this.CalendarF.IndexOf(t);
            if (r > -1) //Si encuentra
            {
                if (this.isnotify == false)
                {
                    this.isnotify = true;
                    this.fNotif = new fNotify();
                    this.fNotif.IncluirMensaje(this.ServerTime.ToShortTimeString());
                    this.fNotif.Show(this);
                    //Mostrar tablero de anuncio
                    this.ShowAnnouncement();
                }
            }
            else
            {
                this.isnotify = false;
            }
        }

#region ManejoErrores

        private void MostrarMensajeError(string msg)
        {
            //EventoSonar(4);
            MessageBox.Show(msg + ". Error DS006.", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        private void MostrarMensajeErrorDesconex()
        {
            this.notTaskBar.Visible=true;
            this.notTaskBar.ShowBalloonTip(30, "e-Teaching - Network problem", "Cannot retrieve data source. Contact with Administrator. (Service Code DS001)", ToolTipIcon.Error);
        }
#endregion

#region Utilidades

        private void WriteLocLabel(int max)
        {
            string rutaloc = "/";
            for (int i = 0; i < max; i++)
            {
                rutaloc += this.locRecurso[i] + "/";
            }
            //Etiqueta UI
            this.lblLoc.Text = rutaloc;
        }

        private void LoadPortada(int IdUnidad)
        {
            string ruta, archivo, tipomedia;

            libro lbr = new libro();
            archivo = lbr.TraerAliasLibro(IdUnidad);
            tipomedia = this.confg.GetSettingValue(this.confg.PerfilConfig, ALIAS_IMAGENES);

            ruta = this.confg.BuildURL(this.confg.UrlBase, this.confg.AliasLibro, tipomedia, archivo);

            try
            {
                this.picPortada.Image = System.Drawing.Bitmap.FromFile(ruta);
            }
            catch (Exception ex)
            {
                this.picPortada.Image = this.picPortada.ErrorImage;
                Logger.Write(ex.Message, "Main.LoadPortada");
            }
        }

        private void LoadAliasLibro(int id)
        {
             this.confg.AliasLibro = this.confg.GetAliasLibro(id);
        }

        private void SetEstado(string texto, bool Sts)
        {
            this.stsEstado.Text = texto;
            this.barCarga.Visible = Sts;
        }

        private void SetEstado(string texto)
        {
            this.stsEstado.Text = texto;
            this.barCarga.Visible = false;
        }

#endregion

#region EnEspera
        private void WaitAnimeOn()
        {
            //this.timBusy.Enabled = true;
        }

        private void WaitAnimeOff()
        {
            //this.timBusy.Enabled = false;
        }
#endregion

#region Eventos

        private void wmpVideo_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                if (this.wmpAudio.playState == WMPPlayState.wmppsPlaying)
                {
                    this.wmpAudio.Ctlcontrols.stop();
                }
            }
            catch
            {

            }

            if (this.wmpVideo.playState == WMPPlayState.wmppsStopped || this.wmpAudio.playState == WMPPlayState.wmppsMediaEnded)
            {
                //Stop static
                this.picWait.Visible = false;                
            }
            else
            {
                //Animacion
                this.picWait.Visible = true;
            }
        }

        private void wmpAudio_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                if (this.wmpVideo.playState == WMPPlayState.wmppsPlaying)
                {
                    this.wmpVideo.Ctlcontrols.stop();
                }
            }
            catch
            {

            }

            if (this.wmpAudio.playState == WMPPlayState.wmppsStopped || this.wmpAudio.playState == WMPPlayState.wmppsMediaEnded)
            {
                //Stop static
                this.picWait.Visible = false;
            }
            else
            {
                //Download
                this.picWait.Visible = true;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notTaskBar_DoubleClick(object sender, EventArgs e)
        {
            this.notTaskBar.Visible = false;
        }

        private void notTaskBar_BalloonTipClosed(object sender, EventArgs e)
        {
            this.notTaskBar.Visible = false;
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            fAbout f = new fAbout();
            f.Show(this);
        }

        private void lstDocs_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvi;
            string ruta;

            lvi = this.lstDocs.SelectedItems[0];

            ruta = lvi.SubItems[2].Text;

            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.FileName = ruta;
                myProcess.Start();
            }
            catch (Win32Exception ex)
            {
               this.MostrarMensajeError(ex.Message);
            }

            //System.Diagnostics.Process.Start(ruta);
        }

        private void mnuCheck_Click(object sender, EventArgs e)
        {
            fDiagnostics dg = new fDiagnostics();
            dg.ShowDialog();
            dg.Dispose();
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.IsDisconnect == false)
            {
                Logger.Write("App Es Cerrado", "General");
            }
        }
#endregion

        private void cmbVideoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            char[] separator = { '|' };
            string[] tmp = this.cmbVideoList.Text.Split(separator);
            this.isOpenNextVideo = true;//Flag
            this.PlayVideo(tmp[0]);
            this.cmbVideoList.Text = "<Choose again>";
        }

        private void picImgs_DoubleClick(object sender, EventArgs e)
        {
            fImgViewr im = new fImgViewr();
            im.SetImagen(this.picImgs.Image);
            im.Show();
        }

        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void GetTemas()
        {
            tema t = new tema();
            DataSet ds;
            ds = t.TraerLista();
            if(ds != null)
            {
                this.WriteMenuTemas(ds);
            }
            else
            {
                //TODO: Error tema
            }
        }

        private void WriteMenuTemas(DataSet ds)
        {
            this.mnuLoading.Visible = false;
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                ToolStripItem tsi = this.tstTemas.DropDownItems.Add(dr[1].ToString(), null, new System.EventHandler(this.mnuTemaMenu));
                tsi.Image = this.mnuLoading.Image;
                tsi.Tag = dr[0];
            }
        }

        private void mnuTema_Click(object sender, EventArgs e)
        {
            //Un tema vacio
            this.InstalarTema(null, null, null);
            this.BackgroundImage = null;
        }

        private void mnuTemaMenu(object sender, EventArgs e)
        {
            EventoSonar(5);
            //this.tstTemas.DropDownItems
            ToolStripItem tsi = (ToolStripItem)sender;
            int id;
            id = Convert.ToInt32(tsi.Tag);
            this.GetTema(id);
            this.toolTip1.SetToolTip(this.panHeader, "Theme: " + tsi.Text);
            
        }

        private void GetTema(int id)
        {
            tema t = new tema();
            Image Header;
            Image Foorter;
            string colorfuente;

            Header = t.TraerHeader(id);
            Foorter = t.TraerFooter(id);
            colorfuente = t.TraerColor(id);

            if (Header != null || Foorter != null)
            {
                this.InstalarTema(Header, Foorter, colorfuente);
            }
            else
            { 
                //TODO: No se encontro tema
            }

        }

        private void InstalarTema(Image imgHeader, Image imgFooter, string ElColor)
        {
            this.panHeader.BackgroundImage = imgHeader;
            this.stsFooter.BackgroundImage = imgFooter;
            //this

            Color c;
            c = SystemColors.ControlText;

            if(ElColor == "blanco")
            {
                c = Color.White;
            }
            else if (ElColor == "amarillo")
            {
                c = Color.Yellow ;
            }
            else if (ElColor == "rojo")
            {
                c = Color.Red;
            }
            else if (ElColor == "azul")
            {
                c = Color.Blue;
            }
            else if (ElColor == "verde")
            {
                c = Color.GreenYellow;
            }
            else if (ElColor == "rojizo")
            {
                c = Color.RosyBrown;
            }

            this.lblTitle.ForeColor = c;
            this.lblLoc.ForeColor = c;
            this.stsEstado.ForeColor = c;
            this.lblServerTime.ForeColor = c;
        }

        private void EventoSonar(int id)
        {
            if (this.isAudioActive == true)
            {
                string fname;
                Random r = new Random();
                int x;
                //random desde 1 hasta <n

                switch (id)
                { 
                    case 1: //Inicio
                        x = r.Next(1, 8);
                        fname = "start" + x.ToString();
                        break;
                    case 2: //clic
                        x = r.Next(1, 4);
                        fname = "mnu" + x.ToString();
                        break;
                    case 3: //Apagado
                        fname = "shut1";
                        break;
                    case 4: //Desconectado
                        //x = r.Next(1, 2);
                        fname = "discon1";
                        break;
                    case 5: //Tema
                        x = r.Next(1, 3);
                        fname = "theme" + x.ToString();
                        break;
                    default:
                        fname = "";
                        break;
                }

                this.Sonar(fname);
            }
        }

        public void Sonar(string fname)
        {
            if (fname.Length != 0)
            {
                fname = Application.StartupPath + "\\sound\\" + fname;

                WINMM m = new WINMM();
                m.Play(fname);
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            //Utiles.ShutDown();
            this.Close();
        }

        private void mnuUISound_Click(object sender, EventArgs e)
        {
            if (this.mnuUISound.Checked == true)
            {
                this.mnuUISound.Checked = false;
                this.isAudioActive = false;
            }
            else
            {
                this.isAudioActive = true;
                EventoSonar(2);
                this.mnuUISound.Checked = true;
            }
        }

        private void timTheme_Tick(object sender, EventArgs e)
        {
            this.TiempoTrans++;
            if (TiempoTrans > this.confg.TiempoParaTema)
            {
                this.GetTemas();
                if (this.confg.idTema == 0)
                {
                    //Un tema vacio
                    this.InstalarTema(null, null, null);
                    this.BackgroundImage = null;
                }
                else
                {
                    this.GetTema(this.confg.idTema);
                    this.timTheme.Enabled = false;
                }
            }
        }

        private void wmpVideo_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            if (this.wmpVideo.playState == WMPPlayState.wmppsPaused)
            {
                this.wmpVideo.Ctlcontrols.play();
            }
            else if(this.wmpVideo.playState == WMPPlayState.wmppsPlaying)
            {
                this.wmpVideo.Ctlcontrols.pause();
            }
        }

        private void wmpAudio_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            if (this.wmpAudio.playState == WMPPlayState.wmppsPaused)
            {
                this.wmpAudio.Ctlcontrols.play();
            }
            else if (this.wmpAudio.playState == WMPPlayState.wmppsPlaying)
            {
                this.wmpAudio.Ctlcontrols.pause();
            }
        }

        private void mnu1x_Click(object sender, EventArgs e)
        {
            this.TamanioFuente(0);
        }
        //Tamanio fuente
        private void TamanioFuente(int factor)
        {
            switch (factor)
            { 
                case 0:
                    this.lblLoc.Font = new Font(this.lblLoc.Font, FontStyle.Regular);
                    this.lsvExercise.Font = new Font(this.lsvExercise.Font, FontStyle.Regular);
                    this.lsvActivity.Font = new Font(this.lsvActivity.Font, FontStyle.Regular);
                    this.lsvUnit.Font = new Font(this.lsvUnit.Font, FontStyle.Regular);
                    this.lsvCourse.Font = new Font(this.lsvCourse.Font, FontStyle.Regular);
                    this.lstDocs.Font = new Font(this.lstDocs.Font, FontStyle.Regular);
                    this.stsEstado.Font = new Font(this.stsEstado.Font, FontStyle.Regular);
                    this.lblServerTime.Font = new Font(this.stsEstado.Font, FontStyle.Regular);
                    break;
                case 1:
                    this.lblLoc.Font = new Font(this.lblLoc.Font, FontStyle.Bold);
                    this.lsvExercise.Font = new Font(this.lsvExercise.Font, FontStyle.Bold);
                    this.lsvActivity.Font = new Font(this.lsvActivity.Font, FontStyle.Bold);
                    this.lsvUnit.Font = new Font(this.lsvUnit.Font, FontStyle.Bold);
                    this.lsvCourse.Font = new Font(this.lsvCourse.Font, FontStyle.Bold);
                    this.lstDocs.Font = new Font(this.lstDocs.Font, FontStyle.Bold);
                    this.stsEstado.Font = new Font(this.stsEstado.Font, FontStyle.Bold);
                    this.lblServerTime.Font = new Font(this.stsEstado.Font, FontStyle.Bold);
                    break;
            }
        }

        private void mnu2x_Click(object sender, EventArgs e)
        {
            this.TamanioFuente(1);
        }

        private void wmpVideo_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (this.isOpenNextVideo == false)
            {
                this.wmpVideo.Ctlcontrols.stop();
            }
            else
            {
                this.wmpVideo.fullScreen = true;
                this.isOpenNextVideo = false;
            }
        }

		private void timCron_Tick(object sender, EventArgs e)
        {
            this.CronActivityAlarm();
            this.ServerTime = this.ServerTime.AddMilliseconds(5000);
            this.lblServerTime.Text = this.ServerTime.ToString();
            this.CronActivityAlarm();
        }
        private bool SimboloBan(int id)
        {
            //0: abrir
            //1: ver examen
            string v;
            bool r;
            try
            {
                v = this.banPermisos.Substring(id, 1);
            }
            catch
            {
                v = "0";
            }

            r = v == "1" ? true : false;
            return r;
        }

        private void EstadoIsBan()
        { 
            picIsBanned.Visible = true;
            picIsBanned.Text = "Restricted content " + this.ipPc + " = {" + this.banPermisos + "}";
        }

        private void btnReportExercise_Click(object sender, EventArgs e)
        {
            fReport f = new fReport();
            string localizacion;
            string comentario;
            bool b;

            localizacion = this.lblLoc.Text;
            
            //Dialogo
            f.txtLocalizador.Text = localizacion;

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                comentario = f.cmbCommenting.Text;
                reportcontent rc = new reportcontent();
                b = rc.SendReport(localizacion, this.ipPc, comentario);
                MessageBox.Show("Thanks for your comment", "Thanks!");
            }
        }

        private void mnuQuickGuide_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.google.com");
        }

        private void ShowAnnouncement()
        { 
            fAnuncio f = new fAnuncio();
            string urlAn = confg.GetSettingValue(confg.PerfilConfig, "ANUNCIO");
            f.Goto(urlAn);
            f.Show();
        }

        private void showAnnouncementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowAnnouncement();
        }

        private void showAlarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fNotif = new fNotify();
            this.fNotif.IncluirMensaje(this.ServerTime.ToShortTimeString());
            this.fNotif.Show(this);
        }

        private void wmpVideo1_Enter(object sender, EventArgs e)
        {

        }

        private void tstTemas_Click(object sender, EventArgs e)
        {

        }
    }
}
