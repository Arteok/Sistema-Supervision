using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaEstudiantes
{
    public partial class EstadisticasEliminar : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        Colegios myColegios;
        bool colegiosCreados = false;
        int cantColegiosUshuaia;
        int cantColegiosGrande;

        string nombreUsuario;
        string tipoUsuario;
        bool logueadoUsuario;

        string idUnico;
        string abreColegio;

       

        string[,] ushuaiaColegios;//nombre,nombreAbreviado, posicion de los colegios de ushuaia ##tomo como cantidad maxima la cantidad de colegios desde colegios tdf
        string[,] grandeColegios;//nombre,nombreAbreviado, posicion de los colegios de rio grande

        public EstadisticasEliminar(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;
            
            idUnico = "";      


            if (colegiosCreados == false)
            {
                myColegios = new Colegios();
                myColegios.CargarColegiosUshuaia();
                myColegios.CargarColegiosGrande();
                cantColegiosUshuaia = myColegios.NumColegiosUshuaia;
                cantColegiosGrande = myColegios.NumColegiosGrande;

                ushuaiaColegios = new string[3, cantColegiosUshuaia];//nombre,posicion,nombreAbreviado de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
                grandeColegios = new string[3, cantColegiosGrande];//nombre,posicion,nombreAbreviado de los colegios de rio grande

                NombreColegios();
                ComboBoxUshuaia();
                ComboBoxGrande();

                colegiosCreados = true;
            }

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
            btnRefresh.Enabled = false;
            btnEliminar.Enabled = false;
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
        private void NombreColegios()
        {
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= cantColegiosUshuaia - 1; j++)
                {
                    ushuaiaColegios[i, j] = myColegios.UshuaiaColegios[i, j];
                }
            }
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= cantColegiosGrande - 1; j++)
                {
                    grandeColegios[i, j] = myColegios.GrandeColegios[i, j];
                }
            }
        }
        private void ComboBoxUshuaia()//cargo los nombres en los comboBox desde colegios tdf
        {
            for (int i = 0; i <= cantColegiosUshuaia - 1; i++)
            {
                cboxColegiosUshuaia.Items.Add(ushuaiaColegios[0, i]);
            }
        }
        private void ComboBoxGrande()//cargo los nombres en los comboBox desde colegios tdf
        {
            for (int i = 0; i <= cantColegiosGrande - 1; i++)
            {
                cboxColegiosGrande.Items.Add(grandeColegios[0, i]);
            }
        }
        private void cboxAño_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxAño.Enabled = false;
            cboxPeriodo.Enabled = true;
            btnRefresh.Enabled = true;
        }

        private void cboxPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = true;
            btnRefresh.Enabled = true;
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
            //string año = cboxAño.SelectedItem.ToString();
            idUnico = cboxAño.SelectedItem.ToString();//Se calcula sumando 3 variables
            abreColegio = ushuaiaColegios[1, cboxColegiosUshuaia.SelectedIndex];
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
            btnRefresh.Enabled = true;
            btnEliminar.Enabled = true;

            try
            {
                DataTable miDataTable = new DataTable();

                // string queryCargarBD = "SELECT Año, Periodo, Departamento, Colegio, Sección, División, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE Año LIKE  '%' + @Buscar + '%'";
                //string queryCargarBD = "SELECT *FROM Planilla ";
                string queryCargarBD = "SELECT Año, Periodo, Departamento, ColegioSelect, ColegioIngresado, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE IdUnico = @Buscar";
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
            abreColegio = grandeColegios[1, cboxColegiosGrande.SelectedIndex];
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
            btnRefresh.Enabled = true;
            btnEliminar.Enabled = true;

            try
            {
                DataTable miDataTable = new DataTable();

                // string queryCargarBD = "SELECT Año, Periodo, Departamento, Colegio, Sección, División, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE Año LIKE  '%' + @Buscar + '%'";
                //string queryCargarBD = "SELECT *FROM Planilla ";
                string queryCargarBD = "SELECT Año, Periodo, Departamento, ColegioSelect, ColegioIngresado, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE IdUnico = @Buscar";
                OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                miDataAdapter.Fill(miDataTable);
                myDataGridView.DataSource = miDataTable;
                myDataGridView.Sort(myDataGridView.Columns[5], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                myDataGridView.Sort(myDataGridView.Columns[4], ListSortDirection.Ascending);


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
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DataTable miDataTable = new DataTable();

            string queryEliminar = "DELETE *FROM Planilla WHERE IdUnico = @idEliminar";
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
    }

   

}
