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
                if (tbxUsuario.Text == "admin" || tbxUsuario.Text == "Admin")
                {
                    if ((tbxUsuario.Text == "admin" || tbxUsuario.Text == "Admin") && tbxContraseña.Text == "1234")
                    {
                        nombreUsuario = "Admin";
                        permisosUsuario = "Admin";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario y/o contraseña incorrectos. Vuelva a intentarlo o comuníquese con el administrador del sistema.", "Sistema Informa");
                    }
                }
                else 
                {
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
                        MessageBox.Show("Usuario y/o contraseña incorrectos. Vuelva a intentarlo o comuníquese con el administrador del sistema.", "Sistema Informa");
                    }
                }              
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Asegúrese de estar conectado a la red adecuada o el servidor no esta ON en este momento.", "Sistema Informa");
                }               

                else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                {
                    MessageBox.Show("El archivo que quiere actualizar y abrir, se encuentra activo, debe cerrarlo.", "Sistema Informa");
                }
                else if (ex.Message.Contains("datos duplicados"))
                {
                    MessageBox.Show("Datos duplicados en base de datos.", "Sistema Informa");
                }
                else if (ex.Message.Contains("No se pudo encontrar el archivo"))
                {
                    MessageBox.Show("No se encontró ninguna estadística para los parámetros especificados.", "Sistema Informa");
                }
                else if (ex.Message.Contains("No es un nombre de archivo válido"))
                {                    
                    MessageBox.Show("La ruta de la base de datos no es correcta. Ingrese en modo admin y asigne una ruta válida.", "Sistema Informa");                   
                }
                else
                {
                    MessageBox.Show(Convert.ToString(ex), "Sistema Informa");
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
