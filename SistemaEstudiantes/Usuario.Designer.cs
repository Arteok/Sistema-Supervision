
namespace SistemaEstudiantes
{
    partial class Usuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuario));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxUsuario = new System.Windows.Forms.TextBox();
            this.tbxContraseña = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnIniSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña";
            // 
            // tbxUsuario
            // 
            this.tbxUsuario.Location = new System.Drawing.Point(123, 24);
            this.tbxUsuario.Name = "tbxUsuario";
            this.tbxUsuario.Size = new System.Drawing.Size(100, 20);
            this.tbxUsuario.TabIndex = 2;
            // 
            // tbxContraseña
            // 
            this.tbxContraseña.Location = new System.Drawing.Point(123, 66);
            this.tbxContraseña.Name = "tbxContraseña";
            this.tbxContraseña.PasswordChar = '*';
            this.tbxContraseña.Size = new System.Drawing.Size(100, 20);
            this.tbxContraseña.TabIndex = 3;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnSalir.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(138, 111);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 33);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            this.btnSalir.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnSalir_MouseMove);
            // 
            // btnIniSesion
            // 
            this.btnIniSesion.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnIniSesion.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnIniSesion.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIniSesion.ForeColor = System.Drawing.Color.White;
            this.btnIniSesion.Location = new System.Drawing.Point(42, 111);
            this.btnIniSesion.Name = "btnIniSesion";
            this.btnIniSesion.Size = new System.Drawing.Size(90, 33);
            this.btnIniSesion.TabIndex = 6;
            this.btnIniSesion.Text = "Iniciar";
            this.btnIniSesion.UseVisualStyleBackColor = false;
            this.btnIniSesion.Click += new System.EventHandler(this.btnIniSesion_Click);
            this.btnIniSesion.MouseLeave += new System.EventHandler(this.btnIniSesion_MouseLeave);
            this.btnIniSesion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnIniSesion_MouseMove);
            // 
            // Usuario
            // 
            this.AcceptButton = this.btnIniSesion;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(270, 157);
            this.Controls.Add(this.btnIniSesion);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.tbxContraseña);
            this.Controls.Add(this.tbxUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Usuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxUsuario;
        private System.Windows.Forms.TextBox tbxContraseña;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnIniSesion;
    }
}