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
    public partial class Usuario : Form
    {        
        OleDbConnection conexionBaseDatos;
        DataTable miDataTable = new DataTable();

        string nombreUsuario;
        string permisosUsuario;
        public Usuario(string nombre, string usuario, OleDbConnection conexionBD)
        {
            InitializeComponent();
            conexionBaseDatos = conexionBD;

        }
        private void btnIniSesion_Click(object sender, EventArgs e)
        {
            try// Revisa si esta en la red correcta
            {//codigo para loguear
                string queryValidarUsuario = "SELECT Usuario, Contraseña, Permisos FROM Usuarios WHERE Usuario = @idUsuario AND Contraseña = @pass";

                OleDbCommand sqlComando = new OleDbCommand(queryValidarUsuario, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idUsuario", tbxUsuario.Text);
                sqlComando.Parameters.AddWithValue("@pass", tbxContraseña.Text);

                OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                miDataAdapter.Fill(miDataTable);

                if (miDataTable.Rows.Count == 1)
                {
                    nombreUsuario = miDataTable.Rows[0][0].ToString();
                    permisosUsuario = miDataTable.Rows[0][2].ToString();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario y/o contraseña incorrectos. Vuelva a intentarlo o comuníquese con el administrador del sistema.");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Asegúrese de estar conectado a la red de la Dirección Provincial o el servidor no esta ON en este momento.", "Sistema Informa");
                }                
            }
        }

        public string NombreUsuario()
        {           
            return nombreUsuario;
        }
        public string PermisosUsuario()
        {
            return permisosUsuario;
        }     

        private void btnIniSesion_MouseMove(object sender, MouseEventArgs e)
        {
            btnIniSesion.BackColor = Color.DimGray;
        }

        private void btnIniSesion_MouseLeave(object sender, EventArgs e)
        {
            btnIniSesion.BackColor = Color.DodgerBlue;
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
