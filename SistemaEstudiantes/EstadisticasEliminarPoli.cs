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
    public partial class EstadisticasEliminarPoli : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        bool logueadoUsuario;

        string idUnico;
        string abreColegio;
        public EstadisticasEliminarPoli(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;

            idUnico = "";
            ordenar();
        }
        private void ordenar()
        {
            cboxAño.SelectedIndex = -1;//reinicia el texto seleccionado pero puede traer problema
            cboxPeriodo.ResetText();//Reinicia el texto seleccionado, es la mejor orma de hacerlo
            cboxDepto.ResetText();
            cboxColegiosUshuaia.ResetText();
            cboxColegiosGrande.ResetText();

            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = false;
            
            btnEliminar.Enabled = false;
            btnEliminar.BackColor = System.Drawing.Color.Silver;

            cboxColegiosUshuaia.Enabled = false;
            cboxColegiosGrande.Enabled = false;
            cboxColegiosUshuaia.Visible = true;
            cboxColegiosGrande.Visible = true;

            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ordenar();
        }
        private void cboxAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxAño.Enabled = false;
            cboxPeriodo.Enabled = true;            
        }

        private void cboxPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = true;            
        }

        private void cboxDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxDepto.Enabled = false;
            btnRefresh.Enabled = true;
            if (cboxDepto.SelectedIndex == 0)
            {
                cboxColegiosUshuaia.Enabled = true;
                cboxColegiosGrande.Visible = false;
            }
            else if (cboxDepto.SelectedIndex == 1)
            {
                cboxColegiosGrande.Enabled = true;
                cboxColegiosUshuaia.Visible = false;
            }
        }    

        private void cboxColegiosUshuaia_SelectedIndexChanged(object sender, EventArgs e)
        {
            idUnico = cboxAño.SelectedItem.ToString();//Se calcula sumando 3 variables
            abreColegio = "PBust";
            if (cboxPeriodo.SelectedItem.ToString() == "Marzo")
            {
                idUnico = idUnico + "M";
            }
            else
            {
                idUnico = idUnico + "S";
            }
            idUnico = idUnico + abreColegio;//termina aca sumando la ultima parte  

            cboxColegiosUshuaia.Enabled = false;
            btnEliminar.Enabled = true;
            btnEliminar.BackColor = System.Drawing.Color.DodgerBlue;

            try
            {
                DataTable miDataTable = new DataTable();
                
                string queryCargarBD = "SELECT  ColegioIngresado, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
                OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                miDataAdapter.Fill(miDataTable);
                myDataGridView.DataSource = miDataTable;
                myDataGridView.Sort(myDataGridView.Columns[5], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                myDataGridView.Sort(myDataGridView.Columns[4], ListSortDirection.Ascending);


                if ((Convert.ToString(myDataGridView.Rows[0].Cells[0].Value) == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                {
                    MessageBox.Show("No se encontró ninguna plantilla cargada del colegio elegido, para ese año y periodo.", "Sistema Informa");
                    ordenar();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                }
            }
        }

        private void cboxColegiosGrande_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string año = cboxAño.SelectedItem.ToString();
            idUnico = cboxAño.SelectedItem.ToString();//Se calcula sumando 3 variables
            abreColegio = "PCot";
            if (cboxPeriodo.SelectedItem.ToString() == "Marzo")
            {
                idUnico = idUnico + "M";
            }
            else
            {
                idUnico = idUnico + "S";
            }
            idUnico = idUnico + abreColegio;//termina aca sumando la ultima parte  

            cboxColegiosGrande.Enabled = false;            
            btnEliminar.Enabled = true;
            btnEliminar.BackColor = System.Drawing.Color.DodgerBlue;

            try
            {
                DataTable miDataTable = new DataTable();
                
                string queryCargarBD = "SELECT  ColegioIngresado, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
                OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                miDataAdapter.Fill(miDataTable);
                myDataGridView.DataSource = miDataTable;                


                if ((Convert.ToString(myDataGridView.Rows[0].Cells[0].Value) == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                {
                    MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");
                    ordenar();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                }
                else
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataTable miDataTable = new DataTable();

            string queryEliminar = "DELETE *FROM PlanillasxOrientacion WHERE IdUnico = @idEliminar";
            OleDbCommand sqlComando = new OleDbCommand(queryEliminar, conexionBaseDatos);
            sqlComando.Parameters.AddWithValue("@idEliminar", idUnico);

            try
            {
                conexionBaseDatos.Open();
                if (sqlComando.ExecuteNonQuery() > 0)
                {
                    myDataGridView.DataSource = null;//reinicia datagv
                    myDataGridView.Rows.Clear();
                    myDataGridView.Refresh();
                    MessageBox.Show("Se elimino la planilla correctamente.", "Sistema Informa");
                }
                else
                {
                    MessageBox.Show("La planilla no fue eliminada, se produzco un error.", "Sistema Informa");
                }
                ordenar();
                conexionBaseDatos.Close();
                myDataGridView.DataSource = miDataTable;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                    conexionBaseDatos.Close();
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            EstadisticasPlanillas myEstadisticasPlanillas = new EstadisticasPlanillas(nombreUsuario, tipoUsuario, logueadoUsuario, conexionBaseDatos);
            this.Hide();
            myEstadisticasPlanillas.Show();
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

        private void btnEliminar_MouseMove(object sender, MouseEventArgs e)
        {
            btnEliminar.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnEliminar_MouseLeave(object sender, EventArgs e)
        {
            btnEliminar.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void btnVolver_MouseMove(object sender, MouseEventArgs e)
        {
            btnVolver.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnVolver_MouseLeave(object sender, EventArgs e)
        {
            btnVolver.BackColor = System.Drawing.Color.DodgerBlue;
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
