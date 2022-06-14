
namespace SistemaEstudiantes
{
    partial class EstadisticasPlanillas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadisticasPlanillas));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPlantillaPoli = new System.Windows.Forms.Button();
            this.btnCargarPlanillas = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEliminarPoli = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.panel2.Size = new System.Drawing.Size(1006, 97);
            this.panel2.TabIndex = 46;
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(800, 27);
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
            this.btnSalir.Location = new System.Drawing.Point(900, 27);
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
            this.label4.Size = new System.Drawing.Size(162, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Estadísticas";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 633);
            this.panel1.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(46, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 69);
            this.label1.TabIndex = 1;
            this.label1.Text = "MINISTERIO\r\n       DE\r\nEDUCACIÓN";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SistemaEstudiantes.Properties.Resources.Escudo_Negro;
            this.pictureBox1.Location = new System.Drawing.Point(7, 194);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 180);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnPlantillaPoli
            // 
            this.btnPlantillaPoli.BackColor = System.Drawing.Color.DimGray;
            this.btnPlantillaPoli.Font = new System.Drawing.Font("Arial", 14F);
            this.btnPlantillaPoli.ForeColor = System.Drawing.Color.White;
            this.btnPlantillaPoli.Location = new System.Drawing.Point(404, 423);
            this.btnPlantillaPoli.Name = "btnPlantillaPoli";
            this.btnPlantillaPoli.Size = new System.Drawing.Size(415, 54);
            this.btnPlantillaPoli.TabIndex = 63;
            this.btnPlantillaPoli.Text = "Cargar Plantillas Polivalentes";
            this.btnPlantillaPoli.UseVisualStyleBackColor = false;
            this.btnPlantillaPoli.Click += new System.EventHandler(this.btnPlantillaPoli_Click);
            this.btnPlantillaPoli.MouseLeave += new System.EventHandler(this.btnPlantillaPoli_MouseLeave);
            this.btnPlantillaPoli.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPlantillaPoli_MouseMove);
            // 
            // btnCargarPlanillas
            // 
            this.btnCargarPlanillas.BackColor = System.Drawing.Color.DimGray;
            this.btnCargarPlanillas.Font = new System.Drawing.Font("Arial", 14F);
            this.btnCargarPlanillas.ForeColor = System.Drawing.Color.White;
            this.btnCargarPlanillas.Location = new System.Drawing.Point(404, 183);
            this.btnCargarPlanillas.Name = "btnCargarPlanillas";
            this.btnCargarPlanillas.Size = new System.Drawing.Size(415, 54);
            this.btnCargarPlanillas.TabIndex = 64;
            this.btnCargarPlanillas.Text = "Cargar Plantillas Generales";
            this.btnCargarPlanillas.UseVisualStyleBackColor = false;
            this.btnCargarPlanillas.Click += new System.EventHandler(this.btnCargarPlanillas_Click);
            this.btnCargarPlanillas.MouseLeave += new System.EventHandler(this.btnCargarPlanillas_MouseLeave);
            this.btnCargarPlanillas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnCargarPlanillas_MouseMove);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.DimGray;
            this.btnEliminar.Font = new System.Drawing.Font("Arial", 14F);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(404, 303);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(415, 54);
            this.btnEliminar.TabIndex = 65;
            this.btnEliminar.Text = "Eliminar Plantillas Generales";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            this.btnEliminar.MouseLeave += new System.EventHandler(this.btnEliminar_MouseLeave);
            this.btnEliminar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnEliminar_MouseMove);
            // 
            // btnEliminarPoli
            // 
            this.btnEliminarPoli.BackColor = System.Drawing.Color.DimGray;
            this.btnEliminarPoli.Font = new System.Drawing.Font("Arial", 14F);
            this.btnEliminarPoli.ForeColor = System.Drawing.Color.White;
            this.btnEliminarPoli.Location = new System.Drawing.Point(404, 543);
            this.btnEliminarPoli.Name = "btnEliminarPoli";
            this.btnEliminarPoli.Size = new System.Drawing.Size(415, 54);
            this.btnEliminarPoli.TabIndex = 66;
            this.btnEliminarPoli.Text = "Eliminar Plantillas Polivalentes";
            this.btnEliminarPoli.UseVisualStyleBackColor = false;
            this.btnEliminarPoli.Click += new System.EventHandler(this.btnEliminarPoli_Click);
            this.btnEliminarPoli.MouseLeave += new System.EventHandler(this.btnEliminarPoli_MouseLeave);
            this.btnEliminarPoli.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnEliminarPoli_MouseMove);
            // 
            // EstadisticasPlanillas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1004, 685);
            this.Controls.Add(this.btnEliminarPoli);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCargarPlanillas);
            this.Controls.Add(this.btnPlantillaPoli);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EstadisticasPlanillas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Supervisión";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnPlantillaPoli;
        private System.Windows.Forms.Button btnCargarPlanillas;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnEliminarPoli;
    }
}