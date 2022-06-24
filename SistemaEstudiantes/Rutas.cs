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
            RutasActuales();
        }
        private void ordenar()
        {
            tbxBDN.Clear();
        }
        private void RutasActuales()
        {
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
                            lblActualBD.Text = node.Attributes[1].Value;
                        }
                    }
                }
            }


        }
        private void BtnIngresarBD_Click(object sender, EventArgs e)
        {
            string valorNuevo = tbxBDN.Text;
            try
            {
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
                                DialogResult accionRealizar = MessageBox.Show("Desea cambiar la ruta:\n\n" + node.Attributes[1].Value+ "\n\nPor la siguiente:\n\n" + valorNuevo + "\n\nSi - Para realizar el cambio.\n" +
                                "\nNo - Para no modificar ningun parametro.\n", "Sistema Informa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                                if (accionRealizar == DialogResult.Yes)
                                {
                                    node.Attributes[1].Value = valorNuevo;
                                    MessageBox.Show("Ruta actualizada correctamente", "Sistema Informa");
                                    //no hay nada que hacer. solo dejar seguir el codigo
                                }
                                else if (accionRealizar == DialogResult.No)
                                {                                   
                                    ordenar();
                                }

                                /*MessageBox.Show(node.Attributes[1].Value);
                                node.Attributes[1].Value = valorNuevo;
                                MessageBox.Show(node.Attributes[1].Value);*/

                            }
                        }
                    }
                }
                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                ConfigurationManager.RefreshSection("appSettings");
                ordenar();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                }

                else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                {
                    MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                }
                else if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                {
                    MessageBox.Show("Datos duplicados en base de datos.", "Sistema Informa");
                }
                else if (ex.Message.Contains("El motor de datos Microsoft Jet no puede abrir el archivo"))
                {
                    MessageBox.Show("Ruta a base de datos incorrecta.", "Sistema Informa");
                }
                else
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
                ordenar();
            }
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
