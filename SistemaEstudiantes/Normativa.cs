using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaEstudiantes;
using System.Data.OleDb;
using System.Diagnostics;



namespace SistemaEstudiantes
{
    public partial class Normativa : Form
    {     

        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        bool permisosDataBase;
        int column;
        int row;
        
        public Normativa(string usuario, string permisos, bool permisosBD, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            lblNombre.Text = usuario;
            permisosDataBase = permisosBD;
            conexionBaseDatos = conexionBD;
            limpiarOrdenar();
            buscar();

        }
        private void limpiarOrdenar()
        {            
            btnVerPdf.Enabled = false;
            btnVerPdf.BackColor = Color.Silver;
            tbxNormaSelec.Enabled = false;
            tbxFechaSelect.Enabled = false;
            tbxTomoSelec.Enabled = false;
            tbxFolio.Enabled = false;
            tbxSintesisSelect.Enabled = false;

            tbxNormaSelec.Clear();
            tbxTomoSelec.Clear();
            tbxFolio.Clear();
            tbxSintesisSelect.Clear();

            //lblCantidadRegistros.Text = "-";

            if (permisosDataBase == true)
            {
                btnBaseDatos.Enabled = true;
                btnBaseDatos.BackColor = Color.DodgerBlue;
                btnEditar.Enabled = false;
                btnEditar.BackColor = Color.Silver;
            }
            else 
            {
                btnBaseDatos.Enabled = false;
                btnBaseDatos.BackColor = Color.Silver;
                btnEditar.Enabled = false;
                btnEditar.BackColor = Color.Silver;
            }            
        }

