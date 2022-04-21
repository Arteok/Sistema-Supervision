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
    public partial class EstadisticasPlanillas : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string permisosUsuario;
        bool logueadoUsuario;
        public EstadisticasPlanillas(string usuario, string permisos, bool permisosBD, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoUsuario = permisosBD;
            conexionBaseDatos = conexionBD;
        }
       
        private void btnCargarPlanillas_Click(object sender, EventArgs e)
        {
            EstadisticasCargar myEstadisticasCargar = new EstadisticasCargar(nombreUsuario, permisosUsuario, logueadoUsuario, conexionBaseDatos);
            this.Visible = false;
            myEstadisticasCargar.Show();

        }
        private void btnPlantillaPoli_Click(object sender, EventArgs e)
        {
            EstadisticasCargarPoli myEstadisticasCargarPoli = new EstadisticasCargarPoli(nombreUsuario, permisosUsuario, logueadoUsuario, conexionBaseDatos);
            this.Visible = false;
            myEstadisticasCargarPoli.Show();

        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EstadisticasEliminar myEstadisticasEliminar = new EstadisticasEliminar(nombreUsuario, permisosUsuario, logueadoUsuario, conexionBaseDatos);
            this.Visible = false;
            myEstadisticasEliminar.Show();

        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Estadisticas myEstadisticas = new Estadisticas(nombreUsuario, permisosUsuario, true, conexionBaseDatos);             
            myEstadisticas.Visible = true;
            this.Close();

        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        

        
    }
}
