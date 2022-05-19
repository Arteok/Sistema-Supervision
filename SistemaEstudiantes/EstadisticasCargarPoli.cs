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
    public partial class EstadisticasCargarPoli : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        Colegios myColegios;
        bool colegiosCreados = false;
        int cantColegiosUshuaia;
        int cantColegiosGrande;

        string nombreUsuario;
        string tipoUsuario;
        bool logueadoUsuario;

         
        string año;
        string periodo;
        string depto;
        string colegio;
        string colegioIngre;
        string idUnico;
        string idNum;
        string abreColegio;
        string numOrden;
        bool plantillaRepetida;
        bool tryCatch;


        string[,] ushuaiaNombreColegios = new string[3, 12];//nombre abreviado de los colegios... a futuro tengo que tomarlos de la base de datos y tiene que ser un array 
        string[,] grandeNombreColegios = new string[3, 13];
        //Lista establecimientos ushuaia
        int[,] ucolegios = new int[12, 48];
        int[,] rcolegios = new int[13, 48];


        string[] uNombreEstablecimientos = new string[12];

        string[,] ushuaiaColegios;//nombre,nombreAbreviado, posicion de los colegios de ushuaia ##tomo como cantidad maxima la cantidad de colegios desde colegios tdf
        string[,] grandeColegios;//nombre,nombreAbreviado, posicion de los colegios de rio grande

        public EstadisticasCargarPoli(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
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

            dataGridView1.Visible = false;//para que nunca se vea el dgv que sirve para comprobar si ya fue ingresada una planilla
            ordenar();

        }
        private void ordenar()
        {
            plantillaRepetida = false;

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

        private void btnSelecExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnImportar_Click(object sender, EventArgs e)
        {

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

        

        /*        
        
      

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
            btnRefresh.Enabled = true;
        }

        private void cboxColegiosGrande_SelectedIndexChanged(object sender, EventArgs e)
        {
            abreColegio = "PCot";
            numOrden = "12";
            colegio = cboxColegiosGrande.SelectedItem.ToString();

            cboxColegiosGrande.Enabled = false;
            btnSelecExcel.Enabled = true;
            btnRefresh.Enabled = true;
        }

        private void btnSelecExcel_Click(object sender, EventArgs e)
        {
            try
            {
                
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
        string queryAgregar = "INSERT INTO PlanillasxOrientacion VALUES (@Año, @Periodo, @Depto, @ColegioSelect, @Sección, @División, " +
            "@Turno, @Orientación, @Perfil, @Horas, @Pedagógica, @Presupuestaria, @Matriculas, @ColegioIngre, @IdUnico, @IdNum, @NumOrdenColegio)";
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
            if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
            {
                MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
            }
            /*else 
            {
                MessageBox.Show(Convert.ToString(ex));
            }

            conexionBaseDatos.Close();
        }
    }

    if (seImporto == true)
    {
        MessageBox.Show("Se agrego la planilla correctamente.", "Sistema Informa");
        ordenar();
    }
}
*/

    }
}
