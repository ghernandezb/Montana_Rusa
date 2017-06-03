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
        PersonaEnojada,
        PersonaAsustada,
        TiempoDeEsperaNoFinalizado,
        AtraccionEnCurso,
        AtraccionVacia
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
            MinutosEspera = minutosEspera;
            MinutosDuracion = minutosDuracion;

            for (int i = 0; i < numeroCarritos; i++)
            {
                Carritos.Add(new Carrito(numeroAsientos));
            }
        }

        public List<ListaChequeo> Chequear(out bool pasoLista)
        {
            List<ListaChequeo> listaChequeo = new List<ListaChequeo>();

            if (FechaSalida != null)
            {
                listaChequeo.Add(ListaChequeo.AtraccionEnCurso);
            }
            else if (FechaEspera != null)
            {
                listaChequeo.Add(ListaChequeo.TiempoDeEsperaNoFinalizado);
            }

            foreach (Carrito carrito in Carritos)
            {
                foreach (Asiento asiento in carrito.Asientos)
                {
                    if (!asiento.Asegurado)
                    {
                        listaChequeo.Add(ListaChequeo.AsientoNoAsegurado);
                    }

                    if (asiento.Persona != null)
                    {
                        if (asiento.Persona.EstadoAnimo == PersonaEstadosAnimos.Asustado)
                        {
                            listaChequeo.Add(ListaChequeo.PersonaAsustada);
                        }
                        else if (asiento.Persona.EstadoAnimo == PersonaEstadosAnimos.Enojado)
                        {
                            listaChequeo.Add(ListaChequeo.PersonaEnojada);
                        }
                    }
                }
            }

            if (EstaVacio())
            {
                listaChequeo.Add(ListaChequeo.AtraccionVacia);
            }

            pasoLista = listaChequeo.Count == 0;
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

        public bool EstaVacio()
        {
            foreach (Carrito carrito in Carritos)
            {
                foreach (Asiento asiento in carrito.Asientos)
                {
                    if (asiento.Persona != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
