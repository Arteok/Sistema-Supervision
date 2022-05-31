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
using System.IO;
using System.Diagnostics;

namespace SistemaEstudiantes
{
    public partial class Estadisticas3 : Form
    {
        int colorPlantilla;//color de los botones.... para que no se buguee.... colores pares son gris
        int colorEstadistica;
        int colorExcel;

        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        Colegios myColegios;
        bool colegiosCreados = false;
        int cantColegiosUshuaia;
        int cantColegiosGrande;

        string nombreUsuario;
        string permisosUsuario;
        bool logueadoUsuario;

        string tituloEstadistica = "";
        string idUnico;
        string abreColegio;
        string numOrden;
        string colegio;
        string deptoColegio;

        string[,] ushuaiaColegios;//nombre,nombreAbreviado, posicion de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
        string[,] grandeColegios;//nombre,nombreAbreviado, posicion de los colegios de rio grande

        int[,] colegiosUshuaiaSEP;//array datos de secciones, EDI Y POT DE USHAUAIA
        int[,] colegiosGrandeSEP;//array datos de secciones, EDI Y POT DE RIO GRANDE

        int[,] uTotalesCicloBasico;//array de totales de secciones,EDI Y POT del ciclo basico de ushuaia
        int[,] uTotalesCicloSuperior;//array de totales de secciones,EDI Y POT del ciclo superior de ushuaia
        int[,] uTotal2Ciclos;//array de sumatoria totales de secciones,EDI Y POT de los 2 ciclos de ushuaia
        int[,] uSubtotalTurnos;//array de subtotales de secciones, EDI Y POT por turno de ushuaia
        int[,] uSubtotal;//array de subtotales secciones, EDI Y POT de ushuaia

        int[,] gTotalesCicloBasico;//array de totales de secciones,EDI Y POT del ciclo basico de Rio grande
        int[,] gTotalesCicloSuperior;//array de totales de secciones,EDI Y POT del ciclo superior deRio grande
        int[,] gTotal2Ciclos;//array de sumatoria totales de secciones,EDI Y POT de los 2 ciclos de Rio grande
        int[,] gSubtotalTurnos;//array de subtotales de secciones, EDI Y POT por turno de Rio grande
        int[,] gSubtotal;//array de subtotales de secciones, EDI Y POTs de Rio grande

        int[] totalesSEP; //vector total de secciones, EDI Y POTs de TDF
        int[] totalJurisdicional;//array de totales generales secciones, EDI Y POT  de TDF
        
        public Estadisticas3(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();

            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;
            lblNombre.Text = usuario;

            idUnico = "";

            ordenar();

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
        }
        private void ordenar()
        {   //Ver Planillas
            cboxAñoPla.ResetText();
            cboxPeriodoPla.ResetText();
            cboxDepto.ResetText();
            cboxColegiosUshuaia.ResetText();
            cboxColegiosGrande.ResetText();

            cboxAñoPla.Enabled = true;
            cboxPeriodoPla.Enabled = false;
            cboxDepto.Enabled = false;
            cboxColegiosUshuaia.Visible = true;
            cboxColegiosGrande.Visible = true;
            cboxColegiosUshuaia.Enabled = false;
            cboxColegiosGrande.Enabled = false;

            btnVerPlanilla.BackColor = System.Drawing.Color.Silver;
            btnVerPlanilla.Enabled = false;

            //Ver estadistica
            cboxPeriodoEst.ResetText();
            cboxAñoEst.ResetText();
            cboxAñoEst.Enabled = true;
            cboxPeriodoEst.Enabled = false;

            btnVerEstadistica.Enabled = false;
            btnVerEstadistica.BackColor = System.Drawing.Color.Silver;

            lblAbriendo.Visible = false;
            lblDescargas.Visible = false;

            //crear estadistica
            cboxAño.ResetText();//Reinicia el texto seleccionado
            cboxPeriodo.ResetText();
            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;

            btnCrearEstadistica.Enabled = false;
            btnCrearExcel.Enabled = false;

            lblProcesando.Visible = false;
            lblCreando.Visible = false;

            btnCrearEstadistica.Enabled = false;
            btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;

            btnCrearExcel.Enabled = false;
            btnCrearExcel.BackColor = System.Drawing.Color.Silver;

            if (permisosUsuario != "SuperUsuario")
            {
                panel3.Enabled = false;
                panel3.BackColor = System.Drawing.Color.Silver;
                btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;
                btnCrearExcel.BackColor = System.Drawing.Color.Silver;
            }
            
            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();

            colegiosUshuaiaSEP = new int[25, 21];//array datos de secciones, EDI Y POT DE USHAUAIA
            colegiosGrandeSEP = new int[25, 21];//array datos de secciones, EDI Y POT DE RIO GRANDE

            uTotalesCicloBasico = new int[25, 3];//array de totales de secciones,EDI Y POT del ciclo basico de ushuaia
            uTotalesCicloSuperior = new int[25, 3];//array de totales de secciones,EDI Y POT del ciclo superior de ushuaia
            uTotal2Ciclos = new int[25, 3];//array de sumatoria totales de secciones,EDI Y POT de los 2 ciclos de ushuaia
            uSubtotalTurnos = new int[1, 21];//array de subtotales de secciones, EDI Y POT por turno de ushuaia
            uSubtotal = new int[1, 10];//array de subtotales secciones, EDI Y POT de ushuaia

            gTotalesCicloBasico = new int[25, 3];//array de totales de secciones,EDI Y POT del ciclo basico de Rio grande
            gTotalesCicloSuperior = new int[25, 3];//array de totales de secciones,EDI Y POT del ciclo superior deRio grande
            gTotal2Ciclos = new int[25, 3];//array de sumatoria totales de secciones,EDI Y POT de los 2 ciclos de Rio grande
            gSubtotalTurnos = new int[1, 21];//array de subtotales de secciones, EDI Y POT por turno de Rio grande
            gSubtotal = new int[1, 10];//array de subtotales de secciones, EDI Y POTs de Rio grande

            totalesSEP = new int[21]; //vector total de secciones, EDI Y POTs de TDF
            totalJurisdicional = new int[6];//array de totales generales secciones, EDI Y POT  de TDF
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
        private void cboxAñoPla_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxAñoPla.Enabled = false;
            cboxPeriodoPla.Enabled = true;
        }

        private void cboxPeriodoPla_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxPeriodoPla.Enabled = false;
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

