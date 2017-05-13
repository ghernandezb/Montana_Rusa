using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontanaRusa
{
    enum ListaChequeo
    {
        AsientoNoAsegurado = 1,

    }

    class MontanaRusa
    {
        public DateTime? FechaSalida { get; set; }
        public DateTime? FechaEspera { get; set; }
        public int MinutosEspera { get; private set; }
        public int MinutosDuracion { get; private set; }
        public float AlturaMinima { get; set; }
        public List<Carrito> Carritos { get; private set; }
        public List<Persona> ListaEspera { get; set; }

        public MontanaRusa(int minutosEspera, int minutosDuracion, float alturaMinima, int numeroCarritos, int numeroAsientos)
        {
            AlturaMinima = alturaMinima;
            Carritos = new List<Carrito>();
            ListaEspera = new List<Persona>();

            for (int i = 0; i < numeroCarritos; i++)
            {
                Carritos.Add(new Carrito(numeroAsientos));
            }
        }

        public List<ListaChequeo> Chequear()
        {
            List<ListaChequeo> listaChequeo = new List<ListaChequeo>();

            foreach (Carrito carrito in Carritos)
            {
                foreach (Asiento asiento in carrito.Asientos)
                {
                    if (!asiento.Asegurado)
                    {
                        listaChequeo.Add(ListaChequeo.AsientoNoAsegurado);
                    }
                }
            }

            return listaChequeo;
        }

        public Asiento ObtenerCampoDisponible()
        {
            foreach (Carrito carrito in Carritos)
            {
                foreach (Asiento asiento in carrito.Asientos)
                {
                    if (asiento.Persona == null)
                    {
                        return asiento;
                    }
                }
            }
            return null;
        }
    }
}
