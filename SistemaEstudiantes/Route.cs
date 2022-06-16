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
           //ruta = "|DataDirectory|BDNormativa.mdb";//para ejecutarlo a modo prueba

            string conexion = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + ruta;
            conexionBaseDatos = new OleDbConnection(conexion);

            return conexionBaseDatos;           
        }
        public string CarpetaPDF(string rutaPDF)
        {
            string carpetaPDF;
            
            //rutaPDF = @"C:\Users\Pablo\Desktop\Sistema-Supervision\SistemaEstudiantes\bin\Debug\carpetaPDF\"; //para ejecutarlo a modo prueba 
            //rutaPDF = @"C:\Users\Arteok\Desktop\Sistema-Supervision\SistemaEstudiantes\bin\Debug\carpetaPDF\"; //para ejecutarlo a modo prueba 
            carpetaPDF = rutaPDF; 

            return carpetaPDF;            
        }
    }
}
