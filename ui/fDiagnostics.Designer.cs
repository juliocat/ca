namespace ca.ui
{
    partial class fDiagnostics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDiagnostics));
            this.btnClose = new System.Windows.Forms.Button();
            this.chkOnError = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtFileAccessTest = new System.Windows.Forms.TextBox();
            this.btnStarFileAccess = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radUnit = new System.Windows.Forms.RadioButton();
            this.radActivity = new System.Windows.Forms.RadioButton();
            this.radExer = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(643, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // chkOnError
            // 
            this.chkOnError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOnError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOnError.Location = new System.Drawing.Point(12, 12);
            this.chkOnError.Multiline = true;
            this.chkOnError.Name = "chkOnError";
            this.chkOnError.ReadOnly = true;
            this.chkOnError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chkOnError.Size = new System.Drawing.Size(706, 198);
            this.chkOnError.TabIndex = 0;
            this.chkOnError.Text = "Iniciar el test de Conexión";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(643, 216);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Iniciar";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtFileAccessTest
            // 
            this.txtFileAccessTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileAccessTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileAccessTest.Location = new System.Drawing.Point(12, 275);
            this.txtFileAccessTest.Multiline = true;
            this.txtFileAccessTest.Name = "txtFileAccessTest";
            this.txtFileAccessTest.ReadOnly = true;
            this.txtFileAccessTest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFileAccessTest.Size = new System.Drawing.Size(706, 153);
            this.txtFileAccessTest.TabIndex = 7;
            this.txtFileAccessTest.Text = "Iniciar el test de acceso a los archivos";
            // 
            // btnStarFileAccess
            // 
            this.btnStarFileAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStarFileAccess.Location = new System.Drawing.Point(464, 246);
            this.btnStarFileAccess.Name = "btnStarFileAccess";
            this.btnStarFileAccess.Size = new System.Drawing.Size(75, 23);
            this.btnStarFileAccess.TabIndex = 5;
            this.btnStarFileAccess.Text = "Iniciar";
            this.btnStarFileAccess.UseVisualStyleBackColor = true;
            this.btnStarFileAccess.Click += new System.EventHandler(this.btnStarFileAccess_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(338, 249);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(281, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID parent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "loc:";
            // 
            // radUnit
            // 
            this.radUnit.AutoSize = true;
            this.radUnit.Checked = true;
            this.radUnit.Location = new System.Drawing.Point(42, 249);
            this.radUnit.Name = "radUnit";
            this.radUnit.Size = new System.Drawing.Size(42, 17);
            this.radUnit.TabIndex = 1;
            this.radUnit.TabStop = true;
            this.radUnit.Text = "unit";
            this.radUnit.UseVisualStyleBackColor = true;
            // 
            // radActivity
            // 
            this.radActivity.AutoSize = true;
            this.radActivity.Enabled = false;
            this.radActivity.Location = new System.Drawing.Point(104, 249);
            this.radActivity.Name = "radActivity";
            this.radActivity.Size = new System.Drawing.Size(58, 17);
            this.radActivity.TabIndex = 2;
            this.radActivity.Text = "activity";
            this.radActivity.UseVisualStyleBackColor = true;
            // 
            // radExer
            // 
            this.radExer.AutoSize = true;
            this.radExer.Location = new System.Drawing.Point(186, 249);
            this.radExer.Name = "radExer";
            this.radExer.Size = new System.Drawing.Size(64, 17);
            this.radExer.TabIndex = 3;
            this.radExer.Text = "exercise";
            this.radExer.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(563, 252);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Si error: continuar";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // fDiagnostics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(730, 496);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.radExer);
            this.Controls.Add(this.radActivity);
            this.Controls.Add(this.radUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnStarFileAccess);
            this.Controls.Add(this.txtFileAccessTest);
            this.Controls.Add(this.chkOnError);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fDiagnostics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Diagnósticos";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox chkOnError;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtFileAccessTest;
        private System.Windows.Forms.Button btnStarFileAccess;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radUnit;
        private System.Windows.Forms.RadioButton radActivity;
        private System.Windows.Forms.RadioButton radExer;
        private System.Windows.Forms.CheckBox checkBox1;

    }
}