            deptoColegio = "Ushuaia";
            cboxColegiosUshuaia.Enabled = false;
            btnVerPlanilla.Enabled = true;
            btnVerPlanilla.BackColor = System.Drawing.Color.DodgerBlue;
            colorPlantilla = 1;
        }
        private void cboxColegiosGrande_SelectedIndexChanged(object sender, EventArgs e)
        {
            abreColegio = grandeColegios[1, cboxColegiosGrande.SelectedIndex];
            numOrden = grandeColegios[2, cboxColegiosGrande.SelectedIndex];
            colegio = cboxColegiosGrande.SelectedItem.ToString();

            deptoColegio = "Grande";
            cboxColegiosGrande.Enabled = false;
            btnVerPlanilla.Enabled = true;
            btnVerPlanilla.BackColor = System.Drawing.Color.DodgerBlue;
            colorPlantilla = 1;
        }
        private void btnVerPlanilla_Click(object sender, EventArgs e)
        {
            colorPlantilla = 0; //color 0 es igual a silver, sirve para que no se bugueeen azul
            string idUnicoVPla;
            string abreColegioVPla;
            btnVerPlanilla.BackColor = System.Drawing.Color.Silver;
            btnVerPlanilla.Enabled = false;

            if (deptoColegio == "Ushuaia")
            {
                idUnicoVPla = cboxAñoPla.SelectedItem.ToString();//Se calcula sumando 3 variables
                abreColegioVPla = ushuaiaColegios[1, cboxColegiosUshuaia.SelectedIndex];
                if (cboxPeriodoPla.SelectedItem.ToString() == "Marzo")
                {
                    idUnicoVPla = idUnicoVPla + "M";
                }
                else
                {
                    idUnicoVPla = idUnicoVPla + "S";
                }
                idUnicoVPla = idUnicoVPla + abreColegioVPla;//termina aca sumando la ultima parte  

                cboxColegiosUshuaia.Enabled = false;

                try
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas, ColegioIngresado, Fecha FROM Planilla WHERE IdUnico = @Buscar";
                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", idUnicoVPla);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    myDataGridView.Sort(myDataGridView.Columns[1], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                    myDataGridView.Sort(myDataGridView.Columns[0], ListSortDirection.Ascending);

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
            else if (deptoColegio == "Grande")
            {
                colorPlantilla = 0;
                btnVerPlanilla.BackColor = System.Drawing.Color.Silver;
                btnVerPlanilla.Enabled = false;

                idUnicoVPla = cboxAñoPla.SelectedItem.ToString();//Se calcula sumando 3 variables
                abreColegioVPla = grandeColegios[1, cboxColegiosGrande.SelectedIndex];
                if (cboxPeriodoPla.SelectedItem.ToString() == "Marzo")
                {
                    idUnicoVPla = idUnicoVPla + "M";
                }
                else
                {
                    idUnicoVPla = idUnicoVPla + "S";
                }
                idUnicoVPla = idUnicoVPla + abreColegioVPla;//termina aca sumando la ultima parte  

                cboxColegiosGrande.Enabled = false;

                try
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas, ColegioIngresado, Fecha FROM Planilla WHERE IdUnico = @Buscar";
                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", idUnicoVPla);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    myDataGridView.Sort(myDataGridView.Columns[1], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                    myDataGridView.Sort(myDataGridView.Columns[0], ListSortDirection.Ascending);


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
        }
        private void cboxAñoEst_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxAñoEst.Enabled = false;
            cboxPeriodoEst.Enabled = true;
        }

        private void cboxPeriodoEst_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxPeriodoEst.Enabled = false;
            btnVerEstadistica.Enabled = true;
            btnVerEstadistica.Enabled = true;
            btnVerEstadistica.BackColor = System.Drawing.Color.DodgerBlue;
            colorEstadistica = 3;
        }
        private void btnVerEstadistica_Click(object sender, EventArgs e)
        {
            bool verDescarga = false;
            lblAbriendo.Visible = true;
            lblAbriendo.Refresh();

            colorEstadistica = 2; //color par es igual a silver, sirve para que no se bugueee azul            
            btnVerEstadistica.BackColor = System.Drawing.Color.Silver;
            btnVerEstadistica.Enabled = false;

            try
            {
                string userName = Environment.UserName;

                string sourceFile = @"//server/Compartida/Sistema/BDSistema Supervision/3-Estadistica EDI-POT/Estadisticas EDI Y POT " + cboxAñoEst.SelectedItem.ToString() + " " + cboxPeriodoEst.SelectedItem.ToString() + ".xlsx";
                string destinationFile = @"C:/Users/" + userName + "/Downloads/Estadisticas EDI Y POT " + cboxAñoEst.SelectedItem.ToString() + " " + cboxPeriodoEst.SelectedItem.ToString() + ".xlsx";

                // To move a file or folder to a new location:
                System.IO.File.Copy(sourceFile, destinationFile, true);//true es importante para que los sobre escriba       

                Process proceso = new Process();
                proceso.StartInfo.FileName = destinationFile;

                proceso.Start();
                lblAbriendo.Visible = true;
                lblAbriendo.Refresh();
            }

            catch (Exception ex)
            {
                verDescarga = true;
                if (ex.Message.Contains("no es una ruta de acceso válida"))
                {
                    MessageBox.Show("Problema con la red.", "Sistema Informa");
                }

                else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                {
                    MessageBox.Show("El archivo excel que quiere actualizar y abrir, se encuentra activo, debe cerrarlo.", "Sistema Informa");
                }
                else if (ex.Message.Contains("datos duplicados"))
                {
                    MessageBox.Show("Datos duplicados en base de datos.", "Sistema Informa");
                }
                else if (ex.Message.Contains("No se pudo encontrar el archivo"))
                {
                    MessageBox.Show("No se encontró ninguna estadística para los parámetros especificados.", "Sistema Informa");
                }
                else
                {
                    MessageBox.Show(Convert.ToString(ex), "Sistema Informa");
                }
            }
            //btnVerEstadistica.BackColor = System.Drawing.Color.Silver;
            btnVerEstadistica.Enabled = false;
            lblAbriendo.Visible = false;
            lblAbriendo.Refresh();
            if (verDescarga == false)
            {
                lblDescargas.Visible = true;
                lblDescargas.Refresh();
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
            btnCrearEstadistica.Enabled = true;
            btnCrearEstadistica.BackColor = System.Drawing.Color.DodgerBlue;
            colorExcel = 5;
        }
        private void btnCrearEstadistica_Click(object sender, EventArgs e)
        {
            lblProcesando.Visible = true;
            lblProcesando.Refresh();

            int contadorFilas = 0;
            int contadorLugarArray = 0;
            string colegiosFaltantes = "";
            bool faltaCargar = false;

            colorExcel = 4; //color par es igual a silver, sirve para que no se bugueee azul            
            btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;
            btnCrearEstadistica.Enabled = false;

            //##crando estadistica de ushuaia
            for (int numColegio = 0; numColegio <= (cantColegiosUshuaia - 1); numColegio++)
            {
                abreColegio = ushuaiaColegios[1, numColegio];//para conseguir el idUnico
                idUnico = cboxAño.SelectedItem.ToString();
                if ((cboxPeriodo.SelectedItem.ToString()) == "Marzo")
                {
                    idUnico = idUnico + "M" + abreColegio;
                }
                else
                {
                    idUnico = idUnico + "S" + abreColegio;
                }
                try
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Año, Periodo, Departamento, ColegioSelect, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE IdUnico = @Buscar";
                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    myDataGridView.Sort(myDataGridView.Columns[6], ListSortDirection.Ascending);
                    myDataGridView.Sort(myDataGridView.Columns[5], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                    myDataGridView.Sort(myDataGridView.Columns[4], ListSortDirection.Ascending);

                    if ((Convert.ToString(myDataGridView.Rows[0].Cells[0].Value) == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        faltaCargar = true;

                        colegiosFaltantes = colegiosFaltantes + " " + "-" + ushuaiaColegios[0, numColegio] + "-";

                        //MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");                        
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
                contadorFilas = 0;
                contadorLugarArray = 0;
                for (int añoCiclo = 1; añoCiclo <= 7; añoCiclo++)
                {
                    while (Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[4].Value) == añoCiclo)//revisa si en que seccion(año) se esta, buscando sobre la 4 columna
                    {
                        colegiosUshuaiaSEP[numColegio, contadorLugarArray] = colegiosUshuaiaSEP[numColegio, contadorLugarArray] + 1;
                        contadorFilas++;//cuenta las filas 
                    }
                    if ((añoCiclo == 2) || (añoCiclo == 3))
                    {
                        colegiosUshuaiaSEP[numColegio, contadorLugarArray + 1] = colegiosUshuaiaSEP[numColegio, contadorLugarArray] * 4;
                        colegiosUshuaiaSEP[numColegio, contadorLugarArray + 2] = colegiosUshuaiaSEP[numColegio, contadorLugarArray] * 4;
                    }
                    else
                    {
                        colegiosUshuaiaSEP[numColegio, contadorLugarArray + 1] = colegiosUshuaiaSEP[numColegio, contadorLugarArray] * 2;
                        colegiosUshuaiaSEP[numColegio, contadorLugarArray + 2] = colegiosUshuaiaSEP[numColegio, contadorLugarArray] * 2;

                    }
                    contadorLugarArray = contadorLugarArray + 3;//suma 3 lugares al cambiar de año
                }
            }
            //##crando estadistica de Rio Grande
            for (int numColegio = 0; numColegio <= (cantColegiosGrande - 1); numColegio++)
            {
                abreColegio = grandeColegios[1, numColegio];//para conseguir el idUnico
                idUnico = cboxAño.SelectedItem.ToString();
                if ((cboxPeriodo.SelectedItem.ToString()) == "Marzo")
                {
                    idUnico = idUnico + "M" + abreColegio;
                }
                else
                {
                    idUnico = idUnico + "S" + abreColegio;
                }
                try
                {
                    DataTable miDataTable = new DataTable();

                    string queryCargarBD = "SELECT Año, Periodo, Departamento, ColegioSelect, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE IdUnico = @Buscar";
                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    myDataGridView.Sort(myDataGridView.Columns[6], ListSortDirection.Ascending);
                    myDataGridView.Sort(myDataGridView.Columns[5], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                    myDataGridView.Sort(myDataGridView.Columns[4], ListSortDirection.Ascending);


                    if ((Convert.ToString(myDataGridView.Rows[0].Cells[0].Value) == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                    {
                        faltaCargar = true;

                        colegiosFaltantes = colegiosFaltantes + " " + "-" + grandeColegios[0, numColegio] + "-";

                        //MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");                        
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
                contadorFilas = 0;
                contadorLugarArray = 0;
                for (int añoCiclo = 1; añoCiclo <= 7; añoCiclo++)
                {
                    while (Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[4].Value) == añoCiclo)//revisa si en que seccion(año) se esta, buscando sobre la 4 columna
                    {
                        colegiosGrandeSEP[numColegio, contadorLugarArray] = colegiosGrandeSEP[numColegio, contadorLugarArray] + 1;
                        contadorFilas++;//cuenta las filas 
                    }
                    if ((añoCiclo == 2) || (añoCiclo == 3))
                    {
                        colegiosGrandeSEP[numColegio, contadorLugarArray + 1] = colegiosGrandeSEP[numColegio, contadorLugarArray] * 4;
                        colegiosGrandeSEP[numColegio, contadorLugarArray + 2] = colegiosGrandeSEP[numColegio, contadorLugarArray] * 4;
                    }
                    else
                    {
                        colegiosGrandeSEP[numColegio, contadorLugarArray + 1] = colegiosGrandeSEP[numColegio, contadorLugarArray] * 2;
                        colegiosGrandeSEP[numColegio, contadorLugarArray + 2] = colegiosGrandeSEP[numColegio, contadorLugarArray] * 2;
                    }
                    contadorLugarArray = contadorLugarArray + 3;//suma 3 lugares al cambiar de año         
                }
            }

            //##si se cargaron todas las plantillas comienza a resolver los arrays
            if (faltaCargar == true)
            {                
                DialogResult accionRealizar = MessageBox.Show("Falta cargar las planillas de los siguientes Colegios: " + colegiosFaltantes + "\nQuiere generar la Estadistica igualmente?", "Sistema Informa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (accionRealizar == DialogResult.Yes)
                {
                    calculosEstadistica();
                }
                else if (accionRealizar == DialogResult.No)
                {
                    ordenar();
                }
            }
            else
            {
                calculosEstadistica();
            }
        }
        private void calculosEstadistica()
        {
            //##arrays ushuaia
            /*Cargar array ciclo basico*/
            for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios que es 12
            {
                for (int j = 0; j <= 8; j = j + 3)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 9
                {
                    uTotalesCicloBasico[i, 0] = uTotalesCicloBasico[i, 0] + colegiosUshuaiaSEP[i, j];//cantidad de las secciones
                    uTotalesCicloBasico[i, 1] = uTotalesCicloBasico[i, 1] + colegiosUshuaiaSEP[i, j + 1];//cantidad de EDI
                    uTotalesCicloBasico[i, 2] = uTotalesCicloBasico[i, 2] + colegiosUshuaiaSEP[i, j + 2];//cantidad de POT
                }
            }

            /*Cargar array ciclo superior*/

            for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios que es 12
            {
                for (int j = 9; j <= 20; j = j + 3)//mientras j sea menor o igual al numero de secciones en el CICLO SUPERIOR QUE ES 21
                {

                    uTotalesCicloSuperior[i, 0] = uTotalesCicloSuperior[i, 0] + colegiosUshuaiaSEP[i, j];//cantidad de las secciones
                    uTotalesCicloSuperior[i, 1] = uTotalesCicloSuperior[i, 1] + colegiosUshuaiaSEP[i, j + 1];//cantidad de EDI
                    uTotalesCicloSuperior[i, 2] = uTotalesCicloSuperior[i, 2] + colegiosUshuaiaSEP[i, j + 2];//cantidad de POT
                }
            }
            /*Cargar array suma 2 ciclos*/

            for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios que es 12
            {
                uTotal2Ciclos[i, 0] = uTotalesCicloBasico[i, 0] + uTotalesCicloSuperior[i, 0];//suma todas las secciones de un colegio
                uTotal2Ciclos[i, 1] = uTotalesCicloBasico[i, 1] + uTotalesCicloSuperior[i, 1];//suma todos lAS EDI
                uTotal2Ciclos[i, 2] = uTotalesCicloBasico[i, 2] + uTotalesCicloSuperior[i, 2];//suma todos lAS POT
            }

            /*Cargar array subtotal turnos POr ushuaia*/

            int contadorColumnasTurnos;//es necesaria
            for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios que es 12
            {
                contadorColumnasTurnos = 0;
                for (int j = 0; j <= 20; j++)
                {
                    uSubtotalTurnos[0, contadorColumnasTurnos] = uSubtotalTurnos[0, contadorColumnasTurnos] + colegiosUshuaiaSEP[i, j];//suma todas las secciones, EDI Y POT por turno de un colegio
                    contadorColumnasTurnos++;
                }
            }

            /*Cargar array subtotales de secciones EDI Y POT de ushuaia*/

            for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios que es 12
            {
                uSubtotal[0, 0] = uSubtotal[0, 0] + uTotalesCicloBasico[i, 0];//suma todas las secciones del ciclo basico de ushauia 
                uSubtotal[0, 1] = uSubtotal[0, 1] + uTotalesCicloBasico[i, 1];//suma todos los EDI del ciclo basico de ushauia 
                uSubtotal[0, 2] = uSubtotal[0, 2] + uTotalesCicloBasico[i, 2];//suma todos los POT del ciclo basico de ushauia

                uSubtotal[0, 3] = uSubtotal[0, 3] + uTotalesCicloSuperior[i, 0];//suma todas las secciones del ciclo Superior de ushauia 
                uSubtotal[0, 4] = uSubtotal[0, 4] + uTotalesCicloSuperior[i, 1];//suma todos los EDI del ciclo Superior de ushauia 
                uSubtotal[0, 5] = uSubtotal[0, 5] + uTotalesCicloSuperior[i, 2];//suma todos los POT del ciclo Superior de ushauia 

                uSubtotal[0, 6] = uSubtotal[0, 6] + uTotal2Ciclos[i, 0];//suma todas las secciones de ushauia 
                uSubtotal[0, 7] = uSubtotal[0, 7] + uTotal2Ciclos[i, 1];//suma todos los EDI de ushauia 
                uSubtotal[0, 8] = uSubtotal[0, 8] + uTotal2Ciclos[i, 2];//suma todos los POT  de ushauia 

                uSubtotal[0, 9] = uSubtotal[0, 7] + uSubtotal[0, 8];//suma todos los EDI Y POT  de ushauia                   
            }

            //##arrays Rio grande
            /*Cargar array ciclo basico*/
            for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios que es 13
            {
                for (int j = 0; j <= 8; j = j + 3)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 9
                {
                    gTotalesCicloBasico[i, 0] = gTotalesCicloBasico[i, 0] + colegiosGrandeSEP[i, j];//cantidad de las secciones
                    gTotalesCicloBasico[i, 1] = gTotalesCicloBasico[i, 1] + colegiosGrandeSEP[i, j + 1];//cantidad de EDI
                    gTotalesCicloBasico[i, 2] = gTotalesCicloBasico[i, 2] + colegiosGrandeSEP[i, j + 2];//cantidad de POT
                }
            }

            /*Cargar array ciclo superior*/
            for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios que es 13
            {
                for (int j = 9; j <= 20; j = j + 3)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 18 - 2(16) y se suma de 2 en 2
                {
                    gTotalesCicloSuperior[i, 0] = gTotalesCicloSuperior[i, 0] + colegiosGrandeSEP[i, j];//cantidad de las secciones
                    gTotalesCicloSuperior[i, 1] = gTotalesCicloSuperior[i, 1] + colegiosGrandeSEP[i, j + 1];//cantidad de EDI
                    gTotalesCicloSuperior[i, 2] = gTotalesCicloSuperior[i, 2] + colegiosGrandeSEP[i, j + 2];//cantidad de POT
                }
            }

            /*Cargar array suma 2 ciclos*/
            for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios que es 13
            {
                gTotal2Ciclos[i, 0] = gTotalesCicloBasico[i, 0] + gTotalesCicloSuperior[i, 0];//suma todas las secciones de un colegio
                gTotal2Ciclos[i, 1] = gTotalesCicloBasico[i, 1] + gTotalesCicloSuperior[i, 1];//suma todos lAS EDI
                gTotal2Ciclos[i, 2] = gTotalesCicloBasico[i, 2] + gTotalesCicloSuperior[i, 2];//suma todos lAS POT
            }

            /*Cargar array subtotal turnos grande*/
            int gContadorColumnasTurnos;//es necesaria
            for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios que es 13
            {
                gContadorColumnasTurnos = 0;
                for (int j = 0; j <= 20; j++)
                {
                    gSubtotalTurnos[0, gContadorColumnasTurnos] = gSubtotalTurnos[0, gContadorColumnasTurnos] + colegiosGrandeSEP[i, j];//suma todas las secciones por turno de un colegio
                    gContadorColumnasTurnos++;
                }
            }

            /*Cargar array subtotales de secciones EDI Y POT de Rio grande*/
            for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios que es 13 por ahora
            {
                gSubtotal[0, 0] = gSubtotal[0, 0] + gTotalesCicloBasico[i, 0];//suma todas las secciones del ciclo basico de Rio grande
                gSubtotal[0, 1] = gSubtotal[0, 1] + gTotalesCicloBasico[i, 1];//suma todos los EDI del ciclo basico de Rio grande
                gSubtotal[0, 2] = gSubtotal[0, 2] + gTotalesCicloBasico[i, 2];//suma todos los POT del ciclo basico de Rio grande

                gSubtotal[0, 3] = gSubtotal[0, 3] + gTotalesCicloSuperior[i, 0];//suma todas las secciones del ciclo Superior de Rio grande
                gSubtotal[0, 4] = gSubtotal[0, 4] + gTotalesCicloSuperior[i, 1];//suma todos los EDI del ciclo Superior de Rio grande 
                gSubtotal[0, 5] = gSubtotal[0, 5] + gTotalesCicloSuperior[i, 2];//suma todos los POT del ciclo Superior de Rio grande

                gSubtotal[0, 6] = gSubtotal[0, 6] + gTotal2Ciclos[i, 0];//suma todas las secciones de Rio grande 
                gSubtotal[0, 7] = gSubtotal[0, 7] + gTotal2Ciclos[i, 1];//suma todos los EDI de Rio grande
                gSubtotal[0, 8] = gSubtotal[0, 8] + gTotal2Ciclos[i, 2];//suma todos los POT  de Rio grande 

                gSubtotal[0, 9] = gSubtotal[0, 7] + gSubtotal[0, 8];//suma todos los EDI Y POT  de Rio grande
            }

            //##arrays totales jurisdiccionales
            for (int i = 0; i <= 20; i++)
            {
                totalesSEP[i] = uSubtotalTurnos[0, i] + gSubtotalTurnos[0, i];
            }
            //Totales generales
            totalJurisdicional[0] = uSubtotal[0, 0] + gSubtotal[0, 0];
            totalJurisdicional[1] = uSubtotal[0, 1] + gSubtotal[0, 1] + uSubtotal[0, 2] + gSubtotal[0, 2];
            totalJurisdicional[2] = uSubtotal[0, 3] + gSubtotal[0, 3];
            totalJurisdicional[3] = uSubtotal[0, 4] + gSubtotal[0, 4] + uSubtotal[0, 5] + gSubtotal[0, 5];
            totalJurisdicional[4] = uSubtotal[0, 6] + gSubtotal[0, 6];
            totalJurisdicional[5] = uSubtotal[0, 7] + gSubtotal[0, 7] + uSubtotal[0, 8] + gSubtotal[0, 8];

            lblProcesando.Visible = false;
            MessageBox.Show("Estadistica generada.", "Sistema Informa");
            btnCrearExcel.Enabled = true;
            btnCrearExcel.BackColor = System.Drawing.Color.DodgerBlue;
            colorExcel = 7;
        }

        private void btnCrearExcel_Click(object sender, EventArgs e)
        {
            lblCreando.Visible = true;
            lblCreando.Refresh();
            colorExcel = 6; //color par es igual a silver, sirve para que no se bugueee azul            
            btnCrearExcel.BackColor = System.Drawing.Color.Silver;
            btnCrearExcel.Enabled = false;

            try
            {
                //creando Estadisticas
                SLDocument sl = new SLDocument();

                string pathFile = @"C:\Users\Pablo\Downloads\Prueba\Estadisticas EDI Y POT " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString() + ".xlsx";
                //string pathFile = @"C:\Users\Arteok\Downloads\Prueba\Estadisticas EDI Y POT " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString() + ".xlsx";

                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "EDI y POT");//renombra la Hoja
                                                                                  //titulo estadistica

                tituloEstadistica = "CANTIDAD DE HORAS EDI Y POT - " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString();

                //FONDOS###########
                SLStyle styleFondoAzulGray = sl.CreateStyle();//Crea el fondo Azul OSCURO por definir
                styleFondoAzulGray.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkCyan, System.Drawing.Color.White);

                SLStyle styleFondoNaranja = sl.CreateStyle();//Crea el fondo Naranja
                styleFondoNaranja.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Coral, System.Drawing.Color.White);

                SLStyle styleFondoAzul = sl.CreateStyle();//Crea el fondo Azul
                styleFondoAzul.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.SteelBlue, System.Drawing.Color.White);

                SLStyle styleFondoVerde = sl.CreateStyle();//Crea el fondo verde
                styleFondoVerde.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.MediumSeaGreen, System.Drawing.Color.White);

                SLStyle styleFondoCeleste = sl.CreateStyle();//Crea el fondo celeste
                styleFondoCeleste.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSkyBlue, System.Drawing.Color.White);

                SLStyle styleFondoGris = sl.CreateStyle();//Crea el fondo Gris
                styleFondoGris.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Silver, System.Drawing.Color.White);

                SLStyle styleFondoCrema = sl.CreateStyle();//Crea el fondo Cremita
                styleFondoCrema.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Wheat, System.Drawing.Color.White);

                SLStyle styleFondoCremaCeleste = sl.CreateStyle();//Crea el fondo Cremita CELESTE
                styleFondoCremaCeleste.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Lavender, System.Drawing.Color.White);

                SLStyle styleFondoCremaVerde = sl.CreateStyle();//Crea el fondo Cremita verde
                styleFondoCremaVerde.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Khaki, System.Drawing.Color.White);

