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
    public partial class EstadisticasCargarPoli : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        bool opcionesPermisos;
        public EstadisticasCargarPoli(string usuario, string permisos, bool permisosOpciones, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            opcionesPermisos = permisosOpciones;
            conexionBaseDatos = conexionBD;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            EstadisticasPlanillas myEstadisticasPlanillas = new EstadisticasPlanillas(nombreUsuario, tipoUsuario, opcionesPermisos, conexionBaseDatos);
            this.Hide();
            myEstadisticasPlanillas.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
