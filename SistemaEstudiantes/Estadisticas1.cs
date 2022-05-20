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
    public partial class Estadisticas1 : Form
    {
        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        Colegios myColegios;
        bool colegiosCreados = false;
        int cantColegiosUshuaia;
        int cantColegiosGrande;

        string nombreUsuario;
        string tipoUsuario;      
        bool logueadoUsuario;

        string tituloEstadistica = "";
        string idUnico;
        string abreColegio;

        string[,] ushuaiaColegios;//nombre,nombreAbreviado, posicion de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
        string[,] grandeColegios;//nombre,nombreAbreviado, posicion de los colegios de rio grande

        int[,] colegiosUshuaiaSyE = new int[20, 42];//array datos de secciones y estudiantes de ushuaia
        int[,] colegiosGrandeSyE = new int[20, 42];//array datos de secciones y estudiantes de grande

        int[,] uTotalesCicloBasico = new int[20, 2];//array de totales de secciones y estudiantes del ciclo basico de ushuaia
        int[,] uTotalesCicloSuperior = new int[20, 2];//array de totales de secciones y estudiantes del ciclo superior de ushuaia
        int[,] uTotal2Ciclos = new int[20, 2];//array de sumatoria totales de secciones y estudiantes de los 2 ciclos de ushuaia
        int[,] uSubtotalTurnos = new int[2, 21];//array de subtotales de secciones y estudiantes por turno de ushuaia
        int[,] uSubtotalAños = new int[2, 7];//array de subtotales de secciones y estudiantes por año de ushuaia
        int[,] uSubtotal = new int[2, 3];//array de subtotales de secciones y estudiantes de ushuaia

        int[,] gTotalesCicloBasico = new int[20, 2];//array de totales de secciones y estudiantes del ciclo basico de Rio Grande
        int[,] gTotalesCicloSuperior = new int[20, 2];//array de totales de secciones y estudiantes del ciclo superior de Rio Grande
        int[,] gTotal2Ciclos = new int[20, 2];//array de sumatoria totales de secciones y estudiantes de los 2 ciclos de Rio Grande
        int[,] gSubtotalTurnos = new int[2, 21];//array de subtotales de secciones y estudiantes por turno de Rio Grande
        int[,] gSubtotalAños = new int[2, 7];//array de subtotales de secciones y estudiantes por año de rio grande
        int[,] gSubtotal = new int[2, 3];//array de subtotales de secciones y estudiantes de Rio Grande

        int[,] totalJurisSecyEstudiates = new int[2, 21];//array de totales de secciones y estudiantes en TDF
        int[,] totalJurisdicionalAños = new int[2, 7];//array de totales de secciones y estudiantes por año de TDF
        int[,] totalJurisdicional = new int[2, 3];//array de totales generales TDF       

        
        public Estadisticas1(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();            
            
            nombreUsuario = usuario;
            tipoUsuario = permisos;
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
                colegiosCreados = true;
            }
        }

        private void ordenar()
        {
            cboxAño.SelectedIndex = -1;//reinicia el texto seleccionado pero puede traer problema
            cboxPeriodo.ResetText();//Reinicia el texto seleccionado

            cboxPeriodo.SelectedIndex = -1;

            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;

            btnRefresh.Enabled = false;
            btnCrearEstadistica.Enabled = false;
            btnCrearExcel.Enabled = false;

            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();
            Array.Clear(colegiosUshuaiaSyE, 12, 48);//reinicia el valor en ese lugar especifico... tengo que encontrar la forma de reiniciar todo.

            lblProcesando.Visible = false;
            lblCreando.Visible = false;
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
            btnCrearEstadistica.Enabled = true;
        }

        private void btnCrearEstadistica_Click(object sender, EventArgs e)
        {
            lblProcesando.Visible = true;
            lblProcesando.Refresh();           

            int contadorFilas = 0;
            int contadorLugarArray = 0;
            string colegiosFaltantes = "";
            bool faltaCargar = false;
            btnRefresh.Enabled = false;
            btnCrearEstadistica.Enabled = false;

            //##creando estadistica de ushuaia
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
                    idUnico = idUnico + "O" + abreColegio;
                }
                //MessageBox.Show(idUnico);
                try
                {
                    DataTable miDataTable = new DataTable();

                    // string queryCargarBD = "SELECT Año, Periodo, Departamento, Colegio, Sección, División, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE Año LIKE  '%' + @Buscar + '%'";
                    //string queryCargarBD = "SELECT *FROM Planilla ";
                    string queryCargarBD = "SELECT Año, Periodo, Departamento, ColegioSelect, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE IdUnico = @Buscar";
                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    myDataGridView.Sort(myDataGridView.Columns[6], ListSortDirection.Ascending);
                    myDataGridView.Sort(myDataGridView.Columns[5], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                    myDataGridView.Sort(myDataGridView.Columns[4], ListSortDirection.Ascending);

                    //MessageBox.Show("Colegio cargado: " + uNombreColegios[0, numColegio]);

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
                        if ((Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "M") || (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "m"))
                        {
                            colegiosUshuaiaSyE[numColegio, contadorLugarArray] = colegiosUshuaiaSyE[numColegio, contadorLugarArray] + 1;
                            colegiosUshuaiaSyE[numColegio, contadorLugarArray + 1] = colegiosUshuaiaSyE[numColegio, contadorLugarArray + 1] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[11].Value);

                        }
                        else if ((Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "T") || (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "t"))
                        {
                            colegiosUshuaiaSyE[numColegio, contadorLugarArray + 2] = colegiosUshuaiaSyE[numColegio, contadorLugarArray + 2] + 1;
                            colegiosUshuaiaSyE[numColegio, contadorLugarArray + 3] = colegiosUshuaiaSyE[numColegio, contadorLugarArray + 3] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[11].Value);
                        }
                        else if ((Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "V") || (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "v"))
                        {
                            colegiosUshuaiaSyE[numColegio, contadorLugarArray + 4] = colegiosUshuaiaSyE[numColegio, contadorLugarArray + 4] + 1;
                            colegiosUshuaiaSyE[numColegio, contadorLugarArray + 5] = colegiosUshuaiaSyE[numColegio, contadorLugarArray + 5] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[11].Value);
                        }
                        contadorFilas++;//cuenta las filas                  
                    }
                    contadorLugarArray = contadorLugarArray + 6;//suma 6 lugares al cambiar de año
                }
                myDataGridView.DataSource = null;//reinicia datagv
                myDataGridView.Rows.Clear();
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
                    idUnico = idUnico + "O" + abreColegio;
                }
                try
                {
                    DataTable miDataTable = new DataTable();

                    // string queryCargarBD = "SELECT Año, Periodo, Departamento, Colegio, Sección, División, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE Año LIKE  '%' + @Buscar + '%'";
                    //string queryCargarBD = "SELECT *FROM Planilla ";
                    string queryCargarBD = "SELECT Año, Periodo, Departamento, ColegioSelect, Sección, División, Turno, Orientación, Horas, Pedagogica, Presupuestaria, Matriculas FROM Planilla WHERE IdUnico = @Buscar";
                    OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                    sqlComando.Parameters.AddWithValue("@idBuscar", idUnico);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    myDataGridView.Sort(myDataGridView.Columns[6], ListSortDirection.Ascending);
                    myDataGridView.Sort(myDataGridView.Columns[5], ListSortDirection.Ascending);//ordena del dataGV por la columna seccion para que no de error
                    myDataGridView.Sort(myDataGridView.Columns[4], ListSortDirection.Ascending);

                    //MessageBox.Show("Colegio cargado: " + grandeNombreAbreColegios[0, numColegio]);

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
                        if ((Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "M") || (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "m"))
                        {
                            colegiosGrandeSyE[numColegio, contadorLugarArray] = colegiosGrandeSyE[numColegio, contadorLugarArray] + 1;
                            colegiosGrandeSyE[numColegio, contadorLugarArray + 1] = colegiosGrandeSyE[numColegio, contadorLugarArray + 1] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[11].Value);

                        }
                        else if ((Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "T") || (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "t"))
                        {
                            colegiosGrandeSyE[numColegio, contadorLugarArray + 2] = colegiosGrandeSyE[numColegio, contadorLugarArray + 2] + 1;
                            colegiosGrandeSyE[numColegio, contadorLugarArray + 3] = colegiosGrandeSyE[numColegio, contadorLugarArray + 3] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[11].Value);
                        }
                        else if ((Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "V") || (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[6].Value) == "v"))
                        {
                            colegiosGrandeSyE[numColegio, contadorLugarArray + 4] = colegiosGrandeSyE[numColegio, contadorLugarArray + 4] + 1;
                            colegiosGrandeSyE[numColegio, contadorLugarArray + 5] = colegiosGrandeSyE[numColegio, contadorLugarArray + 5] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[11].Value);
                        }
                        contadorFilas++;//cuenta las filas                  
                    }
                    contadorLugarArray = contadorLugarArray + 6;//suma 6 lugares al cambiar de año
                }
                myDataGridView.DataSource = null;//reinicia datagv
                myDataGridView.Rows.Clear();
            }

            //##si se cargaron todas las plantillas comienza a resolver los arrays
            if (faltaCargar == true)
            {
                MessageBox.Show("Falta cargar las planillas de los siguientes Colegios:" + colegiosFaltantes, "Sistema Informa");
                ordenar();
            }
            else
            {   //##arrays ushuaia
                /*Cargar array ciclo basico*/
                string pruebaTotalSeccionesB = "";
                string pruebaTotalEstudiantesB = "";
                string mostrarTotalSeccionesB = "";
                string mostrarTotalEstudiantesB = "";

                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios  ushuia que es 12
                {
                    for (int j = 0; j <= 16; j = j + 2)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 18 - 2(16) y se suma de 2 en 2
                    {

                        uTotalesCicloBasico[i, 0] = uTotalesCicloBasico[i, 0] + colegiosUshuaiaSyE[i, j];//cantidad de las secciones
                        uTotalesCicloBasico[i, 1] = uTotalesCicloBasico[i, 1] + colegiosUshuaiaSyE[i, j + 1];//cantidad de las estudiantes
                        pruebaTotalSeccionesB = Convert.ToString(uTotalesCicloBasico[i, 0]);
                        pruebaTotalEstudiantesB = Convert.ToString(uTotalesCicloBasico[i, 1]);
                    }
                    mostrarTotalSeccionesB = mostrarTotalSeccionesB + " " + pruebaTotalSeccionesB;

                    mostrarTotalEstudiantesB = mostrarTotalEstudiantesB + " " + pruebaTotalEstudiantesB;
                }
                //MessageBox.Show(mostrarTotalSeccionesB);
                // MessageBox.Show(mostrarTotalEstudiantesB);

                /*Cargar array ciclo superior*/
                string pruebaTotalSeccionesS = "";
                string pruebaTotalEstudiantesS = "";
                string mostrarTotalSeccionesS = "";
                string mostrarTotalEstudiantesS = "";

                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios ushuaia que es 12
                {
                    for (int j = 18; j <= 40; j = j + 2)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 18 - 2(16) y se suma de 2 en 2
                    {

                        uTotalesCicloSuperior[i, 0] = uTotalesCicloSuperior[i, 0] + colegiosUshuaiaSyE[i, j];//cantidad de las secciones
                        uTotalesCicloSuperior[i, 1] = uTotalesCicloSuperior[i, 1] + colegiosUshuaiaSyE[i, j + 1];//cantidad de las estudiantes
                        pruebaTotalSeccionesS = Convert.ToString(uTotalesCicloSuperior[i, 0]);
                        pruebaTotalEstudiantesS = Convert.ToString(uTotalesCicloSuperior[i, 1]);
                    }
                    mostrarTotalSeccionesS = mostrarTotalSeccionesS + " " + pruebaTotalSeccionesS;

                    mostrarTotalEstudiantesS = mostrarTotalEstudiantesS + " " + pruebaTotalEstudiantesS;
                }
                // MessageBox.Show(mostrarTotalSeccionesS);
                //MessageBox.Show(mostrarTotalEstudiantesS);


                /*Cargar array suma 2 ciclos*/
                string pruebaTotales = "";

                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios ushuaia que es 12
                {

                    uTotal2Ciclos[i, 0] = uTotalesCicloBasico[i, 0] + uTotalesCicloSuperior[i, 0];//suma todas las secciones de un colegio
                    uTotal2Ciclos[i, 1] = uTotalesCicloBasico[i, 1] + uTotalesCicloSuperior[i, 1];//suma todos los estudiantes de un colegio

                    pruebaTotales = pruebaTotales + " " + Convert.ToString(uTotal2Ciclos[i, 0]) + " " + Convert.ToString(uTotal2Ciclos[i, 1]);
                }
                //MessageBox.Show(pruebaTotales);

                /*Cargar array subtotal turnos ushuaia*/
                string pruebaTurnosTotales = "";
                int contadorColumnasTurnos;//es necesaria
                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios ushuaia que es 12
                {
                    contadorColumnasTurnos = 0;
                    for (int j = 0; j <= 40; j = j + 2)
                    {
                        uSubtotalTurnos[0, contadorColumnasTurnos] = uSubtotalTurnos[0, contadorColumnasTurnos] + colegiosUshuaiaSyE[i, j];//suma todas las secciones por turno de un colegio
                        uSubtotalTurnos[1, contadorColumnasTurnos] = uSubtotalTurnos[1, contadorColumnasTurnos] + colegiosUshuaiaSyE[i, j + 1];//suma todos los estudiantes por turno de un colegio
                        contadorColumnasTurnos++;
                    }
                }
                for (int i = 0; i <= 20; i++)//solo es para probar
                {
                    pruebaTurnosTotales = pruebaTurnosTotales + " " + Convert.ToString(uSubtotalTurnos[0, i]) + " " + Convert.ToString(uSubtotalTurnos[1, i]);

                }
                //MessageBox.Show(pruebaTurnosTotales);
                /*Cargar array subtotales de secciones y estudiantes de ushuaia*/
                string pruebaSubTotales = "";
                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)//mientras i sea menor o igual al numero de colegios ushuaia que es 12
                {
                    uSubtotal[0, 0] = uSubtotal[0, 0] + uTotalesCicloBasico[i, 0];//suma todas las secciones del ciclo basico de ushauia 
                    uSubtotal[1, 0] = uSubtotal[1, 0] + uTotalesCicloBasico[i, 1];//suma todos los estudiantes del ciclo basico de ushauia
                    uSubtotal[0, 1] = uSubtotal[0, 1] + uTotalesCicloSuperior[i, 0];//suma todas las secciones del ciclo Superior de ushauia 
                    uSubtotal[1, 1] = uSubtotal[1, 1] + uTotalesCicloSuperior[i, 1];//suma todos los estudiantes del ciclo superoir de ushauia
                    uSubtotal[0, 2] = uSubtotal[0, 2] + uTotal2Ciclos[i, 0];//suma todas las secciones de ushauia 
                    uSubtotal[1, 2] = uSubtotal[1, 2] + uTotal2Ciclos[i, 1];//suma todos los estudiantes de ushauia
                }
                for (int i = 0; i <= 2; i++)//solo es para probar
                {
                    pruebaSubTotales = pruebaSubTotales + " " + Convert.ToString(uSubtotal[0, i]) + " " + Convert.ToString(uSubtotal[1, i]);

                }
                //MessageBox.Show(pruebaSubTotales);

                //##arrays Rio grande
                /*Cargar array ciclo basico*/
                string gPruebaTotalSeccionesB = "";
                string gPruebaTotalEstudiantesB = "";
                string gMostrarTotalSeccionesB = "";
                string gMostrarTotalEstudiantesB = "";

                for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios grande que es 14
                {
                    for (int j = 0; j <= 16; j = j + 2)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 18 - 2(16) y se suma de 2 en 2
                    {

                        gTotalesCicloBasico[i, 0] = gTotalesCicloBasico[i, 0] + colegiosGrandeSyE[i, j];//cantidad de las secciones
                        gTotalesCicloBasico[i, 1] = gTotalesCicloBasico[i, 1] + colegiosGrandeSyE[i, j + 1];//cantidad de las estudiantes
                        gPruebaTotalSeccionesB = Convert.ToString(gTotalesCicloBasico[i, 0]);
                        gPruebaTotalEstudiantesB = Convert.ToString(gTotalesCicloBasico[i, 1]);
                    }
                    gMostrarTotalSeccionesB = gMostrarTotalSeccionesB + " " + gPruebaTotalSeccionesB;

                    gMostrarTotalEstudiantesB = gMostrarTotalEstudiantesB + " " + gPruebaTotalEstudiantesB;
                }
                // MessageBox.Show(gMostrarTotalSeccionesB);
                //MessageBox.Show(gMostrarTotalEstudiantesB);

                /*Cargar array ciclo superior*/
                string gPruebaTotalSeccionesS = "";
                string gPruebaTotalEstudiantesS = "";
                string gMostrarTotalSeccionesS = "";
                string gMostrarTotalEstudiantesS = "";

                for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios grande que es 14
                {
                    for (int j = 18; j <= 40; j = j + 2)//mientras j sea menor o igual al numero de secciones en el primer ciclo que es 18 - 2(16) y se suma de 2 en 2
                    {

                        gTotalesCicloSuperior[i, 0] = gTotalesCicloSuperior[i, 0] + colegiosGrandeSyE[i, j];//cantidad de las secciones
                        gTotalesCicloSuperior[i, 1] = gTotalesCicloSuperior[i, 1] + colegiosGrandeSyE[i, j + 1];//cantidad de las estudiantes
                        gPruebaTotalSeccionesS = Convert.ToString(gTotalesCicloSuperior[i, 0]);
                        gPruebaTotalEstudiantesS = Convert.ToString(gTotalesCicloSuperior[i, 1]);
                    }
                    gMostrarTotalSeccionesS = gMostrarTotalSeccionesS + " " + gPruebaTotalSeccionesS;

                    gMostrarTotalEstudiantesS = gMostrarTotalEstudiantesS + " " + gPruebaTotalEstudiantesS;
                }
                //MessageBox.Show(gMostrarTotalSeccionesS);
                //MessageBox.Show(gMostrarTotalEstudiantesS);


                /*Cargar array suma 2 ciclos*/
                string gPruebaTotales = "";

                for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios grande que es 14
                {

                    gTotal2Ciclos[i, 0] = gTotalesCicloBasico[i, 0] + gTotalesCicloSuperior[i, 0];//suma todas las secciones de un colegio
                    gTotal2Ciclos[i, 1] = gTotalesCicloBasico[i, 1] + gTotalesCicloSuperior[i, 1];//suma todos los estudiantes de un colegio

                    gPruebaTotales = gPruebaTotales + " " + Convert.ToString(gTotal2Ciclos[i, 0]) + " " + Convert.ToString(gTotal2Ciclos[i, 1]);
                }
                //MessageBox.Show(gPruebaTotales);

                /*Cargar array subtotal turnos grande*/
                string gPruebaTurnosTotales = "";
                int gContadorColumnasTurnos;//es necesaria
                for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios grande que es 14
                {
                    gContadorColumnasTurnos = 0;
                    for (int j = 0; j <= 40; j = j + 2)
                    {
                        gSubtotalTurnos[0, gContadorColumnasTurnos] = gSubtotalTurnos[0, gContadorColumnasTurnos] + colegiosGrandeSyE[i, j];//suma todas las secciones por turno de un colegio
                        gSubtotalTurnos[1, gContadorColumnasTurnos] = gSubtotalTurnos[1, gContadorColumnasTurnos] + colegiosGrandeSyE[i, j + 1];//suma todos los estudiantes por turno de un colegio
                        gContadorColumnasTurnos++;
                    }
                }
                for (int i = 0; i <= 20; i++)//solo es para probar
                {
                    gPruebaTurnosTotales = gPruebaTurnosTotales + " " + Convert.ToString(gSubtotalTurnos[0, i]) + " " + Convert.ToString(gSubtotalTurnos[1, i]);

                }
                //MessageBox.Show(gPruebaTurnosTotales);
                /*Cargar array subtotales de secciones y estudiantes de grande*/
                string gPruebaSubTotales = "";
                for (int i = 0; i <= cantColegiosGrande - 1; i++)//mientras i sea menor o igual al numero de colegios grande que es 14
                {
                    gSubtotal[0, 0] = gSubtotal[0, 0] + gTotalesCicloBasico[i, 0];//suma todas las secciones del ciclo basico de ushauia 
                    gSubtotal[1, 0] = gSubtotal[1, 0] + gTotalesCicloBasico[i, 1];//suma todos los estudiantes del ciclo basico de ushauia
                    gSubtotal[0, 1] = gSubtotal[0, 1] + gTotalesCicloSuperior[i, 0];//suma todas las secciones del ciclo Superior de ushauia 
                    gSubtotal[1, 1] = gSubtotal[1, 1] + gTotalesCicloSuperior[i, 1];//suma todos los estudiantes del ciclo superoir de ushauia
                    gSubtotal[0, 2] = gSubtotal[0, 2] + gTotal2Ciclos[i, 0];//suma todas las secciones de ushauia 
                    gSubtotal[1, 2] = gSubtotal[1, 2] + gTotal2Ciclos[i, 1];//suma todos los estudiantes de ushauia
                }
                for (int i = 0; i <= 2; i++)//solo es para probar
                {
                    gPruebaSubTotales = gPruebaSubTotales + " " + Convert.ToString(gSubtotal[0, i]) + " " + Convert.ToString(gSubtotal[1, i]);

                }
                //MessageBox.Show(gPruebaSubTotales);

                //##arrays totales jurisdiccionales
                for (int i = 0; i <= 20; i++)
                {
                    totalJurisSecyEstudiates[0, i] = uSubtotalTurnos[0, i] + gSubtotalTurnos[0, i];
                    totalJurisSecyEstudiates[1, i] = uSubtotalTurnos[1, i] + gSubtotalTurnos[1, i];
                }
                //Totales generales
                totalJurisdicional[0, 0] = uSubtotal[0, 0] + gSubtotal[0, 0];
                totalJurisdicional[1, 0] = uSubtotal[1, 0] + gSubtotal[1, 0];
                totalJurisdicional[0, 1] = uSubtotal[0, 1] + gSubtotal[0, 1];
                totalJurisdicional[1, 1] = uSubtotal[1, 1] + gSubtotal[1, 1];
                totalJurisdicional[0, 2] = uSubtotal[0, 2] + gSubtotal[0, 2];
                totalJurisdicional[1, 2] = uSubtotal[1, 2] + gSubtotal[1, 2];
                //###por año
                //subtotales ushuaia por años
                int counter = 0;
                for (int i = 0; i <= 20; i = i + 3)
                {
                    uSubtotalAños[0, counter] = uSubtotalTurnos[0, i] + uSubtotalTurnos[0, i + 1] + uSubtotalTurnos[0, i + 2];
                    uSubtotalAños[1, counter] = uSubtotalTurnos[1, i] + uSubtotalTurnos[1, i + 1] + uSubtotalTurnos[1, i + 2];
                    counter++;
                }
                //subtotales grande por años
                counter = 0;
                for (int i = 0; i <= 20; i = i + 3)
                {
                    gSubtotalAños[0, counter] = gSubtotalTurnos[0, i] + gSubtotalTurnos[0, i + 1] + gSubtotalTurnos[0, i + 2];
                    gSubtotalAños[1, counter] = gSubtotalTurnos[1, i] + gSubtotalTurnos[1, i + 1] + gSubtotalTurnos[1, i + 2];
                    counter++;
                }
                //Totales TDF por años
                counter = 0;
                for (int i = 0; i <= 20; i = i + 3)
                {
                    totalJurisdicionalAños[0, counter] = totalJurisSecyEstudiates[0, i] + totalJurisSecyEstudiates[0, i + 1] + totalJurisSecyEstudiates[0, i + 2];
                    totalJurisdicionalAños[1, counter] = totalJurisSecyEstudiates[1, i] + totalJurisSecyEstudiates[1, i + 1] + totalJurisSecyEstudiates[1, i + 2];
                    counter++;
                }
                lblProcesando.Visible = false;
                MessageBox.Show("Estadistica generada.", "Sistema Informa");
                btnCrearExcel.Enabled = true; 
            }
        }
        private void btnCrearExcel_Click(object sender, EventArgs e)
        {
            lblCreando.Visible = true;
            Refresh();
            //creando Estadisticas
            SLDocument sl = new SLDocument();
            string pathFile = @"C:\Users\Pablo\Downloads\Prueba\Estadisticas Secciones y Estudiantes " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString() + ".xlsx";
            //string pathFile = @"C:\Users\Arteok\Downloads\Prueba\Estadisticas Secciones y Estudiantes " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString() + ".xlsx";

            //string pathFile = @"C:\Users\Arteok\Downloads\Prueba\Estadisticas Secciones y Estudiantes.xlsx";

            sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Secciones y estudiantes");//renombra la Hoja

            tituloEstadistica = "CANTIDAD DE SECCIONES Y ESTUDIANTES - CICLO " + cboxAño.SelectedItem.ToString() + " " + (cboxPeriodo.SelectedItem.ToString()).ToUpper() + " - COLEGIOS PUBLICOS DE GESTION PUBLICA";

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
            styleFondoGris.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Gainsboro, System.Drawing.Color.White);

            SLStyle styleFondoCrema = sl.CreateStyle();//Crea el fondo Cremita
            styleFondoCrema.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Wheat, System.Drawing.Color.White);

            SLStyle styleFondoCremaCeleste = sl.CreateStyle();//Crea el fondo Cremita CELESTE
            styleFondoCremaCeleste.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Lavender, System.Drawing.Color.White);

            SLStyle styleFondoCremaVerde = sl.CreateStyle();//Crea el fondo Cremita verde
            styleFondoCremaVerde.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.Khaki, System.Drawing.Color.White);

            SLStyle styleFondoCremaNaranja = sl.CreateStyle();//Crea el fondo Cremita Naranja
            styleFondoCremaNaranja.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.NavajoWhite, System.Drawing.Color.White);

            //FONDOSUSHUAIA
            //Titulo FONDO  USHUAIA
            sl.SetCellStyle(1, 1, 1, 40, styleFondoAzulGray);//asigna fondo azul a las entre las siguientes celdas
            //Subtitulo FONDO
            sl.SetCellStyle(2, 3, 2, 38, styleFondoNaranja);
            //secciones fondo
            sl.SetCellStyle(3, 3, 3, 38, styleFondoAzul);
            //años
            sl.SetCellStyle("C4", "T4", styleFondoVerde);
            sl.SetCellStyle("W4", "AT4", styleFondoVerde);
            //fondo de turnos
            sl.SetCellStyle("C5", "T5", styleFondoCremaVerde);
            //fondo S y E
            sl.SetCellStyle("AW6", styleFondoCeleste);
            sl.SetCellStyle("AX6", styleFondoGris);
            //fondo depto y establecimiento
            sl.SetCellStyle("A2", "B5", styleFondoAzulGray);
            //fondo totales           
            sl.SetCellStyle("AW2", "AX5", styleFondoAzulGray);
            //Fondos total secciones y estudiantes
            sl.SetCellStyle("U2", "U6", styleFondoNaranja);
            sl.SetCellStyle("V2", "V6", styleFondoNaranja);
            sl.SetCellStyle("AU2", "AU6", styleFondoNaranja);
            sl.SetCellStyle("AV2", "AV6", styleFondoNaranja);
            ////fondos a trasbajar///////


            //fondo ushuaia
            sl.SetCellStyle("A7", styleFondoCeleste);//Fondo colegios ushuaia celeste
            //subtotal S y Estudiantes
            sl.SetCellStyle(7 + cantColegiosUshuaia, 1, 7 + cantColegiosUshuaia, 2, styleFondoVerde);
            sl.SetCellStyle(8 + cantColegiosUshuaia, 1, 8 + cantColegiosUshuaia, 2, styleFondoVerde);
            sl.SetCellStyle(9 + cantColegiosUshuaia, 1, 9 + cantColegiosUshuaia, 2, styleFondoAzul);
            sl.SetCellStyle(10 + cantColegiosUshuaia, 1, 10 + cantColegiosUshuaia, 2, styleFondoAzul);
            //total secciones
            sl.SetCellStyle(7, 21, 7 + cantColegiosUshuaia - 1, 22, styleFondoGris);//asigna fondo crema naranja a las entre las siguientes celdas
            sl.SetCellStyle(7, 47, 7 + cantColegiosUshuaia - 1, 48, styleFondoGris);//asigna fondo crema naranja a las entre las siguientes celdas
            sl.SetCellStyle(7, 49, 7 + cantColegiosUshuaia - 1, 50, styleFondoCremaNaranja);//asigna fondo crema naranja a las entre las siguientes celdas
            //subtotal ushuaia ciclos
            sl.SetCellStyle(7 + cantColegiosUshuaia, 3, 7 + cantColegiosUshuaia, 20, styleFondoCremaVerde);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(8 + cantColegiosUshuaia, 3, 8 + cantColegiosUshuaia, 20, styleFondoCremaVerde);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(7 + cantColegiosUshuaia, 23, 7 + cantColegiosUshuaia, 46, styleFondoCremaVerde);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(8 + cantColegiosUshuaia, 23, 8 + cantColegiosUshuaia, 46, styleFondoCremaVerde);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(9 + cantColegiosUshuaia, 3, 9 + cantColegiosUshuaia, 20, styleFondoCeleste);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(10 + cantColegiosUshuaia, 3, 10 + cantColegiosUshuaia, 20, styleFondoCeleste);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(9 + cantColegiosUshuaia, 23, 9 + cantColegiosUshuaia, 46, styleFondoCeleste);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(10 + cantColegiosUshuaia, 23, 10 + cantColegiosUshuaia, 46, styleFondoCeleste);//asigna fondo crema celeste a las entre las siguientes celdas

            //subtotales generales ushuaia
            sl.SetCellStyle(7 + cantColegiosUshuaia, 21, styleFondoVerde);//asigna fondo verde a la siguiente celda
            sl.SetCellStyle(9 + cantColegiosUshuaia, 21, styleFondoAzul);//asigna fondo Azul a la siguiente celda
            sl.SetCellStyle(7 + cantColegiosUshuaia, 47, styleFondoVerde);//asigna fondo verde a la siguiente celda
            sl.SetCellStyle(9 + cantColegiosUshuaia, 47, styleFondoAzul);//asigna fondo azul a a la siguiente celda
            sl.SetCellStyle(7 + cantColegiosUshuaia, 49, styleFondoNaranja);//asigna fondo naranja a la siguiente celda
            sl.SetCellStyle(9 + cantColegiosUshuaia, 49, styleFondoAzulGray);//asigna fondo azul oscuro a a la siguiente celda

            //Fondos Rio Grande
            //Rio grande

            sl.SetCellStyle(12 + cantColegiosUshuaia, 1, styleFondoCeleste);
            //subtotal S y Estudiantes
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 1, 12 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoVerde);
            sl.SetCellStyle(13 + cantColegiosUshuaia + cantColegiosGrande, 1, 13 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoVerde);
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 1, 14 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoAzul);
            sl.SetCellStyle(15 + cantColegiosUshuaia + cantColegiosGrande, 1, 15 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoAzul);

            //total secciones
            sl.SetCellStyle(12 + cantColegiosUshuaia, 21, 12 + cantColegiosUshuaia + cantColegiosGrande - 1, 22, styleFondoGris);//asigna fondo crema naranja a las entre las siguientes celdas
            sl.SetCellStyle(12 + cantColegiosUshuaia, 47, 12 + cantColegiosUshuaia + cantColegiosGrande - 1, 48, styleFondoGris);//asigna fondo crema naranja a las entre las siguientes celdas
            sl.SetCellStyle(12 + cantColegiosUshuaia, 49, 12 + cantColegiosUshuaia + cantColegiosGrande - 1, 50, styleFondoCremaNaranja);//asigna fondo crema naranja a las entre las siguientes celdas


            //subtotal ciclos Rio grande
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 3, 12 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCremaVerde);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(13 + cantColegiosUshuaia + cantColegiosGrande, 3, 13 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCremaVerde);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 23, 12 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCremaVerde);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(13 + cantColegiosUshuaia + cantColegiosGrande, 23, 13 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCremaVerde);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 3, 14 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCeleste);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(15 + cantColegiosUshuaia + cantColegiosGrande, 3, 15 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCeleste);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 23, 14 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCeleste);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(15 + cantColegiosUshuaia + cantColegiosGrande, 23, 15 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCeleste);//asigna fondo crema celeste a las entre las siguientes celdas
            //subtotales generales Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 21, styleFondoVerde);//asigna fondo verde a la siguiente celda
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 21, styleFondoAzul);//asigna fondo Azul a la siguiente celda
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 47, styleFondoVerde);//asigna fondo verde a la siguiente celda
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 47, styleFondoAzul);//asigna fondo azul a a la siguiente celda
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 49, styleFondoNaranja);//asigna fondo naranja a la siguiente celda
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 49, styleFondoAzulGray);//asigna fondo azul oscuro a a la siguiente celda

            //Fondos Total jurisdiccional
            //Total jurisdiccional S y Estudiantes
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 1, 17 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoVerde);
            sl.SetCellStyle(18 + cantColegiosUshuaia + cantColegiosGrande, 1, 18 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoVerde);
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 1, 19 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoAzul);
            sl.SetCellStyle(20 + cantColegiosUshuaia + cantColegiosGrande, 1, 20 + cantColegiosUshuaia + cantColegiosGrande, 2, styleFondoAzul);

            //subtotal TDF ciclos
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 3, 17 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCremaVerde);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(18 + cantColegiosUshuaia + cantColegiosGrande, 3, 18 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCremaVerde);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 23, 17 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCremaVerde);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(18 + cantColegiosUshuaia + cantColegiosGrande, 23, 18 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCremaVerde);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 3, 19 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCeleste);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(20 + cantColegiosUshuaia + cantColegiosGrande, 3, 20 + cantColegiosUshuaia + cantColegiosGrande, 20, styleFondoCeleste);//asigna fondo crema celeste a las entre las siguientes celdas
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 23, 19 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCeleste);//asigna fondo crema verde a las entre las siguientes celdas
            sl.SetCellStyle(20 + cantColegiosUshuaia + cantColegiosGrande, 23, 20 + cantColegiosUshuaia + cantColegiosGrande, 46, styleFondoCeleste);//asigna fondo crema celeste a las entre las siguientes celdas

            //subtotales generales TDF
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 21, styleFondoVerde);//asigna fondo verde a la siguiente celda
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 21, styleFondoAzul);//asigna fondo Azul a la siguiente celda
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 47, styleFondoVerde);//asigna fondo verde a la siguiente celda
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 47, styleFondoAzul);//asigna fondo azul a a la siguiente celda
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 49, styleFondoNaranja);//asigna fondo naranja a la siguiente celda
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 49, styleFondoAzulGray);//asigna fondo azul oscuro a a la siguiente celda

            //BORDERS#########//falta
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
            SLStyle styleBorderBasicRight = sl.CreateStyle();
            styleBorderBasicRight.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
            SLStyle styleBorderBasicBottom = sl.CreateStyle();
            styleBorderBasicBottom.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);

            //BORDERS#########
            //bordes horizontales y verticales normales ushuaia                 
            sl.SetCellStyle(1, 1, 10 + cantColegiosUshuaia, 50, styleBorderBasicBottom);
            sl.SetCellStyle(1, 1, 10 + cantColegiosUshuaia, 50, styleBorderBasicRight);

            //bordes horizontales y verticales normales Rio grande            
            sl.SetCellStyle(11 + cantColegiosUshuaia, 1, 15 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBasicBottom);
            sl.SetCellStyle(12 + cantColegiosUshuaia, 1, 15 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBasicRight);

            //bordes horizontales y verticales normales Totales Jurisdiccionales
            sl.SetCellStyle(16 + cantColegiosUshuaia + cantColegiosGrande, 1, 20 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBasicBottom);
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 1, 20 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBasicRight);

            //borders gruesos generales ushuaia
            sl.SetCellStyle(1, 1, 1, 50, styleBorderTM);//asigna el borde top
            sl.SetCellStyle(1, 1, 10 + cantColegiosUshuaia, 1, styleBorderLM);//asigna el borde left
            sl.SetCellStyle(1, 50, 10 + cantColegiosUshuaia, 50, styleBorderRM);//asigna el borde right
            sl.SetCellStyle(10 + cantColegiosUshuaia, 1, 10 + cantColegiosUshuaia, 50, styleBorderBM);//asigna el borde bottom            
            //borders gruesos generales Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 1, 12 + cantColegiosUshuaia, 50, styleBorderTM);//asigna el borde top
            sl.SetCellStyle(12 + cantColegiosUshuaia, 1, 15 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBorderLM);//asigna el borde left
            sl.SetCellStyle(12 + cantColegiosUshuaia, 50, 15 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderRM);//asigna el borde right
            sl.SetCellStyle(15 + cantColegiosUshuaia + cantColegiosGrande, 1, 15 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBM);//asigna el borde bottom 
            //borders gruesos generales Totales Jurisdicionales
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 1, 17 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderTM);//asigna el borde top
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 1, 20 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBorderLM);//asigna el borde left
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 50, 20 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderRM);//asigna el borde right
            sl.SetCellStyle(20 + cantColegiosUshuaia + cantColegiosGrande, 1, 20 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBM);//asigna el borde bottom 

            //Espacio dentro de las celdas########## falta
            //espacios Filas Ushuaia
            sl.SetRowHeight(1, 40);//titulo
            sl.SetRowHeight(2, 38);//ciclo
            sl.SetRowHeight(3, 32);//cantidad
            sl.SetRowHeight(4, 32);//año
            sl.SetRowHeight(5, 40);//turno

            sl.SetRowHeight(7 + cantColegiosUshuaia, 25);//subtotal ushuaia s
            sl.SetRowHeight(8 + cantColegiosUshuaia, 25);//subtotal ushuaia e
            sl.SetRowHeight(9 + cantColegiosUshuaia, 25);//subtotal ushuaia s
            sl.SetRowHeight(10 + cantColegiosUshuaia, 25);//subtotal ushuaia e

            //espacios Filas Rio grande 
            sl.SetRowHeight(12 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia s
            sl.SetRowHeight(13 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia e
            sl.SetRowHeight(14 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia e
            sl.SetRowHeight(15 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia e

            //espacios Filas totales jurisdiccionales
            sl.SetRowHeight(17 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia s
            sl.SetRowHeight(18 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia e
            sl.SetRowHeight(19 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia s
            sl.SetRowHeight(20 + cantColegiosUshuaia + cantColegiosGrande, 25);//subtotal ushuaia e

            //espacios columnas
            sl.SetColumnWidth(1, 12);//Depto
            sl.SetColumnWidth(2, 35);//Establecimiento

            for (int i = 3; i <= 19; i = i + 2)//espacios en S de 1 a 3 año
            {
                sl.SetColumnWidth(i, 5);
            }
            for (int i = 4; i <= 20; i = i + 2)//espacios en  de 1 a 3 año
            {
                sl.SetColumnWidth(i, 7);
            }
            for (int i = 23; i <= 45; i = i + 2)//espacios en S de 4 a 7 año
            {
                sl.SetColumnWidth(i, 5);
            }
            for (int i = 24; i <= 46; i = i + 2)//espacios en  de 4 a 7 año
            {
                sl.SetColumnWidth(i, 7);
            }

            sl.SetColumnWidth(21, 7);//espacios en total secciones y total estudiantes
            sl.SetColumnWidth(22, 7);
            sl.SetColumnWidth(47, 7);
            sl.SetColumnWidth(48, 7);

            sl.SetColumnWidth(49, 7);//espacios en totales
            sl.SetColumnWidth(50, 9);

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

            //Arial 16 Black vertical
            SLStyle styleBlack16_90G = sl.CreateStyle();
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
            sl.MergeWorksheetCells("A1", "AX1");//COMBINAR
            //SubTitulo
            sl.SetCellValue("C2", "CICLO BASICO"); //set value
            sl.SetCellStyle(2, 3, styleWhite18);//
            sl.MergeWorksheetCells("C2", "V2");//COMBINAR

            sl.SetCellValue("W2", "CICLO SUPERIOR"); //set value
            sl.SetCellStyle(2, 23, styleWhite18);//
            sl.MergeWorksheetCells("W2", "AV2");//COMBINAR
            //secciones
            sl.SetCellValue("C3", "Cantidad de Secciones y Estudiantes"); //set value
            sl.SetCellStyle("C3", styleWhite16);//
            sl.MergeWorksheetCells("C3", "T3");//COMBINAR

            sl.SetCellValue("W3", "Cantidad de Secciones y Estudiantes"); //set value
            sl.SetCellStyle("W3", styleWhite16);//
            sl.MergeWorksheetCells("W3", "AT3");//COMBINAR
            //AÑOS
            sl.SetCellValue("C4", "1ro"); //set value
            sl.SetCellStyle("C4", styleWhite16);//
            sl.MergeWorksheetCells("C4", "H4");//COMBINAR

            sl.SetCellValue("I4", "2do"); //set value
            sl.SetCellStyle("I4", styleWhite16);//
            sl.MergeWorksheetCells("I4", "N4");//COMBINAR

            sl.SetCellValue("O4", "3ro"); //set value
            sl.SetCellStyle("O4", styleWhite16);//
            sl.MergeWorksheetCells("O4", "T4");//COMBINAR

            sl.SetCellValue("W4", "4to"); //set value
            sl.SetCellStyle("W4", styleWhite16);//
            sl.MergeWorksheetCells("W4", "AB4");//COMBINAR

            sl.SetCellValue("AC4", "5to"); //set value
            sl.SetCellStyle("AC4", styleWhite16);//
            sl.MergeWorksheetCells("AC4", "AH4");//COMBINAR

            sl.SetCellValue("AI4", "6to"); //set value
            sl.SetCellStyle("AI4", styleWhite16);//
            sl.MergeWorksheetCells("AI4", "AN4");//COMBINAR

            sl.SetCellValue("AO4", "7mo"); //set value
            sl.SetCellStyle("AO4", styleWhite16);//
            sl.MergeWorksheetCells("AO4", "AT4");//COMBINAR
            //TURNOS
            sl.SetCellValue("C5", "TM"); //set value
            sl.SetCellStyle("C5", styleBlack14);//
            sl.MergeWorksheetCells("C5", "D5");//COMBINAR

            sl.SetCellValue("E5", "TT"); //set value
            sl.SetCellStyle("E5", styleBlack14);//
            sl.MergeWorksheetCells("E5", "F5");//COMBINAR

            sl.SetCellValue("G5", "TV"); //set value
            sl.SetCellStyle("G5", styleBlack14);//
            sl.MergeWorksheetCells("G5", "H5");//COMBINAR

            sl.CopyCell("C5", "I5");//copia el contenido de una celda
            sl.MergeWorksheetCells("I5", "J5");//COMBINAR
            sl.CopyCell("C5", "O5");//copia el contenido de una celda
            sl.MergeWorksheetCells("O5", "P5");//COMBINAR
            sl.CopyCell("C5", "W5");//copia el contenido de una celda
            sl.MergeWorksheetCells("W5", "X5");//COMBINAR
            sl.CopyCell("C5", "AC5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AC5", "AD5");//COMBINAR
            sl.CopyCell("C5", "AI5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AI5", "AJ5");//COMBINAR
            sl.CopyCell("C5", "AO5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AO5", "AP5");//COMBINAR

            sl.CopyCell("E5", "K5");//copia el contenido de una celda
            sl.MergeWorksheetCells("K5", "L5");//COMBINAR
            sl.CopyCell("E5", "Q5");//copia el contenido de una celda
            sl.MergeWorksheetCells("Q5", "R5");//COMBINAR
            sl.CopyCell("E5", "Y5");//copia el contenido de una celda
            sl.MergeWorksheetCells("Y5", "Z5");//COMBINAR
            sl.CopyCell("E5", "AE5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AE5", "AF5");//COMBINAR
            sl.CopyCell("E5", "AK5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AK5", "AL5");//COMBINAR
            sl.CopyCell("E5", "AQ5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AQ5", "AR5");//COMBINAR

            sl.CopyCell("G5", "M5");//copia el contenido de una celda
            sl.MergeWorksheetCells("M5", "N5");//COMBINAR
            sl.CopyCell("G5", "S5");//copia el contenido de una celda
            sl.MergeWorksheetCells("S5", "T5");//COMBINAR
            sl.CopyCell("G5", "AA5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AA5", "AB5");//COMBINAR
            sl.CopyCell("G5", "AG5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AG5", "AH5");//COMBINAR
            sl.CopyCell("G5", "AM5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AM5", "AN5");//COMBINAR
            sl.CopyCell("G5", "AS5");//copia el contenido de una celda
            sl.MergeWorksheetCells("AS5", "AT5");//COMBINAR

            //S y E
            sl.SetCellValue("AW6", "S"); //set value
            sl.SetCellStyle("AW6", styleBlack14);
            sl.SetCellValue("AX6", "E"); //set value
            sl.SetCellStyle("AX6", styleBlack14);

            for (int i = 3; i <= 19; i = i + 2)
            {
                sl.CopyCell(6, 49, 6, i);
                for (int j = 4; j <= 20; j = j + 2)
                {
                    sl.CopyCell(6, 50, 6, j);
                }
            }
            for (int i = 23; i <= 45; i = i + 2)
            {
                sl.CopyCell(6, 49, 6, i);
                for (int j = 24; j <= 46; j = j + 2)
                {
                    sl.CopyCell(6, 50, 6, j);
                }
            }
            //Depto y establecimiento
            sl.SetCellValue("A2", "Depto"); //set value
            sl.SetCellStyle("A2", styleWhite18);
            sl.MergeWorksheetCells("A2", "A6");//COMBINAR
            sl.SetCellValue("B2", "Establecimiento"); //set value
            sl.SetCellStyle("B2", styleWhite18);
            sl.MergeWorksheetCells("B2", "B6");//COMBINAR
            //totales
            sl.SetCellValue("AW2", "TOTALES"); //set value
            sl.SetCellStyle("AW2", styleWhite18_90G);
            sl.MergeWorksheetCells("AW2", "AX5");//COMBINAR
            //Total secciones y estudiantes
            sl.SetCellValue("U3", "Total Secciones"); //set value
            sl.SetCellStyle("U3", styleWhite14_90G);
            sl.MergeWorksheetCells("U3", "U6");//COMBINAR
            sl.SetCellValue("V3", "Total Estudiantes"); //set value
            sl.SetCellStyle("V3", styleWhite14_90G);
            sl.MergeWorksheetCells("V3", "V6");//COMBINAR
            sl.SetCellValue("AU3", "Total Secciones"); //set value
            sl.SetCellStyle("AU3", styleWhite14_90G);
            sl.MergeWorksheetCells("AU3", "AU6");//COMBINAR
            sl.SetCellValue("AV3", "Total Estudiantes"); //set value
            sl.SetCellStyle("AV3", styleWhite14_90G);
            sl.MergeWorksheetCells("AV3", "AV6");//COMBINAR
            //ushuaia
            sl.SetCellValue("A7", "Ushuaia"); //set value
            sl.SetCellStyle("A7", styleBlack16_90G);
            sl.MergeWorksheetCells(7, 1, 6 + cantColegiosUshuaia, 1);//COMBINAR

            //subtotal S y Estudiantes
            sl.SetCellValue(7 + cantColegiosUshuaia, 1, "Subtotal Ushuaia Secciones"); //set value
            sl.SetCellStyle(7 + cantColegiosUshuaia, 1, styleWhite14);
            sl.MergeWorksheetCells(7 + cantColegiosUshuaia, 1, 7 + cantColegiosUshuaia, 2);//COMBINAR
            sl.SetCellValue(8 + cantColegiosUshuaia, 1, "Subtotal Secciones por Año"); //set value
            sl.SetCellStyle(8 + cantColegiosUshuaia, 1, styleWhite14);
            sl.MergeWorksheetCells(8 + cantColegiosUshuaia, 1, 8 + cantColegiosUshuaia, 2);//COMBINAR
            sl.SetCellValue(9 + cantColegiosUshuaia, 1, "Subtotal Ushuaia Estudiantes"); //set value
            sl.SetCellStyle(9 + cantColegiosUshuaia, 1, styleWhite14);
            sl.MergeWorksheetCells(9 + cantColegiosUshuaia, 1, 9 + cantColegiosUshuaia, 2);//COMBINAR*/
            sl.SetCellValue(10 + cantColegiosUshuaia, 1, "Subtotal Estudiantes por Año"); //set value
            sl.SetCellStyle(10 + cantColegiosUshuaia, 1, styleWhite14);
            sl.MergeWorksheetCells(10 + cantColegiosUshuaia, 1, 10 + cantColegiosUshuaia, 2);//COMBINAR*/

            //Nombre de los establecimientos Ushuaia
            for (int i = 7; i <= 7 + cantColegiosUshuaia - 1; i++)
            {
                sl.SetCellValue(i, 2, ushuaiaColegios[0, i - 7]); //set value
                sl.SetCellStyle(i, 2, styleBlack14);
            }
            //completa ciclo basico ushuaia
            for (int i = 7; i <= 7 + cantColegiosUshuaia - 1; i++)
            {
                for (int j = 3; j <= 20; j++)
                {
                    if (colegiosUshuaiaSyE[i - 7, j - 3] == 0)//si el valor es 0 le pone un guion
                    {
                        sl.SetCellValue(i, j, "-"); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                    else//si tiene valor, lo pone
                    {
                        sl.SetCellValue(i, j, colegiosUshuaiaSyE[i - 7, j - 3]); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                }
            }
            //completa ciclo superior ushuaia           
            for (int i = 7; i <= 7 + cantColegiosUshuaia - 1; i++)
            {
                for (int j = 23; j <= 46; j++)
                {
                    if (colegiosUshuaiaSyE[i - 7, j - 5] == 0)//si el valor es 0 le pone un guion
                    {
                        sl.SetCellValue(i, j, "-"); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                    else//si tiene valor, lo pone
                    {
                        //sl.SetCellValue(i, j, Convert.ToString(j+i)); //set value
                        sl.SetCellValue(i, j, colegiosUshuaiaSyE[i - 7, j - 5]); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                }
            }

            //completa totales ciclo basico ushuaia           
            for (int i = 7; i <= 7 + cantColegiosUshuaia - 1; i++)//7 porque empieza en la fila 7
            {
                for (int j = 21; j <= 22; j++)//21 porque empieza en la columna 21
                {
                    sl.SetCellValue(i, j, uTotalesCicloBasico[i - 7, j - 21]); //set value
                    sl.SetCellStyle(i, j, styleBlue14);
                }
            }
            //completa totales ciclo superior ushuaia           
            for (int i = 7; i <= 7 + cantColegiosUshuaia - 1; i++)//7 porque empieza en la fila 7
            {
                for (int j = 47; j <= 48; j++)//47 porque empieza en la columna 47
                {
                    sl.SetCellValue(i, j, uTotalesCicloSuperior[i - 7, j - 47]); //set value
                    sl.SetCellStyle(i, j, styleBlue14);
                }
            }
            //completa totales ushuaia           
            for (int i = 7; i <= 7 + cantColegiosUshuaia - 1; i++)//7 porque empieza en la fila 7
            {
                for (int j = 49; j <= 50; j++)//49 porque empieza en la columna 49
                {
                    sl.SetCellValue(i, j, uTotal2Ciclos[i - 7, j - 49]); //set value
                    sl.SetCellStyle(i, j, styleBlack14);
                }
            }

            //completa subtotal ushuaia   
            for (int i = 7 + cantColegiosUshuaia; i <= 8 + cantColegiosUshuaia; i++)
            {
                if (i == 7 + cantColegiosUshuaia)
                {
                    for (int j = 0; j <= 20; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i, j + 3, uSubtotalTurnos[i - (7 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, j + 3, i, j + 3 + 1);//COMBINAR
                        }
                        else if (j >= 9)
                        {
                            sl.SetCellValue(i, (j * 2) + 5, uSubtotalTurnos[i - (7 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i, (j * 2) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 2) + 5, i, (j * 2) + 5 + 1);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i, (j * 2) + 3, uSubtotalTurnos[i - (7 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i, (j * 2) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 2) + 3, i, (j * 2) + 3 + 1);//COMBINAR
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= 20; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i + 1, j + 3, uSubtotalTurnos[i - (7 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i + 1, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, j + 3, i + 1, j + 3 + 1);//COMBINAR
                        }
                        else if (j >= 9)
                        {
                            sl.SetCellValue(i + 1, (j * 2) + 5, uSubtotalTurnos[i - (7 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i + 1, (j * 2) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 2) + 5, i + 1, (j * 2) + 5 + 1);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i + 1, (j * 2) + 3, uSubtotalTurnos[i - (7 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i + 1, (j * 2) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 2) + 3, i + 1, (j * 2) + 3 + 1);//COMBINAR
                        }
                    }
                }
            }

            //completa subtotal ushuaia por año            
            for (int i = 8 + cantColegiosUshuaia; i <= 9 + cantColegiosUshuaia; i++)
            {
                if (i == 8 + cantColegiosUshuaia)
                {
                    for (int j = 0; j <= 6; j++)//3 porque empieza en la columna 3 y suma de a 6 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i, j + 3, uSubtotalAños[i - (8 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, j + 3, i, j + 3 + 5);//COMBINAR
                        }
                        else if (j >= 3)
                        {
                            sl.SetCellValue(i, (j * 6) + 5, uSubtotalAños[i - (8 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i, (j * 6) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 6) + 5, i, (j * 6) + 5 + 5);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i, (j * 6) + 3, uSubtotalAños[i - (8 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i, (j * 6) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 6) + 3, i, (j * 6) + 3 + 5);//COMBINAR
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= 6; j++)//3 porque empieza en la columna 3 y suma de a 6 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i + 1, j + 3, uSubtotalAños[i - (8 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i + 1, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, j + 3, i + 1, j + 3 + 5);//COMBINAR
                        }
                        else if (j >= 3)
                        {
                            sl.SetCellValue(i + 1, (j * 6) + 5, uSubtotalAños[i - (8 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i + 1, (j * 6) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 6) + 5, i + 1, (j * 6) + 5 + 5);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i + 1, (j * 6) + 3, uSubtotalAños[i - (8 + cantColegiosUshuaia), j]);
                            sl.SetCellStyle(i + 1, (j * 6) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 6) + 3, i + 1, (j * 6) + 3 + 5);//COMBINAR
                        }
                    }
                }
            }

            //completa SubTotales Ushuaia             
            sl.SetCellValue(7 + cantColegiosUshuaia, 21, uSubtotal[0, 0]);
            sl.SetCellStyle(7 + cantColegiosUshuaia, 21, styleWhite14);
            sl.MergeWorksheetCells(7 + cantColegiosUshuaia, 21, 8 + cantColegiosUshuaia, 22);//COMBINAR
            sl.SetCellValue(7 + cantColegiosUshuaia, 47, uSubtotal[0, 1]);
            sl.SetCellStyle(7 + cantColegiosUshuaia, 47, styleWhite14);
            sl.MergeWorksheetCells(7 + cantColegiosUshuaia, 47, 8 + cantColegiosUshuaia, 48);//COMBINAR
            sl.SetCellValue(7 + cantColegiosUshuaia, 49, uSubtotal[0, 2]);
            sl.SetCellStyle(7 + cantColegiosUshuaia, 49, styleWhite14);
            sl.MergeWorksheetCells(7 + cantColegiosUshuaia, 49, 8 + cantColegiosUshuaia, 50);//COMBINAR
            sl.SetCellValue(9 + cantColegiosUshuaia, 21, uSubtotal[1, 0]);
            sl.SetCellStyle(9 + cantColegiosUshuaia, 21, styleWhite14);
            sl.MergeWorksheetCells(9 + cantColegiosUshuaia, 21, 10 + cantColegiosUshuaia, 22);//COMBINAR
            sl.SetCellValue(9 + cantColegiosUshuaia, 47, uSubtotal[1, 1]);
            sl.SetCellStyle(9 + cantColegiosUshuaia, 47, styleWhite14);
            sl.MergeWorksheetCells(9 + cantColegiosUshuaia, 47, 10 + cantColegiosUshuaia, 48);//COMBINAR
            sl.SetCellValue(9 + cantColegiosUshuaia, 49, uSubtotal[1, 2]);
            sl.SetCellStyle(9 + cantColegiosUshuaia, 49, styleWhite14);
            sl.MergeWorksheetCells(9 + cantColegiosUshuaia, 49, 10 + cantColegiosUshuaia, 50);//COMBINAR

            //##CONTENIDO DE LAS CELDAS RIO GRANDE
            //Rio Grande
            sl.SetCellValue(12 + cantColegiosUshuaia, 1, "Rio Grande"); //set value
            sl.SetCellStyle(12 + cantColegiosUshuaia, 1, styleBlack16_90G);
            sl.MergeWorksheetCells(12 + cantColegiosUshuaia, 1, 12 + cantColegiosUshuaia + cantColegiosGrande - 1, 1);//COMBINAR
            //subtotal S y Estudiantes Rio grande
            sl.SetCellValue(12 + cantColegiosUshuaia + cantColegiosGrande, 1, "Subtotal Rio Grande Secciones"); //set value
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(12 + cantColegiosUshuaia + cantColegiosGrande, 1, 12 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR
            sl.SetCellValue(13 + cantColegiosUshuaia + cantColegiosGrande, 1, "Subtotal Secciones por Año"); //set value
            sl.SetCellStyle(13 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(13 + cantColegiosUshuaia + cantColegiosGrande, 1, 13 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

            sl.SetCellValue(14 + cantColegiosUshuaia + cantColegiosGrande, 1, "Subtotal Rio Grande Estudiantes"); //set value
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(14 + cantColegiosUshuaia + cantColegiosGrande, 1, 14 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR*/
            sl.SetCellValue(15 + cantColegiosUshuaia + cantColegiosGrande, 1, "Subtotal Estudiantes por año"); //set value
            sl.SetCellStyle(15 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(15 + cantColegiosUshuaia + cantColegiosGrande, 1, 15 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR*/



            //Nombre de los establecimientos Rio grande             
            for (int i = 12 + cantColegiosUshuaia; i <= 12 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)
            {
                sl.SetCellValue(i, 2, grandeColegios[0, i - (12 + cantColegiosUshuaia)]); //set value
                sl.SetCellStyle(i, 2, styleBlack14);
            }
            //completa ciclo basico Rio grande
            for (int i = 12 + cantColegiosUshuaia; i <= 12 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)
            {
                for (int j = 3; j <= 20; j++)
                {
                    if (colegiosGrandeSyE[i - (12 + cantColegiosUshuaia), j - 3] == 0)//si el valor es 0 le pone un guion
                    {
                        sl.SetCellValue(i, j, "-"); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                    else//si tiene valor, lo pone
                    {
                        sl.SetCellValue(i, j, colegiosGrandeSyE[i - (12 + cantColegiosUshuaia), j - 3]); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                }
            }
            //completa ciclo superior Rio grande        
            for (int i = 12 + cantColegiosUshuaia; i <= 12 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)
            {
                for (int j = 23; j <= 46; j++)
                {
                    if (colegiosGrandeSyE[i - (12 + cantColegiosUshuaia), j - 5] == 0)//si el valor es 0 le pone un guion
                    {
                        sl.SetCellValue(i, j, "-"); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                    else//si tiene valor, lo pone
                    {
                        //sl.SetCellValue(i, j, Convert.ToString(j+i)); //set value
                        sl.SetCellValue(i, j, colegiosGrandeSyE[i - (12 + cantColegiosUshuaia), j - 5]); //set value
                        sl.SetCellStyle(i, j, styleBlack14);
                    }
                }
            }
            //completa totales ciclo basico Rio grande          
            for (int i = 12 + cantColegiosUshuaia; i <= 12 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)//22 porque empieza en la fila 22 depende de la cantidad de colegios ushuaia igual
            {
                for (int j = 21; j <= 22; j++)//21 porque empieza en la columna 21
                {
                    sl.SetCellValue(i, j, gTotalesCicloBasico[i - (12 + cantColegiosUshuaia), j - 21]); //set value
                    sl.SetCellStyle(i, j, styleBlue14);
                }
            }
            //completa totales ciclo superior Rio grande          
            for (int i = (12 + cantColegiosUshuaia); i <= 12 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)//22 porque empieza en la fila 22
            {
                for (int j = 47; j <= 48; j++)//47 porque empieza en la columna 47
                {
                    sl.SetCellValue(i, j, gTotalesCicloSuperior[i - (12 + cantColegiosUshuaia), j - 47]); //set value
                    sl.SetCellStyle(i, j, styleBlue14);
                }
            }
            //completa totales Rio grande          
            for (int i = (12 + cantColegiosUshuaia); i <= 12 + cantColegiosUshuaia + cantColegiosGrande - 1; i++)//22 porque empieza en la fila 22
            {
                for (int j = 49; j <= 50; j++)//49 porque empieza en la columna 49
                {
                    sl.SetCellValue(i, j, gTotal2Ciclos[i - (12 + cantColegiosUshuaia), j - 49]); //set value
                    sl.SetCellStyle(i, j, styleBlack14);
                }
            }

            //completa subtotal Rio grande  
            for (int i = 12 + cantColegiosUshuaia + cantColegiosGrande; i <= 13 + cantColegiosUshuaia + cantColegiosGrande; i++)
            {
                if (i == 12 + cantColegiosUshuaia + cantColegiosGrande)
                {
                    for (int j = 0; j <= 20; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i, j + 3, gSubtotalTurnos[i - (12 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, j + 3, i, j + 3 + 1);//COMBINAR
                        }
                        else if (j >= 9)
                        {
                            sl.SetCellValue(i, (j * 2) + 5, gSubtotalTurnos[i - (12 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 2) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 2) + 5, i, (j * 2) + 5 + 1);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i, (j * 2) + 3, gSubtotalTurnos[i - (12 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 2) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 2) + 3, i, (j * 2) + 3 + 1);//COMBINAR
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= 20; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i + 1, j + 3, gSubtotalTurnos[i - (12 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, j + 3, i + 1, j + 3 + 1);//COMBINAR
                        }
                        else if (j >= 9)
                        {
                            sl.SetCellValue(i + 1, (j * 2) + 5, gSubtotalTurnos[i - (12 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 2) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 2) + 5, i + 1, (j * 2) + 5 + 1);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i + 1, (j * 2) + 3, gSubtotalTurnos[i - (12 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 2) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 2) + 3, i + 1, (j * 2) + 3 + 1);//COMBINAR
                        }
                    }
                }
            }
            //completa subtotal grande por año

            for (int i = 13 + cantColegiosUshuaia + cantColegiosGrande; i <= 14 + cantColegiosUshuaia + cantColegiosGrande; i++)
            {
                if (i == 13 + cantColegiosUshuaia + cantColegiosGrande)
                {
                    for (int j = 0; j <= 6; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i, j + 3, gSubtotalAños[i - (13 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, j + 3, i, j + 3 + 5);//COMBINAR
                        }
                        else if (j >= 3)
                        {
                            sl.SetCellValue(i, (j * 6) + 5, gSubtotalAños[i - (13 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 6) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 6) + 5, i, (j * 6) + 5 + 5);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i, (j * 6) + 3, gSubtotalAños[i - (13 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 6) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 6) + 3, i, (j * 6) + 3 + 5);//COMBINAR
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= 6; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i + 1, j + 3, gSubtotalAños[i - (13 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, j + 3, i + 1, j + 3 + 5);//COMBINAR
                        }
                        else if (j >= 3)
                        {
                            sl.SetCellValue(i + 1, (j * 6) + 5, gSubtotalAños[i - (13 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 6) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 6) + 5, i + 1, (j * 6) + 5 + 5);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i + 1, (j * 6) + 3, gSubtotalAños[i - (13 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 6) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 6) + 3, i + 1, (j * 6) + 3 + 5);//COMBINAR
                        }
                    }
                }
            }

            //completa SubTotales Rio Grande             
            sl.SetCellValue(12 + cantColegiosUshuaia + cantColegiosGrande, 21, gSubtotal[0, 0]);
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 21, styleWhite14);
            sl.MergeWorksheetCells(12 + cantColegiosUshuaia + cantColegiosGrande, 21, 13 + cantColegiosUshuaia + cantColegiosGrande, 22);//COMBINAR
            sl.SetCellValue(12 + cantColegiosUshuaia + cantColegiosGrande, 47, gSubtotal[0, 1]);
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 47, styleWhite14);
            sl.MergeWorksheetCells(12 + cantColegiosUshuaia + cantColegiosGrande, 47, 13 + cantColegiosUshuaia + cantColegiosGrande, 48);//COMBINAR
            sl.SetCellValue(12 + cantColegiosUshuaia + cantColegiosGrande, 49, gSubtotal[0, 2]);
            sl.SetCellStyle(12 + cantColegiosUshuaia + cantColegiosGrande, 49, styleWhite14);
            sl.MergeWorksheetCells(12 + cantColegiosUshuaia + cantColegiosGrande, 49, 13 + cantColegiosUshuaia + cantColegiosGrande, 50);//COMBINAR
            sl.SetCellValue(14 + cantColegiosUshuaia + cantColegiosGrande, 21, gSubtotal[1, 0]);
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 21, styleWhite14);
            sl.MergeWorksheetCells(14 + cantColegiosUshuaia + cantColegiosGrande, 21, 15 + cantColegiosUshuaia + cantColegiosGrande, 22);//COMBINAR
            sl.SetCellValue(14 + cantColegiosUshuaia + cantColegiosGrande, 47, gSubtotal[1, 1]);
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 47, styleWhite14);
            sl.MergeWorksheetCells(14 + cantColegiosUshuaia + cantColegiosGrande, 47, 15 + cantColegiosUshuaia + cantColegiosGrande, 48);//COMBINAR
            sl.SetCellValue(14 + cantColegiosUshuaia + cantColegiosGrande, 49, gSubtotal[1, 2]);
            sl.SetCellStyle(14 + cantColegiosUshuaia + cantColegiosGrande, 49, styleWhite14);
            sl.MergeWorksheetCells(14 + cantColegiosUshuaia + cantColegiosGrande, 49, 15 + cantColegiosUshuaia + cantColegiosGrande, 50);//COMBINAR    

            //##CONTENIDO DE LAS CELDAS TOTAL JURISDICCIONAL
            sl.SetCellValue(17 + cantColegiosUshuaia + cantColegiosGrande, 1, "Total Jurisdiccional Secciones"); //set value
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(17 + cantColegiosUshuaia + cantColegiosGrande, 1, 17 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR
            sl.SetCellValue(18 + cantColegiosUshuaia + cantColegiosGrande, 1, "Total TDF Secciones por Año"); //set value
            sl.SetCellStyle(18 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(18 + cantColegiosUshuaia + cantColegiosGrande, 1, 18 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

            sl.SetCellValue(19 + cantColegiosUshuaia + cantColegiosGrande, 1, "Total Jurisdiccional Estudiantes"); //set value
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(19 + cantColegiosUshuaia + cantColegiosGrande, 1, 19 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR
            sl.SetCellValue(20 + cantColegiosUshuaia + cantColegiosGrande, 1, "Total TDF Estudiantes por Año"); //set value
            sl.SetCellStyle(20 + cantColegiosUshuaia + cantColegiosGrande, 1, styleWhite14);
            sl.MergeWorksheetCells(20 + cantColegiosUshuaia + cantColegiosGrande, 1, 20 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

            //completa total TDF 
            for (int i = 17 + cantColegiosUshuaia + cantColegiosGrande; i <= 18 + cantColegiosUshuaia + cantColegiosGrande; i++)
            {
                if (i == 17 + cantColegiosUshuaia + cantColegiosGrande)
                {
                    for (int j = 0; j <= 20; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i, j + 3, totalJurisSecyEstudiates[i - (17 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, j + 3, i, j + 3 + 1);//COMBINAR
                        }
                        else if (j >= 9)
                        {
                            sl.SetCellValue(i, (j * 2) + 5, totalJurisSecyEstudiates[i - (17 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 2) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 2) + 5, i, (j * 2) + 5 + 1);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i, (j * 2) + 3, totalJurisSecyEstudiates[i - (17 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 2) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 2) + 3, i, (j * 2) + 3 + 1);//COMBINAR
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= 20; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i + 1, j + 3, totalJurisSecyEstudiates[i - (17 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, j + 3, i + 1, j + 3 + 1);//COMBINAR
                        }
                        else if (j >= 9)
                        {
                            sl.SetCellValue(i + 1, (j * 2) + 5, totalJurisSecyEstudiates[i - (17 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 2) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 2) + 5, i + 1, (j * 2) + 5 + 1);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i + 1, (j * 2) + 3, totalJurisSecyEstudiates[i - (17 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 2) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 2) + 3, i + 1, (j * 2) + 3 + 1);//COMBINAR
                        }
                    }
                }
            }
            //completa TOTAL TDF por año

            for (int i = 18 + cantColegiosUshuaia + cantColegiosGrande; i <= 19 + cantColegiosUshuaia + cantColegiosGrande; i++)
            {
                if (i == 18 + cantColegiosUshuaia + cantColegiosGrande)
                {
                    for (int j = 0; j <= 6; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i, j + 3, totalJurisdicionalAños[i - (18 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, j + 3, i, j + 3 + 5);//COMBINAR
                        }
                        else if (j >= 3)
                        {
                            sl.SetCellValue(i, (j * 6) + 5, totalJurisdicionalAños[i - (18 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 6) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 6) + 5, i, (j * 6) + 5 + 5);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i, (j * 6) + 3, totalJurisdicionalAños[i - (18 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i, (j * 6) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i, (j * 6) + 3, i, (j * 6) + 3 + 5);//COMBINAR
                        }
                    }
                }
                else
                {
                    for (int j = 0; j <= 6; j++)//3 porque empieza en la columna 3 y suma de a 2 lugares
                    {
                        if (j == 0)
                        {
                            sl.SetCellValue(i + 1, j + 3, totalJurisdicionalAños[i - (18 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, j + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, j + 3, i + 1, j + 3 + 5);//COMBINAR
                        }
                        else if (j >= 3)
                        {
                            sl.SetCellValue(i + 1, (j * 6) + 5, totalJurisdicionalAños[i - (18 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 6) + 5, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 6) + 5, i + 1, (j * 6) + 5 + 5);//COMBINAR
                        }
                        else
                        {
                            sl.SetCellValue(i + 1, (j * 6) + 3, totalJurisdicionalAños[i - (18 + cantColegiosUshuaia + cantColegiosGrande), j]);
                            sl.SetCellStyle(i + 1, (j * 6) + 3, styleBlack14);
                            sl.MergeWorksheetCells(i + 1, (j * 6) + 3, i + 1, (j * 6) + 3 + 5);//COMBINAR
                        }
                    }
                }
            }

            //completa totales FINALES TDF           
            sl.SetCellValue(17 + cantColegiosUshuaia + cantColegiosGrande, 21, totalJurisdicional[0, 0]);
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 21, styleWhite14);
            sl.MergeWorksheetCells(17 + cantColegiosUshuaia + cantColegiosGrande, 21, 18 + cantColegiosUshuaia + cantColegiosGrande, 22);//COMBINAR
            sl.SetCellValue(17 + cantColegiosUshuaia + cantColegiosGrande, 47, totalJurisdicional[0, 1]);
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 47, styleWhite14);
            sl.MergeWorksheetCells(17 + cantColegiosUshuaia + cantColegiosGrande, 47, 18 + cantColegiosUshuaia + cantColegiosGrande, 48);//COMBINAR
            sl.SetCellValue(17 + cantColegiosUshuaia + cantColegiosGrande, 49, totalJurisdicional[0, 2]);
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 49, styleWhite14);
            sl.MergeWorksheetCells(17 + cantColegiosUshuaia + cantColegiosGrande, 49, 18 + cantColegiosUshuaia + cantColegiosGrande, 50);//COMBINAR
            sl.SetCellValue(19 + cantColegiosUshuaia + cantColegiosGrande, 21, totalJurisdicional[1, 0]);
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 21, styleWhite14);
            sl.MergeWorksheetCells(19 + cantColegiosUshuaia + cantColegiosGrande, 21, 20 + cantColegiosUshuaia + cantColegiosGrande, 22);//COMBINAR
            sl.SetCellValue(19 + cantColegiosUshuaia + cantColegiosGrande, 47, totalJurisdicional[1, 1]);
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 47, styleWhite14);
            sl.MergeWorksheetCells(19 + cantColegiosUshuaia + cantColegiosGrande, 47, 20 + cantColegiosUshuaia + cantColegiosGrande, 48);//COMBINAR
            sl.SetCellValue(19 + cantColegiosUshuaia + cantColegiosGrande, 49, totalJurisdicional[1, 2]);
            sl.SetCellStyle(19 + cantColegiosUshuaia + cantColegiosGrande, 49, styleWhite14);
            sl.MergeWorksheetCells(19 + cantColegiosUshuaia + cantColegiosGrande, 49, 20 + cantColegiosUshuaia + cantColegiosGrande, 50);//COMBINAR

            //##bordes gruesos horizontales ushuaia
            sl.SetCellStyle(1, 1, 1, 50, styleBorderBM);//BORDE TITLE
            sl.SetCellStyle(2, 3, 2, 48, styleBorderBM);//BORDE sUBTITLE
            sl.SetCellStyle(3, 3, 3, 48, styleBorderBM);//BORDE Secciones
            sl.SetCellStyle(5, 3, 5, 50, styleBorderBM);//BORDE S/E
            sl.SetCellStyle(6, 1, 6, 50, styleBorderBM);//BORDE S/E 2
            sl.SetCellStyle(6 + cantColegiosUshuaia, 1, 6 + cantColegiosUshuaia, 50, styleBorderBM);//BORDE S/E 3
            sl.SetCellStyle(8 + cantColegiosUshuaia, 1, 8 + cantColegiosUshuaia, 50, styleBorderBM);//BORDE S/E 3
            sl.SetCellStyle(11 + cantColegiosUshuaia + cantColegiosGrande, 1, 11 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBM);//BORDE S/E rio grande
            sl.SetCellStyle(13 + cantColegiosUshuaia + cantColegiosGrande, 1, 13 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBM);//BORDE S/E rio grande
            sl.SetCellStyle(18 + cantColegiosUshuaia + cantColegiosGrande, 1, 18 + cantColegiosUshuaia + cantColegiosGrande, 50, styleBorderBM);//BORDE S/E TDF

            //bordes gruesos verticales
            sl.SetCellStyle(2, 3, 10 + cantColegiosUshuaia, 3, styleBorderLM);//borde 1
            sl.SetCellStyle(2, 2, 6 + cantColegiosUshuaia, 2, styleBorderLM);//borde 1
            sl.SetCellStyle(4, 9, 10 + cantColegiosUshuaia, 9, styleBorderLM);//borde 1ro
            sl.SetCellStyle(4, 15, 10 + cantColegiosUshuaia, 15, styleBorderLM);//borde 2do
            sl.SetCellStyle(3, 21, 10 + cantColegiosUshuaia, 21, styleBorderLM);//borde 3ro
            sl.SetCellStyle(2, 23, 10 + cantColegiosUshuaia, 23, styleBorderLM);//borde 4
            sl.SetCellStyle(4, 29, 10 + cantColegiosUshuaia, 29, styleBorderLM);//borde 4to
            sl.SetCellStyle(4, 35, 10 + cantColegiosUshuaia, 35, styleBorderLM);//borde 5to
            sl.SetCellStyle(4, 41, 10 + cantColegiosUshuaia, 41, styleBorderLM);//borde 6to
            sl.SetCellStyle(3, 47, 10 + cantColegiosUshuaia, 47, styleBorderLM);//borde 7mo
            sl.SetCellStyle(2, 49, 10 + cantColegiosUshuaia, 49, styleBorderLM);//borde 7

            sl.SetCellStyle(12 + cantColegiosUshuaia, 3, 15 + cantColegiosUshuaia + cantColegiosGrande, 3, styleBorderLM);//borde 1 Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 2, 12 + cantColegiosUshuaia + cantColegiosGrande, 2, styleBorderLM);//borde 2 Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 9, 15 + cantColegiosUshuaia + cantColegiosGrande, 9, styleBorderLM);//borde 1ro Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 15, 15 + cantColegiosUshuaia + cantColegiosGrande, 15, styleBorderLM);//borde 2do Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 21, 15 + cantColegiosUshuaia + cantColegiosGrande, 21, styleBorderLM);//borde 3ro Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 23, 15 + cantColegiosUshuaia + cantColegiosGrande, 23, styleBorderLM);//borde 4 Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 29, 15 + cantColegiosUshuaia + cantColegiosGrande, 29, styleBorderLM);//borde 4to Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 35, 15 + cantColegiosUshuaia + cantColegiosGrande, 35, styleBorderLM);//borde 5to Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 41, 15 + cantColegiosUshuaia + cantColegiosGrande, 41, styleBorderLM);//borde 6to Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 47, 15 + cantColegiosUshuaia + cantColegiosGrande, 47, styleBorderLM);//borde 7mo Rio Grande
            sl.SetCellStyle(12 + cantColegiosUshuaia, 49, 15 + cantColegiosUshuaia + cantColegiosGrande, 49, styleBorderLM);//borde 7 Rio Grande

            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 3, 20 + cantColegiosUshuaia + cantColegiosGrande, 3, styleBorderLM);//borde 1 Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 9, 20 + cantColegiosUshuaia + cantColegiosGrande, 9, styleBorderLM);//borde 1ro Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 15, 20 + cantColegiosUshuaia + cantColegiosGrande, 15, styleBorderLM);//borde 2do Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 21, 20 + cantColegiosUshuaia + cantColegiosGrande, 21, styleBorderLM);//borde 3ro Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 23, 20 + cantColegiosUshuaia + cantColegiosGrande, 23, styleBorderLM);//borde 4 Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 29, 20 + cantColegiosUshuaia + cantColegiosGrande, 29, styleBorderLM);//borde 4to TotalTotal
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 35, 20 + cantColegiosUshuaia + cantColegiosGrande, 35, styleBorderLM);//borde 5to Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 41, 20 + cantColegiosUshuaia + cantColegiosGrande, 41, styleBorderLM);//borde 6to Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 47, 20 + cantColegiosUshuaia + cantColegiosGrande, 47, styleBorderLM);//borde 7mo Total
            sl.SetCellStyle(17 + cantColegiosUshuaia + cantColegiosGrande, 49, 20 + cantColegiosUshuaia + cantColegiosGrande, 49, styleBorderLM);//borde 7 Rio Total    

            sl.SaveAs(pathFile);

            btnCrearExcel.Enabled = false;
            lblCreando.Visible = false;
            MessageBox.Show("Excel Generado", "Sistema Informa");

        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            Estadisticas myEstadisticas = new Estadisticas(nombreUsuario, tipoUsuario, logueadoUsuario, conexionBaseDatos);
            myEstadisticas.Visible = true;
            this.Close();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }        
}
