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
    public partial class Estadisticas : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string permisosUsuario;
        bool logueadoBool;

        ColegiosEstadisticas myColegios;

        public Estadisticas(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoBool = logueado;
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;

            myColegios = new ColegiosEstadisticas();
            myColegios.ConexionBD(conexionBaseDatos);
            myColegios.CargarColegiosUshuaia();
            myColegios.CargarColegiosGrande();

            if (permisosUsuario == "SuperUsuario")
            {
                btnMatriculaComp.Enabled = false;
                btnMatriculaComp.BackColor = Color.Silver;
            }
            else
            { 
                btnCantColegios.Enabled = false;
                btnCantColegios.BackColor = Color.Silver;
                btnPlantillas.Enabled = false;
                btnPlantillas.BackColor = Color.Silver;
                btnMatriculaComp.Enabled = false;
                btnMatriculaComp.BackColor = Color.Silver;          
            }          
        }       
        
        private void btnEstadistica1_Click_1(object sender, EventArgs e)
        {
            Estadisticas1 miEstadisticas1 = new Estadisticas1(nombreUsuario, permisosUsuario, logueadoBool, conexionBaseDatos);
            this.Hide();           
            miEstadisticas1.Show();            
        }

        private void btnEstadistica2_Click_1(object sender, EventArgs e)
        {
            Estadisticas2 miEstadisticas2 = new Estadisticas2(nombreUsuario, permisosUsuario, logueadoBool, conexionBaseDatos);
            this.Hide();
            miEstadisticas2.Show();

        }

        private void btnEstadistica3_Click(object sender, EventArgs e)
        {
            Estadisticas3 miEstadisticas3 = new Estadisticas3(nombreUsuario, permisosUsuario, logueadoBool, conexionBaseDatos);
            this.Hide();
            miEstadisticas3.Show();
        }

        private void btnCantColegios_Click(object sender, EventArgs e)
        {
            EstadisticasColegiosTDF myEstadisticasColegiosTDF = new EstadisticasColegiosTDF(nombreUsuario, permisosUsuario, logueadoBool, conexionBaseDatos);
            this.Hide();
            myEstadisticasColegiosTDF.Show();
        }

        private void btmPlantillas_Click(object sender, EventArgs e)
        {
            EstadisticasPlanillas myEstadisticasPlanillas = new EstadisticasPlanillas(nombreUsuario, permisosUsuario, logueadoBool, conexionBaseDatos);
            this.Hide();
            myEstadisticasPlanillas.Show();

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

        private void btnEstadistica1_MouseMove(object sender, MouseEventArgs e)
        {
            btnEstadistica1.BackColor = Color.DodgerBlue;            
        }

        private void btnEstadistica1_MouseLeave(object sender, EventArgs e)
        {
            btnEstadistica1.BackColor = Color.DimGray;
        }

        private void btnEstadistica2_MouseMove(object sender, MouseEventArgs e)
        {
            btnEstadistica2.BackColor = Color.DodgerBlue;
        }

        private void btnEstadistica2_MouseLeave(object sender, EventArgs e)
        {
            btnEstadistica2.BackColor = Color.DimGray;
        }

        private void btnEstadistica3_MouseMove(object sender, MouseEventArgs e)
        {
            btnEstadistica3.BackColor = Color.DodgerBlue;
        }

        private void btnEstadistica3_MouseLeave(object sender, EventArgs e)
        {
            btnEstadistica3.BackColor = Color.DimGray;
        }

        private void btnMatriculaComp_MouseMove(object sender, MouseEventArgs e)
        {
            btnMatriculaComp.BackColor = Color.DodgerBlue;
        }

        private void btnMatriculaComp_MouseLeave(object sender, EventArgs e)
        {
            btnMatriculaComp.BackColor = Color.DimGray;
        }

        private void btnCantColegios_MouseMove(object sender, MouseEventArgs e)
        {
            btnCantColegios.BackColor = Color.DodgerBlue;
        }

        private void btnCantColegios_MouseLeave(object sender, EventArgs e)
        {
            btnCantColegios.BackColor = Color.DimGray;
        }

        private void btmPlantillas_MouseMove(object sender, MouseEventArgs e)
        {
            btnPlantillas.BackColor = Color.DodgerBlue;
        }

        private void btmPlantillas_MouseLeave(object sender, EventArgs e)
        {
            btnPlantillas.BackColor = Color.DimGray;
        }
    }
}
