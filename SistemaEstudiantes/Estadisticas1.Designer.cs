
namespace SistemaEstudiantes
{
    partial class Estadisticas1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Estadisticas1));
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.dataGVBusqueda = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCrearEstadistica = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBusqueda)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(1230, 25);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 40);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
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
            this.btnVolver.Size = new System.Drawing.Size(90, 40);
            this.btnVolver.TabIndex = 12;
            this.btnVolver.Text = "    Atrás";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // dataGVBusqueda
            // 
            this.dataGVBusqueda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGVBusqueda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGVBusqueda.Location = new System.Drawing.Point(12, 390);
            this.dataGVBusqueda.Name = "dataGVBusqueda";
            this.dataGVBusqueda.Size = new System.Drawing.Size(1326, 327);
            this.dataGVBusqueda.TabIndex = 49;
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
            this.panel2.Size = new System.Drawing.Size(1351, 90);
            this.panel2.TabIndex = 50;
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
            this.label4.Size = new System.Drawing.Size(486, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Declaración de Secciones y Matriculas";
            // 
            // btnCrearEstadistica
            // 
            this.btnCrearEstadistica.Font = new System.Drawing.Font("Arial", 10.25F);
            this.btnCrearEstadistica.Location = new System.Drawing.Point(1110, 212);
            this.btnCrearEstadistica.Name = "btnCrearEstadistica";
            this.btnCrearEstadistica.Size = new System.Drawing.Size(183, 47);
            this.btnCrearEstadistica.TabIndex = 59;
            this.btnCrearEstadistica.Text = "Crear Estadistica";
            this.btnCrearEstadistica.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(731, 299);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 23);
            this.button4.TabIndex = 69;
            this.button4.Text = "Ver Planilla";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // cBoxPeriodoEst
            // 
            this.cBoxPeriodoEst.FormattingEnabled = true;
            this.cBoxPeriodoEst.Location = new System.Drawing.Point(316, 230);
            this.cBoxPeriodoEst.Name = "cBoxPeriodoEst";
            this.cBoxPeriodoEst.Size = new System.Drawing.Size(121, 22);
            this.cBoxPeriodoEst.TabIndex = 68;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(249, 180);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 23);
            this.button3.TabIndex = 67;
            this.button3.Text = "Estadisticas";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // cBoxColegio
            // 
            this.cBoxColegio.FormattingEnabled = true;
            this.cBoxColegio.Location = new System.Drawing.Point(808, 254);
            this.cBoxColegio.Name = "cBoxColegio";
            this.cBoxColegio.Size = new System.Drawing.Size(121, 22);
            this.cBoxColegio.TabIndex = 66;
            // 
            // cBoxDepto
            // 
            this.cBoxDepto.FormattingEnabled = true;
            this.cBoxDepto.Location = new System.Drawing.Point(654, 254);
            this.cBoxDepto.Name = "cBoxDepto";
            this.cBoxDepto.Size = new System.Drawing.Size(121, 22);
            this.cBoxDepto.TabIndex = 65;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(731, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 64;
            this.button2.Text = "Planillas";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // cBoxPeriodoPla
            // 
            this.cBoxPeriodoPla.FormattingEnabled = true;
            this.cBoxPeriodoPla.Location = new System.Drawing.Point(808, 202);
            this.cBoxPeriodoPla.Name = "cBoxPeriodoPla";
            this.cBoxPeriodoPla.Size = new System.Drawing.Size(121, 22);
            this.cBoxPeriodoPla.TabIndex = 63;
            // 
            // cBoxAñoPla
            // 
            this.cBoxAñoPla.FormattingEnabled = true;
            this.cBoxAñoPla.Location = new System.Drawing.Point(654, 202);
            this.cBoxAñoPla.Name = "cBoxAñoPla";
            this.cBoxAñoPla.Size = new System.Drawing.Size(121, 22);
            this.cBoxAñoPla.TabIndex = 62;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 61;
            this.button1.Text = "Ver Estadistica";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cBoxAñoEst
            // 
            this.cBoxAñoEst.FormattingEnabled = true;
            this.cBoxAñoEst.Location = new System.Drawing.Point(175, 230);
            this.cBoxAñoEst.Name = "cBoxAñoEst";
            this.cBoxAñoEst.Size = new System.Drawing.Size(121, 22);
            this.cBoxAñoEst.TabIndex = 60;
            // 
            // Estadisticas1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1350, 729);
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
            this.Controls.Add(this.btnCrearEstadistica);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGVBusqueda);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.Name = "Estadisticas1";
            this.Text = "Declaración de Secciones y Matriculas";
            ((System.ComponentModel.ISupportInitialize)(this.dataGVBusqueda)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dataGVBusqueda;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCrearEstadistica;
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
    }
}