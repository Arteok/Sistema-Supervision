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

namespace SistemaEstudiantes
{
    public partial class Estudiantes : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string permisosUsuario;
        bool logueadoBool;
        public Estudiantes(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoBool = logueado;
            lblUsuario.Text = usuario;
            conexionBaseDatos = conexionBD;
        }

        private void btnInscripciones_Click(object sender, EventArgs e)
        {

        }
        private void btnPases_Click(object sender, EventArgs e)
        {

        }

        private void btnColegios_Click(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 miForm1 = new Form1(nombreUsuario, permisosUsuario, true);
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
