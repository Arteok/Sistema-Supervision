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
    public partial class EstadisticasColegiosTDF : Form
    {
        OleDbConnection conexionBaseDatos;
        string nombreUsuario;
        string permisosUsuario;
        bool logueadoUsuario;

        int column; //columna            
        int row; //fila
        public EstadisticasColegiosTDF(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;

            ordenar();
        }        
        
        private void ordenar()
        {
            cboxDepto.ResetText();//Reinicia el texto seleccionado
            tbxNombre.ResetText();//Reinicia el texto seleccionado
            tbxAbrevia.ResetText();//Reinicia el texto seleccionado
            cbxNOrden.ResetText();//Reinicia el texto seleccionado

            cboxDepto.Enabled = true;
            tbxNombre.Enabled = false;
            btnOkNombre.Enabled = false;
            tbxAbrevia.Enabled = false;
            btnOkAbrevia.Enabled = false;
            cbxNOrden.Enabled = false;

            btnCrearColegios.Enabled = false;
            btnRefresh.Enabled = false;

            myDGVUshuaia.DataSource = null;//reinicia datagv
            myDGVUshuaia.Rows.Clear();
            myDGVUshuaia.Refresh();

            cargarDatasGW();
        }
        private void cargarDatasGW()
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT *FROM ColegiosUshuaia";
            //string queryCargarBD = "SELECT Numero Orden, Nombre, Nombre Abreviado FROM ColegiosUshuaia ";
            OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
            miDataAdapter.Fill(miDataTable);
            myDGVUshuaia.DataSource = miDataTable;

            DataTable miDataTableRG = new DataTable();

            string queryCargarBDRG = "SELECT *FROM ColegiosGrande";
            //string queryCargarBD = "SELECT Numero Orden, Nombre, Nombre Abreviado FROM ColegiosUshuaia ";
            OleDbCommand sqlComandoRG = new OleDbCommand(queryCargarBDRG, conexionBaseDatos);

            OleDbDataAdapter miDataAdapterRG = new OleDbDataAdapter(sqlComandoRG);
            miDataAdapterRG.Fill(miDataTableRG);
            myDGVGrande.DataSource = miDataTableRG;

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ordenar();
        }

        private void cboxDepto_TextChanged(object sender, EventArgs e)
        {
            cboxDepto.Enabled = false;
            cbxNOrden.Enabled = true;
            btnRefresh.Enabled = true;
        }

        private void cbxNOrden_TextChanged(object sender, EventArgs e)
        {
            cbxNOrden.Enabled = false;
            tbxNombre.Enabled = true;
            btnOkNombre.Enabled = true;
        }
        private void btnOkNombre_Click(object sender, EventArgs e)
        {
            tbxNombre.Enabled = false;
            btnOkNombre.Enabled = false;
            tbxAbrevia.Enabled = true;
            btnOkAbrevia.Enabled = true;
        }

        private void btnOkAbrevia_Click(object sender, EventArgs e)
        {
            tbxAbrevia.Enabled = false;
            btnOkAbrevia.Enabled = false;
            btnCrearColegios.Enabled = true;
        }

        private void btnCrearColegios_Click(object sender, EventArgs e)
        {
            if (cboxDepto.SelectedItem == "Ushuaia")
            {
                string queryAgregar = "INSERT INTO ColegiosUshuaia VALUES ( @NumeroOrden, @Nombre, @NombreAbreviado)";

                OleDbCommand sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);

                sqlComando.Parameters.AddWithValue("@NumeroOrden", cbxNOrden.SelectedItem);
                sqlComando.Parameters.AddWithValue("@Nombre", tbxNombre.Text);
                sqlComando.Parameters.AddWithValue("@NombreAbreviado", tbxAbrevia.Text);


                conexionBaseDatos.Open();
                if (sqlComando.ExecuteNonQuery() > 0)
                {
                    ordenar();
                    //seImporto = true;
                }
                conexionBaseDatos.Close();
            }
            else if (cboxDepto.SelectedItem == "Rio Grande")
            {
                string queryAgregar = "INSERT INTO ColegiosGrande VALUES ( @NumeroOrden, @Nombre, @NombreAbreviado)";

                OleDbCommand sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);

                sqlComando.Parameters.AddWithValue("@NumeroOrden", cbxNOrden.SelectedItem);
                sqlComando.Parameters.AddWithValue("@Nombre", tbxNombre.Text);
                sqlComando.Parameters.AddWithValue("@NombreAbreviado", tbxAbrevia.Text);


                conexionBaseDatos.Open();
                if (sqlComando.ExecuteNonQuery() > 0)
                {
                    ordenar();
                    //seImporto = true;
                }
                conexionBaseDatos.Close();
            }
        }
        private void btnAtras_Click(object sender, EventArgs e)
        {
            Form1 miForm1 = new Form1(nombreUsuario, permisosUsuario, true);
            miForm1.Visible = true;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void myDGVUshuaia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1) //Evita que se haga click en la fila -1 y columna -1       
            {
                column = e.ColumnIndex; //columna            
                row = e.RowIndex; //fila

                //Rellena los tbx 
                tbxColegioEliminar.Text = Convert.ToString(myDGVUshuaia.Rows[e.RowIndex].Cells[1].Value);
                
                
                /*
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
                }*/

            }
        }
    }   
    
}
