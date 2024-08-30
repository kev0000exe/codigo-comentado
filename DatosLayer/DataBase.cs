using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DatosLayer
{
    public class DataBase
    {
        // Propiedad estática que obtiene la cadena de conexión a la base de datos.
        public static string ConnectionString
        {
            get
            {
                // Obtenemos la cadena de conexión desde el archivo de configuración.
                string CadenaConexion = ConfigurationManager
                    .ConnectionStrings["NWConnection"]
                    .ConnectionString;

                // Creamos un SqlConnectionStringBuilder para manipular la cadena de conexión.
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Establecemos el nombre de la aplicación, si está definido.
                conexionBuilder.ApplicationName =
                    ApplicationName ?? conexionBuilder.ApplicationName;

                // Establecemos el tiempo de espera de conexión, si es mayor que 0.
                conexionBuilder.ConnectTimeout = (ConnectionTimeout > 0)
                    ? ConnectionTimeout : conexionBuilder.ConnectTimeout;

                // Devolvemos la cadena de conexión final.
                return conexionBuilder.ToString();
            }
        }

        // Propiedad estática para definir el tiempo de espera de la conexión.
        public static int ConnectionTimeout { get; set; }

        // Propiedad estática para definir el nombre de la aplicación que se conectará.
        public static string ApplicationName { get; set; }

        // Método estático que abre y devuelve una conexión SQL.
        public static SqlConnection GetSqlConnection()
        {
            // Creamos una nueva instancia de SqlConnection utilizando la cadena de conexión configurada.
            SqlConnection conexion = new SqlConnection(ConnectionString);

            // Abrimos la conexión a la base de datos.
            conexion.Open();

            // Devolvemos la conexión abierta.
            return conexion;
        }
    }
}
