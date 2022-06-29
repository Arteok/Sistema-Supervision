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
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

namespace SistemaEstudiantes
{
    public partial class EstadisticasCargarPoli : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        OleDbCommand sqlComando;
        string ruta;

        string nombreUsuario;
        string tipoUsuario;
        bool logueadoUsuario;
         
        string año;
        string periodo;
        string depto;
        string colegio;
        string colegioIngre;
        string idUnico;
        string abreColegio;
        string numOrden;
        bool plantillaRepetida;
        bool tryCatch;
        string fecha;


        public EstadisticasCargarPoli(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;

            idUnico = "";           

            dataGridView1.Visible = false;//para que nunca se vea el dgv que sirve para comprobar si ya fue ingresada una planilla
            ordenar();
        }
        private void ordenar()
        {
            ruta = "";

            plantillaRepetida = false;

            abreColegio = "";
            colegio = "";
            numOrden = "";

            cboxAño.SelectedIndex = -1;
            cboxPeriodo.SelectedIndex = -1;
            cboxDepto.SelectedIndex = -1;
            
            cboxColegiosUshuaia.ResetText();
            cboxColegiosGrande.ResetText();
            btnSelecExcel.Enabled = false;
            btnImportar.Enabled = false;
            tbxColegioSel.Clear();

            lblIngresando.Visible = false;
            lblCargando.Visible = false;

            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = false;
            cboxColegiosUshuaia.Enabled = false;
            cboxColegiosGrande.Enabled = false;
            tbxColegioSel.Enabled = false;

            btnSelecExcel.Enabled = false;
            btnImportar.Enabled = false;
            btnSelecExcel.BackColor = System.Drawing.Color.Silver;
            btnImportar.BackColor = System.Drawing.Color.Silver;


            cboxColegiosUshuaia.Visible = true;
            cboxColegiosGrande.Visible = true;

            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();

            dataGridView1.DataSource = null;//reinicia datagv
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ordenar();
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
            abreColegio = "PBust";
            numOrden = "9";
            colegio = cboxColegiosUshuaia.SelectedItem.ToString();

            cboxColegiosUshuaia.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnSelecExcel.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void cboxColegiosGrande_SelectedIndexChanged(object sender, EventArgs e)
        {
            abreColegio = "PCot";
            numOrden = "12";
            colegio = cboxColegiosGrande.SelectedItem.ToString();

            cboxColegiosGrande.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnSelecExcel.BackColor = System.Drawing.Color.DodgerBlue;

        }

        private void btnSelecExcel_Click(object sender, EventArgs e)
        {
            try
            {
                btnRefresh.Enabled = true;
                DataTable myDataTableColegios = new DataTable();

                myDataTableColegios.Columns.Add(new DataColumn("Sección", typeof(int)));
                myDataTableColegios.Columns.Add(new DataColumn("División", typeof(string)));
                myDataTableColegios.Columns.Add(new DataColumn("Turno", typeof(string)));
                myDataTableColegios.Columns.Add(new DataColumn("Orientacion", typeof(string)));
                myDataTableColegios.Columns.Add(new DataColumn("Perfil", typeof(string)));
                myDataTableColegios.Columns.Add(new DataColumn("Horas", typeof(int)));
                myDataTableColegios.Columns.Add(new DataColumn("Pedagógica", typeof(string)));
                myDataTableColegios.Columns.Add(new DataColumn("Presupuestaria", typeof(string)));
                myDataTableColegios.Columns.Add(new DataColumn("Matrícula", typeof(int)));

                //para seleccionarlo manualmente
                string nombreHoja = "Sheet1";                

                OpenFileDialog myOpenFileDialog = new OpenFileDialog();
                myOpenFileDialog.Filter = "Excel Files |* .xlsx";
                myOpenFileDialog.Title = "Seleccione el archivo Excel";

                if (myOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (myOpenFileDialog.FileName.Equals("") == false)
                    {
                        ruta = myOpenFileDialog.FileName;
                    }

                }
                lblIngresando.Visible = true;
                lblIngresando.Refresh();
                
                //llena el dgv con el contenido del dataset
                int row;

                using (SLDocument sl = new SLDocument(ruta, nombreHoja))//crear un document con el excel seleccionado
                {
                    SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
                    int iStartColumnIndex = stats.StartColumnIndex;

                    for (row = 8; row <= stats.EndRowIndex; ++row)//llena el date table con el documento
                    {
                        myDataTableColegios.Rows.Add(
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 1),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 2),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 3),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 4),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 5),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 6),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 7),
                            sl.GetCellValueAsString(row, iStartColumnIndex + 8),
                            sl.GetCellValueAsInt32(row, iStartColumnIndex + 9)
                            );
                    }
                    tbxColegioSel.Text = sl.GetCellValueAsString(5, 5);
                    fecha = sl.GetCellValueAsString(7, 10);
                }
                myDataGridView.DataSource = myDataTableColegios;//llena el dataGV con data table
                /*revisar si ya esta ingresada la plantilla*/

                año = cboxAño.SelectedItem.ToString();
                periodo = cboxPeriodo.SelectedItem.ToString();
                idUnico = año;//Se calcula sumando 3 variables
                if (periodo == "Marzo")
                {
                    idUnico = idUnico + "M";
                }
                else
                {
                    idUnico = idUnico + "S";
                }
                idUnico = idUnico + abreColegio;//termina aca sumando la ultima parte

                DataTable miDataTable = new DataTable();
                
                string queryCargarBD = "SELECT IdUnico FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
                OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                miDataAdapter.Fill(miDataTable);
                dataGridView1.DataSource = miDataTable;

                if ((Convert.ToString(dataGridView1.Rows[0].Cells[0].Value) == idUnico))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                {
                    plantillaRepetida = true;

                    //MessageBox.Show(Convert.ToString(myDataGridView.Rows[0].Cells[0].Value), "Sistema Informa");                        
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                {
                    MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                }
                else if (ex.Message.Contains("No se puede dejar vacío el nombre de la ruta de acceso"))
                {
                    MessageBox.Show("No se seleccionó ningún archivo.", "Sistema Informa");
                    ordenar();
                }
                else
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
                tryCatch = true;
            }

            if (plantillaRepetida == true)
            {
                MessageBox.Show("Esta plantilla ya está cargada y no puede volver a ser ingresada. Revise si la está cargando correctamente.", "Sistema Informa");
                ordenar();
            }
            else if ((plantillaRepetida == false) && (tryCatch == true))//si tiro algun error
            {
                tryCatch = false;
            }
            else//si se cargo normalmente
            {
                btnSelecExcel.BackColor = System.Drawing.Color.Silver;
                btnSelecExcel.Enabled = false;
                btnImportar.Enabled = true;
                btnImportar.BackColor = System.Drawing.Color.DodgerBlue;
            }
            lblIngresando.Visible = false;            
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {            
            lblCargando.Visible = true;
            lblCargando.Refresh();          

            bool seImporto = false;

            año = cboxAño.SelectedItem.ToString();
            periodo = cboxPeriodo.SelectedItem.ToString();
            depto = cboxDepto.SelectedItem.ToString();

            colegioIngre = tbxColegioSel.Text;
            idUnico = año;//Se calcula sumando 3 variables
            if (periodo == "Marzo")
            {
                idUnico = idUnico + "M";
            }
            else
            {
                idUnico = idUnico + "S";
            }
            idUnico = idUnico + abreColegio;//termina aca sumando la ultima parte           
            

            for (int i = 0; Convert.ToInt32(myDataGridView.Rows[i].Cells[0].Value) > 0; i++)   //revisa en la primer columna que en la celda el valor sea mayor a 0              
            {
                string queryAgregar = "INSERT INTO PlanillasxOrientacion VALUES (@Año, @Periodo, @Depto, @ColegioSelect, @Sección, @División, " +
                    "@Turno, @Orientación, @Perfil, @Horas, @Pedagógica, @Presupuestaria, @Matriculas, @ColegioIngre, @IdUnico, @Fecha, @NumOrdenColegio, @RutaPlanilla)";
                sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);

                sqlComando.Parameters.AddWithValue("@Año", año);
                sqlComando.Parameters.AddWithValue("@Periodo", periodo);
                sqlComando.Parameters.AddWithValue("@Depto", depto);
                sqlComando.Parameters.AddWithValue("@ColegioSelect", colegio);
                sqlComando.Parameters.AddWithValue("@Sección", myDataGridView.Rows[i].Cells[0].Value);
                sqlComando.Parameters.AddWithValue("@División", myDataGridView.Rows[i].Cells[1].Value);
                sqlComando.Parameters.AddWithValue("@Turno", myDataGridView.Rows[i].Cells[2].Value);
                sqlComando.Parameters.AddWithValue("@Orientación", myDataGridView.Rows[i].Cells[3].Value);
                sqlComando.Parameters.AddWithValue("@Perfil", myDataGridView.Rows[i].Cells[4].Value);
                sqlComando.Parameters.AddWithValue("@Horas", myDataGridView.Rows[i].Cells[5].Value);
                sqlComando.Parameters.AddWithValue("@Pedagógica", myDataGridView.Rows[i].Cells[6].Value);
                sqlComando.Parameters.AddWithValue("@Presupuestaria", myDataGridView.Rows[i].Cells[7].Value);
                sqlComando.Parameters.AddWithValue("@Matriculas", myDataGridView.Rows[i].Cells[8].Value);

                sqlComando.Parameters.AddWithValue("@ColegioIngre", colegioIngre);

                sqlComando.Parameters.AddWithValue("@IdUnico", idUnico);
                sqlComando.Parameters.AddWithValue("@Fecha", fecha);
                sqlComando.Parameters.AddWithValue("@NumOrdenColegio", numOrden);
                sqlComando.Parameters.AddWithValue("@RutaPlanilla", ruta);

                try
                {
                    conexionBaseDatos.Open();
                    if (sqlComando.ExecuteNonQuery() > 0)
                    {
                        seImporto = true;
                    }
                    conexionBaseDatos.Close();

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                    {
                        MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                    }
                    else
                    {
                        MessageBox.Show(Convert.ToString(ex));
                    }
                    conexionBaseDatos.Close();
                }
            }

            if (seImporto == true)
            {
                MessageBox.Show("Se agrego la planilla correctamente.", "Sistema Informa");
                btnImportar.BackColor = System.Drawing.Color.Silver;
                btnImportar.Enabled = false;
                ordenar();
            }            
            lblCargando.Visible = false;
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
        private void btnSelecExcel_MouseMove(object sender, MouseEventArgs e)
        {
            btnSelecExcel.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnSelecExcel_MouseLeave(object sender, EventArgs e)
        {
            btnSelecExcel.BackColor = System.Drawing.Color.DodgerBlue;
        }

        private void btnImportar_MouseMove(object sender, MouseEventArgs e)
        {
            btnImportar.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnImportar_MouseLeave(object sender, EventArgs e)
        {
            btnImportar.BackColor = System.Drawing.Color.DodgerBlue;
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
