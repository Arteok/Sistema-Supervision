
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditUsuarios = new System.Windows.Forms.Button();
            this.btnRutas = new System.Windows.Forms.Button();
            this.btnResoPantalla = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDepurarPDF = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkOrange;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblNombre);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 70);
            this.panel1.TabIndex = 41;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(20, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 18);
            this.label9.TabIndex = 16;
            this.label9.Text = "Usuario:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(92, 42);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(50, 18);
            this.lblNombre.TabIndex = 14;
            this.lblNombre.Text = "label9";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(700, 22);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 33);
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
            this.btnVolver.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(580, 22);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(90, 33);
            this.btnVolver.TabIndex = 12;
            this.btnVolver.Text = "    Atrás";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            this.btnVolver.MouseLeave += new System.EventHandler(this.btnVolver_MouseLeave);
            this.btnVolver.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnVolver_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 19.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Opciones";
            // 
            // btnEditUsuarios
            // 
            this.btnEditUsuarios.BackColor = System.Drawing.Color.DimGray;
            this.btnEditUsuarios.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnEditUsuarios.Location = new System.Drawing.Point(370, 126);
            this.btnEditUsuarios.Name = "btnEditUsuarios";
            this.btnEditUsuarios.Size = new System.Drawing.Size(300, 45);
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
            this.btnRutas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRutas.ForeColor = System.Drawing.Color.White;
            this.btnRutas.Location = new System.Drawing.Point(370, 200);
            this.btnRutas.Name = "btnRutas";
            this.btnRutas.Size = new System.Drawing.Size(300, 45);
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
            this.btnResoPantalla.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResoPantalla.ForeColor = System.Drawing.Color.White;
            this.btnResoPantalla.Location = new System.Drawing.Point(370, 363);
            this.btnResoPantalla.Name = "btnResoPantalla";
            this.btnResoPantalla.Size = new System.Drawing.Size(300, 45);
            this.btnResoPantalla.TabIndex = 20;
            this.btnResoPantalla.Text = "Resolución de pantalla";
            this.btnResoPantalla.UseVisualStyleBackColor = false;
            this.btnResoPantalla.Click += new System.EventHandler(this.btnResoPantalla_Click);
            this.btnResoPantalla.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            this.btnResoPantalla.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button2_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(0, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 508);
            this.panel2.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(41, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 69);
            this.label2.TabIndex = 1;
            this.label2.Text = "MINISTERIO\r\n       DE\r\nEDUCACIÓN";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SistemaEstudiantes.Properties.Resources.Escudo_Negro;
            this.pictureBox1.Location = new System.Drawing.Point(8, 102);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 180);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnDepurarPDF
            // 
            this.btnDepurarPDF.BackColor = System.Drawing.Color.DimGray;
            this.btnDepurarPDF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepurarPDF.ForeColor = System.Drawing.Color.White;
            this.btnDepurarPDF.Location = new System.Drawing.Point(370, 281);
            this.btnDepurarPDF.Name = "btnDepurarPDF";
            this.btnDepurarPDF.Size = new System.Drawing.Size(300, 45);
            this.btnDepurarPDF.TabIndex = 19;
            this.btnDepurarPDF.Text = "Depurar Base de Datos de PDF";
            this.btnDepurarPDF.UseVisualStyleBackColor = false;
            this.btnDepurarPDF.Click += new System.EventHandler(this.btnDepurarPDF_Click);
            this.btnDepurarPDF.MouseLeave += new System.EventHandler(this.btnDepurarPDF_MouseLeave);
            this.btnDepurarPDF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnDepurarPDF_MouseMove);
            // 
            // Opciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.btnDepurarPDF);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnResoPantalla);
            this.Controls.Add(this.btnRutas);
            this.Controls.Add(this.btnEditUsuarios);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Opciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditUsuarios;
        private System.Windows.Forms.Button btnRutas;
        private System.Windows.Forms.Button btnResoPantalla;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDepurarPDF;
    }
}