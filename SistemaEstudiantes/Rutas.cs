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
    public partial class Rutas : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;

        public Rutas(string usuario, string permisos, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Opciones miOpciones = new Opciones(nombreUsuario, tipoUsuario,true, conexionBaseDatos )
                ;
            miOpciones.Visible = true;
            this.Close();
        }

        private void BtnIngresarBD_Click(object sender, EventArgs e)
        {


        }

        private void btnIngresarRR_Click(object sender, EventArgs e)
        {

        }

       
    }
}
