
namespace SistemaEstudiantes
{
    partial class EstadisticasEliminar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadisticasEliminar));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxColegiosGrande = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.myDataGridView = new System.Windows.Forms.DataGridView();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxDepto = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboxPeriodo = new System.Windows.Forms.ComboBox();
            this.cboxAño = new System.Windows.Forms.ComboBox();
            this.cboxColegiosUshuaia = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Controls.Add(this.btnVolver);
            this.panel2.Controls.Add(this.btnSalir);
            this.panel2.Controls.Add(this.lblNombre);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1351, 97);
            this.panel2.TabIndex = 48;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(1110, 27);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(90, 43);
            this.btnVolver.TabIndex = 12;
            this.btnVolver.Text = "    Atrás";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.btnVolver.MouseLeave += new System.EventHandler(this.btnVolver_MouseLeave);
            this.btnVolver.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnVolver_MouseMove);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(1230, 27);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 43);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            this.btnSalir.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnSalir_MouseMove);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(120, 54);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(50, 18);
            this.lblNombre.TabIndex = 14;
            this.lblNombre.Text = "label9";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(50, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "Usuario:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 19.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(40, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(361, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Eliminar Plantillas Generales";
            // 
            // cboxColegiosGrande
            // 
            this.cboxColegiosGrande.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxColegiosGrande.FormattingEnabled = true;
            this.cboxColegiosGrande.Location = new System.Drawing.Point(1103, 21);
            this.cboxColegiosGrande.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxColegiosGrande.Name = "cboxColegiosGrande";
            this.cboxColegiosGrande.Size = new System.Drawing.Size(160, 24);
            this.cboxColegiosGrande.TabIndex = 71;
            this.cboxColegiosGrande.SelectedIndexChanged += new System.EventHandler(this.cboxColegiosGrande_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Image = global::SistemaEstudiantes.Properties.Resources.Button_Refresh_icon;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(600, 113);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 40);
            this.btnRefresh.TabIndex = 70;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.btnRefresh_MouseLeave);
            this.btnRefresh.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnRefresh_MouseMove);
            // 
            // myDataGridView
            // 
            this.myDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.myDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridView.Location = new System.Drawing.Point(99, 354);
            this.myDataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.myDataGridView.Name = "myDataGridView";
            this.myDataGridView.Size = new System.Drawing.Size(1320, 385);
            this.myDataGridView.TabIndex = 61;
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEliminar.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(582, 71);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(160, 50);
            this.btnEliminar.TabIndex = 69;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            this.btnEliminar.MouseLeave += new System.EventHandler(this.btnEliminar_MouseLeave);
            this.btnEliminar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnEliminar_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label1.Location = new System.Drawing.Point(55, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Año:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label3.Location = new System.Drawing.Point(1019, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 68;
            this.label3.Text = "Colegios:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label5.Location = new System.Drawing.Point(353, 24);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "Periodo:";
            // 
            // cboxDepto
            // 
            this.cboxDepto.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxDepto.FormattingEnabled = true;
            this.cboxDepto.Items.AddRange(new object[] {
            "Ushuaia",
            "Rio Grande"});
            this.cboxDepto.Location = new System.Drawing.Point(776, 21);
            this.cboxDepto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxDepto.Name = "cboxDepto";
            this.cboxDepto.Size = new System.Drawing.Size(160, 24);
            this.cboxDepto.TabIndex = 67;
            this.cboxDepto.SelectedIndexChanged += new System.EventHandler(this.cboxDepto_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label6.Location = new System.Drawing.Point(666, 24);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 64;
            this.label6.Text = "Departamento:";
            // 
            // cboxPeriodo
            // 
            this.cboxPeriodo.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxPeriodo.FormattingEnabled = true;
            this.cboxPeriodo.Items.AddRange(new object[] {
            "Marzo",
            "Septiembre"});
            this.cboxPeriodo.Location = new System.Drawing.Point(422, 21);
            this.cboxPeriodo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxPeriodo.Name = "cboxPeriodo";
            this.cboxPeriodo.Size = new System.Drawing.Size(160, 24);
            this.cboxPeriodo.TabIndex = 66;
            this.cboxPeriodo.SelectedIndexChanged += new System.EventHandler(this.cboxPeriodo_SelectedIndexChanged);
            // 
            // cboxAño
            // 
            this.cboxAño.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxAño.FormattingEnabled = true;
            this.cboxAño.Items.AddRange(new object[] {
            "2021",
            "2022",
            "2023",
            "2024",
            "2025",
            "2026",
            "2027",
            "2028",
            "2029",
            "2030"});
            this.cboxAño.Location = new System.Drawing.Point(104, 21);
            this.cboxAño.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxAño.Name = "cboxAño";
            this.cboxAño.Size = new System.Drawing.Size(160, 24);
            this.cboxAño.TabIndex = 65;
            this.cboxAño.SelectedIndexChanged += new System.EventHandler(this.cboxAño_SelectedIndexChanged);
            // 
            // cboxColegiosUshuaia
            // 
            this.cboxColegiosUshuaia.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxColegiosUshuaia.FormattingEnabled = true;
            this.cboxColegiosUshuaia.Location = new System.Drawing.Point(1103, 21);
            this.cboxColegiosUshuaia.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxColegiosUshuaia.Name = "cboxColegiosUshuaia";
            this.cboxColegiosUshuaia.Size = new System.Drawing.Size(160, 24);
            this.cboxColegiosUshuaia.TabIndex = 72;
            this.cboxColegiosUshuaia.SelectedIndexChanged += new System.EventHandler(this.cboxColegiosUshuaia_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.cboxAño);
            this.panel1.Controls.Add(this.cboxColegiosUshuaia);
            this.panel1.Controls.Add(this.cboxPeriodo);
            this.panel1.Controls.Add(this.cboxColegiosGrande);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cboxDepto);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1320, 140);
            this.panel1.TabIndex = 73;
            // 
            // EstadisticasEliminar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1346, 725);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.myDataGridView);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EstadisticasEliminar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eliminar Estadísticas";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxColegiosGrande;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView myDataGridView;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxDepto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboxPeriodo;
        private System.Windows.Forms.ComboBox cboxAño;
        private System.Windows.Forms.ComboBox cboxColegiosUshuaia;
        private System.Windows.Forms.Panel panel1;
    }
}