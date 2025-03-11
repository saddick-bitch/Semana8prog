using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehiculosApp.Modelos;

namespace VehiculosApp.DAL
{
    public class VehiculosDAL
    {
        // Lista para simular la base de datos
        private static List<Vehiculo> _vehiculos = new List<Vehiculo>();
        private static int _nextId = 1;

        // Método para obtener todos los vehículos
        public List<Vehiculo> ObtenerVehiculos()
        {
            return _vehiculos;
        }

        // Método para guardar un vehículo
        public void GuardarVehiculo(string marca, string modelo, int año, string tipo, int puertas = 0)
        {
            Vehiculo nuevoVehiculo;

            if (tipo.Equals("Automovil", StringComparison.OrdinalIgnoreCase))
            {
                nuevoVehiculo = new Automovil(marca, modelo, año, puertas);
            }
            else
            {
                nuevoVehiculo = new Motocicleta(marca, modelo, año);
            }

            nuevoVehiculo.Id = _nextId++;
            _vehiculos.Add(nuevoVehiculo);

            Console.WriteLine("Vehículo guardado correctamente.");
        }

        // Método para actualizar un vehículo
        public bool ActualizarVehiculo(int id, string marca, string modelo, int año, string tipo, int puertas = 0)
        {
            // Buscar el vehículo por ID
            int index = _vehiculos.FindIndex(v => v.Id == id);
            if (index == -1)
                return false;

            // Eliminar el vehículo anterior
            _vehiculos.RemoveAt(index);

            // Crear un nuevo vehículo con los datos actualizados
            Vehiculo vehiculoActualizado;
            if (tipo.Equals("Automovil", StringComparison.OrdinalIgnoreCase))
            {
                vehiculoActualizado = new Automovil(marca, modelo, año, puertas);
            }
            else
            {
                vehiculoActualizado = new Motocicleta(marca, modelo, año);
            }

            // Mantener el mismo ID
            vehiculoActualizado.Id = id;

            // Agregar el vehículo actualizado a la lista
            _vehiculos.Insert(index, vehiculoActualizado);

            return true;
        }
    }
}