                SLStyle styleFondoCremaNaranja = sl.CreateStyle();//Crea el fondo Cremita Naranja
                styleFondoCremaNaranja.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.NavajoWhite, System.Drawing.Color.White);

                SLStyle styleFondoLime = sl.CreateStyle();//Crea el fondo LightGreen
                styleFondoLime.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightGreen, System.Drawing.Color.White);

                SLStyle styleFondoRosa = sl.CreateStyle();//Crea el fondo naranja claro
                styleFondoRosa.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.MistyRose, System.Drawing.Color.White);

                //FONDOSUSHUAIA
                //Titulo FONDO  USHUAIA
                sl.SetCellStyle(1, 1, 1, 32, styleFondoAzulGray);//asigna fondo azul a las entre las siguientes celdas
                                                                 //Subtitulo FONDO
                sl.SetCellStyle(2, 3, 2, 29, styleFondoNaranja);
                //secciones fondo
                sl.SetCellStyle(3, 3, 3, 11, styleFondoAzul);
                sl.SetCellStyle(3, 15, 3, 26, styleFondoAzul);
                //años
                sl.SetCellStyle("C4", "K4", styleFondoVerde);
                sl.SetCellStyle("O4", "Z4", styleFondoVerde);
                //fondo de turnos
                sl.SetCellStyle("C5", "E5", styleFondoCremaVerde);
                //fondo depto y establecimiento
                sl.SetCellStyle("A2", "B5", styleFondoAzulGray);

                //Fondos total secciones, EDI Y POT
                sl.SetCellStyle("L3", "N3", styleFondoLime);
                sl.SetCellStyle("AA3", "AC3", styleFondoLime);
                //totales
                sl.SetCellStyle("AD2", "AF2", styleFondoAzul);

                //Fondos gris secciones ushuaia
                for (int i = 3; i <= 9; i = i + 3)
                {
                    sl.SetCellStyle(6, i, 6 + cantColegiosUshuaia, i, styleFondoGris);
                }
                for (int i = 15; i <= 24; i = i + 3)
                {
                    sl.SetCellStyle(6, i, 6 + cantColegiosUshuaia, i, styleFondoGris);
                }
                //Fondos Rosa total secciones ushuaia
                for (int i = 12; i <= 14; i++)
                {
                    sl.SetCellStyle(6, i, 6 + cantColegiosUshuaia, i, styleFondoRosa);
                }
                for (int i = 27; i <= 29; i++)
                {
                    sl.SetCellStyle(6, i, 6 + cantColegiosUshuaia, i, styleFondoRosa);
                }

                //fondo ushuaia ARRANCAR A PONER CON CANTIDAD de colegios
                sl.SetCellStyle(6, 1, 5 + cantColegiosUshuaia, 1, styleFondoCeleste);
                //fondo subtotal ushuaia
                sl.SetCellStyle(6 + cantColegiosUshuaia, 1, 6 + cantColegiosUshuaia, 32, styleFondoLime);
                //fondo subtotal ciclo basico ushuaia
                sl.SetCellStyle(6 + cantColegiosUshuaia, 12, 6 + cantColegiosUshuaia, 14, styleFondoNaranja);
                //fondo subtotal ciclo superior ushuaia
                sl.SetCellStyle(6 + cantColegiosUshuaia, 27, 6 + cantColegiosUshuaia, 29, styleFondoNaranja);
                //fondo subtotal  2ciclo ushuaia
                sl.SetCellStyle(6 + cantColegiosUshuaia, 30, 6 + cantColegiosUshuaia, 32, styleFondoVerde);
                //fondo total ushuaia
                sl.SetCellStyle(7 + cantColegiosUshuaia, 31, 7 + cantColegiosUshuaia, 32, styleFondoNaranja);

                //fondo rio grande ARRANCAR A PONER CON CANTIDAD de colegios
                sl.SetCellStyle(8 + cantColegiosUshuaia, 1, 8 + cantColegiosUshuaia + cantColegiosGrande, 1, styleFondoCeleste);
                //fondo subtotal rio grande
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 1, 8 + cantColegiosUshuaia + cantColegiosGrande, 32, styleFondoLime);
                //fondo subtotal ciclo basico rio grande
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 12, 8 + cantColegiosUshuaia + cantColegiosGrande, 14, styleFondoNaranja);
                //fondo subtotal ciclo superior rio grande
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 27, 8 + cantColegiosUshuaia + cantColegiosGrande, 29, styleFondoNaranja);
                //fondo subtotal  2ciclo rio grande
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 30, 8 + cantColegiosUshuaia + cantColegiosGrande, 32, styleFondoVerde);

                //fondo total rio grande
                sl.SetCellStyle(9 + cantColegiosUshuaia + cantColegiosGrande, 31, 9 + cantColegiosUshuaia + cantColegiosGrande, 32, styleFondoNaranja);

                //Fondos gris secciones Rio grande
                for (int i = 3; i <= 9; i = i + 3)
                {
                    sl.SetCellStyle(8 + cantColegiosUshuaia, i, 7 + cantColegiosUshuaia + cantColegiosGrande, i, styleFondoGris);
                }
                for (int i = 15; i <= 24; i = i + 3)
                {
                    sl.SetCellStyle(8 + cantColegiosUshuaia, i, 7 + cantColegiosUshuaia + cantColegiosGrande, i, styleFondoGris);
                }
                //Fondos Rosa total secciones Rio grande
                for (int i = 12; i <= 14; i++)
                {
                    sl.SetCellStyle(8 + cantColegiosUshuaia, i, 7 + cantColegiosUshuaia + cantColegiosGrande, i, styleFondoRosa);
                }
                for (int i = 27; i <= 29; i++)
                {
                    sl.SetCellStyle(8 + cantColegiosUshuaia, i, 7 + cantColegiosUshuaia + cantColegiosGrande, i, styleFondoRosa);
                }
                //fondos total provincial            
                //fondo subtotal provincial   
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 1, 10 + cantColegiosUshuaia + cantColegiosGrande, 32, styleFondoLime);
                //fondo subtotal ciclo basico provincial   
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 12, 10 + cantColegiosUshuaia + cantColegiosGrande, 14, styleFondoNaranja);
                //fondo subtotal ciclo superior provincial   
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 27, 10 + cantColegiosUshuaia + cantColegiosGrande, 29, styleFondoNaranja);
                //fondo subtotal  2ciclo provincial
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 30, styleFondoVerde);
                //fondo totalprovincial   
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 31, 10 + cantColegiosUshuaia + cantColegiosGrande, 32, styleFondoAzul);

                //BORDERS#########
                //bordes gruesos generales
                SLStyle styleBorderTM = sl.CreateStyle();//bordes medium Top, left,Right and bottom
                styleBorderTM.SetTopBorder(BorderStyleValues.Medium, System.Drawing.Color.Black);
                SLStyle styleBorderRM = sl.CreateStyle();
                styleBorderRM.SetRightBorder(BorderStyleValues.Medium, System.Drawing.Color.Black);
                SLStyle styleBorderLM = sl.CreateStyle();
                styleBorderLM.SetLeftBorder(BorderStyleValues.Medium, System.Drawing.Color.Black);
                SLStyle styleBorderBM = sl.CreateStyle();
                styleBorderBM.SetBottomBorder(BorderStyleValues.Medium, System.Drawing.Color.Black);
                //bordes normales
                SLStyle styleBorderBasicLeft = sl.CreateStyle();
                styleBorderBasicLeft.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                SLStyle styleBorderBasicTop = sl.CreateStyle();
                styleBorderBasicTop.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

                //BORDERS#########
                //bordes horizontales y verticales normales USHUAIA
                sl.SetCellStyle(1, 1, 7 + cantColegiosUshuaia, 32, styleBorderBasicTop);    //7 PORQUE ES EL BORDE DE TOP      
                sl.SetCellStyle(1, 1, 6 + cantColegiosUshuaia, 33, styleBorderBasicLeft);  //33 porque el borde es left  
                sl.SetCellStyle(7 + cantColegiosUshuaia, 31, 7 + cantColegiosUshuaia, 33, styleBorderBasicLeft); //bordes en total ushauaia

                //bordes horizontales y verticales normales Rio grande
                sl.SetCellStyle((8 + cantColegiosUshuaia), 1, (9 + cantColegiosUshuaia + cantColegiosGrande), 32, styleBorderBasicTop);    //8 PORQUE ES EL BORDE DE TOP        
                sl.SetCellStyle((8 + cantColegiosUshuaia), 1, (8 + cantColegiosUshuaia + cantColegiosGrande), 33, styleBorderBasicLeft);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 31, 9 + cantColegiosUshuaia + cantColegiosGrande, 33, styleBorderBasicLeft); //bordes en total rio grande

                //bordes horizontales y verticales normales Totales provincial
                sl.SetCellStyle((10 + cantColegiosUshuaia + cantColegiosGrande), 1, (11 + cantColegiosUshuaia + cantColegiosGrande), 32, styleBorderBasicTop);//10 PORQUE ES EL BORDE DE TOP        
                sl.SetCellStyle((10 + cantColegiosUshuaia + cantColegiosGrande), 1, (10 + cantColegiosUshuaia + cantColegiosGrande), 33, styleBorderBasicLeft);

                //Espacio dentro de las celdas##########
                //##espacios Filas Ushuaia
                sl.SetRowHeight(1, 36);//titulo
                sl.SetRowHeight(2, 32);//ciclo
                sl.SetRowHeight(3, 26);//cantidad
                sl.SetRowHeight(4, 26);//año
                sl.SetRowHeight(5, 90);//edi y pot

                sl.SetRowHeight(6 + cantColegiosUshuaia, 22);//subtotal ushauia
                sl.SetRowHeight(7 + cantColegiosUshuaia, 22);//total ushauai
                sl.SetRowHeight(8 + cantColegiosUshuaia + cantColegiosGrande, 22);//subtotal rio grande
                sl.SetRowHeight(9 + cantColegiosUshuaia + cantColegiosGrande, 22);//total rio grande
                sl.SetRowHeight(10 + cantColegiosUshuaia + cantColegiosGrande, 22);//total provincial

                //##espacios columnas
                sl.SetColumnWidth(1, 11);//Depto
                sl.SetColumnWidth(2, 35);//Establecimiento

                for (int i = 3; i <= 32; i++)//espacios en las columnas 3 al 32
                {
                    sl.SetColumnWidth(i, 7);
                }
                sl.SetColumnWidth(12, 8);//totales
                sl.SetColumnWidth(13, 8);
                sl.SetColumnWidth(14, 8);
                sl.SetColumnWidth(27, 8);
                sl.SetColumnWidth(28, 8);
                sl.SetColumnWidth(29, 8);
                sl.SetColumnWidth(30, 8);
                sl.SetColumnWidth(31, 8);
                sl.SetColumnWidth(32, 8);

                //LETRAS############
                //Titulo 20 WHITE
                SLStyle styleTitle = sl.CreateStyle();
                styleTitle.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleTitle.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleTitle.Font.FontColor = System.Drawing.Color.White;//color
                styleTitle.Font.FontName = "Arial";//tipo de fuente
                styleTitle.Font.FontSize = 20;//tamaño
                styleTitle.Font.Bold = true;//negrita  

                //Arial 18 WHITE
                SLStyle styleWhite18 = sl.CreateStyle();
                styleWhite18.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleWhite18.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleWhite18.Font.FontColor = System.Drawing.Color.White;//color
                styleWhite18.Font.FontName = "Arial";//tipo de fuente
                styleWhite18.Font.FontSize = 18;//tamaño
                styleWhite18.Font.Bold = true;//negrita 
                                              //Arial 16 WHITE
                SLStyle styleWhite16 = sl.CreateStyle();
                styleWhite16.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleWhite16.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleWhite16.Font.FontColor = System.Drawing.Color.White;//color
                styleWhite16.Font.FontName = "Arial";//tipo de fuente
                styleWhite16.Font.FontSize = 16;//tamaño
                styleWhite16.Font.Bold = true;//negrita

                //Arial 14 WHITE
                SLStyle styleWhite14 = sl.CreateStyle();
                styleWhite14.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleWhite14.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleWhite14.Font.FontColor = System.Drawing.Color.White;//color
                styleWhite14.Font.FontName = "Arial";//tipo de fuente
                styleWhite14.Font.FontSize = 14;//tamaño
                styleWhite14.Font.Bold = true;//negrita

                //Arial 14 WHITE 90 GRADOS
                SLStyle styleWhite14_90G = sl.CreateStyle();
                styleWhite14_90G.SetWrapText(true);//ajustar texto
                styleWhite14_90G.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleWhite14_90G.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleWhite14_90G.Alignment.TextRotation = 90;//rota 90 grados el texto
                styleWhite14_90G.Font.FontColor = System.Drawing.Color.White;//color
                styleWhite14_90G.Font.FontName = "Arial";//tipo de fuente
                styleWhite14_90G.Font.FontSize = 14;//tamaño
                styleWhite14_90G.Font.Bold = true;//negrita

                //Arial 18 WHITE 90 GRADOS
                SLStyle styleWhite18_90G = sl.CreateStyle();
                styleWhite18_90G.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleWhite18_90G.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleWhite18_90G.Alignment.TextRotation = 90;//rota 90 grados el texto
                styleWhite18_90G.Font.FontColor = System.Drawing.Color.White;//color
                styleWhite18_90G.Font.FontName = "Arial";//tipo de fuente
                styleWhite18_90G.Font.FontSize = 18;//tamaño
                styleWhite18_90G.Font.Bold = true;//negrita

                //Arial 14 Black
                SLStyle styleBlack14 = sl.CreateStyle();
                styleBlack14.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack14.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlack14.Font.FontColor = System.Drawing.Color.Black;//color
                styleBlack14.Font.FontName = "Arial";//tipo de fuente
                styleBlack14.Font.FontSize = 14;//tamaño
                styleBlack14.Font.Bold = true;//negrita

                //Arial 14 Black vertical
                SLStyle styleBlack14_90G = sl.CreateStyle();
                styleBlack14_90G.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack14_90G.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlack14_90G.Alignment.TextRotation = 90;//rota 90 grados el texto
                styleBlack14_90G.Font.FontColor = System.Drawing.Color.Black;//color
                styleBlack14_90G.Font.FontName = "Arial";//tipo de fuente
                styleBlack14_90G.Font.FontSize = 14;//tamaño
                styleBlack14_90G.Font.Bold = true;//negrita

                //Arial 16 Black vertical
                SLStyle styleBlack16_90G = sl.CreateStyle();

                styleBlack16_90G.SetWrapText(true);//ajustar texto
                styleBlack16_90G.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack16_90G.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlack16_90G.Alignment.TextRotation = 90;//rota 90 grados el texto
                styleBlack16_90G.Font.FontColor = System.Drawing.Color.Black;//color
                styleBlack16_90G.Font.FontName = "Arial";//tipo de fuente
                styleBlack16_90G.Font.FontSize = 16;//tamaño
                styleBlack16_90G.Font.Bold = true;//negrita

                //Arial 14 blue
                SLStyle styleBlue14 = sl.CreateStyle();
                styleBlue14.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack14.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlue14.Font.FontColor = System.Drawing.Color.Blue;//color
                styleBlue14.Font.FontName = "Arial";//tipo de fuente
                styleBlue14.Font.FontSize = 14;//tamaño
                styleBlue14.Font.Bold = true;//negrita

                //##CONTENIDO Celdas USHUAIA###### 
                //Titulo
                sl.SetCellValue("A1", tituloEstadistica); //set value
                sl.SetCellStyle(1, 1, styleTitle);//
                sl.MergeWorksheetCells("A1", "AF1");//COMBINAR
                                                    //SubTitulo
                sl.SetCellValue("C2", "CICLO BASICO"); //set value
                sl.SetCellStyle(2, 3, styleWhite18);//
                sl.MergeWorksheetCells("C2", "N2");//COMBINAR

                sl.SetCellValue("O2", "CICLO SUPERIOR"); //set value
                sl.SetCellStyle(2, 15, styleWhite18);//
                sl.MergeWorksheetCells("O2", "AC2");//COMBINAR
                                                    //secciones
                sl.SetCellValue("C3", "Cantidad de Secciones"); //set value
                sl.SetCellStyle("C3", styleWhite16);//
                sl.MergeWorksheetCells("C3", "K3");//COMBINAR

                sl.SetCellValue("O3", "Cantidad de Secciones"); //set value
                sl.SetCellStyle("O3", styleWhite16);//
                sl.MergeWorksheetCells("O3", "Z3");//COMBINAR
                                                   //AÑOS
                sl.SetCellValue("C4", "1ro"); //set value
                sl.SetCellStyle("C4", styleWhite16);//
                sl.MergeWorksheetCells("C4", "E4");//COMBINAR

                sl.SetCellValue("F4", "2do"); //set value
                sl.SetCellStyle("F4", styleWhite16);//
                sl.MergeWorksheetCells("F4", "H4");//COMBINAR

                sl.SetCellValue("I4", "3ro"); //set value
                sl.SetCellStyle("I4", styleWhite16);//
                sl.MergeWorksheetCells("I4", "K4");//COMBINAR

                sl.SetCellValue("O4", "4to"); //set value
                sl.SetCellStyle("O4", styleWhite16);//
                sl.MergeWorksheetCells("O4", "Q4");//COMBINAR

                sl.SetCellValue("R4", "5to"); //set value
                sl.SetCellStyle("R4", styleWhite16);//
                sl.MergeWorksheetCells("R4", "T4");//COMBINAR

                sl.SetCellValue("U4", "6to"); //set value
                sl.SetCellStyle("U4", styleWhite16);//
                sl.MergeWorksheetCells("U4", "W4");//COMBINAR

                sl.SetCellValue("X4", "7mo"); //set value
                sl.SetCellStyle("X4", styleWhite16);//
                sl.MergeWorksheetCells("X4", "Z4");//COMBINAR
                                                   //EDI Y POT
                sl.SetCellValue("C5", "Secciones"); //set value
                sl.SetCellStyle("C5", styleBlack16_90G);//

                sl.SetCellValue("D5", "EDI"); //set value
                sl.SetCellStyle("D5", styleBlack14_90G);//

                sl.SetCellValue("E5", "POT"); //set value
                sl.SetCellStyle("E5", styleBlack14_90G);//

                sl.CopyCell("C5", "F5");//copia el contenido de una celda
                sl.CopyCell("C5", "I5");//copia el contenido de una celda
                sl.CopyCell("C5", "O5");//copia el contenido de una celda
                sl.CopyCell("C5", "R5");//copia el contenido de una celda
                sl.CopyCell("C5", "U5");//copia el contenido de una celda
                sl.CopyCell("C5", "X5");//copia el contenido de una celda

                sl.CopyCell("D5", "G5");//copia el contenido de una celda
                sl.CopyCell("D5", "J5");//copia el contenido de una celda
                sl.CopyCell("D5", "P5");//copia el contenido de una celda
                sl.CopyCell("D5", "S5");//copia el contenido de una celda
                sl.CopyCell("D5", "V5");//copia el contenido de una celda
                sl.CopyCell("D5", "Y5");//copia el contenido de una celda

                sl.CopyCell("E5", "H5");//copia el contenido de una celda
                sl.CopyCell("E5", "K5");//copia el contenido de una celda
                sl.CopyCell("E5", "Q5");//copia el contenido de una celda
                sl.CopyCell("E5", "T5");//copia el contenido de una celda
                sl.CopyCell("E5", "W5");//copia el contenido de una celda
                sl.CopyCell("E5", "Z5");//copia el contenido de una celda

                //Depto y establecimiento
                sl.SetCellValue("A2", "Depto."); //set value
                sl.SetCellStyle("A2", styleWhite18);
                sl.MergeWorksheetCells("A2", "A5");//COMBINAR
                sl.SetCellValue("B2", "Establecimiento"); //set value
                sl.SetCellStyle("B2", styleWhite18);
                sl.MergeWorksheetCells("B2", "B5");//COMBINAR           
                                                   //Total secciones edi y pot
                sl.SetCellValue("L3", "Total Secciones Ciclo Básico"); //set value
                sl.SetCellStyle("L3", styleBlack16_90G);
                sl.MergeWorksheetCells("L3", "L5");//COMBINAR
                sl.SetCellValue("M3", "Total Horas EDI"); //set value
                sl.SetCellStyle("M3", styleBlack16_90G);
                sl.MergeWorksheetCells("M3", "M5");//COMBINAR
                sl.SetCellValue("N3", "Total Horas POT"); //set value
                sl.SetCellStyle("N3", styleBlack16_90G);
                sl.MergeWorksheetCells("N3", "N5");//COMBINAR

                sl.SetCellValue("AA3", "Total Secciones Ciclo Superior"); //set value
                sl.SetCellStyle("AA3", styleBlack16_90G);
                sl.MergeWorksheetCells("AA3", "AA5");//COMBINAR
                sl.CopyCell("M3", "AB3");//copia el contenido de una celda
                sl.MergeWorksheetCells("AB3", "AB5");//COMBINAR
                sl.CopyCell("N3", "AC3");//copia el contenido de una celda
                sl.MergeWorksheetCells("AC3", "AC5");//COMBINAR
                                                     //totales generales
                sl.SetCellValue("AD2", "Total Secciones "); //set value
                sl.SetCellStyle("AD2", styleWhite18_90G);
                sl.MergeWorksheetCells("AD2", "AD5");//COMBINAR
                sl.SetCellValue("AE2", "Total Horas EDI"); //set value
                sl.SetCellStyle("AE2", styleWhite18_90G);
                sl.MergeWorksheetCells("AE2", "AE5");//COMBINAR
                sl.SetCellValue("AF2", "Total Horas POT"); //set value
                sl.SetCellStyle("AF2", styleWhite18_90G);
                sl.MergeWorksheetCells("AF2", "AF5");//COMBINAR

                //ushuaia
                sl.SetCellValue("A6", "Ushuaia"); //set value
                sl.SetCellStyle("A6", styleBlack16_90G);
                sl.MergeWorksheetCells(6, 1, 5 + cantColegiosUshuaia, 1);//COMBINAR

                //Nombre de los establecimientos Ushuaia
                for (int i = 6; i <= (6 + cantColegiosUshuaia - 1); i++)
                {
                    sl.SetCellValue(i, 2, ushuaiaColegios[0, i - 6]); //set value
                    sl.SetCellStyle(i, 2, styleBlack14);
                }
                //subtotal USHUAIA
                sl.SetCellValue(6 + cantColegiosUshuaia, 1, "Subtotal Ushuaia "); //set value
                sl.SetCellStyle(6 + cantColegiosUshuaia, 1, styleBlack14);
                sl.MergeWorksheetCells(6 + cantColegiosUshuaia, 1, 6 + cantColegiosUshuaia, 2);//COMBINAR

                //completa ciclo basico ushuaia
                for (int i = 6; i <= 5 + cantColegiosUshuaia; i++)//I=COLEGIO
                {
                    for (int j = 3; j <= 11; j++)//J= columna que son 9 en el primer ciclo
                    {
                        if (colegiosUshuaiaSEP[i - 6, j - 3] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosUshuaiaSEP[i - 6, j - 3]); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                    }
                }
                //completa ciclo superior ushuaia           
                for (int i = 6; i <= 5 + cantColegiosUshuaia; i++)
                {
                    for (int j = 15; j <= 26; j++)
                    {
                        if (colegiosUshuaiaSEP[i - 6, j - 6] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosUshuaiaSEP[i - 6, j - 6]); //set value... 4 porque necesitamos que sea el lugar 10 en el array
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                    }
                }
                //completa totales ciclo basico ushuaia           
                for (int i = 6; i <= 5 + cantColegiosUshuaia; i++)//6 porque empieza en la fila 6
                {
                    for (int j = 12; j <= 14; j++)//12 porque empieza en la columna 12
                    {
                        sl.SetCellValue(i, j, uTotalesCicloBasico[i - 6, j - 12]); //set value
                        sl.SetCellStyle(i, j, styleBlue14);
                    }
                }

                //completa totales ciclo superior ushuaia           
                for (int i = 6; i <= 5 + cantColegiosUshuaia; i++)//6 porque empieza en la fila 6
                {
                    for (int j = 27; j <= 29; j++)//27 porque empieza en la columna 27
                    {
                        sl.SetCellValue(i, j, uTotalesCicloSuperior[i - 6, j - 27]); //set value
                        sl.SetCellStyle(i, j, styleBlue14);
                    }
                }
                //completa totales ushuaia           
                for (int i = 6; i <= 5 + cantColegiosUshuaia; i++)//6 porque empieza en la fila 6
                {
                    for (int j = 30; j <= 32; j++)//30 porque empieza en la columna 30
                    {
                        sl.SetCellValue(i, j, uTotal2Ciclos[i - 6, j - 30]); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                }
                //completa subtotal ciclo basico ushuaia           
                for (int j = 3; j <= 11; j++)//3 porque empieza en la columna 3 
                {
                    sl.SetCellValue(6 + cantColegiosUshuaia, j, uSubtotalTurnos[0, j - 3]); //set value
                    sl.SetCellStyle(6 + cantColegiosUshuaia, j, styleBlack14);
                }
                //completa subtotal ciclo superior ushuaia 
                for (int j = 15; j <= 26; j++)//15 porque empieza en la columna 15
                {
                    sl.SetCellValue(6 + cantColegiosUshuaia, j, uSubtotalTurnos[0, j - 6]); //set value
                    sl.SetCellStyle(6 + cantColegiosUshuaia, j, styleBlack14);
                }
                //completa SubTotales Ushuaia             
                sl.SetCellValue(6 + cantColegiosUshuaia, 12, uSubtotal[0, 0]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 12, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 13, uSubtotal[0, 1]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 13, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 14, uSubtotal[0, 2]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 14, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 27, uSubtotal[0, 3]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 27, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 28, uSubtotal[0, 4]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 28, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 29, uSubtotal[0, 5]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 29, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 30, uSubtotal[0, 6]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 30, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 31, uSubtotal[0, 7]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 31, styleWhite14);
                sl.SetCellValue(6 + cantColegiosUshuaia, 32, uSubtotal[0, 8]);
                sl.SetCellStyle(6 + cantColegiosUshuaia, 32, styleWhite14);

                sl.SetCellValue(7 + cantColegiosUshuaia, 31, uSubtotal[0, 9]);
                sl.SetCellStyle(7 + cantColegiosUshuaia, 31, styleWhite14);
                sl.MergeWorksheetCells(7 + cantColegiosUshuaia, 31, 7 + cantColegiosUshuaia, 32);//COMBINAR

                //##CONTENIDO DE LAS CELDAS RIO GRANDE
                //Rio Grande
                sl.SetCellValue(8 + cantColegiosUshuaia, 1, "Rio Grande"); //set value
                sl.SetCellStyle(8 + cantColegiosUshuaia, 1, styleBlack16_90G);
                sl.MergeWorksheetCells(8 + cantColegiosUshuaia, 1, 7 + cantColegiosUshuaia + cantColegiosGrande, 1);//COMBINAR

                //Nombre de los establecimientos RIO GRANDE
                for (int i = (8 + cantColegiosUshuaia); i <= 8 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)
                {
                    sl.SetCellValue(i, 2, grandeColegios[0, i - (8 + cantColegiosUshuaia)]); //set value
                    sl.SetCellStyle(i, 2, styleBlack14);
                }
                //subtotal Rio grande
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 1, "Subtotal Rio Grande "); //set value
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBlack14);
                sl.MergeWorksheetCells(8 + cantColegiosUshuaia + cantColegiosGrande, 1, 8 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

                //completa ciclo basico Rio grande
                for (int i = 8 + cantColegiosUshuaia; i <= 7 + cantColegiosUshuaia + cantColegiosGrande; i++)//I=COLEGIO
                {
                    for (int j = 3; j <= 11; j++)//J= columna que son 9 en el primer ciclo (3+9-1)
                    {
                        if (colegiosGrandeSEP[i - (8 + cantColegiosUshuaia), j - 3] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosGrandeSEP[i - (8 + cantColegiosUshuaia), j - 3]); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                    }
                }
                //completa ciclo superior Rio grande         
                for (int i = 8 + cantColegiosUshuaia; i <= 7 + cantColegiosUshuaia + cantColegiosGrande; i++)//I=COLEGIO
                {
                    for (int j = 15; j <= 26; j++)
                    {
                        if (colegiosGrandeSEP[i - (8 + cantColegiosUshuaia), j - 6] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosGrandeSEP[i - (8 + cantColegiosUshuaia), j - 6]); //set value... 4 porque necesitamos que sea el lugar 10 en el array
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                    }
                }
                //completa totales ciclo basico Rio grande         
                for (int i = 8 + cantColegiosUshuaia; i <= 7 + cantColegiosUshuaia + cantColegiosGrande; i++)
                {
                    for (int j = 12; j <= 14; j++)//12 porque empieza en la columna 12
                    {
                        sl.SetCellValue(i, j, gTotalesCicloBasico[i - (8 + cantColegiosUshuaia), j - 12]); //set value
                        sl.SetCellStyle(i, j, styleBlue14);
                    }
                }
                //completa totales ciclo superior Rio grande          
                for (int i = 8 + cantColegiosUshuaia; i <= 7 + cantColegiosUshuaia + cantColegiosGrande; i++)
                {
                    for (int j = 27; j <= 29; j++)//27 porque empieza en la columna 27
                    {
                        sl.SetCellValue(i, j, gTotalesCicloSuperior[i - (8 + cantColegiosUshuaia), j - 27]); //set value
                        sl.SetCellStyle(i, j, styleBlue14);
                    }
                }
                //completa totales Rio grande          
                for (int i = 8 + cantColegiosUshuaia; i <= 7 + cantColegiosUshuaia + cantColegiosGrande; i++)//8 filas mas la cantidad de colegios en ushuaia
                {
                    for (int j = 30; j <= 32; j++)//30 porque empieza en la columna 30
                    {
                        sl.SetCellValue(i, j, gTotal2Ciclos[i - (8 + cantColegiosUshuaia), j - 30]); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                }
                //completa subtotal ciclo basico Rio Grande            
                for (int j = 3; j <= 11; j++)//3 porque empieza en la columna 3 
                {
                    sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, j, gSubtotalTurnos[0, j - 3]); //set value
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, j, styleBlack14);
                }
                //completa subtotal ciclo superior Rio Grande   
                for (int j = 15; j <= 26; j++)//15 porque empieza en la columna 15
                {
                    sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, j, gSubtotalTurnos[0, j - 6]); //set value
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, j, styleBlack14);
                }
                //completa SubTotales Rio Grande             
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 12, gSubtotal[0, 0]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 12, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 13, gSubtotal[0, 1]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 13, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 14, gSubtotal[0, 2]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 14, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 27, gSubtotal[0, 3]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 27, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 28, gSubtotal[0, 4]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 28, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 29, gSubtotal[0, 5]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 29, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 30, gSubtotal[0, 6]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 30, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 31, gSubtotal[0, 7]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 31, styleWhite14);
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 32, gSubtotal[0, 8]);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 32, styleWhite14);

                sl.SetCellValue(9 + cantColegiosUshuaia + cantColegiosGrande, 31, gSubtotal[0, 9]);
                sl.SetCellStyle(9 + cantColegiosUshuaia + cantColegiosGrande, 31, styleWhite14);
                sl.MergeWorksheetCells(9 + cantColegiosUshuaia + cantColegiosGrande, 31, 9 + cantColegiosUshuaia + cantColegiosGrande, 32);//COMBINAR

                //##CONTENIDO DE LAS CELDAS TOTAL PROVINCIAL
                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 1, "Total Provincial "); //set value
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBlack14);
                sl.MergeWorksheetCells(10 + cantColegiosUshuaia + cantColegiosGrande, 1, 10 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

                //completa totales ciclo basico totales TDF
                for (int j = 3; j <= 11; j++)//3 porque empieza en la columna 3 
                {
                    sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, j, totalesSEP[j - 3]); //set value
                    sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, j, styleBlack14);
                }

                //completa totales ciclo superior TDF          
                for (int j = 15; j <= 26; j++)//15 porque empieza en la columna 15
                {
                    sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, j, totalesSEP[j - 6]); //set value
                    sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, j, styleBlack14);
                }
                //completa totales FINALES TDF           
                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 12, totalJurisdicional[0]);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 12, styleWhite14);

                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 13, totalJurisdicional[1]);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 13, styleWhite14);
                sl.MergeWorksheetCells(10 + cantColegiosUshuaia + cantColegiosGrande, 13, 10 + cantColegiosUshuaia + cantColegiosGrande, 14);//COMBINAR

                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 27, totalJurisdicional[2]);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 27, styleWhite14);

                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 28, totalJurisdicional[3]);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 28, styleWhite14);
                sl.MergeWorksheetCells(10 + cantColegiosUshuaia + cantColegiosGrande, 28, 10 + cantColegiosUshuaia + cantColegiosGrande, 29);//COMBINAR

                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 30, totalJurisdicional[4]);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 30, styleWhite14);

                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 31, totalJurisdicional[5]);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 31, styleWhite14);
                sl.MergeWorksheetCells(10 + cantColegiosUshuaia + cantColegiosGrande, 31, 10 + cantColegiosUshuaia + cantColegiosGrande, 32);//COMBINAR            

                sl.SaveAs(pathFile);

                btnCrearExcel.Enabled = false;
                lblCreando.Visible = false;
                MessageBox.Show("Excel Generado", "Sistema Informa");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                {
                    MessageBox.Show("El archivo excel que quiere remplazar esta abierto, debe cerrarlo.", "Sistema Informa");
                    lblCreando.Visible = false;
                    btnCrearExcel.Enabled = true;
                    btnCrearExcel.BackColor = System.Drawing.Color.DodgerBlue;
                    colorExcel = 7;
                }
                else
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Estadisticas myEstadisticas = new Estadisticas(nombreUsuario, permisosUsuario, logueadoUsuario, conexionBaseDatos);
            myEstadisticas.Visible = true;
            this.Close();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnVerPlanilla_MouseMove(object sender, MouseEventArgs e)
        {
            btnVerPlanilla.BackColor = System.Drawing.Color.DimGray;
        }
        private void btnVerPlanilla_MouseLeave(object sender, EventArgs e)
        {
            if (colorPlantilla == 0)//unica forma de que funcione
            {
                btnVerPlanilla.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorPlantilla == 1)
            {
                btnVerPlanilla.BackColor = System.Drawing.Color.DodgerBlue;
            }
        }
        private void btnVerEstadistica_MouseMove(object sender, MouseEventArgs e)
        {
            btnVerEstadistica.BackColor = System.Drawing.Color.DimGray;
        }
        private void btnVerEstadistica_MouseLeave(object sender, EventArgs e)
        {
            if (colorEstadistica == 2)//unica forma de que funcione
            {
                btnVerEstadistica.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorEstadistica == 3)
            {
                btnVerEstadistica.BackColor = System.Drawing.Color.DodgerBlue;
            }
        }
        private void btnCrearEstadistica_MouseMove(object sender, MouseEventArgs e)
        {
            btnCrearEstadistica.BackColor = System.Drawing.Color.DimGray;
        }
        private void btnCrearEstadistica_MouseLeave(object sender, EventArgs e)
        {
            if (colorExcel == 4)//unica forma de que funcione
            {
                btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorExcel == 5)
            {
                btnCrearEstadistica.BackColor = System.Drawing.Color.DodgerBlue;
            }
        }

        private void btnCrearExcel_MouseMove(object sender, MouseEventArgs e)
        {
            btnCrearExcel.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnCrearExcel_MouseLeave(object sender, EventArgs e)
        {
            if (colorExcel == 6)//unica forma de que funcione
            {
                btnCrearExcel.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorExcel == 7)
            {
                btnCrearExcel.BackColor = System.Drawing.Color.DodgerBlue;
            }
        }
        private void btnRefresh_MouseMove(object sender, MouseEventArgs e)
        {
            btnRefresh.BackColor = System.Drawing.Color.DimGray;
        }
        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackColor = System.Drawing.Color.DodgerBlue;
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

