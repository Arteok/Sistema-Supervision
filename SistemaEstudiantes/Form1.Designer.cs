
namespace SistemaEstudiantes
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnNormativa = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPlantas = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnOpciones = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNormativa
            // 
            this.btnNormativa.BackColor = System.Drawing.Color.DimGray;
            this.btnNormativa.Font = new System.Drawing.Font("Arial", 14F);
            this.btnNormativa.ForeColor = System.Drawing.Color.White;
            this.btnNormativa.Location = new System.Drawing.Point(575, 243);
            this.btnNormativa.Name = "btnNormativa";
            this.btnNormativa.Size = new System.Drawing.Size(415, 54);
            this.btnNormativa.TabIndex = 5;
            this.btnNormativa.Text = "Normativa";
            this.btnNormativa.UseVisualStyleBackColor = false;
            this.btnNormativa.Click += new System.EventHandler(this.btnNormativa_Click);
            this.btnNormativa.MouseLeave += new System.EventHandler(this.btnNormativa_MouseLeave);
            this.btnNormativa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnNormativa_MouseMove);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkOrange;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblUsuario);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1351, 97);
            this.panel1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(30, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "Usuario:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(100, 50);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(13, 18);
            this.lblUsuario.TabIndex = 14;
            this.lblUsuario.Text = "-";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 19.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 32);
            this.label1.TabIndex = 12;
            this.label1.Text = "Supervisión de Nivel Secundario";
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
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            this.btnSalir.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnSalir_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 633);
            this.panel2.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(46, 375);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 69);
            this.label2.TabIndex = 1;
            this.label2.Text = "MINISTERIO\r\n       DE\r\nEDUCACIÓN";
            // 
            // btnPlantas
            // 
            this.btnPlantas.BackColor = System.Drawing.Color.DimGray;
            this.btnPlantas.Font = new System.Drawing.Font("Arial", 14F);
            this.btnPlantas.ForeColor = System.Drawing.Color.White;
            this.btnPlantas.Location = new System.Drawing.Point(575, 343);
            this.btnPlantas.Name = "btnPlantas";
            this.btnPlantas.Size = new System.Drawing.Size(415, 54);
            this.btnPlantas.TabIndex = 8;
            this.btnPlantas.Text = "Plantas Organicas Funcionales";
            this.btnPlantas.UseVisualStyleBackColor = false;
            this.btnPlantas.Click += new System.EventHandler(this.btnPlantas_Click);
            this.btnPlantas.MouseLeave += new System.EventHandler(this.btnPlantas_MouseLeave);
            this.btnPlantas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnPlantas_MouseMove);
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.BackColor = System.Drawing.Color.DimGray;
            this.btnEstadisticas.Font = new System.Drawing.Font("Arial", 14F);
            this.btnEstadisticas.ForeColor = System.Drawing.Color.White;
            this.btnEstadisticas.Location = new System.Drawing.Point(575, 443);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(415, 54);
            this.btnEstadisticas.TabIndex = 9;
            this.btnEstadisticas.Text = "Estadisticas";
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            this.btnEstadisticas.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.btnEstadisticas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button1_MouseMove);
            // 
            // btnOpciones
            // 
            this.btnOpciones.BackColor = System.Drawing.Color.DimGray;
            this.btnOpciones.Font = new System.Drawing.Font("Arial", 14F);
            this.btnOpciones.ForeColor = System.Drawing.Color.White;
            this.btnOpciones.Location = new System.Drawing.Point(575, 543);
            this.btnOpciones.Name = "btnOpciones";
            this.btnOpciones.Size = new System.Drawing.Size(415, 54);
            this.btnOpciones.TabIndex = 10;
            this.btnOpciones.Text = "Opciones";
            this.btnOpciones.UseVisualStyleBackColor = false;
            this.btnOpciones.Click += new System.EventHandler(this.btnOpciones_Click);
            this.btnOpciones.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            this.btnOpciones.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button2_MouseMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(7, 194);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(207, 182);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.btnOpciones);
            this.Controls.Add(this.btnEstadisticas);
            this.Controls.Add(this.btnPlantas);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnNormativa);
            this.Font = new System.Drawing.Font("Arial", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Supervisión";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNormativa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPlantas;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnOpciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

