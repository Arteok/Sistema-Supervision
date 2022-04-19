using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEstudiantes
{


    public partial class Estadisticas1Cargar : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        bool opcionesPermisos;
        public Estadisticas1Cargar(string usuario, string permisos, bool permisosOpciones, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            opcionesPermisos = permisosOpciones;
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Estadisticas1 myEstadisticas1 = new Estadisticas1(nombreUsuario, tipoUsuario, true, conexionBaseDatos);
            myEstadisticas1.Visible = true;
            this.Close();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
