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
using SistemaEstudiantes;

namespace SistemaEstudiantes
{
    public partial class BaseDatosNorm : Form
    {
        public DataSet miDataSet = new DataSet();        
        OleDbConnection conexionBaseDatos;
        OleDbCommand sqlComando;
        
        int row;
        public string idNormaSelecionada;

        public bool agregando = false;
        public bool modificando = false;
        public bool eliminando = false;

        string nombreUsuario;
        string tipoUsuario;
        public BaseDatosNorm(string usuario, string permisos, OleDbConnection conexionBD)
        {    
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            lblUsuario.Text = usuario;
            conexionBaseDatos = conexionBD;

            CargarDataGVBaseDatos();
            DeshabilitarLimpiar();           
        }
        public BaseDatosNorm(string usuario, string permisos, string idNorma, OleDbConnection conexionBD)
        {
            InitializeComponent();
            idNormaSelecionada = idNorma;
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            conexionBaseDatos = conexionBD;
            lblUsuario.Text = usuario;
            Modificar();     
        }

        private void DeshabilitarLimpiar()
        {
            tbxNorma.Enabled = false;
            tbxFecha.Enabled = false;
            tbxTomo.Enabled = false;           
            
            tbxFolio.Enabled = false;
            tbxTitulo.Enabled = false;
            tbxTipo.Enabled = false;
            tbxSintesis.Enabled = false;            

            btnAgregar.Visible = false;
            btnModificarA.Visible = false;
            btnCancelar.Visible = false;

            btnNuevo.Visible = true;
            btnModificar.Visible = true;
            btnEliminar.Visible = true;
            btnModificar.Enabled = false;
            btnModificar.BackColor = Color.Silver;
            btnEliminar.Enabled = false;
            btnEliminar.BackColor = Color.Silver;

            tbxTomo.Clear();
            tbxFecha.Clear();
            tbxNorma.Clear();
            tbxFolio.Clear();
            tbxTitulo.Clear();
            tbxTipo.Clear();
            tbxSintesis.Clear();

            agregando = false;
            modificando = false;
            eliminando = false;
            
            dataGridViewBD.Enabled = true;
        }
        private void Modificar()
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT Usuario, Norma, Fecha, Tomo, Folio, Titulo, Tipo, Sintesis FROM BDNORMATIVA WHERE Norma = @idBuscar";

            OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
            sqlComando.Parameters.AddWithValue("@idBuscar", idNormaSelecionada);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

            miDataAdapter.Fill(miDataTable);

            dataGridViewBD.DataSource = miDataTable;

            modificando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta modificanco

            tbxTomo.Enabled = true;
            tbxFecha.Enabled = true;
            tbxNorma.Enabled = true;
            tbxFolio.Enabled = true;
            tbxTitulo.Enabled = true;
            tbxTipo.Enabled = true;
            tbxSintesis.Enabled = true;

