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
        protected string Marca;
        protected string Modelo;
        protected int Año;

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
            return $"Automovil\nMarca: {base.Marca}\nModelo: {base.Modelo}\nAño: {base.Año}\nN° Puertas: {this.NumeroPuertas}";
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
            return $"Motocicleta\nMarca: {base.Marca}\nModelo: {base.Modelo}\nAño: {base.Año}";
        }
    }
}
