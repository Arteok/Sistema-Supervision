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
    public partial class EstadisticasCargar : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        OleDbCommand sqlComando;
        Colegios myColegios;
        string ruta;

        int colorBtnExcel;
        int colorImportar;

        bool colegiosCreados = false;
        int cantColegiosUshuaia;
        int cantColegiosGrande;

        string nombreUsuario;
        string tipoUsuario;
        bool logueadoUsuario;

        string abreColegio;
        bool plantillaIngresada;
        bool planillaSelec;
        string numOrden;
        string colegio;        
        
        string año;
        string periodo;
        string depto;
        string colegioIngre;
        string fecha;
        string idUnico;
        string idNum;

        string[,] ushuaiaColegios;//nombre,nombreAbreviado, posicion de los colegios de ushuaia ##tomo como cantidad maxima la cantidad de colegios desde colegios tdf
        string[,] grandeColegios;//nombre,nombreAbreviado, posicion de los colegios de rio grande

        public EstadisticasCargar(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;
            

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
            dataGridView1.Visible = false;//para que nunca se vea el dgv que sirve para comprobar si ya fue ingresada una planilla
            ordenar();
        }
        private void ordenar()
        {
            ruta = "";

            plantillaIngresada = false;
            planillaSelec = true;

            abreColegio = "";
            colegio = "";            
            numOrden = "";
            fecha = "";

            cboxAño.ResetText();
            cboxPeriodo.ResetText();
            cboxDepto.ResetText();
            
            cboxColegiosUshuaia.ResetText();
            cboxColegiosGrande.ResetText();
            btnSelecExcel.Enabled = false;
            btnImportar.Enabled = false;
            tbxColegioSel.Clear();

            btnSelecExcel.BackColor = System.Drawing.Color.Silver;
            btnImportar.BackColor = System.Drawing.Color.Silver;
            lblIngresando.Visible = false;
            lblCargando.Visible = false;

            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = false;
            cboxColegiosUshuaia.Enabled = false;
            cboxColegiosGrande.Enabled = false;
            btnSelecExcel.Enabled = false;
            btnImportar.Enabled = false;
            tbxColegioSel.Enabled = false;
           
            cboxColegiosUshuaia.Visible = true;
            cboxColegiosGrande.Visible = true;

            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();

            dataGridView1.DataSource = null;//reinicia datagv
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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
                cboxColegiosUshuaia.Items.Add(ushuaiaColegios[0,i]);
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
        }

        private void cboxPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {            
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = true;            
        }

        private void cboxDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxDepto.Enabled = false;            
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
            abreColegio = ushuaiaColegios[1, cboxColegiosUshuaia.SelectedIndex];
            numOrden = ushuaiaColegios[2, cboxColegiosUshuaia.SelectedIndex];
            colegio = cboxColegiosUshuaia.SelectedItem.ToString();

            cboxColegiosUshuaia.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnSelecExcel.BackColor = System.Drawing.Color.DodgerBlue;
            colorBtnExcel = 1;
        }

        private void cboxColegiosGrande_SelectedIndexChanged(object sender, EventArgs e)
        {
            abreColegio = grandeColegios[1, cboxColegiosGrande.SelectedIndex];
            numOrden = grandeColegios[2, cboxColegiosGrande.SelectedIndex];
            colegio = cboxColegiosGrande.SelectedItem.ToString();

            cboxColegiosGrande.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnSelecExcel.BackColor = System.Drawing.Color.DodgerBlue;
            colorBtnExcel = 1;

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ordenar();
        }
        private void btnSelecExcel_Click(object sender, EventArgs e)
        {
                       
           
            DataTable myDataTableColegios = new DataTable();

            myDataTableColegios.Columns.Add(new DataColumn("Sección", typeof(int)));
            myDataTableColegios.Columns.Add(new DataColumn("División", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Turno", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Orientacion", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Horas", typeof(int)));
            myDataTableColegios.Columns.Add(new DataColumn("Pedagógica", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Presupuestaria", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Matrícula", typeof(int)));

            string nombreHoja = "Sheet1";            

            try
            {
                //para seleccionarlo manualmente                

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
                        sl.GetCellValueAsInt32(row, iStartColumnIndex + 5),
                        sl.GetCellValueAsString(row, iStartColumnIndex + 6),
                        sl.GetCellValueAsString(row, iStartColumnIndex + 7),
                        sl.GetCellValueAsInt32(row, iStartColumnIndex + 8)

                        );
                    }
                    tbxColegioSel.Text = sl.GetCellValueAsString(5, 5);
                    fecha = sl.GetCellValueAsString(7, 9);
                }
                myDataGridView.DataSource = myDataTableColegios;//llena el dataGV con data table
                                                                //revisar si ya esta ingresada la plantilla

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
                
                string queryCargarBD = "SELECT IdUnico FROM Planilla WHERE IdUnico = @Buscar";
                OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                miDataAdapter.Fill(miDataTable);
                dataGridView1.DataSource = miDataTable;

                if ((Convert.ToString(dataGridView1.Rows[0].Cells[0].Value) == idUnico))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                {
                    plantillaIngresada = true;                                         
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                }
                else if (ex.Message.Contains("No se puede dejar vacío el nombre de la ruta de acceso"))
                {
                    MessageBox.Show("No se seleccionó ningún archivo.", "Sistema Informa");                    
                    planillaSelec = false;

                }
                //falta hacer que salga si es que hay error
            }

            if (plantillaIngresada == true)
            {
                MessageBox.Show("Esta plantilla ya está cargada y no puede volver a ser ingresada. Revise si la está cargando correctamente.", "Sistema Informa");
                ordenar();
            }
            else if ((plantillaIngresada == false) && (planillaSelec == false))
            {
                ordenar();
            }
            else
            {
                colorBtnExcel = 0;
                btnSelecExcel.Enabled = false;
                btnSelecExcel.BackColor = System.Drawing.Color.Silver;
                btnImportar.Enabled = true;
                btnImportar.BackColor = System.Drawing.Color.DodgerBlue;
                colorImportar = 1;
            }
            lblIngresando.Visible = false;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            lblCargando.Visible = true;
            lblCargando.Refresh();
            colorImportar = 0;
            btnImportar.BackColor = System.Drawing.Color.Silver;
            btnImportar.Enabled = false;            

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
                idNum = idUnico + Convert.ToString(i + 1);
                
                string queryAgregar = "INSERT INTO Planilla VALUES (@Año, @Periodo, @Depto, @ColegioSelect, @Sección, @División, " +
                "@Turno, @Orientación, @Horas, @Pedagógica, @Presupuestaria, @Matriculas, @ColegioIngre, @IdUnico,@Fecha, @NumOrdenColegio, @RutaPlanilla)";
                sqlComando = new OleDbCommand(queryAgregar, conexionBaseDatos);

                sqlComando.Parameters.AddWithValue("@Año", año);
                sqlComando.Parameters.AddWithValue("@Periodo", periodo);
                sqlComando.Parameters.AddWithValue("@Depto", depto);
                sqlComando.Parameters.AddWithValue("@ColegioSelect", colegio);
                sqlComando.Parameters.AddWithValue("@Sección", myDataGridView.Rows[i].Cells[0].Value);
                sqlComando.Parameters.AddWithValue("@División", myDataGridView.Rows[i].Cells[1].Value);
                sqlComando.Parameters.AddWithValue("@Turno", myDataGridView.Rows[i].Cells[2].Value);
                sqlComando.Parameters.AddWithValue("@Orientación", myDataGridView.Rows[i].Cells[3].Value);
                sqlComando.Parameters.AddWithValue("@Horas", myDataGridView.Rows[i].Cells[4].Value);
                sqlComando.Parameters.AddWithValue("@Pedagógica", myDataGridView.Rows[i].Cells[5].Value);
                sqlComando.Parameters.AddWithValue("@Presupuestaria", myDataGridView.Rows[i].Cells[6].Value);
                sqlComando.Parameters.AddWithValue("@Matriculas", myDataGridView.Rows[i].Cells[7].Value);

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
                    MessageBox.Show(Convert.ToString(ex));
                    conexionBaseDatos.Close();
                }
            }

            if (seImporto == true)
            {
                MessageBox.Show("Se agrego la planilla correctamente.", "Sistema Informa");
                ordenar();
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
        private void btnSelecExcel_MouseMove(object sender, MouseEventArgs e)
        {
            btnSelecExcel.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnSelecExcel_MouseLeave(object sender, EventArgs e)
        {   
            if (colorBtnExcel == 0)//unica forma de que funcione
            {
                btnSelecExcel.BackColor = System.Drawing.Color.DimGray;
            }
            else if (colorBtnExcel == 1)
            {
                btnSelecExcel.BackColor = System.Drawing.Color.DodgerBlue;
            }
        }

        private void btnImportar_MouseMove(object sender, MouseEventArgs e)
        {
            btnImportar.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnImportar_MouseLeave(object sender, EventArgs e)
        {
            if (colorImportar == 0)//unica forma de que funcione
            {
                btnImportar.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorImportar == 1)
            {
                btnImportar.BackColor = System.Drawing.Color.DodgerBlue;
            }
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