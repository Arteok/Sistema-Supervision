using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace SistemaEstudiantes
{
    public partial class DepurarPDF : Form
    {               
        //FileStream File = new FileStream();
        
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos

        string nombreUsuario;
        string tipoUsuario;
            
        public DepurarPDF(string usuario, string permisos, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;            
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
           string archivo = @"‪C:\Users\Pablo\Downloads\Carpeta1\consi.pdf";
           string destino = @"‪C:\Users\Pablo\Downloads\Carpeta2\consi.pdf";
            
            //string destino = @"‪C:\Users\Pablo\Desktop\Carpeta2\Prueba.txt";
            //MessageBox.Show(file);

            File.Move(archivo,destino);
                       
            

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 miForm1 = new Form1(nombreUsuario, tipoUsuario, true);
            miForm1.Visible = true;
            miForm1.Enabled = true;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
    }
}