        private void dataGVBusqueda_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1) //Evita que se haga click en la fila -1 y columna -1       
            {
                column = e.ColumnIndex; //columna            
                row = e.RowIndex; //fila
                                  
                //Rellena los tbx 
                tbxNormaSelec.Text = Convert.ToString(dataGVBusqueda.Rows[e.RowIndex].Cells[0].Value);
                tbxFechaSelect.Text = Convert.ToString(dataGVBusqueda.Rows[e.RowIndex].Cells[1].Value);
                tbxTomoSelec.Text = Convert.ToString(dataGVBusqueda.Rows[e.RowIndex].Cells[2].Value);
                tbxFolio.Text = Convert.ToString(dataGVBusqueda.Rows[e.RowIndex].Cells[3].Value);
                tbxSintesisSelect.Text = Convert.ToString(dataGVBusqueda.Rows[e.RowIndex].Cells[5].Value);         
                
                tbxNormaSelec.Enabled = true;
                tbxFechaSelect.Enabled = true;
                tbxTomoSelec.Enabled = true;
                tbxFolio.Enabled = true;
                tbxSintesisSelect.Enabled = true;

                btnVerPdf.Enabled = true;
                btnVerPdf.BackColor = Color.DodgerBlue;

                if (permisosDataBase == true)
                {
                    btnEditar.Enabled = true;
                    btnEditar.BackColor = Color.DodgerBlue;
                }                
                   
            }
        }
        private void buscar()
        {
            try
            {
                if (tbxNorma.Text != "")
                {
                    DataTable miDataTable = new DataTable();
                    //string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio, Tipo, Sintesis FROM BDNORMATIVA WHERE Norma = @idBuscar"; //busca por el parametro exacto
                    string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio, Tipo, Sintesis FROM BDNORMATIVA WHERE Norma LIKE  '%' + @Buscar + '%'";

                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", tbxNorma.Text);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    dataGVBusqueda.DataSource = miDataTable;


                    if ((Convert.ToString(dataGVBusqueda.Rows[0].Cells[0].Value) != ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        lblCantidadRegistros.Text = Convert.ToString(dataGVBusqueda.Rows.Count - 1);
                    }
                    else
                    {
                        lblCantidadRegistros.Text = "-";
                        MessageBox.Show("No se encontró ninguna coincidencia con el parámetro ingresado.");
                    }

                }
                else if (tbxSintesis.Text != "")
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio, Tipo, Sintesis FROM BDNORMATIVA WHERE Sintesis LIKE  '%' + @Buscar + '%'"; //busca por una cualquier valor agregado

                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@Buscar", tbxSintesis.Text);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);

                    dataGVBusqueda.DataSource = miDataTable;

                    if ((Convert.ToString(dataGVBusqueda.Rows[0].Cells[0].Value) != ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        lblCantidadRegistros.Text = Convert.ToString(dataGVBusqueda.Rows.Count - 1);
                    }
                    else
                    {
                        lblCantidadRegistros.Text = "-";
                        MessageBox.Show("No se encontró ninguna coincidencia con el parámetro ingresado.");
                    }
                }
                else if (tbxFecha.Text != "")
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio, Tipo, Sintesis FROM BDNORMATIVA WHERE Fecha LIKE  '%' + @Buscar + '%'"; //busca por una cualquier valor agregado

                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@Buscar", tbxFecha.Text);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);

                    dataGVBusqueda.DataSource = miDataTable;

                    if ((Convert.ToString(dataGVBusqueda.Rows[0].Cells[0].Value) != ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        lblCantidadRegistros.Text = Convert.ToString(dataGVBusqueda.Rows.Count - 1);
                    }
                    else
                    {
                        lblCantidadRegistros.Text = "-";
                        MessageBox.Show("No se encontró ninguna coincidencia con el parámetro ingresado.");
                    }
                }
                else if (tbxTipo.Text != "")
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio, Tipo, Sintesis FROM BDNORMATIVA WHERE Tipo LIKE  '%' + @Buscar + '%'"; //busca por una cualquier valor agregado

                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@Buscar", tbxTipo.Text);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);

                    dataGVBusqueda.DataSource = miDataTable;

                    if ((Convert.ToString(dataGVBusqueda.Rows[0].Cells[0].Value) != ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        lblCantidadRegistros.Text = Convert.ToString(dataGVBusqueda.Rows.Count - 1);
                    }
                    else
                    {
                        lblCantidadRegistros.Text = "-";
                        MessageBox.Show("No se encontró ninguna coincidencia con el parámetro ingresado.");
                    }
                }
                else
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio, Tipo, Sintesis FROM BDNORMATIVA";

                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);

                    dataGVBusqueda.DataSource = miDataTable;

                    if ((Convert.ToString(dataGVBusqueda.Rows[0].Cells[0].Value) != ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        lblCantidadRegistros.Text = Convert.ToString(dataGVBusqueda.Rows.Count - 1);
                    }
                    else
                    {
                        lblCantidadRegistros.Text = "-";
                        MessageBox.Show("No se encontró ninguna coincidencia con el parámetro ingresado.");
                    }
                }
                limpiarOrdenar();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                }
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();            
        }

        private void btnVerPdf_Click(object sender, EventArgs e)
        {
            try
            {
                Process proceso = new Process();                     
                proceso.StartInfo.FileName = @"\\server\Compartida\Sistema\BDSistema Supervision\Resoluciones PDF\" + tbxNormaSelec.Text + ".pdf";
                //string pdfAbrir = "C:\Users\Pablo\Desktop\Programas\SistemaEstudiantes 15 -11\SistemaEstudiantes\carpetaPDF\" + tbxNormaSelec.Text + (".pdf");.pdf
                proceso.Start();
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("encontrar el archivo especificado"))
                {
                    MessageBox.Show("No se ha encontrado el archivo PDF perteneciente a esta Norma.");
                }
                else
                {
                    MessageBox.Show(ex.ToString());
                }                        
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string idNormaSelecionada = Convert.ToString(dataGVBusqueda.Rows[row].Cells[0].Value); // Devuelve la norma seleccionada en el dgView que esta en la posicion 0
            BaseDatosNorm miBDNorm = new BaseDatosNorm(nombreUsuario, tipoUsuario, idNormaSelecionada, conexionBaseDatos);
            this.Visible = false;
            miBDNorm.Show();

        }

        private void btnBaseDatos_Click(object sender, EventArgs e)
        {
           BaseDatosNorm miBDNorm = new BaseDatosNorm(nombreUsuario, tipoUsuario, conexionBaseDatos);            
           this.Visible = false;           
           miBDNorm.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {            
            Form1 miForm1 = new Form1(nombreUsuario, tipoUsuario, true);            
            miForm1.Visible = true;            
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void tbxNorma_Click(object sender, EventArgs e) //evita que tengamos valores en 2 campos antes de realizar la busqueda
        {
            tbxNorma.Clear();
            tbxSintesis.Clear();
            tbxFecha.Clear();
            tbxTipo.Clear();
        }

        private void tbxTema_Click(object sender, EventArgs e)//evita que tengamos valores en 2 campos antes de realizar la busqueda
        {
            tbxNorma.Clear();
            tbxSintesis.Clear();
            tbxFecha.Clear();
            tbxTipo.Clear();
        }
        private void tbxFecha_MouseClick(object sender, MouseEventArgs e)
        {
            tbxNorma.Clear();
            tbxSintesis.Clear();
            tbxFecha.Clear();
            tbxTipo.Clear();
        }
        private void tbxTipo_MouseClick(object sender, MouseEventArgs e)
        {
            tbxNorma.Clear();
            tbxSintesis.Clear();
            tbxFecha.Clear();
            tbxTipo.Clear();
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

        private void btnBuscar_MouseMove(object sender, MouseEventArgs e)
        {
            btnBuscar.BackColor = Color.DimGray;
        }

        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            btnBuscar.BackColor = Color.DodgerBlue;
        }

        private void btnVerPdf_MouseMove(object sender, MouseEventArgs e)
        {
            btnVerPdf.BackColor = Color.DimGray;
        }

        private void btnVerPdf_MouseLeave(object sender, EventArgs e)
        {
            btnVerPdf.BackColor = Color.DodgerBlue;
        }

        private void btnBaseDatos_MouseMove(object sender, MouseEventArgs e)
        {
            btnBaseDatos.BackColor = Color.DimGray;
        }

        private void btnBaseDatos_MouseLeave(object sender, EventArgs e)
        {
            btnBaseDatos.BackColor = Color.DodgerBlue;
        }

        private void btnEditar_MouseMove(object sender, MouseEventArgs e)
        {
            btnEditar.BackColor = Color.DimGray;
        }

        private void btnEditar_MouseLeave(object sender, EventArgs e)
        {
            btnEditar.BackColor = Color.DodgerBlue;
        }
    }
}