            tbxNorma.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[1].Value);
            tbxFecha.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[2].Value);
            tbxTomo.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[3].Value);
            tbxFolio.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[4].Value);
            tbxTitulo.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[5].Value);
            tbxTipo.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[6].Value);
            tbxSintesis.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[7].Value);

            dataGridViewBD.Enabled = false;            

            btnNuevo.Visible = false;
            btnAgregar.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;

            btnModificarA.Enabled = true;
            btnModificarA.Visible = true;
            btnCancelar.Enabled = true;
            btnCancelar.Visible = true;
        }
        private void CargarDataGVBaseDatos() //Metodo para cargar/actualizar dataGVBase de datos
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT Usuario, Norma, Fecha, Tomo, Folio, Titulo, Tipo, Sintesis FROM BDNORMATIVA";            
            //string queryCargarBD = "SELECT * FROM BDNORMATIVA";

            sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

            miDataAdapter.Fill(miDataTable);

            dataGridViewBD.DataSource = miDataTable;

            lblCantidadRegistros.Text = Convert.ToString(dataGridViewBD.Rows.Count - 1);            
        }
        private void dataGridViewBD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //devuelve el valor al que hacemos click en el dataGV
                                  
            row = e.RowIndex; //fila
                        
            if ((row != -1) && (e.ColumnIndex != -1) && (modificando == false) && (eliminando == false) && (agregando == false)) //ver que pasa con el index
            {
                //Rellena los tbx 

                tbxNorma.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[1].Value);
                tbxFecha.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[2].Value);
                tbxTomo.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[3].Value);
                tbxFolio.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[4].Value);
                tbxTitulo.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[5].Value);
                tbxTipo.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[6].Value);
                tbxSintesis.Text = Convert.ToString(dataGridViewBD.Rows[row].Cells[7].Value);

                btnModificar.Enabled = true;
                btnModificar.BackColor = Color.DodgerBlue;
                btnEliminar.Enabled = true;
                btnEliminar.BackColor = Color.DodgerBlue;

                idNormaSelecionada = Convert.ToString(dataGridViewBD.Rows[row].Cells[1].Value); // Devuelve la norma seleccionada en el dgView que esta en la posicion 0                
            }
        }       
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            agregando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta agregando

            tbxTomo.Clear();
            tbxFecha.Clear();
            tbxNorma.Clear();
            tbxFolio.Clear();
            tbxTitulo.Clear();
            tbxTipo.Clear();
            tbxSintesis.Clear();
            tbxTomo.Enabled = true;
            tbxFecha.Enabled = true;
            tbxNorma.Enabled = true;
            tbxFolio.Enabled = true;
            tbxTitulo.Enabled = true;
            tbxTipo.Enabled = true;
            tbxSintesis.Enabled = true;

            dataGridViewBD.Enabled = false;

            btnNuevo.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;

            btnAgregar.Enabled = true;
            btnAgregar.Visible = true;
            btnCancelar.Enabled = true;
            btnCancelar.Visible = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            modificando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta modificanco

            tbxTomo.Enabled = true;
            tbxFecha.Enabled = true;
            tbxNorma.Enabled = true;
            tbxFolio.Enabled = true;
            tbxTitulo.Enabled = true;
            tbxTipo.Enabled = true;
            tbxSintesis.Enabled = true;

            dataGridViewBD.Enabled = false;

            btnNuevo.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;

            btnModificarA.Enabled = true;
            btnModificarA.Visible = true;
            btnCancelar.Enabled = true;
            btnCancelar.Visible = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminando = true;
            dataGridViewBD.Enabled = false;

            DialogResult preguntaEliminar = MessageBox.Show("Desea eliminar el registro seleccionado?", "Registro Informa",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);

            if (preguntaEliminar == DialogResult.OK)
            {
                string queryEliminar = "DELETE *FROM BDNORMATIVA WHERE Norma = @idEliminar";

                sqlComando = new OleDbCommand(queryEliminar, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idEliminar", idNormaSelecionada);

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Se elimino el registro correctamente", "Sistema Informa");
                    }
                    else
                    {
                        MessageBox.Show("Registro no eliminado. Se produzco un error.");
                    }
                    conexionBaseDatos.Close();
                    CargarDataGVBaseDatos();
                    DeshabilitarLimpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex.HResult));
                    conexionBaseDatos.Close();
                }
            }
            else 
            {
                DeshabilitarLimpiar();
            }
        }      

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if ((tbxNorma.Text == ""||tbxNorma.Text == "0") || (tbxFecha.Text == "" || tbxFecha.Text == "0") || (tbxTomo.Text == "" || tbxTomo.Text == "0") ||(tbxFolio.Text == "" || tbxFolio.Text == "0") || (tbxTitulo.Text == "" || tbxTitulo.Text == "0") || (tbxTipo.Text == ""|| tbxTipo.Text == "0") || (tbxSintesis.Text == "" || tbxSintesis.Text == "0"))
            {
                MessageBox.Show("Hay campos sin completar o con valor cero."); 
            }
            else
            {                
                string queryAgregar = "INSERT INTO BDNORMATIVA VALUES (@norma, @fecha, @tomo, @folio, @titulo, @tipo, @Sintesis, @usuario)";
                sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);                

                sqlComando.Parameters.AddWithValue("@norma", tbxNorma.Text);
                sqlComando.Parameters.AddWithValue("@fecha", tbxFecha.Text);
                sqlComando.Parameters.AddWithValue("@tomo", tbxTomo.Text);
                sqlComando.Parameters.AddWithValue("@folio", tbxFolio.Text);
                sqlComando.Parameters.AddWithValue("@titulo", tbxTitulo.Text);
                sqlComando.Parameters.AddWithValue("@tipo", tbxTipo.Text);
                sqlComando.Parameters.AddWithValue("@Sintesis", tbxSintesis.Text);
                sqlComando.Parameters.AddWithValue("@usuario", nombreUsuario);

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Se agrego el registro correctamente", "Sistema Informa");
                    }
                    else
                    {
                        MessageBox.Show("Registro no agregado. Se produzco un error.", "Sistema Informa");
                    }
                    conexionBaseDatos.Close();
                    CargarDataGVBaseDatos();
                    DeshabilitarLimpiar();                    
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                    {
                        conexionBaseDatos.Close();

                        DataTable miDataTable = new DataTable();
                        string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio,Titulo, Tipo, Sintesis, usuario FROM BDNORMATIVA WHERE Norma = @idBuscar";
                        sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", tbxNorma.Text);
                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        dataGridViewBD.DataSource = miDataTable;

                        lblCantidadRegistros.Text = Convert.ToString(dataGridViewBD.Rows.Count - 1);

                        DialogResult accionRealizar = MessageBox.Show("Este documento ya esta ingresado en la base de datos.\n\nSi - Para realizar un cambio en el N° de Norma  del documento que se está ingresando.\n" +
                            "\nNo - Para modificar algún parámetro en la documento existente en la base de datos.\n" +
                            "\nCancelar - Para salir y no realizar ninguna acción.", "Sistema Informa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                        if (accionRealizar == DialogResult.Yes)
                        {
                            //no hay nada que hacer. solo dejar seguir el codigo
                        }
                        else if (accionRealizar == DialogResult.No)
                        {
                            modificando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta modificanco

                            tbxTomo.Enabled = true;
                            tbxFecha.Enabled = true;
                            tbxNorma.Enabled = true;
                            tbxFolio.Enabled = true;
                            tbxTitulo.Enabled = true;
                            tbxTipo.Enabled = true;
                            tbxSintesis.Enabled = true;

                            dataGridViewBD.Enabled = false;

                            btnNuevo.Visible = false;
                            btnModificar.Visible = false;                            
                            btnEliminar.Visible = false;

                            btnAgregar.Visible = false;
                            btnModificarA.Visible = true;                            
                            btnCancelar.Visible = true;

                            tbxNorma.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[1].Value);
                            tbxFecha.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[2].Value);
                            tbxTomo.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[3].Value);
                            tbxFolio.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[4].Value);
                            tbxTitulo.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[5].Value);
                            tbxTipo.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[6].Value);
                            tbxSintesis.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[7].Value);

                            idNormaSelecionada = Convert.ToString(dataGridViewBD.Rows[0].Cells[1].Value);
                        }
                        else if (accionRealizar == DialogResult.Cancel)
                        {
                            DeshabilitarLimpiar();
                            CargarDataGVBaseDatos();
                        }
                    }
                }                
            }
        }
        
        private void btnModificarA_Click(object sender, EventArgs e)
        {
            if ((tbxNorma.Text == "" || tbxNorma.Text == "0") || (tbxFecha.Text == "" || tbxFecha.Text == "0") || (tbxTomo.Text == "" || tbxTomo.Text == "0") || (tbxFolio.Text == "" || tbxFolio.Text == "0") || (tbxTitulo.Text == "" || tbxTitulo.Text == "0") || (tbxTipo.Text == "" || tbxTipo.Text == "0") || (tbxSintesis.Text == "" || tbxSintesis.Text == "0"))
            {
                MessageBox.Show("Hay campos sin completar o con valor cero.");
            }
            else
            {
                string queryAgregar = "UPDATE BDNORMATIVA SET Norma = @norma, Fecha = @fecha, Tomo = @tomo, Folio = @folio, Titulo = @titulo, Tipo = @tipo, Sintesis = @sintesis, Usuario = @usuario WHERE Norma = @idNorma ";
                sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);

                sqlComando.Parameters.AddWithValue("@norma", tbxNorma.Text);
                sqlComando.Parameters.AddWithValue("@fecha", tbxFecha.Text);
                sqlComando.Parameters.AddWithValue("@tomo", tbxTomo.Text);
                sqlComando.Parameters.AddWithValue("@folio", tbxFolio.Text);
                sqlComando.Parameters.AddWithValue("@titulo", tbxTitulo.Text);
                sqlComando.Parameters.AddWithValue("@tipo", tbxTipo.Text);
                sqlComando.Parameters.AddWithValue("@sintesis", tbxSintesis.Text);
                sqlComando.Parameters.AddWithValue("@usuario", nombreUsuario);
                sqlComando.Parameters.AddWithValue("@idNorma", idNormaSelecionada);

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Se Modifico el registro correctamente", "Sistema Informa");
                    }
                    else
                    {
                        MessageBox.Show("Registro no Modificado. Se produzco un error.", "Sistema Informa");
                    }
                    conexionBaseDatos.Close();
                    CargarDataGVBaseDatos();
                    DeshabilitarLimpiar();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                    {
                        conexionBaseDatos.Close();

                        DataTable miDataTable = new DataTable();
                        string queryCargarBD = "SELECT Norma, Fecha, Tomo, Folio,Titulo, Tipo, Sintesis, usuario FROM BDNORMATIVA WHERE Norma = @idBuscar";
                        sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", tbxNorma.Text);
                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        dataGridViewBD.DataSource = miDataTable;

                        lblCantidadRegistros.Text = Convert.ToString(dataGridViewBD.Rows.Count - 1);

                        DialogResult accionRealizar = MessageBox.Show("Este documento ya esta ingresado en la base de datos.\n\nSi - Para realizar un cambio en el N° de Norma  del documento que se está ingresando.\n" +
                            "\nNo - Para modificar algún parámetro en la documento existente en la base de datos.\n" +
                            "\nCancelar - Para salir y no realizar ninguna acción.", "Sistema Informa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                        if (accionRealizar == DialogResult.Yes)
                        {
                            //no hay nada que hacer. solo dejar seguir el codigo
                        }
                        else if (accionRealizar == DialogResult.No)
                        {
                            modificando = true; //variable utilizada para impedir cambios en el dataGV mientras se esta modificanco

                            tbxTomo.Enabled = true;
                            tbxFecha.Enabled = true;
                            tbxNorma.Enabled = true;
                            tbxFolio.Enabled = true;
                            tbxTitulo.Enabled = true;
                            tbxTipo.Enabled = true;
                            tbxSintesis.Enabled = true;

                            dataGridViewBD.Enabled = false;

                            btnNuevo.Visible = false;
                            btnModificar.Visible = false;
                            btnEliminar.Visible = false;

                            btnAgregar.Visible = false;
                            btnModificarA.Visible = true;
                            btnCancelar.Visible = true;

                            tbxNorma.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[1].Value);
                            tbxFecha.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[2].Value);
                            tbxTomo.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[3].Value);
                            tbxFolio.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[4].Value);
                            tbxTitulo.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[5].Value);
                            tbxTipo.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[6].Value);
                            tbxSintesis.Text = Convert.ToString(dataGridViewBD.Rows[0].Cells[7].Value);

                            idNormaSelecionada = Convert.ToString(dataGridViewBD.Rows[0].Cells[1].Value);
                        }
                        else if (accionRealizar == DialogResult.Cancel)
                        {
                            DeshabilitarLimpiar();
                            CargarDataGVBaseDatos();
                        }
                    }
                }        
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {            
            DeshabilitarLimpiar();
            CargarDataGVBaseDatos();
        }
        private void btnOpciones_Click(object sender, EventArgs e)
        {

        }

        private void btnVolverBD_Click(object sender, EventArgs e)
        {
            Normativa miNormativa = new Normativa(nombreUsuario,tipoUsuario, true, conexionBaseDatos); //pongo los permisos en verdadero porque si entro  la base de datos es porque ya tenia permisos
            miNormativa.Visible = true;            
            this.Close();
        }

        private void btnSalirBD_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVolverBD_MouseMove(object sender, MouseEventArgs e)
        {
            btnVolverBD.BackColor = Color.DimGray;
        }

        private void btnVolverBD_MouseLeave(object sender, EventArgs e)
        {
            btnVolverBD.BackColor = Color.DodgerBlue;
        }

        private void btnSalirBD_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalirBD.BackColor = Color.DimGray;
        }

        private void btnSalirBD_MouseLeave(object sender, EventArgs e)
        {
            btnSalirBD.BackColor = Color.DodgerBlue;
        }

        private void btnNuevo_MouseMove(object sender, MouseEventArgs e)
        {
            btnNuevo.BackColor = Color.DimGray;
        }

        private void btnNuevo_MouseLeave(object sender, EventArgs e)
        {
            btnNuevo.BackColor = Color.DodgerBlue;
        }

        private void btnModificar_MouseMove(object sender, MouseEventArgs e)
        {
            btnModificar.BackColor = Color.DimGray;
        }

        private void btnModificar_MouseLeave(object sender, EventArgs e)
        {
            btnModificar.BackColor = Color.DodgerBlue;
        }

        private void btnEliminar_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminar.BackColor = Color.DimGray;
        }
        private void btnEliminar_MouseLeave(object sender, EventArgs e)
        {
            btnEliminar.BackColor = Color.DodgerBlue;
        }

        private void btnAgregar_MouseMove(object sender, MouseEventArgs e)
        {
            btnAgregar.BackColor = Color.DimGray;
        }

        private void btnAgregar_MouseLeave(object sender, EventArgs e)
        {
            btnAgregar.BackColor = Color.DodgerBlue;
        }

        private void btnModificarA_MouseMove(object sender, MouseEventArgs e)
        {
            btnModificarA.BackColor = Color.DimGray;
        }

        private void btnModificarA_MouseLeave(object sender, EventArgs e)
        {
            btnModificarA.BackColor = Color.DodgerBlue;
        }

        private void btnCancelar_MouseMove(object sender, MouseEventArgs e)
        {
            btnCancelar.BackColor = Color.DimGray;
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.BackColor = Color.DodgerBlue;
        }
      
    }
}
