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
            bool activado = true;

            do
            {
                Console.WriteLine("Bienvenido a la Concesionaria de Vehículos El Salvador");
                Separador();
                Console.WriteLine("Seleccione una opción: ");
                Separador();
                Console.WriteLine("1. Insertar un vehiculo\n2. Mostrar los vehiculos almacenados\n3. Actualizar un vehículo\n0. Salir");
                Separador();

                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Entrada inválida. Ingrese un número.");
                    Separador();
                    continue;
                }

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
            if (vehiculos.Count == 0)
            {
                Console.WriteLine("No hay vehículos para mostrar.");
                return;
            }

            Separador();
            foreach (Vehiculo vehiculo in vehiculos)
            {
                Console.WriteLine(vehiculo.MostrarDetalles());
                Separador();
            }
        }

        private static void InsertarVehiculo()
        {
            VehiculosDAL vehiculoDAL = new VehiculosDAL();

            Console.Write("Escribe la Marca: ");
            string marca = Console.ReadLine();

            Console.Write("Escribe el Modelo: ");
            string modelo = Console.ReadLine();

            Console.Write("Digital el Año: ");
            int año;
            if (!int.TryParse(Console.ReadLine(), out año))
            {
                Console.WriteLine("Año inválido. Se usará 2025 por defecto.");
                año = 2025;
            }

            Console.Write("Elige el Tipo de Vehículo (Automovil/Motocicleta): ");
            string tipo = Console.ReadLine();

            if (tipo.Equals("Automovil", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Digital el N° de Puertas: ");
                int puertas;
                if (!int.TryParse(Console.ReadLine(), out puertas))
                {
                    Console.WriteLine("Número de puertas inválido. Se usará 4 por defecto.");
                    puertas = 4;
                }

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
            List<Vehiculo> vehiculos = vehiculoDAL.ObtenerVehiculos();
            
            if (vehiculos.Count == 0)
            {
                Console.WriteLine("No hay vehículos para actualizar.");
                return;
            }

            // Solicitar el ID del vehículo a actualizar
            Console.Write("Ingrese el ID del vehículo que desea actualizar: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Obtener los datos actuales del vehículo para completar los campos vacíos
            Vehiculo vehiculoActual = vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculoActual == null)
            {
                Console.WriteLine("No se encontró un vehículo con el ID especificado.");
                return;
            }

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
            if (tipo.Equals("Automovil", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(tipo))
            {
                Console.Write("Nuevo N° de Puertas (0 para mantener actual): ");
                string puertasStr = Console.ReadLine();
                puertas = !string.IsNullOrEmpty(puertasStr) ? int.Parse(puertasStr) : 0;
            }

            // Completar los datos que no se actualizaron
            if (string.IsNullOrEmpty(marca))
                marca = vehiculoActual.Marca;

            if (string.IsNullOrEmpty(modelo))
                modelo = vehiculoActual.Modelo;

            if (año == 0)
                año = vehiculoActual.Año;

            if (string.IsNullOrEmpty(tipo))
            {
                if (vehiculoActual is Automovil)
                    tipo = "Automovil";
                else
                    tipo = "Motocicleta";
            }

            if (puertas == 0 && tipo.Equals("Automovil", StringComparison.OrdinalIgnoreCase))
            {
                if (vehiculoActual is Automovil autoActual)
                    puertas = autoActual.NumeroPuertas;
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