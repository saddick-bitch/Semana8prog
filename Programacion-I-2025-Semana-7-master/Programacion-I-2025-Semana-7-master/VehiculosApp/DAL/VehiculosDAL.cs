using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiculosApp.Modelos;

namespace VehiculosApp.DAL
{
    public class VehiculosDAL
    {
        // Crear una cadena de conexión hacia nuestra base de datos.
        private static readonly string cadenaConexion = "Data Source=DESKTOP-F4UDN3G\\SQLEXPRESS;Initial Catalog=VehiculosDB;Integrated Security=True;";

        // 1. Crear una método para mostrar un listado de vehiculos
        public List<Vehiculo> ObtenerVehiculos()
        {
            // Creamos nuestra lista de vehiculos a retornar
            List<Vehiculo> vehiculos = new List<Vehiculo>();

            // 2. Creamos nuestra cadena de conexión
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                // 3. Crear nuestro comando a ejecutar
                string textoComando = "SP_SELECT_VEHICULOS";

                SqlCommand comando = new SqlCommand(textoComando, conexion);

                // 4. Especificamos que el comando es un Procedimiento Almacenado
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                // 5. Abrir la conexión
                conexion.Open();

                // 6. Crear nuestro DataReader para leer la información

                SqlDataReader lectura = comando.ExecuteReader();

                // 7. Recorrer el resultado
                while (lectura.Read())
                {
                    // Almacenar los vehiculos obtenidos en el listado de 'vehiculos'
                    string marca = lectura.GetString(1);
                    string modelo = lectura.GetString(2);
                    int año = lectura.GetInt32(3);
                    int puertas = lectura.GetInt32(4);
                    string tipo = lectura.GetString(5);

                    if (tipo.Equals("Automovil"))
                    {
                        vehiculos.Add(new Automovil(marca, modelo, año, puertas));
                    }
                    else
                    {
                        vehiculos.Add(new Motocicleta(marca, modelo, año));
                    }
                }                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }           

            // 8. Retornar el resultado
            return vehiculos;
        }        
        
        // 2. Crear un método para almacenar un vehiculo
        public void GuardarVehiculo(string marca, string modelo, int año, string tipo, int puertas = 0) // párametro opcional
        {
            // Crear la conexión con SqlConnection
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                // Creamos el comando
                string textoComando = "SP_CREAR_VEHICULOS";

                SqlCommand comando = new SqlCommand(textoComando, conexion);

                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Marca", marca);
                comando.Parameters.AddWithValue("@Modelo", modelo);
                comando.Parameters.AddWithValue("@Año", año);
                comando.Parameters.AddWithValue("@NumeroPuertas", puertas);
                comando.Parameters.AddWithValue("@TipoVehiculo", tipo);

                // Abrir la conexion
                conexion.Open();

                // Ejecutamos el comandod y guardamos el Id resultado
                int resultado = Convert.ToInt32(comando.ExecuteScalar());

                if (resultado > 0) {
                    Console.WriteLine("El vehículo fue registrado con exito!!");
                }
                else
                {
                    Console.WriteLine("Ocurrio un error en el registro");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ocurrio un excepción");
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}
