using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiculosApp.DAL;
using VehiculosApp.Modelos;

namespace VehiculosApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// 
            //Vehiculo vehiculo1 = new Automovil("Nissan", "Frontier", 2010, 2);
            //Vehiculo vehiculo2 = new Motocicleta("Yamaha","YMT-03", 2015);

            //// Mostrar el detalle de cada objetos
            //Console.WriteLine(vehiculo1.MostrarDetalles());
            //Console.WriteLine(new string('-', 10));
            //Console.WriteLine(vehiculo2.MostrarDetalles());

            bool activado = true;

            do
            {
                Console.WriteLine("Bienvenido a la Consesionaria de Vehículos El Salvador");
                Separador();
                Console.WriteLine("Seleccione una opción: ");
                Separador();
                Console.WriteLine("1. Insertar un vehiculo\n2. Mostrar los vehiculos almacenados\n3. Actualizar un vehículo\n0. Salir");
                Separador();

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 0:
                        Console.WriteLine("Gracias por visitarnos, lo esperamos pronto");
                        activado = false;
                        break;
                    case 1:
                        InsertarVehiculo();
                        break;
                    case 2:
                        MostrarVehiculos();
                        break;
                    case 3:
                        ActualizarVehiculo();
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta vuelve a intentarlo");
                        break;
                }
                Separador();

            } while (activado);

            Console.ReadLine();
        }

        private static void MostrarVehiculos()
        {
            VehiculosDAL vehiculoDAL = new VehiculosDAL();

            List<Vehiculo> vehiculos = vehiculoDAL.ObtenerVehiculos();
            Separador();
            foreach (Vehiculo vehiculo in vehiculos)
            {
                Console.WriteLine(vehiculo.MostrarDetalles());
                Separador();
            }
            Separador();
        }

        private static void InsertarVehiculo()
        {
            VehiculosDAL vehiculoDAL = new VehiculosDAL();

            Console.Write("Escribe la Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Escribe el Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Digital el Año: ");
            int año = int.Parse(Console.ReadLine());

            Console.Write("Elige el Tipo de Vehículo (Automovil/Motocicleta): ");
            string tipo = Console.ReadLine();

            if (tipo.Equals("Automovil"))
            {
                Console.Write("Digital el N° de Puertas: ");
                int puertas = int.Parse(Console.ReadLine());

                vehiculoDAL.GuardarVehiculo(marca, modelo, año, tipo, puertas);
            }
            else
            {
                vehiculoDAL.GuardarVehiculo(marca, modelo, año, tipo);
            }           
        }

        private static void ActualizarVehiculo()
        {
            // Primero mostrar los vehículos para que el usuario pueda ver los IDs
            Console.WriteLine("Vehículos disponibles para actualizar:");
            MostrarVehiculos();

            VehiculosDAL vehiculoDAL = new VehiculosDAL();

            // Solicitar el ID del vehículo a actualizar
            Console.Write("Ingrese el ID del vehículo que desea actualizar: ");
            int id = int.Parse(Console.ReadLine());

            // Solicitar los nuevos datos
            Console.Write("Nueva Marca (deje en blanco para mantener actual): ");
            string marca = Console.ReadLine();

            Console.Write("Nuevo Modelo (deje en blanco para mantener actual): ");
            string modelo = Console.ReadLine();

            Console.Write("Nuevo Año (0 para mantener actual): ");
            string añoStr = Console.ReadLine();
            int año = !string.IsNullOrEmpty(añoStr) ? int.Parse(añoStr) : 0;

            Console.Write("Nuevo Tipo de Vehículo (Automovil/Motocicleta) (deje en blanco para mantener actual): ");
            string tipo = Console.ReadLine();

            int puertas = 0;
            if (tipo.Equals("Automovil") || string.IsNullOrEmpty(tipo))
            {
                Console.Write("Nuevo N° de Puertas (0 para mantener actual): ");
                string puertasStr = Console.ReadLine();
                puertas = !string.IsNullOrEmpty(puertasStr) ? int.Parse(puertasStr) : 0;
            }

            // Obtener los datos actuales del vehículo para completar los campos vacíos
            List<Vehiculo> vehiculos = vehiculoDAL.ObtenerVehiculos();
            Vehiculo vehiculoActual = vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculoActual == null)
            {
                Console.WriteLine("No se encontró un vehículo con el ID especificado.");
                return;
            }

            // Completar los datos que no se actualizaron
            if (string.IsNullOrEmpty(marca))
                marca = ((dynamic)vehiculoActual).Marca;

            if (string.IsNullOrEmpty(modelo))
                modelo = ((dynamic)vehiculoActual).Modelo;

            if (año == 0)
                año = ((dynamic)vehiculoActual).Año;

            if (string.IsNullOrEmpty(tipo))
            {
                if (vehiculoActual is Automovil)
                    tipo = "Automovil";
                else
                    tipo = "Motocicleta";
            }

            if (puertas == 0 && tipo.Equals("Automovil"))
            {
                if (vehiculoActual is Automovil autoActual)
                    puertas = ((dynamic)autoActual).NumeroPuertas;
                else
                    puertas = 4; // Valor predeterminado
            }

            // Actualizar el vehículo
            bool actualizado = vehiculoDAL.ActualizarVehiculo(id, marca, modelo, año, tipo, puertas);

            if (actualizado)
            {
                Console.WriteLine("Vehículo actualizado correctamente.");
            }
            else
            {
                Console.WriteLine("No se pudo actualizar el vehículo.");
            }
        }

        private static void Separador()
        {
            Console.WriteLine(new string('-', 20));
        }
    }
}