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
using System.IO;

namespace SistemaEstudiantes
{
    public partial class Rutas : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        string valorNuevoBD;
        string valorNuevoPDF;
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
            tbxReso.Clear();
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
            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes[0].Value == "rutaPDF")
                        {
                            lblActualPDF.Text = node.Attributes[1].Value;
                        }
                    }
                }
            }
        }
        private void tbxBDN_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            myOpenFileDialog.Filter = "Access Files |* .mdb";
            myOpenFileDialog.Title = "Seleccione el archivo Access";

            if (myOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (myOpenFileDialog.FileName.Equals("") == false)
                {
                    valorNuevoBD = myOpenFileDialog.FileName;
                    tbxBDN.Text = valorNuevoBD;
                }
            }
        }

        private void BtnIngresarBD_Click(object sender, EventArgs e)
        {
            valorNuevoBD = tbxBDN.Text;//por si es necesario ponerlo manualmente
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
                                DialogResult accionRealizar = MessageBox.Show("Desea cambiar la ruta:\n" + node.Attributes[1].Value + "\n\nPor la siguiente:\n" + valorNuevoBD + "\n\nSi - Para realizar el cambio.\n" +
                                "\nNo - Para no modificar ningun parametro.\n", "Sistema Informa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                                if (accionRealizar == DialogResult.Yes)
                                {
                                    node.Attributes[1].Value = valorNuevoBD;
                                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                                    ConfigurationManager.RefreshSection("appSettings");
                                    ordenar();
                                    RutasActuales();
                                    lblActualBD.Refresh();

                                    MessageBox.Show("Ruta actualizada correctamente", "Sistema Informa");

                                }
                                else if (accionRealizar == DialogResult.No)
                                {
                                    ordenar();
                                }
                            }
                        }
                    }
                }
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
        private void tbxReso_MouseClick(object sender, MouseEventArgs e)
        {   /*Es distinta a la anterior porque es la unica forma que funcione*/  
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            myOpenFileDialog.ValidateNames = false;
            myOpenFileDialog.CheckFileExists = false;
            myOpenFileDialog.CheckPathExists = true;
            // Always default to Folder Selection.
            myOpenFileDialog.FileName = "Folder Selection.";
            //myOpenFileDialog.Title = "Seleccione la carpeta de destino";

            if (myOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                valorNuevoPDF = Path.GetDirectoryName(myOpenFileDialog.FileName);
                tbxReso.Text = valorNuevoPDF;           
            }
        }


        private void btnIngresarRR_Click(object sender, EventArgs e)
        {
            valorNuevoPDF = tbxReso.Text; //por si es necesario ponerlo manualmente

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
                            if (node.Attributes[0].Value == "rutaPDF")
                            {
                                DialogResult accionRealizar = MessageBox.Show("Desea cambiar la ruta:\n" + node.Attributes[1].Value + "\n\nPor la siguiente:\n" + valorNuevoPDF + "\n\nSi - Para realizar el cambio.\n" +
                                "\nNo - Para no modificar ningun parametro.\n", "Sistema Informa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                                if (accionRealizar == DialogResult.Yes)
                                {
                                    node.Attributes[1].Value = valorNuevoPDF;
                                    xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                                    ConfigurationManager.RefreshSection("appSettings");
                                    ordenar();
                                    RutasActuales();
                                    lblActualPDF.Refresh();

                                    MessageBox.Show("Ruta actualizada correctamente", "Sistema Informa");
                                }
                                else if (accionRealizar == DialogResult.No)
                                {
                                    ordenar();
                                }
                            }
                        }
                    }
                }
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

        private void BtnIngresarBD_MouseMove(object sender, MouseEventArgs e)
        {
            BtnIngresarBD.BackColor = Color.DimGray;
        }

        private void BtnIngresarBD_MouseLeave(object sender, EventArgs e)
        {
            BtnIngresarBD.BackColor = Color.DodgerBlue;
        }

        private void btnIngresarRR_MouseMove(object sender, MouseEventArgs e)
        {
            btnIngresarRR.BackColor = Color.DimGray;
        }

        private void btnIngresarRR_MouseLeave(object sender, EventArgs e)
        {
            btnIngresarRR.BackColor = Color.DodgerBlue;
        }




        /*
        private void btnSalir_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalir.BackColor = Color.DimGray;
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = Color.DodgerBlue;*/

    }
}
