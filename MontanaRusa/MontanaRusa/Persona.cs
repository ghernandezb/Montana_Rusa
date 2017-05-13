using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontanaRusa
{
    enum PersonaEstadosAnimos
    {
        Normal = 1,
        Asustado,
        Enojado
    }

    class Persona
    {
        public string Nombre { get; private set; }
        public float Altura { get; private set; }
        public PersonaEstadosAnimos EstadoAnimo { get; private set; }

        public Persona (string nombre, float altura, PersonaEstadosAnimos estadoAnimo)
        {
            Nombre = nombre;
            Altura = altura;
            EstadoAnimo = estadoAnimo;
        }
    }
}
