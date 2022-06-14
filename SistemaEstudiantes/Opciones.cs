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
    public partial class Opciones : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        bool logueado;
        public Opciones(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            lblNombre.Text = usuario;
            logueado = true;
            conexionBaseDatos = conexionBD;            

            if (tipoUsuario == "SuperUsuario")
            {
                btnEditUsuarios.Enabled = true;
                btnEditUsuarios.BackColor = Color.DimGray;
                btnRutas.Enabled = false;
                btnRutas.BackColor = Color.Silver;
                btnDepurarPDF.Enabled = false;
                btnDepurarPDF.BackColor = Color.Silver;
                btnResoPantalla.Enabled = false;
                btnResoPantalla.BackColor = Color.Silver;                
            }
            else 
            {
                btnEditUsuarios.Enabled = false;
                btnEditUsuarios.BackColor = Color.Silver;
                btnRutas.Enabled = false;
                btnRutas.BackColor = Color.Silver;
                btnResoPantalla.Enabled = false;
                btnResoPantalla.BackColor = Color.Silver;
                btnDepurarPDF.Enabled = false;
                btnDepurarPDF.BackColor = Color.Silver;
            }            
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

        private void btnEditUsuarios_Click(object sender, EventArgs e)
        {
            CrearEditar miCrearEditarU = new CrearEditar(nombreUsuario, tipoUsuario, conexionBaseDatos);
            this.Visible = false;
            miCrearEditarU.Show();
        }
        private void btnRutas_Click(object sender, EventArgs e)
        {
            Rutas misRutas = new Rutas(nombreUsuario, tipoUsuario, conexionBaseDatos);
            this.Visible = false;
            misRutas.Show();
        }
        private void btnDepurarPDF_Click(object sender, EventArgs e)
        {
            DepurarPDF miDepurar = new DepurarPDF(nombreUsuario, tipoUsuario, conexionBaseDatos);
            this.Visible = false;
            miDepurar.Show();
        }

        private void btnResoPantalla_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalir.BackColor = Color.DimGray;
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = Color.DodgerBlue;
        }

        private void btnVolver_MouseMove(object sender, MouseEventArgs e)
        {
            btnVolver.BackColor = Color.DimGray;
        }

        private void btnVolver_MouseLeave(object sender, EventArgs e)
        {
            btnVolver.BackColor = Color.DodgerBlue;
        }
        private void btnEditUsuarios_MouseMove(object sender, MouseEventArgs e)
        {
            btnEditUsuarios.BackColor = Color.DodgerBlue;
        }
        private void btnEditUsuarios_MouseLeave(object sender, EventArgs e)
        {
            btnEditUsuarios.BackColor = Color.DimGray;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            btnRutas.BackColor = Color.DodgerBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            btnRutas.BackColor = Color.DimGray;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            btnResoPantalla.BackColor = Color.DodgerBlue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            btnResoPantalla.BackColor = Color.DimGray;
        }
        private void btnDepurarPDF_MouseMove(object sender, MouseEventArgs e)
        {
            btnDepurarPDF.BackColor = Color.DodgerBlue;
        }
        private void btnDepurarPDF_MouseLeave(object sender, EventArgs e)
        {
            btnDepurarPDF.BackColor = Color.DimGray;
        }

       
    }
}
