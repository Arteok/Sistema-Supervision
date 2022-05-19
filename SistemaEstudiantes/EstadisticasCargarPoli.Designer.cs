
namespace SistemaEstudiantes
{
    partial class EstadisticasCargarPoli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadisticasCargarPoli));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxColegiosGrande = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tbxColegioSel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnImportar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboxDepto = new System.Windows.Forms.ComboBox();
            this.cboxPeriodo = new System.Windows.Forms.ComboBox();
            this.cboxAño = new System.Windows.Forms.ComboBox();
            this.cboxColegiosUshuaia = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelecExcel = new System.Windows.Forms.Button();
            this.myDataGridView = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView)).BeginInit();
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
            this.label4.Size = new System.Drawing.Size(405, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Cargar Plantillas de Polivalentes";
            // 
            // cboxColegiosGrande
            // 
            this.cboxColegiosGrande.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxColegiosGrande.FormattingEnabled = true;
            this.cboxColegiosGrande.Items.AddRange(new object[] {
            "Polivalente Cotorruelo"});
            this.cboxColegiosGrande.Location = new System.Drawing.Point(1150, 146);
            this.cboxColegiosGrande.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxColegiosGrande.Name = "cboxColegiosGrande";
            this.cboxColegiosGrande.Size = new System.Drawing.Size(160, 24);
            this.cboxColegiosGrande.TabIndex = 78;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(991, 127);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(136, 43);
            this.dataGridView1.TabIndex = 77;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnRefresh.Location = new System.Drawing.Point(556, 103);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 50);
            this.btnRefresh.TabIndex = 76;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tbxColegioSel
            // 
            this.tbxColegioSel.Font = new System.Drawing.Font("Arial", 10.25F);
            this.tbxColegioSel.Location = new System.Drawing.Point(495, 276);
            this.tbxColegioSel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbxColegioSel.Name = "tbxColegioSel";
            this.tbxColegioSel.Size = new System.Drawing.Size(514, 23);
            this.tbxColegioSel.TabIndex = 75;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label5.Location = new System.Drawing.Point(334, 279);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 16);
            this.label5.TabIndex = 74;
            this.label5.Text = "Colegio Seleccionado:";
            // 
            // btnImportar
            // 
            this.btnImportar.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnImportar.Location = new System.Drawing.Point(1098, 257);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(200, 60);
            this.btnImportar.TabIndex = 73;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label1.Location = new System.Drawing.Point(1060, 199);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 16);
            this.label1.TabIndex = 72;
            this.label1.Text = "Colegios:";
            // 
            // cboxDepto
            // 
            this.cboxDepto.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxDepto.FormattingEnabled = true;
            this.cboxDepto.Items.AddRange(new object[] {
            "Ushuaia",
            "Rio Grande"});
            this.cboxDepto.Location = new System.Drawing.Point(815, 196);
            this.cboxDepto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxDepto.Name = "cboxDepto";
            this.cboxDepto.Size = new System.Drawing.Size(160, 24);
            this.cboxDepto.TabIndex = 70;
            // 
            // cboxPeriodo
            // 
            this.cboxPeriodo.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxPeriodo.FormattingEnabled = true;
            this.cboxPeriodo.Items.AddRange(new object[] {
            "Marzo",
            "Octubre"});
            this.cboxPeriodo.Location = new System.Drawing.Point(404, 196);
            this.cboxPeriodo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxPeriodo.Name = "cboxPeriodo";
            this.cboxPeriodo.Size = new System.Drawing.Size(160, 24);
            this.cboxPeriodo.TabIndex = 69;
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
            this.cboxAño.Location = new System.Drawing.Point(98, 196);
            this.cboxAño.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxAño.Name = "cboxAño";
            this.cboxAño.Size = new System.Drawing.Size(160, 24);
            this.cboxAño.TabIndex = 68;
            // 
            // cboxColegiosUshuaia
            // 
            this.cboxColegiosUshuaia.Font = new System.Drawing.Font("Arial", 10.25F);
            this.cboxColegiosUshuaia.FormattingEnabled = true;
            this.cboxColegiosUshuaia.Items.AddRange(new object[] {
            "Polivalente Bustelo"});
            this.cboxColegiosUshuaia.Location = new System.Drawing.Point(1138, 196);
            this.cboxColegiosUshuaia.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboxColegiosUshuaia.Name = "cboxColegiosUshuaia";
            this.cboxColegiosUshuaia.Size = new System.Drawing.Size(160, 24);
            this.cboxColegiosUshuaia.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label3.Location = new System.Drawing.Point(701, 199);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 67;
            this.label3.Text = "Departamento:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label6.Location = new System.Drawing.Point(331, 199);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 66;
            this.label6.Text = "Periodo:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 10.25F);
            this.label7.Location = new System.Drawing.Point(49, 199);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 16);
            this.label7.TabIndex = 65;
            this.label7.Text = "Año:";
            // 
            // btnSelecExcel
            // 
            this.btnSelecExcel.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnSelecExcel.Location = new System.Drawing.Point(53, 257);
            this.btnSelecExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelecExcel.Name = "btnSelecExcel";
            this.btnSelecExcel.Size = new System.Drawing.Size(200, 60);
            this.btnSelecExcel.TabIndex = 64;
            this.btnSelecExcel.Text = "Seleccionar Excel";
            this.btnSelecExcel.UseVisualStyleBackColor = true;
            this.btnSelecExcel.Click += new System.EventHandler(this.btnSelecExcel_Click);
            // 
            // myDataGridView
            // 
            this.myDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.myDataGridView.Location = new System.Drawing.Point(13, 329);
            this.myDataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.myDataGridView.Name = "myDataGridView";
            this.myDataGridView.Size = new System.Drawing.Size(1320, 385);
            this.myDataGridView.TabIndex = 63;
            // 
            // EstadisticasCargarPoli
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1346, 725);
            this.Controls.Add(this.cboxColegiosGrande);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tbxColegioSel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboxDepto);
            this.Controls.Add(this.cboxPeriodo);
            this.Controls.Add(this.cboxAño);
            this.Controls.Add(this.cboxColegiosUshuaia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSelecExcel);
            this.Controls.Add(this.myDataGridView);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EstadisticasCargarPoli";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EstadisticasCargarPoli";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxColegiosGrande;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox tbxColegioSel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxDepto;
        private System.Windows.Forms.ComboBox cboxPeriodo;
        private System.Windows.Forms.ComboBox cboxAño;
        private System.Windows.Forms.ComboBox cboxColegiosUshuaia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSelecExcel;
        private System.Windows.Forms.DataGridView myDataGridView;
    }
}