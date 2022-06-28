
namespace SistemaEstudiantes
{
    partial class Rutas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rutas));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxBDN = new System.Windows.Forms.TextBox();
            this.tbxReso = new System.Windows.Forms.TextBox();
            this.BtnIngresarBD = new System.Windows.Forms.Button();
            this.btnIngresarRR = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblActualBD = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblActualPDF = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1006, 97);
            this.panel1.TabIndex = 41;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(49, 55);
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
            this.lblNombre.Location = new System.Drawing.Point(121, 55);
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
            this.btnSalir.Location = new System.Drawing.Point(900, 27);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 40);
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
            this.btnVolver.Location = new System.Drawing.Point(800, 27);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(90, 40);
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
            this.label1.Location = new System.Drawing.Point(41, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Rutas de acceso";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12.25F);
            this.label2.Location = new System.Drawing.Point(14, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 19);
            this.label2.TabIndex = 42;
            this.label2.Text = "Nueva ruta de Base de Datos:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12.25F);
            this.label3.Location = new System.Drawing.Point(14, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 19);
            this.label3.TabIndex = 43;
            this.label3.Text = "Nueva ruta a Carpeta PDF:";
            // 
            // tbxBDN
            // 
            this.tbxBDN.Location = new System.Drawing.Point(247, 65);
            this.tbxBDN.Name = "tbxBDN";
            this.tbxBDN.Size = new System.Drawing.Size(622, 20);
            this.tbxBDN.TabIndex = 44;
            this.tbxBDN.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbxBDN_MouseClick);
            // 
            // tbxReso
            // 
            this.tbxReso.Location = new System.Drawing.Point(227, 66);
            this.tbxReso.Name = "tbxReso";
            this.tbxReso.Size = new System.Drawing.Size(642, 20);
            this.tbxReso.TabIndex = 45;
            this.tbxReso.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tbxReso_MouseClick);
            // 
            // BtnIngresarBD
            // 
            this.BtnIngresarBD.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnIngresarBD.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnIngresarBD.Font = new System.Drawing.Font("Arial", 12.25F);
            this.BtnIngresarBD.ForeColor = System.Drawing.Color.White;
            this.BtnIngresarBD.Location = new System.Drawing.Point(890, 53);
            this.BtnIngresarBD.Name = "BtnIngresarBD";
            this.BtnIngresarBD.Size = new System.Drawing.Size(100, 40);
            this.BtnIngresarBD.TabIndex = 17;
            this.BtnIngresarBD.Text = "Ingresar";
            this.BtnIngresarBD.UseVisualStyleBackColor = false;
            this.BtnIngresarBD.Click += new System.EventHandler(this.BtnIngresarBD_Click);
            this.BtnIngresarBD.MouseLeave += new System.EventHandler(this.BtnIngresarBD_MouseLeave);
            this.BtnIngresarBD.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BtnIngresarBD_MouseMove);
            // 
            // btnIngresarRR
            // 
            this.btnIngresarRR.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnIngresarRR.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIngresarRR.Font = new System.Drawing.Font("Arial", 12.25F);
            this.btnIngresarRR.ForeColor = System.Drawing.Color.White;
            this.btnIngresarRR.Location = new System.Drawing.Point(890, 54);
            this.btnIngresarRR.Name = "btnIngresarRR";
            this.btnIngresarRR.Size = new System.Drawing.Size(100, 40);
            this.btnIngresarRR.TabIndex = 46;
            this.btnIngresarRR.Text = "Ingresar";
            this.btnIngresarRR.UseVisualStyleBackColor = false;
            this.btnIngresarRR.Click += new System.EventHandler(this.btnIngresarRR_Click);
            this.btnIngresarRR.MouseLeave += new System.EventHandler(this.btnIngresarRR_MouseLeave);
            this.btnIngresarRR.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnIngresarRR_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12.25F);
            this.label4.Location = new System.Drawing.Point(14, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 19);
            this.label4.TabIndex = 47;
            this.label4.Text = "Ruta actual a la Base de Datos:";
            // 
            // lblActualBD
            // 
            this.lblActualBD.AutoSize = true;
            this.lblActualBD.Font = new System.Drawing.Font("Arial", 12.25F);
            this.lblActualBD.Location = new System.Drawing.Point(258, 21);
            this.lblActualBD.Name = "lblActualBD";
            this.lblActualBD.Size = new System.Drawing.Size(15, 19);
            this.lblActualBD.TabIndex = 48;
            this.lblActualBD.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12.25F);
            this.label5.Location = new System.Drawing.Point(14, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 19);
            this.label5.TabIndex = 49;
            this.label5.Text = "Ruta actual a Carpeta PDF:";
            // 
            // lblActualPDF
            // 
            this.lblActualPDF.AutoSize = true;
            this.lblActualPDF.Font = new System.Drawing.Font("Arial", 12.25F);
            this.lblActualPDF.Location = new System.Drawing.Point(230, 22);
            this.lblActualPDF.Name = "lblActualPDF";
            this.lblActualPDF.Size = new System.Drawing.Size(15, 19);
            this.lblActualPDF.TabIndex = 50;
            this.lblActualPDF.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbxBDN);
            this.groupBox1.Controls.Add(this.lblActualBD);
            this.groupBox1.Controls.Add(this.BtnIngresarBD);
            this.groupBox1.Location = new System.Drawing.Point(0, 125);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1006, 105);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblActualPDF);
            this.groupBox2.Controls.Add(this.btnIngresarRR);
            this.groupBox2.Controls.Add(this.tbxReso);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(0, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1006, 105);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            // 
            // Rutas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1004, 685);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Rutas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Supervisión";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxBDN;
        private System.Windows.Forms.TextBox tbxReso;
        private System.Windows.Forms.Button BtnIngresarBD;
        private System.Windows.Forms.Button btnIngresarRR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblActualBD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblActualPDF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}