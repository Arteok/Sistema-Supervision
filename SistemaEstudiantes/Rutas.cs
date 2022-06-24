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

using System.Configuration;
using System.Xml;

namespace SistemaEstudiantes
{
    public partial class Rutas : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        public Rutas(string usuario, string permisos, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            lblNombre.Text = usuario;
            conexionBaseDatos = conexionBD;
        }
        private void BtnIngresarBD_Click(object sender, EventArgs e)
        {
            string valorNuevo = tbxBDN.Text;
             
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);//load el xml desde la direccion donde esta

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "rutaBD")
                        {
                            MessageBox.Show(node.Attributes[1].Value);
                            node.Attributes[1].Value = valorNuevo;
                            MessageBox.Show(node.Attributes[1].Value);
                        }                            
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void btnIngresarRR_Click(object sender, EventArgs e)
        {
            string valorNuevo = tbxReso.Text;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);//load el xml desde la direccion donde esta

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "rutaPDF")
                        {
                            MessageBox.Show(node.Attributes[1].Value);
                            node.Attributes[1].Value = valorNuevo;
                            MessageBox.Show(node.Attributes[1].Value);
                        }
                    }
                }
            }
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("appSettings");

        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Opciones miOpciones = new Opciones(nombreUsuario, tipoUsuario, true, conexionBaseDatos);
            miOpciones.Visible = true;
            this.Close();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
