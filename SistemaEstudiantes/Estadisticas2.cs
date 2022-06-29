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
    public partial class Estadisticas2 : Form
    {
        int colorPlantilla;//color de los botones.... para que no se buguee
        int colorEstadistica;
        int colorExcel;
        int colorDecla;

        OleDbConnection conexionBaseDatos;//variable que recibe la direccion de la base de datos
        ColegiosEstadisticas myColegios;
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

        int counterFilaArray = 0;//para ver en que fila de array se encuentra
                                 //Para sacar estadisicas

        DataTable miDataTable = new DataTable();

        string[,] ushuaiaColegios;//nombre,nombreAbreviado, posicion de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
        string[,] grandeColegios;//nombre,nombreAbreviado, posicion de los colegios de rio grande

        string[,] educacionTecnica = new string[3, 10];//nombreAbreviado,nombreAbreviadoCargado, nombre de las orientaciones Educación Técnica 
        string[,] bachOrientado = new string[3, 12];
        string[,] bachEspecializado = new string[3, 9];


        int[,] colegiosUshuaiaET;//array datos de estudiantes por orientacion Educación Técnica USHUAIA
        int[,] colegiosGrandeET;//array datos de estudiantes por orientacion Educación Técnica RIO GRANDE

        int[,] colegiosUshuaiaBO;//array datos de estudiantes por orientacion  bachiller orientado USHUAIA
        int[,] colegiosGrandeBO;//array datos de estudiantes por orientacion bachiller orientado  RIO GRANDE

        int[,] colegiosUshuaiaEsp ;//array datos de estudiantes por orientacion  bachiller especializado USHUAIA
        int[,] colegiosGrandeEsp ;//array datos de estudiantes por orientacion bachiller especializado RIO GRANDE

        int[] subTotalesUshuaiaET;
        int[] subTotalesUshuaiaBO;
        int[] subTotalesUshuaiaEsp;

        int[] subTotalesGrandeET;
        int[] subTotalesGrandeBO;
        int[] subTotalesGrandeEsp;

        int[] totalUshuaia;
        int[] totalGrande;

        int[] totalesProvET;
        int[] totalesProvBO;
        int[] totalesProvEsp;

        int totalGenUshuaia;
        int totalGenGrande;
        int totalGenProvincial;            

        public Estadisticas2(string usuario, string permisos, bool logueado, OleDbConnection conexionBD)
        {
            InitializeComponent();
            nombreUsuario = usuario;
            permisosUsuario = permisos;
            logueadoUsuario = logueado;
            conexionBaseDatos = conexionBD;
            lblNombre.Text = usuario;

            myDataGridView1.Visible = false;

            idUnico = "";
            ordenar();

            if (colegiosCreados == false)
            {
                myColegios = new ColegiosEstadisticas();
                myColegios.ConexionBD(conexionBaseDatos);
                myColegios.CargarColegiosUshuaia();
                myColegios.CargarColegiosGrande();
                cantColegiosUshuaia = myColegios.NumColegiosUshuaia;
                cantColegiosGrande = myColegios.NumColegiosGrande;

                ushuaiaColegios = new string[3, cantColegiosUshuaia];//nombre,posicion,nombreAbreviado de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
                grandeColegios = new string[3, cantColegiosGrande];//nombre,posicion,nombreAbreviado de los colegios de rio grande

                NombreColegios();
                OrientacionesColegios();
                ComboBoxUshuaia();
                ComboBoxGrande();
                colegiosCreados = true;
            }
        }
        private void ordenar()
        {//Ver Planillas
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

            btnDeclaracion.BackColor = System.Drawing.Color.Silver;
            btnDeclaracion.Enabled = false;
            lblAbriendoDec.Visible = false;

            // Ver estadistica
            cboxPeriodoEst.ResetText();
            cboxAñoEst.ResetText();
            cboxAñoEst.Enabled = true;
            cboxPeriodoEst.Enabled = false;

            btnVerEstadistica.Enabled = false;
            btnVerEstadistica.BackColor = System.Drawing.Color.Silver;

            lblAbriendo.Visible = false;
            lblDescargas.Visible = false;

            //crear estadistica
            cboxAño.ResetText();
            cboxPeriodo.ResetText();//Reinicia el texto seleccionado
            cboxAño.Enabled = true;
            cboxPeriodo.Enabled = false;

            lblProcesando.Visible = false;
            lblCreando.Visible = false;

            btnCrearEstadistica.Enabled = false;
            btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;

            btnCrearExcel.Enabled = false;
            btnCrearExcel.BackColor = System.Drawing.Color.Silver;

            if (permisosUsuario != "SuperUsuario")
            {
                panel1.Enabled = false;
                panel1.BackColor = System.Drawing.Color.Silver;
                btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;
                btnCrearExcel.BackColor = System.Drawing.Color.Silver;
            }

            myDataGridView.DataSource = null;//reinicia datagv
            myDataGridView.Rows.Clear();
            myDataGridView.Refresh();

            colegiosUshuaiaET = new int[25, 10];//array datos de estudiantes por orientacion Educación Técnica USHUAIA
            colegiosGrandeET = new int[25, 10];//array datos de estudiantes por orientacion Educación Técnica RIO GRANDE

            colegiosUshuaiaBO = new int[25, 12];//array datos de estudiantes por orientacion  bachiller orientado USHUAIA
            colegiosGrandeBO = new int[25, 12];//array datos de estudiantes por orientacion bachiller orientado  RIO GRANDE

            colegiosUshuaiaEsp = new int[25, 9];//array datos de estudiantes por orientacion  bachiller especializado USHUAIA
            colegiosGrandeEsp = new int[25, 9];//array datos de estudiantes por orientacion bachiller especializado RIO GRANDE

            subTotalesUshuaiaET = new int[10];
            subTotalesUshuaiaBO = new int[12];
            subTotalesUshuaiaEsp = new int[9];

            subTotalesGrandeET = new int[10];
            subTotalesGrandeBO = new int[12];
            subTotalesGrandeEsp = new int[9];

            totalUshuaia = new int[25];
            totalGrande = new int[25];

            totalesProvET = new int[10];
            totalesProvBO = new int[12];
            totalesProvEsp = new int[9]; 
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
        private void OrientacionesColegios()
        {
            educacionTecnica[0, 0] = "Alim";
            educacionTecnica[0, 1] = "CM";
            educacionTecnica[0, 2] = "EeIEm";
            educacionTecnica[0, 3] = "Elec";
            educacionTecnica[0, 4] = "GyAO";
            educacionTecnica[0, 5] = "IPyP";
            educacionTecnica[0, 6] = "MMO";
            educacionTecnica[0, 7] = "P Ag";
            educacionTecnica[0, 8] = "Prog";
            educacionTecnica[0, 9] = "TIC";
            educacionTecnica[1, 0] = "Técnico en Alimentos";
            educacionTecnica[1, 1] = "Comunicación Multimedial";
            educacionTecnica[1, 2] = "Equipos e Instalaciones Electromecánicas";
            educacionTecnica[1, 3] = "Electrónica";
            educacionTecnica[1, 4] = "Gestión y Administración de las Organizaciones";
            educacionTecnica[1, 5] = "Infomática Profesional y Personal";
            educacionTecnica[1, 6] = "Maestro Mayor de Obra";
            educacionTecnica[1, 7] = "Producción Agropecuaria";
            educacionTecnica[1, 8] = "Programación";
            educacionTecnica[1, 9] = "Técnico en Informacion de la Comunicación";
            educacionTecnica[2, 0] = "Alim";
            educacionTecnica[2, 1] = "CM";
            educacionTecnica[2, 2] = "EeIEm";
            educacionTecnica[2, 3] = "Elec";
            educacionTecnica[2, 4] = "GyAO";
            educacionTecnica[2, 5] = "IPyP";
            educacionTecnica[2, 6] = "MMO";
            educacionTecnica[2, 7] = "P.Ag";
            educacionTecnica[2, 8] = "Prog";
            educacionTecnica[2, 9] = "TIC";

            bachOrientado[0, 0] = "ADanza";
            bachOrientado[0, 1] = "AMúsica";
            bachOrientado[0, 2] = "AVis";
            bachOrientado[0, 3] = "AyA";
            bachOrientado[0, 4] = "CNat";
            bachOrientado[0, 5] = "Com";
            bachOrientado[0, 6] = "CSoc";
            bachOrientado[0, 7] = "EyA";
            bachOrientado[0, 8] = "Ed Fís";
            bachOrientado[0, 9] = "Inf";
            bachOrientado[0, 10] = "Leng";
            bachOrientado[0, 11] = "Tur";
            bachOrientado[1, 0] = "Arte Danza";
            bachOrientado[1, 1] = "Arte Música";
            bachOrientado[1, 2] = "Artes Visuales";
            bachOrientado[1, 3] = "Afro y Ambiente";
            bachOrientado[1, 4] = "Ciencias Naturales";
            bachOrientado[1, 5] = "Comunicación";
            bachOrientado[1, 6] = "Ciencias Sociales";
            bachOrientado[1, 7] = "Economía y Administración";
            bachOrientado[1, 8] = "Educación Física";
            bachOrientado[1, 9] = "Informática";
            bachOrientado[1, 10] = "Lengua";
            bachOrientado[1, 11] = "Turismo";
            bachOrientado[2, 0] = "ADanza";
            bachOrientado[2, 1] = "AMúsica";
            bachOrientado[2, 2] = "AVis";
            bachOrientado[2, 3] = "AyA";
            bachOrientado[2, 4] = "C.N.";
            bachOrientado[2, 5] = "Com";
            bachOrientado[2, 6] = "C.S.";
            bachOrientado[2, 7] = "EyA";
            bachOrientado[2, 8] = "Ed Fis";
            bachOrientado[2, 9] = "Inf";
            bachOrientado[2, 10] = "Leng";
            bachOrientado[2, 11] = "Tur";

            bachEspecializado[0, 0] = "AV-AyNM";
            bachEspecializado[0, 1] = "AV-C";
            bachEspecializado[0, 2] = "AV-GyAI";
            bachEspecializado[0, 3] = "AV-P";
            bachEspecializado[0, 4] = "AV-Pr";
            bachEspecializado[0, 5] = "DOE";
            bachEspecializado[0, 6] = "M-C";
            bachEspecializado[0, 7] = "M-G";
            bachEspecializado[0, 8] = "M-P";
            bachEspecializado[1, 0] = "Artes Visuales - Arte y Nuevos Medios";
            bachEspecializado[1, 1] = "Artes Visuales - Cerámica";
            bachEspecializado[1, 2] = "Artes Visuales - Gravado y Arte Impreso";
            bachEspecializado[1, 3] = "Artes Visuales - Pintura";
            bachEspecializado[1, 4] = "Artes Visuales - Producción";
            bachEspecializado[1, 5] = "Danza de Origen Escénico";
            bachEspecializado[1, 6] = "Música - Canto";
            bachEspecializado[1, 7] = "Música - Guitarra";
            bachEspecializado[1, 8] = "Música - Piano";
            bachEspecializado[2, 0] = "AV-AyNM";
            bachEspecializado[2, 1] = "AV-C";
            bachEspecializado[2, 2] = "AV-GyA";
            bachEspecializado[2, 3] = "AV-P";
            bachEspecializado[2, 4] = "AV-Pr";
            bachEspecializado[2, 5] = "DOE";
            bachEspecializado[2, 6] = "M-C";
            bachEspecializado[2, 7] = "M-G";
            bachEspecializado[2, 8] = "M-P";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ordenar();
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

        //VER PLANILLA
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

            btnDeclaracion.Enabled = true;
            btnDeclaracion.BackColor = System.Drawing.Color.DodgerBlue;
            colorDecla = 1;
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

            btnDeclaracion.Enabled = true;
            btnDeclaracion.BackColor = System.Drawing.Color.DodgerBlue;
            colorDecla = 1;
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

                if (abreColegioVPla == "PBust")
                {
                    try
                    {
                        DataTable miDataTable = new DataTable();

                        string queryCargarBD = "SELECT Sección, División, Turno, Orientación, Perfil, Horas, Pedagogica, Presupuestaria, Matriculas, ColegioIngresado, Fecha FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
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
                else
                {
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

                if ( abreColegioVPla == "PCot")
                {
                    try
                    {
                        DataTable miDataTable = new DataTable();

                        string queryCargarBD = "SELECT Sección, División, Turno, Orientación, Perfil, Horas, Pedagogica, Presupuestaria, Matriculas, ColegioIngresado, Fecha FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
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
                else
                {
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
        }
        private void btnDeclaracion_Click(object sender, EventArgs e)
        {
            bool verDescarga = false;
            lblAbriendoDec.Visible = true;
            lblAbriendoDec.Refresh();
            string userName = Environment.UserName;
            string idUnicoVPla;
            string abreColegioVPla;
            string rutaInicio = "";

            colorDecla = 0;
            btnDeclaracion.BackColor = System.Drawing.Color.Silver;
            btnDeclaracion.Enabled = false;


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

                if (abreColegioVPla == "PBust")
                {
                    try
                    {
                        DataTable miDataTable = new DataTable();

                        string queryCargarBD = "SELECT RutaPlanilla FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
                        OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", idUnicoVPla);

                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        myDataGridView1.DataSource = miDataTable;
                        rutaInicio = Convert.ToString(myDataGridView1.Rows[0].Cells[0].Value);
                        myDataGridView1.DataSource = null;//reinicia datagv
                        myDataGridView1.Rows.Clear();
                        myDataGridView1.Refresh();

                        if ((rutaInicio == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                        {
                            MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();

                        }
                        else
                        {
                            string destinationFile = @"C:/Users/" + userName + "/Downloads/" + cboxColegiosUshuaia.SelectedItem.ToString() +" xOrientacion "+ cboxAñoPla.SelectedItem.ToString() + " " + cboxPeriodoPla.SelectedItem.ToString() + ".xlsx";

                            System.IO.File.Copy(rutaInicio, destinationFile, true);//true es importante para que los sobre escriba       

                            Process proceso = new Process();
                            proceso.StartInfo.FileName = destinationFile;

                            proceso.Start();
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("no es una ruta de acceso válida"))
                        {
                            MessageBox.Show("Problema con la red.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                        {
                            MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                        {
                            MessageBox.Show("Datos duplicados en abse de datos.", "Sistema Informa");
                        }
                        else
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                }
                else 
                {       
                    try
                    {
                        DataTable miDataTable = new DataTable();

                        string queryCargarBD = "SELECT RutaPlanilla FROM Planilla WHERE IdUnico = @Buscar";
                        OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", idUnicoVPla);

                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        myDataGridView1.DataSource = miDataTable;
                        rutaInicio = Convert.ToString(myDataGridView1.Rows[0].Cells[0].Value);
                        myDataGridView1.DataSource = null;//reinicia datagv
                        myDataGridView1.Rows.Clear();
                        myDataGridView1.Refresh();

                        if ((rutaInicio == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                        {
                            MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();

                        }
                        else
                        {
                            string destinationFile = @"C:/Users/" + userName + "/Downloads/" + cboxColegiosUshuaia.SelectedItem.ToString() + " " + cboxAñoPla.SelectedItem.ToString() + " " + cboxPeriodoPla.SelectedItem.ToString() + ".xlsx";

                            System.IO.File.Copy(rutaInicio, destinationFile, true);//true es importante para que los sobre escriba       

                            Process proceso = new Process();
                            proceso.StartInfo.FileName = destinationFile;

                            proceso.Start();
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("no es una ruta de acceso válida"))
                        {
                            MessageBox.Show("Problema con la red.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                        {
                            MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                        {
                            MessageBox.Show("Datos duplicados en abse de datos.", "Sistema Informa");
                        }
                        else
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                     
                }   
            }
            else if (deptoColegio == "Grande")
            {
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

                if (abreColegioVPla == "PCot")
                {
                    try
                    {
                        DataTable miDataTable = new DataTable();

                        string queryCargarBD = "SELECT RutaPlanilla FROM PlanillasxOrientacion WHERE IdUnico = @Buscar";
                        OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", idUnicoVPla);

                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        myDataGridView1.DataSource = miDataTable;
                        rutaInicio = Convert.ToString(myDataGridView1.Rows[0].Cells[0].Value);
                        myDataGridView1.DataSource = null;//reinicia datagv
                        myDataGridView1.Rows.Clear();
                        myDataGridView1.Refresh();

                        if ((rutaInicio == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                        {
                            MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();

                        }
                        else
                        {
                            string destinationFile = @"C:/Users/" + userName + "/Downloads/" + cboxColegiosUshuaia.SelectedItem.ToString() + " " + cboxAñoPla.SelectedItem.ToString() + " " + cboxPeriodoPla.SelectedItem.ToString() + ".xlsx";

                            System.IO.File.Copy(rutaInicio, destinationFile, true);//true es importante para que los sobre escriba       

                            Process proceso = new Process();
                            proceso.StartInfo.FileName = destinationFile;

                            proceso.Start();
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("no es una ruta de acceso válida"))
                        {
                            MessageBox.Show("Problema con la red.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                        {
                            MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                        {
                            MessageBox.Show("Datos duplicados en abse de datos.", "Sistema Informa");
                        }
                        else
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                }
                else
                {
                    try
                    {
                        DataTable miDataTable = new DataTable();

                        string queryCargarBD = "SELECT RutaPlanilla FROM Planilla WHERE IdUnico = @Buscar";
                        OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);
                        sqlComando.Parameters.AddWithValue("@idBuscar", idUnicoVPla);

                        OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
                        miDataAdapter.Fill(miDataTable);
                        myDataGridView1.DataSource = miDataTable;
                        rutaInicio = Convert.ToString(myDataGridView1.Rows[0].Cells[0].Value);
                        myDataGridView1.DataSource = null;//reinicia datagv
                        myDataGridView1.Rows.Clear();
                        myDataGridView1.Refresh();

                        if ((rutaInicio == ""))//revisa si hay se ha encontrado algo... esta escrito de esta forma sino tiraba error critico
                        {
                            MessageBox.Show("No se encontró ninguna planilla para los parámetros especificados.", "Sistema Informa");
                        }
                        else
                        {
                            string destinationFile = @"C:/Users/" + userName + "/Downloads/" + cboxColegiosGrande.SelectedItem.ToString() + " " + cboxAñoPla.SelectedItem.ToString() + " " + cboxPeriodoPla.SelectedItem.ToString() + ".xlsx";

                            System.IO.File.Copy(rutaInicio, destinationFile, true);//true es importante para que los sobre escriba       

                            Process proceso = new Process();
                            proceso.StartInfo.FileName = destinationFile;

                            proceso.Start();
                            lblAbriendoDec.Visible = false;
                            lblAbriendoDec.Refresh();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("no es una ruta de acceso válida"))
                        {
                            MessageBox.Show("Problema con la red.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("porque está siendo utilizado en otro proceso"))
                        {
                            MessageBox.Show("El archivo excel que quiere cargar esta abierto, debe cerrarlo.", "Sistema Informa");
                        }
                        else if (ex.Message.Contains("datos duplicados"))// revisa en el mensaje de la excepcion si el error es por norma duplicada
                        {
                            MessageBox.Show("Datos duplicados en abse de datos.", "Sistema Informa");
                        }
                        else
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                }                    
            }
        }
        //VER ESTADISTICA
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

                string sourceFile = @"//server/BASES/Sistema/BDSistema Supervision/2-Estadistica xOrientacion/Por Orientación " + cboxAñoEst.SelectedItem.ToString() + " " + cboxPeriodoEst.SelectedItem.ToString() + ".xlsx";
                string destinationFile = @"C:/Users/" + userName + "/Downloads/Por Orientación " + cboxAñoEst.SelectedItem.ToString() + " " + cboxPeriodoEst.SelectedItem.ToString() + ".xlsx";

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
            btnCrearEstadistica.Enabled = false;
            lblProcesando.Refresh();
            int contadorFilas = 0;
            int lugarArray = 0;
            

            colorExcel = 4; //color par es igual a silver, sirve para que no se bugueee azul            
            btnCrearEstadistica.BackColor = System.Drawing.Color.Silver;
            btnCrearEstadistica.Enabled = false;
            try
            {
                for (int eduTecnica = 0; eduTecnica <= 9; eduTecnica++)
                {
                    lugarArray = eduTecnica;
                    string queryCantOrientacion = "SELECT NumOrdenColegio, Departamento, ColegioSelect, Matriculas FROM Planilla WHERE Año = @Año AND Periodo = @Mes AND Orientación = @Especialidad";
                    
                    OleDbCommand sqlComando = new OleDbCommand(queryCantOrientacion, conexionBaseDatos);

                    sqlComando.Parameters.AddWithValue("@Año ", cboxAño.SelectedItem.ToString());
                    sqlComando.Parameters.AddWithValue("@Mes", cboxPeriodo.SelectedItem.ToString());
                    sqlComando.Parameters.AddWithValue("@Especialidad", educacionTecnica[2, eduTecnica]);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;

                    contadorFilas = 0;
                    int accumulatorUshuaia = 0;
                    int accumulatorGrande = 0;

                    while (Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) > 0)
                    {
                        if (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[1].Value) == "Ushuaia")
                        {
                            //ubica el colegio... le resta 1 porque es el lugar en el array y le suma las matriculas de esa orientacion
                            colegiosUshuaiaET[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] = colegiosUshuaiaET[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            //colegiosUshuaiaET[12 - 1, lugarArray] = colegiosUshuaiaET[12 - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            accumulatorUshuaia = accumulatorUshuaia + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);                           
                        }
                        else
                        {
                            colegiosGrandeET[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] = colegiosGrandeET[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            //colegiosGrandeET[13-1, lugarArray] = colegiosGrandeET[13-1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value); 
                            accumulatorGrande = accumulatorGrande + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);                            
                        }
                        contadorFilas++;
                    }                   
                    accumulatorUshuaia = 0;
                    accumulatorGrande = 0;
                    miDataTable.Clear();
                    myDataGridView.DataSource = null;//reinicia datagv
                    myDataGridView.Rows.Clear();
                    myDataGridView.Refresh();
                }
                for (int eduOrientado = 0; eduOrientado <= 11; eduOrientado++)
                {
                    lugarArray = eduOrientado;
                    string queryCantOrientacion = "SELECT NumOrdenColegio, Departamento, ColegioSelect, Matriculas FROM Planilla WHERE Año = @Año AND Periodo = @Mes AND Orientación = @Especialidad";
                    
                    OleDbCommand sqlComando = new OleDbCommand(queryCantOrientacion, conexionBaseDatos);

                    sqlComando.Parameters.AddWithValue("@Año ", cboxAño.SelectedItem.ToString());
                    sqlComando.Parameters.AddWithValue("@Mes", cboxPeriodo.SelectedItem.ToString());
                    sqlComando.Parameters.AddWithValue("@Especialidad", bachOrientado[2, eduOrientado]);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    contadorFilas = 0;
                    int accumulatorUshuaia = 0;
                    int accumulatorGrande = 0;                    

                    while (Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) > 0)
                    {
                        if (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[1].Value) == "Ushuaia")
                        {
                            //ubica el colegio... le resta 1 porque es el lugar en el array y le suma las matriculas de esa orientacion
                            colegiosUshuaiaBO[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] = colegiosUshuaiaBO[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            //colegiosUshuaiaET[12 - 1, lugarArray] = colegiosUshuaiaET[12 - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            accumulatorUshuaia = accumulatorUshuaia + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);                            
                        }
                        else
                        {
                            colegiosGrandeBO[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] = colegiosGrandeBO[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            //colegiosGrandeET[13-1, lugarArray] = colegiosGrandeET[13-1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value); 
                            accumulatorGrande = accumulatorGrande + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);                            
                        }
                        contadorFilas++;
                    }
                    accumulatorUshuaia = 0;
                    accumulatorGrande = 0;
                    miDataTable.Clear();
                    myDataGridView.DataSource = null;//reinicia datagv
                    myDataGridView.Rows.Clear();
                    myDataGridView.Refresh();
                }
                for (int eduEspecializado = 0; eduEspecializado <= 8; eduEspecializado++)
                {
                    lugarArray = eduEspecializado;
                    string queryCantOrientacion = "SELECT NumOrdenColegio, Departamento, ColegioSelect, Matriculas FROM PlanillasxOrientacion WHERE Año = @Año AND Periodo = @Mes AND Orientación = @Especialidad";
                    
                    OleDbCommand sqlComando = new OleDbCommand(queryCantOrientacion, conexionBaseDatos);

                    sqlComando.Parameters.AddWithValue("@Año ", cboxAño.SelectedItem.ToString());
                    sqlComando.Parameters.AddWithValue("@Mes", cboxPeriodo.SelectedItem.ToString());
                    sqlComando.Parameters.AddWithValue("@Especialidad", bachEspecializado[2, eduEspecializado]);

                    OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);

                    miDataAdapter.Fill(miDataTable);
                    myDataGridView.DataSource = miDataTable;
                    contadorFilas = 0;
                    int accumulatorUshuaia = 0;
                    int accumulatorGrande = 0;                    

                    while (Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) > 0)
                    {
                        if (Convert.ToString(myDataGridView.Rows[contadorFilas].Cells[1].Value) == "Ushuaia")
                        {
                            //ubica el colegio... le resta 1 porque es el lugar en el array y le suma las matriculas de esa orientacion
                            colegiosUshuaiaEsp[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] = colegiosUshuaiaEsp[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            //colegiosUshuaiaET[12 - 1, lugarArray] = colegiosUshuaiaET[12 - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            accumulatorUshuaia = accumulatorUshuaia + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);                            
                        }
                        else
                        {
                            colegiosGrandeEsp[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] = colegiosGrandeEsp[Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[0].Value) - 1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);
                            //colegiosGrandeET[13-1, lugarArray] = colegiosGrandeET[13-1, lugarArray] + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value); 
                            accumulatorGrande = accumulatorGrande + Convert.ToInt32(myDataGridView.Rows[contadorFilas].Cells[3].Value);                            
                        }
                        contadorFilas++;
                    }
                    
                    accumulatorUshuaia = 0;
                    accumulatorGrande = 0;
                    miDataTable.Clear();
                    myDataGridView.DataSource = null;//reinicia datagv
                    myDataGridView.Rows.Clear();
                    myDataGridView.Refresh();
                }
                //suma array subtotalesUshuaiaET
                for (int i = 0; i <= subTotalesUshuaiaET.Length - 1; i++)
                {
                    for (int j = 0; j <= cantColegiosUshuaia - 1; j++)
                    {
                        subTotalesUshuaiaET[i] = subTotalesUshuaiaET[i] + colegiosUshuaiaET[j, i];
                    }
                }
                //suma array subtotalesUshuaiaBO
                for (int i = 0; i <= subTotalesUshuaiaBO.Length - 1; i++)
                {
                    for (int j = 0; j <= cantColegiosUshuaia - 1; j++)
                    {
                        subTotalesUshuaiaBO[i] = subTotalesUshuaiaBO[i] + colegiosUshuaiaBO[j, i];
                    }
                }
                //suma array subtotalesUshuaiaEsp
                for (int i = 0; i <= subTotalesUshuaiaEsp.Length - 1; i++)
                {
                    for (int j = 0; j <= cantColegiosUshuaia - 1; j++)
                    {
                        subTotalesUshuaiaEsp[i] = subTotalesUshuaiaEsp[i] + colegiosUshuaiaEsp[j, i];
                    }
                }
                //suma array subtotalesGrandeET
                for (int i = 0; i <= subTotalesGrandeET.Length - 1; i++)
                {
                    for (int j = 0; j <= cantColegiosGrande - 1; j++)
                    {
                        subTotalesGrandeET[i] = subTotalesGrandeET[i] + colegiosGrandeET[j, i];
                    }
                }
                //suma array subtotalesGrandeBO
                for (int i = 0; i <= subTotalesGrandeBO.Length - 1; i++)
                {
                    for (int j = 0; j <= cantColegiosGrande - 1; j++)
                    {
                        subTotalesGrandeBO[i] = subTotalesGrandeBO[i] + colegiosGrandeBO[j, i];
                    }
                }
                //suma array subTotalesGrandeEsp
                for (int i = 0; i <= subTotalesGrandeEsp.Length - 1; i++)
                {
                    for (int j = 0; j <= cantColegiosGrande - 1; j++)
                    {
                        subTotalesGrandeEsp[i] = subTotalesGrandeEsp[i] + colegiosGrandeEsp[j, i];
                    }
                }
                //Suma array total ushuauia
                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)
                {
                    for (int j = 0; j <= subTotalesUshuaiaET.Length - 1; j++)
                    {
                        totalUshuaia[i] = totalUshuaia[i] + colegiosUshuaiaET[i, j];
                    }
                    for (int j = 0; j <= subTotalesUshuaiaBO.Length - 1; j++)
                    {
                        totalUshuaia[i] = totalUshuaia[i] + colegiosUshuaiaBO[i, j];
                    }
                    for (int j = 0; j <= subTotalesUshuaiaEsp.Length - 1; j++)
                    {
                        totalUshuaia[i] = totalUshuaia[i] + colegiosUshuaiaEsp[i, j];
                    }
                }
                //Suma array total Rio grande
                for (int i = 0; i <= cantColegiosGrande - 1; i++)
                {
                    for (int j = 0; j <= subTotalesGrandeET.Length - 1; j++)
                    {
                        totalGrande[i] = totalGrande[i] + colegiosGrandeET[i, j];
                    }
                    for (int j = 0; j <= subTotalesGrandeBO.Length - 1; j++)
                    {
                        totalGrande[i] = totalGrande[i] + colegiosGrandeBO[i, j];
                    }
                    for (int j = 0; j <= subTotalesGrandeEsp.Length - 1; j++)
                    {
                        totalGrande[i] = totalGrande[i] + colegiosGrandeEsp[i, j];
                    }
                }
                //totalGeneral Ushuaia
                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)
                {
                    totalGenUshuaia = totalGenUshuaia + totalUshuaia[i];
                }
                //totalGeneral Ushuaia
                for (int i = 0; i <= cantColegiosGrande - 1; i++)
                {
                    totalGenGrande = totalGenGrande + totalGrande[i];
                }
                //totales provinciales 
                for (int i = 0; i <= totalesProvET.Length - 1; i++)
                {
                    totalesProvET[i] = totalesProvET[i] + subTotalesUshuaiaET[i];
                    totalesProvET[i] = totalesProvET[i] + subTotalesGrandeET[i];

                }
                for (int i = 0; i <= totalesProvBO.Length - 1; i++)
                {
                    totalesProvBO[i] = totalesProvBO[i] + subTotalesUshuaiaBO[i];
                    totalesProvBO[i] = totalesProvBO[i] + subTotalesGrandeBO[i];

                }
                for (int i = 0; i <= totalesProvEsp.Length - 1; i++)
                {
                    totalesProvEsp[i] = totalesProvEsp[i] + subTotalesUshuaiaEsp[i];
                    totalesProvEsp[i] = totalesProvEsp[i] + subTotalesGrandeEsp[i];
                }
                //totl general provincial
                totalGenProvincial = totalGenUshuaia + totalGenGrande;

                lblProcesando.Visible = false;
                MessageBox.Show("Estadistica generada.", "Sistema Informa");
                btnCrearExcel.Enabled = true;                
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
            }
            btnCrearExcel.Enabled = true;
            btnCrearExcel.BackColor = System.Drawing.Color.DodgerBlue;
            colorExcel = 7;
        }

        private void btnCrearExcel_Click(object sender, EventArgs e)
        {
            lblCreando.Visible = true;
            Refresh();
            colorExcel = 6; //color par es igual a silver, sirve para que no se bugueee azul            
            btnCrearExcel.BackColor = System.Drawing.Color.Silver;
            btnCrearExcel.Enabled = false;
            try
            {              
                //creando Estadisticas
                string userName = Environment.UserName;

                string destinationFile = @"C:/Users/" + userName + "/Downloads/Por Orientación " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString() + ".xlsx";

                SLDocument sl = new SLDocument();

                string pathFile = destinationFile;

                sl.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Por Orientación");//renombra la Hoja
                                                                                        //titulo estadistica
                tituloEstadistica = "ESTUDIANTES POR ORIENTACIÓN Y/O ESPECIALIDAD (CICLO SUPERIOR) - " + cboxAño.SelectedItem.ToString() + " " + cboxPeriodo.SelectedItem.ToString();

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
                styleFondoRosa.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSalmon, System.Drawing.Color.White);

                SLStyle styleFondoGrisA = sl.CreateStyle();//Crea el fondo GRISaZUL
                styleFondoGrisA.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSteelBlue, System.Drawing.Color.White);

                SLStyle styleFondoBordo = sl.CreateStyle();//Crea el fondo bordo
                styleFondoBordo.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.DarkRed, System.Drawing.Color.White);

                SLStyle styleFondoVerdeCrema = sl.CreateStyle();//Crea el fondo verde claro
                styleFondoVerdeCrema.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.PaleGreen, System.Drawing.Color.White);

                SLStyle styleFondoIndianRed = sl.CreateStyle();//Crea el fondo indian red
                styleFondoIndianRed.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.IndianRed, System.Drawing.Color.White);

                //FONDOSUSHUAIA
                //Titulo FONDO  USHUAIA
                sl.SetCellStyle(1, 1, 1, 32, styleFondoAzulGray);//asigna fondo azul a las entre las siguientes celdas

                //tITULOS DE ORIENTACIONES Educación Técnica
                sl.SetCellStyle(2, 3, 2, 12, styleFondoGrisA);
                sl.SetCellStyle(3, 3, 3, 12, styleFondoGrisA);
                //tITULOS DE ORIENTACIONES Bachiller Orientado 
                sl.SetCellStyle(2, 13, 2, 24, styleFondoRosa);
                sl.SetCellStyle(3, 13, 3, 24, styleFondoRosa);
                //tITULOS DE ORIENTACIONES Bachiller especializado en Arte
                sl.SetCellStyle(2, 25, 2, 33, styleFondoLime);
                sl.SetCellStyle(3, 25, 3, 33, styleFondoLime);

                //fondo depto y establecimiento
                sl.SetCellStyle("A2", "B3", styleFondoAzulGray);
                //fondo TOTAL
                sl.SetCellStyle("Ah2", "Ah3", styleFondoAzul);


                //fondo ushuaia ARRANCAR A PONER CON CANTIDAD de colegios
                sl.SetCellStyle(4, 1, 4 + cantColegiosUshuaia - 1, 1, styleFondoCeleste);

                //fondo rio grande ARRANCAR A PONER CON CANTIDAD de colegios
                sl.SetCellStyle(6 + cantColegiosUshuaia, 1, 6 + cantColegiosUshuaia + cantColegiosGrande - 1, 1, styleFondoCeleste);

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
                sl.SetCellStyle(1, 1, 5 + cantColegiosUshuaia, 34, styleBorderBasicTop);    //5 PORQUE ES EL BORDE DE TOP      
                sl.SetCellStyle(1, 1, 4 + cantColegiosUshuaia, 35, styleBorderBasicLeft);  //35 porque el borde es left  
                sl.SetCellStyle(7 + cantColegiosUshuaia, 31, 7 + cantColegiosUshuaia, 33, styleBorderBasicLeft); //bordes en total ushauaia

                //bordes horizontales y verticales normales Rio grande
                sl.SetCellStyle((6 + cantColegiosUshuaia), 1, (7 + cantColegiosUshuaia + cantColegiosGrande), 34, styleBorderBasicTop);    //6 PORQUE ES EL BORDE DE TOP        
                sl.SetCellStyle((6 + cantColegiosUshuaia), 1, (6 + cantColegiosUshuaia + cantColegiosGrande), 35, styleBorderBasicLeft);
                sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 34, 6 + cantColegiosUshuaia + cantColegiosGrande, 34, styleBorderBasicLeft); //bordes en total rio grande

                //bordes horizontales y verticales normales Totales provincial
                sl.SetCellStyle((8 + cantColegiosUshuaia + cantColegiosGrande), 1, (9 + cantColegiosUshuaia + cantColegiosGrande), 34, styleBorderBasicTop);//8 PORQUE ES EL BORDE DE TOP        
                sl.SetCellStyle((8 + cantColegiosUshuaia + cantColegiosGrande), 1, (8 + cantColegiosUshuaia + cantColegiosGrande), 35, styleBorderBasicLeft);

                //bordes horizontales y verticale pie de pagina
                sl.SetCellStyle((10 + cantColegiosUshuaia + cantColegiosGrande), 1, (22 + cantColegiosUshuaia + cantColegiosGrande), 34, styleBorderBasicTop);
                sl.SetCellStyle((10 + cantColegiosUshuaia + cantColegiosGrande), 1, (21 + cantColegiosUshuaia + cantColegiosGrande), 35, styleBorderBasicLeft);


                //Espacio dentro de las celdas##########
                //##espacios Filas Ushuaia
                sl.SetRowHeight(1, 36);//titulo
                sl.SetRowHeight(2, 28);//ciclo
                sl.SetRowHeight(3, 24);//ciclo
                sl.SetRowHeight(4 + cantColegiosUshuaia, 22);//subtotal ushauia
                sl.SetRowHeight(6 + cantColegiosUshuaia + cantColegiosGrande, 22);//subtotal rio grande

                sl.SetRowHeight(8 + cantColegiosUshuaia + cantColegiosGrande, 22);//total provincial

                //##espacios columnas
                sl.SetColumnWidth(1, 11);//Depto
                sl.SetColumnWidth(2, 35);//Establecimiento

                for (int i = 3; i <= 34; i++)//espacios en las columnas 3 al 32
                {
                    sl.SetColumnWidth(i, 8);
                }
                sl.SetColumnWidth(5, 10);
                sl.SetColumnWidth(7, 10);
                sl.SetColumnWidth(13, 12);
                sl.SetColumnWidth(14, 13);
                sl.SetColumnWidth(21, 10);
                sl.SetColumnWidth(25, 16);
                sl.SetColumnWidth(26, 10);
                sl.SetColumnWidth(27, 14);
                sl.SetColumnWidth(28, 10);
                sl.SetColumnWidth(29, 11);
                sl.SetColumnWidth(34, 10);
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

                //Arial 16 Black
                SLStyle styleBlack16 = sl.CreateStyle();
                styleBlack16.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack16.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlack16.Font.FontColor = System.Drawing.Color.Black;//color
                styleBlack16.Font.FontName = "Arial";//tipo de fuente
                styleBlack16.Font.FontSize = 16;//tamaño
                styleBlack16.Font.Bold = true;//negrita

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

                //Arial 18 Black
                SLStyle styleBlack18 = sl.CreateStyle();
                styleBlack18.SetWrapText(true);//ajustar texto
                styleBlack18.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack18.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlack18.Font.FontColor = System.Drawing.Color.Black;//color
                styleBlack18.Font.FontName = "Arial";//tipo de fuente
                styleBlack18.Font.FontSize = 18;//tamaño
                styleBlack18.Font.Bold = true;//negrita

                //Arial 14 blue
                SLStyle styleBlue14 = sl.CreateStyle();
                styleBlue14.Alignment.Horizontal = HorizontalAlignmentValues.Center;//aliniacion
                styleBlack14.Alignment.Vertical = VerticalAlignmentValues.Center;
                styleBlue14.Font.FontColor = System.Drawing.Color.Blue;//color
                styleBlue14.Font.FontName = "Arial";//tipo de fuente
                styleBlue14.Font.FontSize = 14;//tamaño
                styleBlue14.Font.Bold = true;//negrita

                styleWhite14_90G.SetWrapText(true);//ajustar texto

                //##CONTENIDO Celdas USHUAIA###### 
                //Titulo
                sl.SetCellValue("A1", tituloEstadistica); //set value
                sl.SetCellStyle(1, 1, styleTitle);//
                sl.MergeWorksheetCells("A1", "AH1");//COMBINAR
                                                    //subtitulo 1
                sl.SetCellValue(2, 3, "Educación Técnica"); //set value
                sl.SetCellStyle(2, 3, styleBlack16);//
                sl.MergeWorksheetCells(2, 3, 2, 12);//COMBINAR

                //subtitulo 2
                sl.SetCellValue(2, 13, "Bachiller Orientado"); //set value
                sl.SetCellStyle(2, 13, styleBlack16);//
                sl.MergeWorksheetCells(2, 13, 2, 24);//COMBINAR

                //subtitulo 3
                sl.SetCellValue(2, 25, "Bachiller Especializado en Arte"); //set value
                sl.SetCellStyle(2, 25, styleBlack16);//
                sl.MergeWorksheetCells(2, 25, 2, 33);//COMBINAR

                //orientaciones Educación Técnica
                for (int i = 3; i <= 3 + 9; i++)//3+9 porque arranca en lugar 3 y pinta la cantidad de orientanciones
                {
                    sl.SetCellValue(3, i, educacionTecnica[0, i - 3]); //set value
                    sl.SetCellStyle(3, i, styleBlack16);//
                }
                //orientaciones Bachiller Orientado"
                for (int i = 13; i <= 13 + 11; i++)//3+11 porque arranca en lugar 3 y pinta la cantidad de orientanciones
                {
                    sl.SetCellValue(3, i, bachOrientado[0, i - 13]); //set value
                    sl.SetCellStyle(3, i, styleBlack16);//
                }
                //orientaciones Bachiller especializado en Arte
                for (int i = 25; i <= 25 + 9 - 1; i++)//3+9-1 porque arranca en lugar 3 y pinta la cantidad de orientanciones
                {
                    sl.SetCellValue(3, i, bachEspecializado[0, i - 25]); //set value
                    sl.SetCellStyle(3, i, styleBlack16);//
                }

                //Depto y establecimiento
                sl.SetCellValue("A2", "Depto."); //set value
                sl.SetCellStyle("A2", styleWhite18);
                sl.MergeWorksheetCells("A2", "A3");//COMBINAR
                sl.SetCellValue("B2", "Establecimiento"); //set value
                sl.SetCellStyle("B2", styleWhite18);
                sl.MergeWorksheetCells("B2", "B3");//COMBINAR           
                                                   //ushuaia
                sl.SetCellValue("A4", "Ushuaia"); //set value
                sl.SetCellStyle("A4", styleBlack16_90G);
                sl.MergeWorksheetCells(4, 1, 4 + cantColegiosUshuaia - 1, 1);//COMBINAR

                //total
                sl.SetCellValue("AH2", "TOTAL"); //set value
                sl.SetCellStyle("AH2", styleWhite14);
                sl.MergeWorksheetCells("AH2", "AH3");//COMBINAR  

                //Nombre de los establecimientos Ushuaia
                for (int i = 4; i <= (cantColegiosUshuaia + 4-1); i++)
                {
                    sl.SetCellValue(i, 2, ushuaiaColegios[0, i - 4]); //set value
                    sl.SetCellStyle(i, 2, styleBlack14);
                }
                //subtotal USHUAIA
                sl.SetCellValue(4 + cantColegiosUshuaia, 1, "Subtotal Ushuaia "); //set value
                sl.SetCellStyle(4 + cantColegiosUshuaia, 1, styleBlack14);
                sl.SetCellStyle(4 + cantColegiosUshuaia, 1, styleFondoCremaNaranja);
                sl.MergeWorksheetCells(4 + cantColegiosUshuaia, 1, 4 + cantColegiosUshuaia, 2);//COMBINAR

                //completa educacion tecnica ushuaia           
                counterFilaArray = 0;
                for (int i = 4; i <= 4 + cantColegiosUshuaia - 1; i++)//I=COLEGIO
                {
                    for (int j = 3; j <= 12; j++)
                    {
                        if (colegiosUshuaiaET[counterFilaArray, j - 3] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosUshuaiaET[counterFilaArray, j - 3]); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                            sl.SetCellStyle(i, j, styleFondoGrisA);
                        }
                    }
                    counterFilaArray++;
                }

                //completa bachiller orientado ushuaia           
                counterFilaArray = 0;
                for (int i = 4; i <= 4 + cantColegiosUshuaia - 1; i++)//I=COLEGIO
                {
                    for (int j = 13; j <= 24; j++)
                    {
                        if (colegiosUshuaiaBO[counterFilaArray, j - 13] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosUshuaiaBO[counterFilaArray, j - 13]); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                            sl.SetCellStyle(i, j, styleFondoRosa);
                        }
                    }
                    counterFilaArray++;
                }
                //completa bachiller especializado ushuaia           
                counterFilaArray = 0;
                for (int i = 4; i <= 4 + cantColegiosUshuaia - 1; i++)//I=COLEGIO
                {
                    for (int j = 25; j <= 33; j++)
                    {
                        if (colegiosUshuaiaEsp[counterFilaArray, j - 25] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i, j, "-"); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i, j, colegiosUshuaiaEsp[counterFilaArray, j - 25]); //set value
                            sl.SetCellStyle(i, j, styleBlack14);
                            sl.SetCellStyle(i, j, styleFondoLime);
                        }
                    }
                    counterFilaArray++;
                }

                //subtotales ushuaia educacion tecnica
                for (int i = 0; i <= subTotalesUshuaiaET.Length - 1; i++)
                {
                    sl.SetCellValue(4 + cantColegiosUshuaia, 3 + i, subTotalesUshuaiaET[i]); //set value
                    sl.SetCellStyle(4 + cantColegiosUshuaia, 3 + i, styleWhite14);
                    sl.SetCellStyle(4 + cantColegiosUshuaia, 3 + i, styleFondoAzul);
                }

                //subtotales ushuaia bachiller orientado
                for (int i = 0; i <= subTotalesUshuaiaBO.Length - 1; i++)
                {
                    sl.SetCellValue(4 + cantColegiosUshuaia, 13 + i, subTotalesUshuaiaBO[i]); //set value
                    sl.SetCellStyle(4 + cantColegiosUshuaia, 13 + i, styleWhite14);
                    sl.SetCellStyle(4 + cantColegiosUshuaia, 13 + i, styleFondoIndianRed);
                }

                //subtotales ushuaia bachiller especializado
                for (int i = 0; i <= subTotalesUshuaiaEsp.Length - 1; i++)
                {
                    sl.SetCellValue(4 + cantColegiosUshuaia, 25 + i, subTotalesUshuaiaEsp[i]); //set value
                    sl.SetCellStyle(4 + cantColegiosUshuaia, 25 + i, styleWhite14);
                    sl.SetCellStyle(4 + cantColegiosUshuaia, 25 + i, styleFondoVerde);
                }
                //totales ushuaia 
                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)
                {
                    sl.SetCellValue(4 + i, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, totalUshuaia[i]); //set value
                    sl.SetCellStyle(4 + i, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleBlack14);
                }
                //total general ushuaia
                sl.SetCellValue(4 + cantColegiosUshuaia, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, totalGenUshuaia); //set value
                sl.SetCellStyle(4 + cantColegiosUshuaia, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleWhite14);
                sl.SetCellStyle(4 + cantColegiosUshuaia, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleFondoAzul);

                //subtotales Ushuaia
                for (int i = 0; i <= cantColegiosUshuaia - 1; i++)
                {
                    sl.SetCellValue(4 + i, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, totalUshuaia[i]); //set value
                    sl.SetCellStyle(4 + i, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleBlack14);
                    sl.SetCellStyle(4 + i, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleFondoCremaCeleste);
                }

                //##CONTENIDO DE LAS CELDAS RIO GRANDE
                //Rio Grande
                sl.SetCellValue(6 + cantColegiosUshuaia, 1, "Rio Grande"); //set value
                sl.SetCellStyle(6 + cantColegiosUshuaia, 1, styleBlack16_90G);
                sl.MergeWorksheetCells(6 + cantColegiosUshuaia, 1, 5 + cantColegiosUshuaia + cantColegiosGrande, 1);//COMBINAR

                //Nombre de los establecimientos RIO GRANDE
                for (int i = (6 + cantColegiosUshuaia); i <= (cantColegiosGrande + cantColegiosUshuaia + 6 - 1); i++)
                {
                    sl.SetCellValue(i, 2, grandeColegios[0, (i - 6 - cantColegiosUshuaia)]); //set value
                    sl.SetCellStyle(i, 2, styleBlack14);
                }

                //subtotal Rio grande
                sl.SetCellValue(6 + cantColegiosUshuaia + cantColegiosGrande, 1, "Subtotal Rio Grande "); //set value
                sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBlack14);
                sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 1, styleFondoCremaNaranja);
                sl.MergeWorksheetCells(6 + cantColegiosUshuaia + cantColegiosGrande, 1, 6 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

                //completa educacion tecnica Rio Grande        
                counterFilaArray = 0;
                for (int i = 6; i <= 6 + cantColegiosGrande - 1; i++)
                {
                    for (int j = 3; j <= 12; j++)
                    {
                        if (colegiosGrandeET[counterFilaArray, j - 3] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i + cantColegiosUshuaia, j, "-"); //set value
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i + cantColegiosUshuaia, j, colegiosGrandeET[counterFilaArray, j - 3]); //set value
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleBlack14);
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleFondoGrisA);
                        }
                    }
                    counterFilaArray++;
                }

                //completa bachiller orientado Rio Grande          
                counterFilaArray = 0;
                for (int i = 6; i <= 6 + cantColegiosGrande - 1; i++)
                {
                    for (int j = 13; j <= 24; j++)
                    {
                        if (colegiosGrandeBO[counterFilaArray, j - 13] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i + cantColegiosUshuaia, j, "-"); //set value
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i + cantColegiosUshuaia, j, colegiosGrandeBO[counterFilaArray, j - 13]); //set value
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleBlack14);
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleFondoRosa);
                        }
                    }
                    counterFilaArray++;
                }
                //completa bachiller especializado Rio grande        
                counterFilaArray = 0;
                for (int i = 6; i <= 6 + cantColegiosGrande - 1; i++)
                {
                    for (int j = 25; j <= 33; j++)
                    {
                        if (colegiosGrandeEsp[counterFilaArray, j - 25] == 0)//si el valor es 0 le pone un guion
                        {
                            sl.SetCellValue(i + cantColegiosUshuaia, j, "-"); //set value
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleBlack14);
                        }
                        else//si tiene valor, lo pone
                        {
                            sl.SetCellValue(i + cantColegiosUshuaia, j, colegiosGrandeEsp[counterFilaArray, j - 25]); //set value
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleBlack14);
                            sl.SetCellStyle(i + cantColegiosUshuaia, j, styleFondoLime);
                        }
                    }
                    counterFilaArray++;
                }

                //subtotales Grande educacion tecnica
                for (int i = 0; i <= subTotalesGrandeET.Length - 1; i++)
                {
                    sl.SetCellValue(6 + cantColegiosUshuaia + cantColegiosGrande, 3 + i, subTotalesGrandeET[i]); //set value
                    sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 3 + i, styleWhite14);
                    sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 3 + i, styleFondoAzul);

                }

                //subtotales Grande bachiller orientado
                for (int i = 0; i <= subTotalesGrandeBO.Length - 1; i++)
                {
                    sl.SetCellValue(6 + cantColegiosUshuaia + cantColegiosGrande, 13 + i, subTotalesGrandeBO[i]); //set value
                    sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 13 + i, styleWhite14);
                    sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 13 + i, styleFondoIndianRed);
                }

                //subtotales Grande bachiller especializado
                for (int i = 0; i <= subTotalesGrandeEsp.Length - 1; i++)
                {
                    sl.SetCellValue(6 + cantColegiosUshuaia + cantColegiosGrande, 25 + i, subTotalesGrandeEsp[i]); //set value
                    sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 25 + i, styleWhite14);
                    sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 25 + i, styleFondoVerde);

                }
                //totales Rio grande
                for (int i = 0; i <= cantColegiosGrande - 1; i++)
                {
                    sl.SetCellValue(6 + cantColegiosUshuaia + i, 3 + subTotalesGrandeET.Length + subTotalesGrandeBO.Length + subTotalesGrandeEsp.Length, totalGrande[i]); //set value
                    sl.SetCellStyle(6 + cantColegiosUshuaia + i, 3 + subTotalesGrandeET.Length + subTotalesGrandeBO.Length + subTotalesGrandeEsp.Length, styleBlack14);
                    sl.SetCellStyle(6 + cantColegiosUshuaia + i, 3 + subTotalesGrandeET.Length + subTotalesGrandeBO.Length + subTotalesGrandeEsp.Length, styleFondoCremaCeleste);
                }
                //total general Grande
                sl.SetCellValue(6 + cantColegiosUshuaia + cantColegiosGrande, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, totalGenGrande); //set value
                sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleWhite14);
                sl.SetCellStyle(6 + cantColegiosUshuaia + cantColegiosGrande, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleFondoAzul);

                //##CONTENIDO DE LAS CELDAS TOTAL PROVINCIAL
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 1, "Total Provincial "); //set value
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBlack14);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 1, styleFondoVerdeCrema);

                sl.MergeWorksheetCells(8 + cantColegiosUshuaia + cantColegiosGrande, 1, 8 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR

                //totales Provinciales
                for (int i = 0; i <= totalesProvET.Length - 1; i++)
                {
                    sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + i, totalesProvET[i]); //set value
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + i, styleWhite14);
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + i, styleFondoAzul);
                }
                for (int i = 0; i <= totalesProvBO.Length - 1; i++)
                {
                    sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + totalesProvET.Length + i, totalesProvBO[i]); //set value
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + totalesProvET.Length + i, styleWhite14);
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + totalesProvET.Length + i, styleFondoIndianRed);
                }
                for (int i = 0; i <= totalesProvEsp.Length - 1; i++)
                {
                    sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + totalesProvET.Length + totalesProvBO.Length + i, totalesProvEsp[i]); //set value
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + totalesProvET.Length + totalesProvBO.Length + i, styleWhite14);
                    sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + totalesProvET.Length + totalesProvBO.Length + i, styleFondoVerde);
                }
                //total general provincial
                sl.SetCellValue(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, totalGenProvincial); //set value
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleWhite14);
                sl.SetCellStyle(8 + cantColegiosUshuaia + cantColegiosGrande, 3 + subTotalesUshuaiaET.Length + subTotalesUshuaiaBO.Length + subTotalesUshuaiaEsp.Length, styleFondoBordo);

                //##pie de pagina
                sl.SetCellValue(10 + cantColegiosUshuaia + cantColegiosGrande, 1, "ORIENTACIONES \ny/o \nESPECIALIDADES"); //set value
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 1, styleBlack18);
                sl.SetCellStyle(10 + cantColegiosUshuaia + cantColegiosGrande, 1, styleFondoCeleste);
                sl.MergeWorksheetCells(10 + cantColegiosUshuaia + cantColegiosGrande, 1, 21 + cantColegiosUshuaia + cantColegiosGrande, 2);//COMBINAR
                                                                                                                                           //educacion tecnica
                for (int i = 0; i <= subTotalesUshuaiaET.Length - 1; i++)
                {
                    sl.SetCellValue(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 3, educacionTecnica[0, i]); //set value
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 3, styleBlack14);
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 3, styleFondoGrisA);
                    sl.MergeWorksheetCells(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 3, i + 10 + cantColegiosUshuaia + cantColegiosGrande, 4);//COMBINAR

                    sl.SetCellValue(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 5, educacionTecnica[1, i]); //set value
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 5, styleBlack14);
                    sl.MergeWorksheetCells(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 5, i + 10 + cantColegiosUshuaia + cantColegiosGrande, 12);//COMBINAR
                }
                sl.MergeWorksheetCells(20 + cantColegiosUshuaia + cantColegiosGrande, 3, 21 + cantColegiosUshuaia + cantColegiosGrande, 12);//COMBINAR espacion en blanco

                //Bachiller Orientado
                for (int i = 0; i <= subTotalesUshuaiaBO.Length - 1; i++)
                {
                    sl.SetCellValue(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 13, bachOrientado[0, i]); //set value
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 13, styleBlack14);
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 13, styleFondoRosa);
                    sl.MergeWorksheetCells(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 13, i + 10 + cantColegiosUshuaia + cantColegiosGrande, 14);//COMBINAR

                    sl.SetCellValue(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 15, bachOrientado[1, i]); //set value
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 15, styleBlack14);
                    sl.MergeWorksheetCells(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 15, i + 10 + cantColegiosUshuaia + cantColegiosGrande, 24);//COMBINAR
                }

                //Bachiller Especializado
                for (int i = 0; i <= subTotalesUshuaiaEsp.Length - 1; i++)
                {
                    sl.SetCellValue(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 25, bachEspecializado[0, i]); //set value
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 25, styleBlack14);
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 25, styleFondoLime);
                    sl.MergeWorksheetCells(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 25, i + 10 + cantColegiosUshuaia + cantColegiosGrande, 26);//COMBINAR

                    sl.SetCellValue(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 27, bachEspecializado[1, i]); //set value
                    sl.SetCellStyle(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 27, styleBlack14);
                    sl.MergeWorksheetCells(i + 10 + cantColegiosUshuaia + cantColegiosGrande, 27, i + 10 + cantColegiosUshuaia + cantColegiosGrande, 34);//COMBINAR
                }
                sl.MergeWorksheetCells(19 + cantColegiosUshuaia + cantColegiosGrande, 25, 21 + cantColegiosUshuaia + cantColegiosGrande, 34);//COMBINAR espacion en blanco

                sl.SaveAs(pathFile);
                btnCrearExcel.Enabled = false;
                lblCreando.Visible = false;
                MessageBox.Show("Excel Generado", "Sistema Informa");
                ordenar();
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
        private void btnDeclaracion_MouseMove(object sender, MouseEventArgs e)
        {
            btnDeclaracion.BackColor = System.Drawing.Color.DimGray;
        }

        private void btnDeclaracion_MouseLeave(object sender, EventArgs e)
        {
            if (colorDecla == 0)//unica forma de que funcione
            {
                btnDeclaracion.BackColor = System.Drawing.Color.Silver;
            }
            else if (colorDecla == 1)
            {
                btnDeclaracion.BackColor = System.Drawing.Color.DodgerBlue;
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
