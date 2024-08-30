using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {
        // Método que obtiene todos los registros de la tabla Customers
        public List<Customers> ObtenerTodos()
        {
            // Establecemos la conexión con la base de datos utilizando un objeto SqlConnection.
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para seleccionar todos los campos de la tabla Customers.
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Ejecutamos la consulta utilizando un SqlCommand.
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader();
                    List<Customers> Customers = new List<Customers>();

                    // Iteramos sobre los resultados de la consulta y los agregamos a la lista de clientes.
                    while (reader.Read())
                    {
                        var customers = LeerDelDataReader(reader);
                        Customers.Add(customers);
                    }
                    // Devolvemos la lista completa de clientes.
                    return Customers;
                }
            }
        }

        // Método que obtiene un cliente específico por su ID.
        public Customers ObtenerPorID(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para seleccionar un cliente específico por su ID.
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + $"  Where CustomerID = @customerId";

                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    // Asignamos el valor del parámetro CustomerID.
                    comando.Parameters.AddWithValue("customerId", id);

                    // Ejecutamos la consulta y obtenemos el resultado.
                    var reader = comando.ExecuteReader();
                    Customers customers = null;

                    // Si hay un registro, lo leemos y lo asignamos a la variable customers.
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader);
                    }
                    // Devolvemos el cliente encontrado.
                    return customers;
                }
            }
        }

        // Método que lee un cliente desde un SqlDataReader y lo mapea a un objeto Customers.
        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers customers = new Customers();

            // Asignamos los valores de las columnas al objeto Customers, validando si son nulos.
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];

            // Devolvemos el objeto Customers con los datos cargados.
            return customers;
        }

        // Método para insertar un nuevo cliente en la base de datos.
        public int InsertarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para insertar un nuevo cliente en la tabla Customers.
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                // Ejecutamos la consulta de inserción y devolvemos el número de filas insertadas.
                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    int insertados = parametrosCliente(customer, comando);
                    return insertados;
                }
            }
        }

        // Método para actualizar un cliente existente en la base de datos.
        public int ActualizarCliente(Customers customer)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para actualizar un cliente específico por su ID.
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";

                // Ejecutamos la consulta de actualización y devolvemos el número de filas afectadas.
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                {
                    int actualizados = parametrosCliente(customer, comando);
                    return actualizados;
                }
            }
        }

        // Método para asignar los parámetros de un cliente a un SqlCommand.
        public int parametrosCliente(Customers customer, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName);
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);

            // Ejecutamos la consulta y devolvemos el número de filas afectadas.
            var insertados = comando.ExecuteNonQuery();
            return insertados;
        }

        // Método para eliminar un cliente de la base de datos por su ID.
        public int EliminarCliente(string id)
        {
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Construimos la consulta SQL para eliminar un cliente específico por su ID.
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";

                // Ejecutamos la consulta de eliminación y devolvemos el número de filas eliminadas.
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                {
                    comando.Parameters.AddWithValue("@CustomerID", id);
                    int eliminados = comando.ExecuteNonQuery();
                    return eliminados;
                }
            }
        }
    }
}
