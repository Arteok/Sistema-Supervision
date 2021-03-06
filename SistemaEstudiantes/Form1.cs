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
    public partial class Form1 : Form
    {
        OleDbConnection conexionBaseDatos = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = |DataDirectory|BDNormativa.mdb");
        //OleDbConnection conexionBaseDatos = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\Pablo\Desktop\Programas\SistemaEstudiantes 06-12-2021\SistemaEstudiantes\bin\Debug\BDNormativa.mdb");
        //OleDbConnection conexionBaseDatos = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \\server\Compartida\Sistema\BDSistema Supervision\BDSistSupervision.mdb");

        string nombreUsuario;
        string tipoUsuario;
        bool opcionesPermisos;//variable para permitir acceso a editarUsuarios y a rutas
        bool permisosBD;//variable para permitir acceso para editar la base de datos
        public Form1(string usuario, string permisos, bool logueado)
        {
            InitializeComponent();
            btnNormativa.Enabled = false;
            btnNormativa.BackColor = Color.Silver;
            btnPlantas.Enabled = false;
            btnPlantas.BackColor = Color.Silver;
            btnEstadisticas.Enabled = false;
            btnEstadisticas.BackColor = Color.Silver;
            btnOpciones.Enabled = false;
            btnOpciones.BackColor = Color.Silver;

            this.Show();
            //codigo para hacer que 
            if (logueado == false)
            {
                IniciarSesion(usuario, permisos);                
            }
            else 
            {
                nombreUsuario = usuario; 
                tipoUsuario = permisos;
                ComprobarUsuario(usuario, permisos);       
            }                       
        }

        private void IniciarSesion(string nombre, string permisos)//faltaria que cargue la contraseña 
        {
            Usuario miUsuario = new Usuario(nombre, permisos,conexionBaseDatos);
            DialogResult iniciarSeccion;
            iniciarSeccion = miUsuario.ShowDialog();           

            //Se definen los tipos de usuarios para saber que puede hacer y que no

            if (iniciarSeccion == DialogResult.No)
            {
                Application.Exit();
            }
            else if (iniciarSeccion == DialogResult.Yes)
            {
                nombreUsuario = miUsuario.NombreUsuario();
                tipoUsuario = miUsuario.PermisosUsuario();
                ComprobarUsuario(nombreUsuario, tipoUsuario);
            }
            else
            {
                IniciarSesion(nombreUsuario, tipoUsuario);//revisar 
            }            
        }
        private void ComprobarUsuario(string usuario,string permisos)
        {         
            if (tipoUsuario == "Admin")//Necesario para arrancar el programa y no se puede eliminar este usuario
            {
                btnNormativa.Enabled = true;
                btnNormativa.BackColor = Color.DimGray;
                btnPlantas.Enabled = false;
                btnEstadisticas.Enabled = false;
                btnOpciones.Enabled = true;
                btnOpciones.BackColor = Color.DimGray;

                opcionesPermisos = true;
                permisosBD = true;
            }
            else if (tipoUsuario == "SuperUsuario")//usuario con privilegios como el admin
            {
                btnNormativa.Enabled = true;
                btnNormativa.BackColor = Color.DimGray;
                btnPlantas.Enabled = false;
                btnEstadisticas.Enabled = true;
                btnEstadisticas.BackColor = Color.DimGray;
                btnOpciones.Enabled = true;
                btnOpciones.BackColor = Color.DimGray;

                opcionesPermisos = true;
                permisosBD = true;
            }
            else if (tipoUsuario == "Supervisor")
            {
                btnNormativa.Enabled = true;
                btnNormativa.BackColor = Color.DimGray;
                btnPlantas.Enabled = false;
                btnEstadisticas.Enabled = false;
                btnOpciones.Enabled = true;
                btnOpciones.BackColor = Color.DimGray;

                opcionesPermisos = false;
                permisosBD = false;
            }
            else if (tipoUsuario == "SecretarioGeneral")
            {
                btnNormativa.Enabled = true;
                btnNormativa.BackColor = Color.DimGray;
                btnPlantas.Enabled = false;
                btnEstadisticas.Enabled = false;
                btnOpciones.Enabled = true;
                btnOpciones.BackColor = Color.DimGray;

                opcionesPermisos = false;
                permisosBD = false;
            }
            else if (tipoUsuario == "Secretario")
            {
                btnNormativa.Enabled = true;
                btnNormativa.BackColor = Color.DimGray;
                btnPlantas.Enabled = false;
                btnEstadisticas.Enabled = false;
                btnOpciones.Enabled = true;
                btnOpciones.BackColor = Color.DimGray;

                opcionesPermisos = false;
                permisosBD = false;
            }      
            else if ((tipoUsuario == "Usuariolvl1")||(tipoUsuario == "UsuarioBasico"))// se dividen los usuarios porque no se puede poner ams de 6 else
            {
                if (tipoUsuario == "Usuariolvl1")
                {
                    btnNormativa.Enabled = true;
                    btnNormativa.BackColor = Color.DimGray;
                    btnPlantas.Enabled = false;
                    btnEstadisticas.Enabled = false;
                    btnOpciones.Enabled = true;
                    btnOpciones.BackColor = Color.DimGray;

                    opcionesPermisos = false;
                    permisosBD = false;
                }
                else 
                {
                    btnNormativa.Enabled = true;
                    btnNormativa.BackColor = Color.DimGray;
                    btnPlantas.Enabled = false;
                    btnEstadisticas.Enabled = false;
                    btnOpciones.Enabled = true;
                    btnOpciones.BackColor = Color.DimGray;

                    opcionesPermisos = false;
                    permisosBD = false;
                }
            }
            else
            {
                IniciarSesion(nombreUsuario, tipoUsuario);//si no hay usuario valido vuelve a llamar al formulario iniciar sesion
            }
            lblUsuario.Text = nombreUsuario;
        }

        private void btnNormativa_Click(object sender, EventArgs e)
        {
            Normativa miNormativa = new Normativa(nombreUsuario,tipoUsuario,permisosBD,conexionBaseDatos);
            this.Hide();
            miNormativa.Show();    
        }
        private void btnOpciones_Click(object sender, EventArgs e)
        {
            Opciones miOpciones = new Opciones(nombreUsuario, tipoUsuario,opcionesPermisos,conexionBaseDatos);
            this.Hide();
            miOpciones.Show();
        }
        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            Estadisticas miEstadisticas = new Estadisticas(nombreUsuario, tipoUsuario, opcionesPermisos, conexionBaseDatos);
            this.Hide();
            miEstadisticas.Show();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSalir_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalir.BackColor = Color.DimGray;
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = Color.DodgerBlue;
        }

        private void btnNormativa_MouseMove(object sender, MouseEventArgs e)
        {
            btnNormativa.BackColor = Color.DodgerBlue;
        }

        private void btnNormativa_MouseLeave(object sender, EventArgs e)
        {
            btnNormativa.BackColor = Color.DimGray;
        }

        private void btnPlantas_MouseMove(object sender, MouseEventArgs e)
        {
            btnPlantas.BackColor = Color.DodgerBlue;
        }

        private void btnPlantas_MouseLeave(object sender, EventArgs e)
        {
            btnPlantas.BackColor = Color.DimGray;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            btnEstadisticas.BackColor = Color.DodgerBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            btnEstadisticas.BackColor = Color.DimGray;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            btnOpciones.BackColor = Color.DodgerBlue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            btnOpciones.BackColor = Color.DimGray;
        }

        private void btnPlantas_Click(object sender, EventArgs e)
        {

        }
    }
}
