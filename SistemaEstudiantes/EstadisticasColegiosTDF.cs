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
        int colorAgregar;
        int colorEliminar;

        OleDbCommand sqlComando;
        OleDbConnection conexionBaseDatos;
        string nombreUsuario;
        string tipoUsuario;
        bool logueadoUsuario;

        int column; //columna            
        int row; //fila
        int idColegioEliminar;

        //string[,] ushuaiaColegios = new string[3, 25];//nombre,posicion,nombreAbreviado de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
        //string[,] grandeColegios = new string[3, 25];//nombre,posicion,nombreAbreviado de los colegios de rio grande
        //int numColegiosUshuaia;
        //int numColegiosGrande;
        public EstadisticasColegiosTDF(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;
            ordenar();
        }        
        
        private void ordenar()
        {
            cboxDepto.ResetText();//Reinicia el texto seleccionado
            tbxNombre.ResetText();
            tbxAbrevia.ResetText();
            cbxNOrden.ResetText();           

            cboxDepto.Enabled = true;
            tbxNombre.Enabled = false;
            btnOkNombre.Enabled = false;
            tbxAbrevia.Enabled = false;
            btnOkAbrevia.Enabled = false;
            cbxNOrden.Enabled = false;

            btnCrearColegios.Enabled = false;

            btnCrearColegios.BackColor = System.Drawing.Color.Silver;

            // btnRefresh.Enabled = false;

            //eliminar
            btnEliminarColegiosUsh.Enabled = false;
            btnEliminarColegiosGrande.Enabled = false;
            btnEliminarColegiosUsh.Visible= true;
            btnEliminarColegiosGrande.Visible = true;
            tbxColegioEliminar.Clear();

            tbxColegioEliminar.Enabled = false;
            btnEliminarColegiosGrande.BackColor = System.Drawing.Color.Silver;
            btnEliminarColegiosUsh.BackColor = System.Drawing.Color.Silver;
            myDGVUshuaia.DataSource = null;//reinicia datagv
            myDGVUshuaia.Rows.Clear();
            myDGVUshuaia.Refresh();

            cargarDatasGView();
        }
        private void cargarDatasGView()
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT *FROM ColegiosUshuaia";
            //string queryCargarBD = "SELECT Numero Orden, Nombre, Nombre Abreviado FROM ColegiosUshuaia ";
            OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
            miDataAdapter.Fill(miDataTable);
            miDataTable.DefaultView.Sort = "NumeroOrden";
            myDGVUshuaia.DataSource = miDataTable;

            DataTable miDataTableRG = new DataTable();

            string queryCargarBDRG = "SELECT *FROM ColegiosGrande";           
            OleDbCommand sqlComandoRG = new OleDbCommand(queryCargarBDRG, conexionBaseDatos);

            OleDbDataAdapter miDataAdapterRG = new OleDbDataAdapter(sqlComandoRG);
            miDataAdapterRG.Fill(miDataTableRG);
            miDataTable.DefaultView.Sort = "NumeroOrden";
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
            btnCrearColegios.BackColor = System.Drawing.Color.DodgerBlue;
            colorAgregar = 1;
        }
        private void myDGVUshuaia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1) //Evita que se haga click en la fila -1 y columna -1       
            {
                string colegioEliminar;
                column = e.ColumnIndex; //columna            
                row = e.RowIndex; //fila

                //Rellena los tbx 
                colegioEliminar = Convert.ToString(myDGVUshuaia.Rows[e.RowIndex].Cells[0].Value);//necesario para eliminar el error de nulo
                tbxColegioEliminar.Text = Convert.ToString(myDGVUshuaia.Rows[e.RowIndex].Cells[1].Value);
                if (Convert.ToString(myDGVUshuaia.Rows[e.RowIndex].Cells[1].Value) == (""))//comprueba si es nulo
                {
                    btnEliminarColegiosGrande.Visible = false;
                    btnEliminarColegiosUsh.Visible = true;
                    btnEliminarColegiosUsh.BackColor = System.Drawing.Color.Silver;
                    btnEliminarColegiosUsh.Enabled = false;
                }
                else 
                {
                    colegioEliminar = Convert.ToString(myDGVUshuaia.Rows[e.RowIndex].Cells[0].Value);
                    idColegioEliminar = Convert.ToInt32(colegioEliminar);
                    btnEliminarColegiosUsh.Enabled = true;
                    btnEliminarColegiosGrande.Enabled = false;
                    btnEliminarColegiosUsh.Visible = true;
                    btnEliminarColegiosGrande.Visible = false;
                    colorEliminar = 1;
                    btnEliminarColegiosUsh.BackColor = System.Drawing.Color.DodgerBlue;
                }
            }
        }
        private void myDGVGrande_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1) //Evita que se haga click en la fila -1 y columna -1       
            {
                string colegioEliminar;
                column = e.ColumnIndex; //columna            
                row = e.RowIndex; //fila

                //Rellena los tbx 
                colegioEliminar = Convert.ToString(myDGVGrande.Rows[e.RowIndex].Cells[0].Value);//necesario para eliminar el error de nulo
                tbxColegioEliminar.Text = Convert.ToString(myDGVGrande.Rows[e.RowIndex].Cells[1].Value);
                if (Convert.ToString(myDGVGrande.Rows[e.RowIndex].Cells[1].Value) == (""))//comprueba si es nulo
                {
                    btnEliminarColegiosUsh.Visible= false;
                    btnEliminarColegiosGrande.Enabled = false;
                    btnEliminarColegiosGrande.BackColor = System.Drawing.Color.Silver;
                    btnEliminarColegiosGrande.Visible = true;
                }
                else
                {
                    
                    colegioEliminar = Convert.ToString(myDGVGrande.Rows[e.RowIndex].Cells[0].Value);
                    idColegioEliminar = Convert.ToInt32(colegioEliminar);
                    
                    btnEliminarColegiosUsh.Enabled = false;
                    btnEliminarColegiosUsh.Visible = false;
                    btnEliminarColegiosGrande.Visible = true;
                    btnEliminarColegiosGrande.Enabled = true;
                    btnEliminarColegiosGrande.BackColor = System.Drawing.Color.DodgerBlue;
                    btnEliminarColegiosGrande.Refresh();
                    colorEliminar = 1;
                    
                }
            }
        }

        private void btnCrearColegios_Click(object sender, EventArgs e)
        {
            colorAgregar = 0; //color 0 es igual a silver, sirve para que no se bugueeen azul\
            if (cboxDepto.SelectedItem == "Ushuaia")
            {
                try
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
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Los cambios solicitados en la tabla no se realizaron correctamente porque crearían valores duplicados"))
                    {
                        MessageBox.Show("El número de orden que está queriendo utilizar ya se encuentra ocupado. Por favor elija otro que este libre.", "Sistema Informa");
                    }
                    else
                    {
                        MessageBox.Show(Convert.ToString(ex));
                    }
                    conexionBaseDatos.Close();
                }
                btnCrearColegios.BackColor = Color.Silver;
                btnCrearColegios.Enabled = false;
            }
            else if (cboxDepto.SelectedItem == "Rio Grande")
            {
                try
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
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Los cambios solicitados en la tabla no se realizaron correctamente porque crearían valores duplicados"))
                    {
                        MessageBox.Show("El número de orden que está queriendo utilizar ya se encuentra ocupado. Por favor elija otro que este libre.", "Sistema Informa");                       
                        
                    }
                    else
                    {
                        MessageBox.Show(Convert.ToString(ex));
                    }
                    conexionBaseDatos.Close();
                }
                btnCrearColegios.BackColor = Color.Silver;
                btnCrearColegios.Enabled = false;                
            }
        }      
        private void btnEliminarColegiosUsh_Click(object sender, EventArgs e)
        {
            
            DialogResult preguntaEliminar = MessageBox.Show("Desea eliminar el colegio seleccionado?", "Sistema Informa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (preguntaEliminar == DialogResult.OK)
            {
                string queryEliminar = "DELETE *FROM ColegiosUshuaia WHERE NumeroOrden = @idEliminar";

                sqlComando = new OleDbCommand(queryEliminar, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idEliminar", idColegioEliminar);

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Se elimino el colegio correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Colegio no eliminado. Se produzco un error.");
                    }
                    conexionBaseDatos.Close();
                    cargarDatasGView();
                    ordenar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex.HResult));
                    conexionBaseDatos.Close();
                    ordenar();
                }
            }
            else
            {
                ordenar();
            }
        }
        private void btnEliminarColegiosGrande_Click(object sender, EventArgs e)
        {
            DialogResult preguntaEliminar = MessageBox.Show("Desea eliminar el colegio seleccionado?", "Sistema Informa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (preguntaEliminar == DialogResult.OK)
            {
                string queryEliminar = "DELETE *FROM ColegiosGrande WHERE NumeroOrden = @idEliminar";

                sqlComando = new OleDbCommand(queryEliminar, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idEliminar", idColegioEliminar);

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Se elimino el colegio correctamente");
                    }
                    else
                    {
                        MessageBox.Show("Colegio no eliminado. Se produzco un error.");
                    }
                    conexionBaseDatos.Close();
                    cargarDatasGView();
                    ordenar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex.HResult));
                    conexionBaseDatos.Close();
                    ordenar();
                }
            }
            else
            {
                ordenar();
            }
        }
        private void btnAtras_Click(object sender, EventArgs e)
        {
            Estadisticas myEstadisticas = new Estadisticas(nombreUsuario, tipoUsuario, logueadoUsuario, conexionBaseDatos);
            this.Hide();
            myEstadisticas.Show();           
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }       

        private void btnRefresh_MouseMove(object sender, MouseEventArgs e)
        {
            btnRefresh.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void btnCrearColegios_MouseMove(object sender, MouseEventArgs e)
        {
            btnCrearColegios.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnCrearColegios_MouseLeave(object sender, EventArgs e)
        {
            if (colorAgregar == 0)//unica forma de que funcione
            {
                btnCrearColegios.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorAgregar == 1)
            {
                btnCrearColegios.BackColor = System.Drawing.Color.DodgerBlue;
            }           
        }

        private void btnEliminarColegiosUsh_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminarColegiosUsh.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnEliminarColegiosUsh_MouseLeave(object sender, EventArgs e)
        {
            if (colorEliminar == 0)//unica forma de que funcione
            {
                btnEliminarColegiosUsh.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorEliminar == 1)
            {
                btnEliminarColegiosUsh.BackColor = System.Drawing.Color.DodgerBlue;
            }
        }

        private void btnEliminarColegiosGrande_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminarColegiosGrande.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnEliminarColegiosGrande_MouseLeave(object sender, EventArgs e)
        {
            if (colorEliminar == 0)//unica forma de que funcione
            {
                btnEliminarColegiosGrande.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorEliminar == 1)
            {
                btnEliminarColegiosGrande.BackColor = System.Drawing.Color.DodgerBlue;
            }            
        }

        private void btnAtras_MouseMove(object sender, MouseEventArgs e)
        {
            btnAtras.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnAtras_MouseLeave(object sender, EventArgs e)
        {
            btnAtras.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void btnSalir_MouseMove(object sender, MouseEventArgs e)
        {
            btnSalir.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnSalir_MouseLeave(object sender, EventArgs e)
        {
            btnSalir.BackColor = System.Drawing.Color.DodgerBlue;
        }        
    }      
}
