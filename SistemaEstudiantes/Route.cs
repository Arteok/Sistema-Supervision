using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace SistemaEstudiantes
{
    class Route
    {
        OleDbConnection conexionBaseDatos;       

        public OleDbConnection ConexionBaseDatos(string ruta)
        {
            string conexion = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ruta;
            conexionBaseDatos = new OleDbConnection(conexion);

            return conexionBaseDatos;           
        }
    }
}
