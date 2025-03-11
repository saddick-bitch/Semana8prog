using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using VehiculosApp.DAL;
using VehiculosApp.Modelos;

namespace VehiculosApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Your existing Main method code...
            
            // If you want to test database connection, call it here
            // TestDatabaseConnection();
        }

        // Your existing methods like InsertarVehiculo, MostrarVehiculos, etc.
        
        // Add the test database connection method inside the class
        static void TestDatabaseConnection()
        {
            string connectionString = @"Data Source=YOURCOMPUTERNAME\SQLEXPRESS;Initial Catalog=VehiculosDB;Integrated Security=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connection successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database connection failed: {ex.Message}");
            }
        }
        
        static void Separador()
        {
            Console.WriteLine(new string('-', 20));
        }
    }
}
namespace VehiculosApp.Modelos
{
    // Creado una clase abstracta llamada Vehiculo
    public abstract class Vehiculo
    {
        // Se han agregado propiedades
        protected string Marca;
        protected string Modelo;
        protected int Año;
        
        // Propiedad para el ID del vehículo
        public int Id { get; set; }

        // Se creo un constructor
        public Vehiculo(string marca, string modelo, int año)
        {
            Marca = marca;
            Modelo = modelo;
            Año = año;
        }

        // Se creo un método abstracto
        public abstract string MostrarDetalles();
    }

    // Se crea una clase hija Automovil que hereda de la clase Vehiculo
    public class Automovil : Vehiculo
    {
        protected int NumeroPuertas;

        // Implementar un constructor para pasar los argumentos necesarios
        // para inicializar la clase padre
        public Automovil(string marca, string modelo, int año, int numeroPuertas) : base(marca, modelo, año)
        {
            NumeroPuertas = numeroPuertas;
        }

        // Implementar el método abstracto 'MostrarDetalles()' en la sub-clase
        public override string MostrarDetalles()
        {
            return $"ID: {Id}\nAutomovil\nMarca: {base.Marca}\nModelo: {base.Modelo}\nAño: {base.Año}\nN° Puertas: {this.NumeroPuertas}";
        }
    }

    // Se crea una clase hija Motocicleta que hereda de la clase Vehiculo
    public class Motocicleta : Vehiculo
    {
        // Implementar un constructor para pasar los argumentos necesarios
        // para inicializar la clase padre
        public Motocicleta(string marca, string modelo, int año) : base(marca, modelo, año)
        {
        }

        // Implementar el método abstracto 'MostrarDetalles()' en la sub-clase
        public override string MostrarDetalles()
        {
            return $"ID: {Id}\nMotocicleta\nMarca: {base.Marca}\nModelo: {base.Modelo}\nAño: {base.Año}";
        }
    }
}