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
        string tipoUsuario;
        bool opcionesPermisos;
        public Estadisticas(string usuario, string permisos, bool permisosOpciones, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            opcionesPermisos = permisosOpciones;
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;
        }       
        
        private void btnEstadistica1_Click_1(object sender, EventArgs e)
        {
            Estadisticas1 miEstadisticas1 = new Estadisticas1(nombreUsuario, tipoUsuario, opcionesPermisos, conexionBaseDatos);
            this.Hide();
            miEstadisticas1.Show();

        }

        private void btnEstadistica2_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEstadistica3_Click(object sender, EventArgs e)
        {

        }

        private void btnCantColegios_Click(object sender, EventArgs e)
        {

        }

        private void btmPlantillas_Click(object sender, EventArgs e)
        {

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

        

        /** private void btnEstadistica1_MouseMove(object sender, MouseEventArgs e)
         {
             btnEstadistica1.BackColor = Color.DodgerBlue;
         }

         private void btnEstadistica1_MouseLeave(object sender, EventArgs e)
         {
             btnEstadistica1.BackColor = Color.DimGray;
         }*/


    }
}
