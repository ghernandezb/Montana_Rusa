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
                Console.WriteLine("4. Asegurar asientos no asegurados");
                Console.WriteLine("5. Bajar gente asustada");
                Console.WriteLine("6. Bajar gente violenta");
                Console.WriteLine("0. Salir del programa");
                Console.WriteLine("");

                string respuestaMenu = Console.ReadLine();
                numeroMenu = ComprobacionNumero(respuestaMenu, valorInicialMenu, valorFinalMenu);
                if (DateTime.Now.AddMinutes(-montanaRusa.MinutosDuracion) > montanaRusa.FechaSalida)
                {
                    montanaRusa.FechaSalida = null;
                }
                switch (numeroMenu)
                {
                    case 1:
                        // Montar pasajeros
                        int numeroIngresados = 0;
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
                                        numeroIngresados++;
                                        Console.WriteLine("Ha ingresado " + persona.Nombre);
                                        Console.WriteLine("");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No hay mas campos disponibles");
                                        Console.WriteLine("");
                                        break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(persona.Nombre + " no cumple con la estatura requerida, se ha removido de la fila ");
                                    Console.WriteLine("");
                                    montanaRusa.ListaEspera.RemoveAt(i);
                                    i--;
                                }
                            }
                            Console.WriteLine("Han ingresado "+ numeroIngresados + " nuevos pasajeros.");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                        }
                        else
                        {
                            Console.WriteLine("La atraccion no se ha detenido");
                            Console.WriteLine("");
                        }                       

                        break;

                    case 2:
                        // Correr atraccion

                        break;

                    case 3:
                        // Refrescar lista de espera

                        break;

                    case 4:
                        // Asegurar asientos no asegurados

                        break;

                    case 5:
                        // bajar gente asustada

                        break;

                    case 6:
                        // Bajar gente violenta

                        break;

                    case 0:
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
