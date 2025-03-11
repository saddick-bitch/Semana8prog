using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehiculosApp.Modelos
{
    // Creado una clase abstracta llamada Vehiculo
    public abstract class Vehiculo
    {
        // Se han agregado propiedades
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        
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
        public int NumeroPuertas { get; set; }

        // Implementar un constructor para pasar los argumentos necesarios
        // para inicializar la clase padre
        public Automovil(string marca, string modelo, int año, int numeroPuertas) : base(marca, modelo, año)
        {
            NumeroPuertas = numeroPuertas;
        }

        // Implementar el método abstracto 'MostrarDetalles()' en la sub-clase
        public override string MostrarDetalles()
        {
            return $"ID: {Id}\nAutomovil\nMarca: {Marca}\nModelo: {Modelo}\nAño: {Año}\nN° Puertas: {NumeroPuertas}";
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
            return $"ID: {Id}\nMotocicleta\nMarca: {Marca}\nModelo: {Modelo}\nAño: {Año}";
        }
    }
}