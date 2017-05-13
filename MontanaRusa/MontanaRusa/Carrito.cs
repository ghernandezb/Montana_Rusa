using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontanaRusa
{
    class Carrito
    {
        public List<Asiento> Asientos { get; private set; }

        public Carrito(int cantidadAsientos = 0)
        {
            Asientos = new List<Asiento>();

            for (int i = 0; i < cantidadAsientos; i++)
            {
                Asientos.Add(new Asiento());
            }
        }
    }
}
