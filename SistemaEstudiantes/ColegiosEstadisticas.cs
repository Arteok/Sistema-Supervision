using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEstudiantes
{
    class ColegiosEstadisticas
    {
        //OleDbConnection conexionBaseDatos = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = |DataDirectory|BDNormativa.mdb");  //variable que recibe la direccion de la base de datos
        //OleDbConnection conexionBaseDatos = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = \\server\BASES\Sistema\BDSistema Supervision\BDSistSupervision.mdb");
        OleDbConnection conexionBaseDatos;
        string[,] ushuaiaColegios = new string[3, 25];//nombre,posicion,nombreAbreviado de los colegios de ushuaia ##tomo como cantidad maxima 25 colegios por depto
        string[,] grandeColegios = new string[3, 25];//nombre,posicion,nombreAbreviado de los colegios de rio grande
        int numColegiosUshuaia;
        int numColegiosGrande;

        public void ConexionBD(OleDbConnection conexionBD)
        {
            conexionBaseDatos = conexionBD;
        }
        public void CargarColegiosUshuaia()
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT Nombre, NombreAbreviado,NumeroOrden FROM ColegiosUshuaia";
            OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
            miDataAdapter.Fill(miDataTable);
            miDataTable.DefaultView.Sort = "NumeroOrden";//ordena el datatable de forma ascendente por la columna que se le indica

            DataRow[] rows = miDataTable.Select();

            // Print the value one column of each DataRow.
            for (int i = 0; i < rows.Length; i++)
            {
                ushuaiaColegios[0, i] = Convert.ToString(rows[i]["Nombre"]);
                ushuaiaColegios[1, i] = Convert.ToString(rows[i]["NombreAbreviado"]);
                ushuaiaColegios[2, i] = Convert.ToString(rows[i]["NumeroOrden"]);
                numColegiosUshuaia++;
            }
        }
        public void CargarColegiosGrande()
        {
            DataTable miDataTable = new DataTable();

            string queryCargarBD = "SELECT Nombre, NombreAbreviado, NumeroOrden FROM ColegiosGrande";
            OleDbCommand sqlComando = new OleDbCommand(queryCargarBD, conexionBaseDatos);

            OleDbDataAdapter miDataAdapter = new OleDbDataAdapter(sqlComando);
            miDataAdapter.Fill(miDataTable);
            miDataTable.DefaultView.Sort = "NumeroOrden";//ordena el datatable de forma ascendente por la columna que se le indica

            DataRow[] rows = miDataTable.Select();

            // Print the value one column of each DataRow.
            for (int i = 0; i < rows.Length; i++)
            {
                grandeColegios[0, i] = Convert.ToString(rows[i]["Nombre"]);
                grandeColegios[1, i] = Convert.ToString(rows[i]["NombreAbreviado"]);
                grandeColegios[2, i] = Convert.ToString(rows[i]["NumeroOrden"]);
                numColegiosGrande++;
            }
        }
        public int NumColegiosUshuaia
        {
            get
            {
                return numColegiosUshuaia;
            }
        }
        public int NumColegiosGrande
        {
            get
            {
                return numColegiosGrande;
            }
        }

        public string[,] UshuaiaColegios
        {
            get
            {
                return ushuaiaColegios;
            }            
        }
        public string[,] GrandeColegios
        {
            get
            {
                return grandeColegios;
            }
        }
    }
}
