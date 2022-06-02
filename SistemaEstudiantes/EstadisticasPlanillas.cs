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
        private void btnEliminarPoli_Click(object sender, EventArgs e)
        {
            EstadisticasEliminarPoli myEstadisticasEliminarPoli = new EstadisticasEliminarPoli(nombreUsuario, permisosUsuario, logueadoUsuario, conexionBaseDatos);
            this.Visible = false;
            myEstadisticasEliminarPoli.Show();
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

        private void btnCargarPlanillas_MouseMove(object sender, MouseEventArgs e)
        {
            btnCargarPlanillas.BackColor = Color.DodgerBlue;
        }
        private void btnCargarPlanillas_MouseLeave(object sender, EventArgs e)
        {
            btnCargarPlanillas.BackColor = Color.DimGray;
        }

        private void btnEliminar_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminar.BackColor = Color.DodgerBlue;
        }

        private void btnEliminar_MouseLeave(object sender, EventArgs e)
        {
            btnEliminar.BackColor = Color.DimGray;
        }

        private void btnPlantillaPoli_MouseMove(object sender, MouseEventArgs e)
        {
            btnPlantillaPoli.BackColor = Color.DodgerBlue;
        }

        private void btnPlantillaPoli_MouseLeave(object sender, EventArgs e)
        {
            btnPlantillaPoli.BackColor = Color.DimGray;
        }

        private void btnEliminarPoli_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminarPoli.BackColor = Color.DodgerBlue;
        }

        private void btnEliminarPoli_MouseLeave(object sender, EventArgs e)
        {
            btnEliminarPoli.BackColor = Color.DimGray;
        }

        private void btnVolver_MouseMove(object sender, MouseEventArgs e)
        {
            btnVolver.BackColor = Color.DimGray;
        }

        private void btnVolver_MouseLeave(object sender, EventArgs e)
        {
            btnVolver.BackColor = Color.DodgerBlue;
        }

        private void btnSalir_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalir.BackColor = Color.DimGray;
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = Color.DodgerBlue;
        }

        
    }
}
