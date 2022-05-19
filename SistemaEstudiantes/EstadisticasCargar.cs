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
    public partial class EstadisticasCargar : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        string nombreUsuario;
        string tipoUsuario;
        bool opcionesPermisos;
        public EstadisticasCargar(string usuario, string permisos, bool permisosOpciones, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            tipoUsuario = permisos;
            opcionesPermisos = permisosOpciones;
            conexionBaseDatos = conexionBD;

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            EstadisticasPlanillas myEstadisticasPlanillas = new EstadisticasPlanillas(nombreUsuario, tipoUsuario, opcionesPermisos, conexionBaseDatos);
            this.Hide();
            myEstadisticasPlanillas.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnSelecExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

        }
        /*
          public partial class Estadisticas1Cargar : Form
    {
        string nombreUsuario;
        string permisosUsuario;
        bool logueadoUsuario;
        OleDbConnection conexionBaseDatos = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = |DataDirectory|BDEstadisticas.mdb");
        //OleDbConnection conexionBaseDatos = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\Users\Pablo\Desktop\Estadisticas Trabajando 23-03-2022\BDEstadisticas.mdb");
        OleDbCommand sqlComando;
        string año;
        string periodo;
        string depto;
        string colegio;
        string colegioIngre;
        string idUnico;
        string idNum;
        string abreColegio;
        string numOrden;
        bool plantillaIngresada;


        string[,] ushuaiaNombreColegios = new string[3, 12];//nombre abreviado de los colegios... a futuro tengo que tomarlos de la base de datos y tiene que ser un array 
        string[,] grandeNombreColegios = new string[3, 14];
        //Lista establecimientos ushuaia
        int[,] ucolegios = new int[12, 48];
        int[,] rcolegios = new int[14, 48];


        string[] uNombreEstablecimientos = new string[12];

        public Estadisticas1Cargar(string usuario, string permisos, bool logueado)
        {
            InitializeComponent();

            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoUsuario = logueado;
            ushuaiaNombreColegios[0, 0] = "LosA";
            ushuaiaNombreColegios[0, 1] = "SobB";
            ushuaiaNombreColegios[0, 2] = "SobT";
            ushuaiaNombreColegios[0, 3] = "Klok";
            ushuaiaNombreColegios[0, 4] = "EvaDP";
            ushuaiaNombreColegios[0, 5] = "SabB";
            ushuaiaNombreColegios[0, 6] = "SabT";
            ushuaiaNombreColegios[0, 7] = "Marti";
            ushuaiaNombreColegios[0, 8] = "PBust";
            ushuaiaNombreColegios[0, 9] = "MMarte";
            ushuaiaNombreColegios[0, 10] = "Alak";
            ushuaiaNombreColegios[0, 11] = "OBA";
            ushuaiaNombreColegios[1, 0] = "1";
            ushuaiaNombreColegios[1, 1] = "2";
            ushuaiaNombreColegios[1, 2] = "3";
            ushuaiaNombreColegios[1, 3] = "4";
            ushuaiaNombreColegios[1, 4] = "5";
            ushuaiaNombreColegios[1, 5] = "6";
            ushuaiaNombreColegios[1, 6] = "7";
            ushuaiaNombreColegios[1, 7] = "8";
            ushuaiaNombreColegios[1, 8] = "9";
            ushuaiaNombreColegios[1, 9] = "10";
            ushuaiaNombreColegios[1, 10] = "11";
            ushuaiaNombreColegios[1, 11] = "12";
            grandeNombreColegios[0, 0] = "Zink";
            grandeNombreColegios[0, 1] = "AArg";
            grandeNombreColegios[0, 2] = "Sobe";
            grandeNombreColegios[0, 3] = "Mara";
            grandeNombreColegios[0, 4] = "PBue";
            grandeNombreColegios[0, 5] = "GueB";
            grandeNombreColegios[0, 6] = "GueT";
            grandeNombreColegios[0, 7] = "AMor";
            grandeNombreColegios[0, 8] = "TNoel";
            grandeNombreColegios[0, 9] = "Hasp";
            grandeNombreColegios[0, 10] = "Fava";
            grandeNombreColegios[0, 11] = "PCot";
            grandeNombreColegios[0, 12] = "CPET";
            grandeNombreColegios[0, 13] = "Malvinas";
            grandeNombreColegios[1, 0] = "1";
            grandeNombreColegios[1, 1] = "2";
            grandeNombreColegios[1, 2] = "3";
            grandeNombreColegios[1, 3] = "4";
            grandeNombreColegios[1, 4] = "5";
            grandeNombreColegios[1, 5] = "6";
            grandeNombreColegios[1, 6] = "7";
            grandeNombreColegios[1, 7] = "8";
            grandeNombreColegios[1, 8] = "9";
            grandeNombreColegios[1, 9] = "10";
            grandeNombreColegios[1, 10] = "11";
            grandeNombreColegios[1, 11] = "12";
            grandeNombreColegios[1, 12] = "13";
            grandeNombreColegios[1, 13] = "14";

            dataGridView1.Visible = false;//para que nunca se vea el dgv que sirve para comprobar si ya fue ingresada una planilla
            ordenar();
        }
        private void ordenar()
        {
            plantillaIngresada = false;

            abreColegio = "";
            colegio = "";
            numOrden = "";

            cboxAño.SelectedIndex = -1;
            cboxPeriodo.SelectedIndex = -1;
            cboxDepto.SelectedIndex = -1;
            //cboxColegiosUshuaia.SelectedIndex = -1; Traen problema es mejor resettext
            //cboxColegiosGrande.SelectedIndex = -1;
            cboxColegiosUshuaia.ResetText();
            cboxColegiosGrande.ResetText();
            btnSelecExcel.Enabled = false;
            btnImportar.Enabled = false;
            tbxColegioSel.Clear();

            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;
            cboxDepto.Enabled = false;
            cboxColegiosUshuaia.Enabled = false;
            cboxColegiosGrande.Enabled = false;
            btnSelecExcel.Enabled = false;
            btnImportar.Enabled = false;
            tbxColegioSel.Enabled = false;
            btnRefresh.Enabled = false;
            cboxColegiosUshuaia.Visible = true;
            cboxColegiosGrande.Visible = true;


            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();

            dataGridView1.DataSource = null;//reinicia datagv
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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

        private void cboxColegios_SelectedIndexChanged(object sender, EventArgs e)
        {
            abreColegio = ushuaiaNombreColegios[0, cboxColegiosUshuaia.SelectedIndex];
            numOrden = ushuaiaNombreColegios[1, cboxColegiosUshuaia.SelectedIndex];
            colegio = cboxColegiosUshuaia.SelectedItem.ToString();

            cboxColegiosUshuaia.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnRefresh.Enabled = true;


        }
        private void cboxColegiosGrande_SelectedIndexChanged(object sender, EventArgs e)
        {
            abreColegio = grandeNombreColegios[0, cboxColegiosGrande.SelectedIndex];
            numOrden = grandeNombreColegios[1, cboxColegiosGrande.SelectedIndex];
            colegio = cboxColegiosGrande.SelectedItem.ToString();

            cboxColegiosGrande.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnRefresh.Enabled = true;
        }


        private void btnSelecExcel_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = true;
            DataTable myDataTableColegios = new DataTable();

            myDataTableColegios.Columns.Add(new DataColumn("Sección", typeof(int)));
            myDataTableColegios.Columns.Add(new DataColumn("División", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Turno", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Orientacion", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Horas", typeof(int)));
            myDataTableColegios.Columns.Add(new DataColumn("Pedagógica", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Presupuestaria", typeof(string)));
            myDataTableColegios.Columns.Add(new DataColumn("Matrícula", typeof(int)));

            //para seleccionarlo manualmente
            string nombreHoja = "Sheet1";
            string ruta = "";

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
                idUnico = idUnico + "O";
            }
idUnico = idUnico + abreColegio;//termina aca sumando la ultima parte            

try
{
    DataTable miDataTable = new DataTable();

    // string queryCargarBD = "SELECT Año, Periodo, Departamento, Colegio, Sección, División, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE Año LIKE  '%' + @Buscar + '%'";
    //string queryCargarBD = "SELECT *FROM Planilla ";
    string queryCargarBD = "SELECT IdUnico FROM Planilla WHERE IdUnico = @Buscar";
    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
    sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
    miDataAdapter.Fill(miDataTable);
    dataGridView1.DataSource = miDataTable;

    if ((Convert.ToString(dataGridView1.Rows[0].Cells[0].Value) == idUnico))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
    {
        plantillaIngresada = true;

        //MessageBox.Show(Convert.ToString(myDataGridView.Rows[0].Cells[0].Value), "Sistema Informa");                        
    }
}
catch (Exception ex)
{
    if (ex.Message.Contains("no es una ruta de acceso válida"))
    {
        MessageBox.Show("Problema con la red.", "Sistema Informa");
    }
    //falta hacer que salga si es que hay error
}

if (plantillaIngresada == true)
{
    MessageBox.Show("Esta plantilla ya está cargada y no puede volver a ser ingresada. Revise si la está cargando correctamente.", "Sistema Informa");
    ordenar();
}
else
{
    btnSelecExcel.Enabled = false;
    btnImportar.Enabled = true;
}
        }

        private void btnImportar_Click(object sender, EventArgs e)
{
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
        idUnico = idUnico + "O";
    }
    idUnico = idUnico + abreColegio;//termina aca sumando la ultima parte            
    MessageBox.Show(abreColegio + " " + numOrden + " " + idUnico);

    for (int i = 0; Convert.ToInt32(myDataGridView.Rows[i].Cells[0].Value) > 0; i++)   //revisa en la primer columna que en la celda el valor sea mayor a 0              
    {
        idNum = idUnico + Convert.ToString(i + 1);
        string queryAgregar = "INSERT INTO Planilla VALUES (@Año, @Periodo, @Depto, @ColegioSelect, @Sección, @División, " +
            "@Turno, @Orientación, @Horas, @Pedagógica, @Presupuestaria, @Matriculas, @ColegioIngre, @IdUnico, @IdNum, @NumOrdenColegio)";
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
        sqlComando.Parameters.AddWithValue("@IdNum", idNum);
        sqlComando.Parameters.AddWithValue("@NumOrdenColegio", numOrden);

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

private void btnRefresh_Click(object sender, EventArgs e)
{
    ordenar();
}


*/
    }
}
