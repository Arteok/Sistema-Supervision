
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
            this.panel1.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(810, 70);
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
            this.btnSalir.Location = new System.Drawing.Point(700, 20);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 33);
            this.btnSalir.TabIndex = 13;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnVolver.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVolver.ForeColor = System.Drawing.Color.White;
            this.btnVolver.Image = ((System.Drawing.Image)(resources.GetObject("btnVolver.Image")));
            this.btnVolver.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVolver.Location = new System.Drawing.Point(580, 20);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(90, 33);
            this.btnVolver.TabIndex = 12;
            this.btnVolver.Text = "    Atrás";
            this.btnVolver.UseVisualStyleBackColor = false;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 19.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Rutas de acceso";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12.25F);
            this.label2.Location = new System.Drawing.Point(12, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 19);
            this.label2.TabIndex = 42;
            this.label2.Text = "Ruta de Base de Datos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12.25F);
            this.label3.Location = new System.Drawing.Point(12, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 19);
            this.label3.TabIndex = 43;
            this.label3.Text = "Ruta de Resoluciones";
            // 
            // tbxBDN
            // 
            this.tbxBDN.Location = new System.Drawing.Point(191, 135);
            this.tbxBDN.Name = "tbxBDN";
            this.tbxBDN.Size = new System.Drawing.Size(479, 20);
            this.tbxBDN.TabIndex = 44;
            // 
            // tbxReso
            // 
            this.tbxReso.Location = new System.Drawing.Point(191, 211);
            this.tbxReso.Name = "tbxReso";
            this.tbxReso.Size = new System.Drawing.Size(479, 20);
            this.tbxReso.TabIndex = 45;
            // 
            // BtnIngresarBD
            // 
            this.BtnIngresarBD.BackColor = System.Drawing.Color.DodgerBlue;
            this.BtnIngresarBD.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnIngresarBD.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnIngresarBD.ForeColor = System.Drawing.Color.White;
            this.BtnIngresarBD.Location = new System.Drawing.Point(700, 127);
            this.BtnIngresarBD.Name = "BtnIngresarBD";
            this.BtnIngresarBD.Size = new System.Drawing.Size(90, 33);
            this.BtnIngresarBD.TabIndex = 17;
            this.BtnIngresarBD.Text = "Ingresar";
            this.BtnIngresarBD.UseVisualStyleBackColor = false;
            this.BtnIngresarBD.Click += new System.EventHandler(this.BtnIngresarBD_Click);
            // 
            // btnIngresarRR
            // 
            this.btnIngresarRR.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnIngresarRR.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIngresarRR.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresarRR.ForeColor = System.Drawing.Color.White;
            this.btnIngresarRR.Location = new System.Drawing.Point(700, 203);
            this.btnIngresarRR.Name = "btnIngresarRR";
            this.btnIngresarRR.Size = new System.Drawing.Size(90, 33);
            this.btnIngresarRR.TabIndex = 46;
            this.btnIngresarRR.Text = "Ingresar";
            this.btnIngresarRR.UseVisualStyleBackColor = false;
            this.btnIngresarRR.Click += new System.EventHandler(this.btnIngresarRR_Click);
            // 
            // Rutas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.btnIngresarRR);
            this.Controls.Add(this.BtnIngresarBD);
            this.Controls.Add(this.tbxReso);
            this.Controls.Add(this.tbxBDN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Rutas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rutas";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}