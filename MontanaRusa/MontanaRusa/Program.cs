using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontanaRusa
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
            
        }

        //----- MENU -----//
        public static void Menu()
        {
            MontanaRusa montanaRusa = new MontanaRusa(2, 3, 1.6f, 3, 5);

            bool opcionMenu = true;
            int numeroMenu = 0;
            int valorInicialMenu = 1;
            int valorFinalMenu = 4;


            Console.WriteLine("Hola!");
            Console.WriteLine("");

            do
            {
                Console.WriteLine("Que desea hacer?");
                Console.WriteLine("");
                Console.WriteLine("1. Montar pasajeros");
                Console.WriteLine("2. Correr atraccion");
                Console.WriteLine("3. Refrescar lista de espera");
                Console.WriteLine("4. Salir del programa");
                Console.WriteLine("");

                string respuestaMenu = Console.ReadLine();
                numeroMenu = ComprobacionNumero(respuestaMenu, valorInicialMenu, valorFinalMenu);
                if (DateTime.Now.AddMinutes(-5) > montanaRusa.FechaSalida)
                {
                    montanaRusa.FechaSalida = null;
                }
                switch (numeroMenu)
                {
                    case 1:
                        // Montar pasajeros
                        if (montanaRusa.FechaSalida == null)
                        {
                            for (int i = 0; i < montanaRusa.ListaEspera.Count; i++)
                            {
                                Persona persona = montanaRusa.ListaEspera[i];
                                if (persona.Altura >= montanaRusa.AlturaMinima)
                                {
                                    Asiento asiento = montanaRusa.ObtenerCampoDisponible();
                                    if (asiento != null)
                                    {
                                        asiento.Persona = persona;
                                        montanaRusa.ListaEspera.RemoveAt(i);
                                        i--;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    montanaRusa.ListaEspera.RemoveAt(i);
                                    i--;
                                }
                            }
                        }                        

                        break;

                    case 2:
                        // Correr atraccion

                        break;

                    case 3:
                        // Refrescar lista de espera

                        break;

                    case 4:
                        // Salir
                        opcionMenu = false;
                        break;

                    default:
                        // Error
                        Console.WriteLine("");
                        Console.WriteLine("El valor de ´" + respuestaMenu + "´, no es valido.");
                        Console.WriteLine("");
                        Console.WriteLine("Por favor ingrese un valor entre " + valorInicialMenu + " y " + valorFinalMenu + ".");
                        Console.WriteLine("");
                        Console.WriteLine("Por favor indiquenos,");
                        break;
                }

            } while (opcionMenu);
        }

        //----- FUNCION PROBAR NUMERO DE MENU -----//
        public static int ComprobacionNumero(string datoIngresado, int numA, int numB)
        {
            if (!(int.TryParse(datoIngresado, out int opcion) && opcion >= numA && opcion <= numB))
            {
                opcion = -1;
            }
            return opcion;
        }

    }
}
