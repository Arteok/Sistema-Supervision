
namespace SistemaEstudiantes
{
    partial class Estadisticas2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Estadisticas2));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblCreando = new System.Windows.Forms.Label();
            this.lblProcesando = new System.Windows.Forms.Label();
            this.btnCrearExcel = new System.Windows.Forms.Button();
            this.btnCrearEstadistica = new System.Windows.Forms.Button();
            this.cboxAño = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxPeriodo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.cBoxPeriodoEst = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cBoxColegio = new System.Windows.Forms.ComboBox();
            this.cBoxDepto = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cBoxPeriodoPla = new System.Windows.Forms.ComboBox();
            this.cBoxAñoPla = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cBoxAñoEst = new System.Windows.Forms.ComboBox();
            this.myDataGridView = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Controls.Add(this.btnSalir);
            this.panel2.Controls.Add(this.lblNombre);
            this.panel2.Controls.Add(this.btnVolver);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1351, 97);
            this.panel2.TabIndex = 51;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(1230, 25);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 43);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(120, 50);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(50, 18);
            this.lblNombre.TabIndex = 14;
            this.lblNombre.Text = "label9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(50, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "Usuario:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 19.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(40, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(532, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Matriculas por orientación y/o especialidad";
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(1110, 25);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(90, 43);
            this.btnVolver.TabIndex = 12;
            this.btnVolver.Text = "    Atrás";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnRefresh.Location = new System.Drawing.Point(573, 104);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 40);
            this.btnRefresh.TabIndex = 109;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblCreando
            // 
            this.lblCreando.AutoSize = true;
            this.lblCreando.Location = new System.Drawing.Point(1183, 290);
            this.lblCreando.Name = "lblCreando";
            this.lblCreando.Size = new System.Drawing.Size(57, 14);
            this.lblCreando.TabIndex = 108;
            this.lblCreando.Text = "Creando...";
            // 
            // lblProcesando
            // 
            this.lblProcesando.AutoSize = true;
            this.lblProcesando.Location = new System.Drawing.Point(918, 290);
            this.lblProcesando.Name = "lblProcesando";
            this.lblProcesando.Size = new System.Drawing.Size(74, 14);
            this.lblProcesando.TabIndex = 107;
            this.lblProcesando.Text = "Procesando...";
            // 
            // btnCrearExcel
            // 
            this.btnCrearExcel.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnCrearExcel.Location = new System.Drawing.Point(1112, 222);
            this.btnCrearExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCrearExcel.Name = "btnCrearExcel";
            this.btnCrearExcel.Size = new System.Drawing.Size(200, 60);
            this.btnCrearExcel.TabIndex = 106;
            this.btnCrearExcel.Text = "Crear Excel";
            this.btnCrearExcel.UseVisualStyleBackColor = true;
            this.btnCrearExcel.Click += new System.EventHandler(this.btnCrearExcel_Click);
            // 
            // btnCrearEstadistica
            // 
            this.btnCrearEstadistica.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnCrearEstadistica.Location = new System.Drawing.Point(856, 222);
            this.btnCrearEstadistica.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCrearEstadistica.Name = "btnCrearEstadistica";
            this.btnCrearEstadistica.Size = new System.Drawing.Size(200, 60);
            this.btnCrearEstadistica.TabIndex = 105;
            this.btnCrearEstadistica.Text = "Crear Estadística";
            this.btnCrearEstadistica.UseVisualStyleBackColor = true;
            this.btnCrearEstadistica.Click += new System.EventHandler(this.btnCrearEstadistica_Click);
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
            this.cboxAño.Location = new System.Drawing.Point(893, 148);
            this.cboxAño.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxAño.Name = "cboxAño";
            this.cboxAño.Size = new System.Drawing.Size(160, 24);
            this.cboxAño.TabIndex = 103;
            this.cboxAño.SelectedIndexChanged += new System.EventHandler(this.cboxAño_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label2.Location = new System.Drawing.Point(848, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 101;
            this.label2.Text = "Año:";
            // 
            // cboxPeriodo
            // 
            this.cboxPeriodo.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxPeriodo.FormattingEnabled = true;
            this.cboxPeriodo.Items.AddRange(new object[] {
            "Marzo",
            "Septiembre"});
            this.cboxPeriodo.Location = new System.Drawing.Point(1152, 148);
            this.cboxPeriodo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxPeriodo.Name = "cboxPeriodo";
            this.cboxPeriodo.Size = new System.Drawing.Size(160, 24);
            this.cboxPeriodo.TabIndex = 104;
            this.cboxPeriodo.SelectedIndexChanged += new System.EventHandler(this.cboxPeriodo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label1.Location = new System.Drawing.Point(1083, 151);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 102;
            this.label1.Text = "Periodo:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(189, 281);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 100;
            this.button4.Text = "Ver Planilla";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // cBoxPeriodoEst
            // 
            this.cBoxPeriodoEst.FormattingEnabled = true;
            this.cBoxPeriodoEst.Location = new System.Drawing.Point(681, 232);
            this.cBoxPeriodoEst.Name = "cBoxPeriodoEst";
            this.cBoxPeriodoEst.Size = new System.Drawing.Size(121, 22);
            this.cBoxPeriodoEst.TabIndex = 99;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(614, 182);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 23);
            this.button3.TabIndex = 98;
            this.button3.Text = "Estadisticas";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cBoxColegio
            // 
            this.cBoxColegio.FormattingEnabled = true;
            this.cBoxColegio.Location = new System.Drawing.Point(266, 236);
            this.cBoxColegio.Name = "cBoxColegio";
            this.cBoxColegio.Size = new System.Drawing.Size(121, 22);
            this.cBoxColegio.TabIndex = 97;
            // 
            // cBoxDepto
            // 
            this.cBoxDepto.FormattingEnabled = true;
            this.cBoxDepto.Location = new System.Drawing.Point(112, 236);
            this.cBoxDepto.Name = "cBoxDepto";
            this.cBoxDepto.Size = new System.Drawing.Size(121, 22);
            this.cBoxDepto.TabIndex = 96;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 134);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 95;
            this.button2.Text = "Planillas";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cBoxPeriodoPla
            // 
            this.cBoxPeriodoPla.FormattingEnabled = true;
            this.cBoxPeriodoPla.Location = new System.Drawing.Point(266, 184);
            this.cBoxPeriodoPla.Name = "cBoxPeriodoPla";
            this.cBoxPeriodoPla.Size = new System.Drawing.Size(121, 22);
            this.cBoxPeriodoPla.TabIndex = 94;
            // 
            // cBoxAñoPla
            // 
            this.cBoxAñoPla.FormattingEnabled = true;
            this.cBoxAñoPla.Location = new System.Drawing.Point(112, 184);
            this.cBoxAñoPla.Name = "cBoxAñoPla";
            this.cBoxAñoPla.Size = new System.Drawing.Size(121, 22);
            this.cBoxAñoPla.TabIndex = 93;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(614, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 92;
            this.button1.Text = "Ver Estadistica";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cBoxAñoEst
            // 
            this.cBoxAñoEst.FormattingEnabled = true;
            this.cBoxAñoEst.Location = new System.Drawing.Point(540, 232);
            this.cBoxAñoEst.Name = "cBoxAñoEst";
            this.cBoxAñoEst.Size = new System.Drawing.Size(121, 22);
            this.cBoxAñoEst.TabIndex = 91;
            // 
            // myDataGridView
            // 
            this.myDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.myDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridView.Location = new System.Drawing.Point(10, 332);
            this.myDataGridView.Name = "myDataGridView";
            this.myDataGridView.Size = new System.Drawing.Size(1326, 385);
            this.myDataGridView.TabIndex = 90;
            // 
            // Estadisticas2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1346, 725);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblCreando);
            this.Controls.Add(this.lblProcesando);
            this.Controls.Add(this.btnCrearExcel);
            this.Controls.Add(this.btnCrearEstadistica);
            this.Controls.Add(this.cboxAño);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboxPeriodo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.cBoxPeriodoEst);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cBoxColegio);
            this.Controls.Add(this.cBoxDepto);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cBoxPeriodoPla);
            this.Controls.Add(this.cBoxAñoPla);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cBoxAñoEst);
            this.Controls.Add(this.myDataGridView);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Estadisticas2";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Supervisión";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblCreando;
        private System.Windows.Forms.Label lblProcesando;
        private System.Windows.Forms.Button btnCrearExcel;
        private System.Windows.Forms.Button btnCrearEstadistica;
        private System.Windows.Forms.ComboBox cboxAño;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxPeriodo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox cBoxPeriodoEst;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cBoxColegio;
        private System.Windows.Forms.ComboBox cBoxDepto;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cBoxPeriodoPla;
        private System.Windows.Forms.ComboBox cBoxAñoPla;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cBoxAñoEst;
        private System.Windows.Forms.DataGridView myDataGridView;
    }
}