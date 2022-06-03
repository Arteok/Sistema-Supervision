
namespace SistemaEstudiantes
{
    partial class Opciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opciones));
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnEditUsuarios = new System.Windows.Forms.Button();
            this.btnRutas = new System.Windows.Forms.Button();
            this.btnResoPantalla = new System.Windows.Forms.Button();
            this.btnDepurarPDF = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            // btnEditUsuarios
            // 
            this.btnEditUsuarios.BackColor = System.Drawing.Color.DimGray;
            this.btnEditUsuarios.Font = new System.Drawing.Font("Arial", 14F);
            this.btnEditUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnEditUsuarios.Location = new System.Drawing.Point(587, 235);
            this.btnEditUsuarios.Name = "btnEditUsuarios";
            this.btnEditUsuarios.Size = new System.Drawing.Size(415, 54);
            this.btnEditUsuarios.TabIndex = 17;
            this.btnEditUsuarios.Text = "Crear - editar usuarios";
            this.btnEditUsuarios.UseVisualStyleBackColor = false;
            this.btnEditUsuarios.Click += new System.EventHandler(this.btnEditUsuarios_Click);
            this.btnEditUsuarios.MouseLeave += new System.EventHandler(this.btnEditUsuarios_MouseLeave);
            this.btnEditUsuarios.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnEditUsuarios_MouseMove);
            // 
            // btnRutas
            // 
            this.btnRutas.BackColor = System.Drawing.Color.DimGray;
            this.btnRutas.Font = new System.Drawing.Font("Arial", 14F);
            this.btnRutas.ForeColor = System.Drawing.Color.White;
            this.btnRutas.Location = new System.Drawing.Point(587, 335);
            this.btnRutas.Name = "btnRutas";
            this.btnRutas.Size = new System.Drawing.Size(415, 54);
            this.btnRutas.TabIndex = 18;
            this.btnRutas.Text = "Rutas de acceso";
            this.btnRutas.UseVisualStyleBackColor = false;
            this.btnRutas.Click += new System.EventHandler(this.btnRutas_Click);
            this.btnRutas.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.btnRutas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button1_MouseMove);
            // 
            // btnResoPantalla
            // 
            this.btnResoPantalla.BackColor = System.Drawing.Color.DimGray;
            this.btnResoPantalla.Font = new System.Drawing.Font("Arial", 14F);
            this.btnResoPantalla.ForeColor = System.Drawing.Color.White;
            this.btnResoPantalla.Location = new System.Drawing.Point(587, 535);
            this.btnResoPantalla.Name = "btnResoPantalla";
            this.btnResoPantalla.Size = new System.Drawing.Size(415, 54);
            this.btnResoPantalla.TabIndex = 20;
            this.btnResoPantalla.Text = "Resolución de pantalla";
            this.btnResoPantalla.UseVisualStyleBackColor = false;
            this.btnResoPantalla.Click += new System.EventHandler(this.btnResoPantalla_Click);
            this.btnResoPantalla.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            this.btnResoPantalla.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button2_MouseMove);
            // 
            // btnDepurarPDF
            // 
            this.btnDepurarPDF.BackColor = System.Drawing.Color.DimGray;
            this.btnDepurarPDF.Font = new System.Drawing.Font("Arial", 14F);
            this.btnDepurarPDF.ForeColor = System.Drawing.Color.White;
            this.btnDepurarPDF.Location = new System.Drawing.Point(587, 435);
            this.btnDepurarPDF.Name = "btnDepurarPDF";
            this.btnDepurarPDF.Size = new System.Drawing.Size(415, 54);
            this.btnDepurarPDF.TabIndex = 19;
            this.btnDepurarPDF.Text = "Depurar Base de Datos de PDF";
            this.btnDepurarPDF.UseVisualStyleBackColor = false;
            this.btnDepurarPDF.Click += new System.EventHandler(this.btnDepurarPDF_Click);
            this.btnDepurarPDF.MouseLeave += new System.EventHandler(this.btnDepurarPDF_MouseLeave);
            this.btnDepurarPDF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnDepurarPDF_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkOrange;
            this.panel2.Controls.Add(this.btnVolver);
            this.panel2.Controls.Add(this.btnSalir);
            this.panel2.Controls.Add(this.lblNombre);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1351, 97);
            this.panel2.TabIndex = 46;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(50, 54);
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
            this.label4.Location = new System.Drawing.Point(40, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Opciones";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 97);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 633);
            this.panel1.TabIndex = 47;
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
            this.pictureBox1.Location = new System.Drawing.Point(7, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 180);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Opciones
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1346, 725);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnDepurarPDF);
            this.Controls.Add(this.btnResoPantalla);
            this.Controls.Add(this.btnRutas);
            this.Controls.Add(this.btnEditUsuarios);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Opciones";
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
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnEditUsuarios;
        private System.Windows.Forms.Button btnRutas;
        private System.Windows.Forms.Button btnResoPantalla;
        private System.Windows.Forms.Button btnDepurarPDF;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}