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
            MontanaRusa montanaRusa = new MontanaRusa(2, 3, 1.5f, 3, 5);

            bool opcionMenu = true;
            int numeroMenu = -1;
            int valorInicialMenu = 0;
            int valorFinalMenu = 6;

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

                            Console.WriteLine("Han ingresado " + numeroIngresados + " nuevos pasajeros.");
                            Console.WriteLine("");

                            if (montanaRusa.ObtenerCampoDisponible() == null)
                            {
                                montanaRusa.FechaEspera = null;
                                Console.WriteLine("Todos los carritos estan llenos, listo para salir.");
                                Console.WriteLine("");
                            }
                            else if (numeroIngresados > 0 && montanaRusa.FechaEspera == null)
                            {
                                montanaRusa.FechaEspera = DateTime.Now;
                                Console.WriteLine("Carritos aun no llenos, esperando mas pasajeros.");
                                Console.WriteLine("");
                            }
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
                        Random ran = new Random();
                        int numeroRandom = ran.Next(0, 15);

                        for (int i = 1; i<= numeroRandom; i++)
                        {
                            string nombre = Listas.nombres[ran.Next(0, Listas.nombres.Count - 1)] 
                                          + " " 
                                          + Listas.apellidos[ran.Next(0, Listas.apellidos.Count - 1)] 
                                          + " " 
                                          + Listas.apellidos[ran.Next(0, Listas.apellidos.Count - 1)];
                            float altura = (float)Math.Round(NextDouble(1, 2), 2);
                            PersonaEstadosAnimos animoActual;
                            int estado = ran.Next(0, 100);

                            if (estado <= 33)
                            {
                                animoActual = PersonaEstadosAnimos.Normal;
                            }
                            else if (estado <= 66)
                            {
                                animoActual = PersonaEstadosAnimos.Asustado;
                            }
                            else
                            {
                                animoActual = PersonaEstadosAnimos.Enojado;
                            }
                            montanaRusa.ListaEspera.Add(new Persona(nombre, altura, animoActual));
                            Console.WriteLine(nombre + " ha agregado a la fila.");
                            Console.WriteLine("");
                        }

                        Console.WriteLine("Han ingresado " + numeroRandom + " a la fila.");
                        Console.WriteLine("");

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
        public static double NextDouble(double min, double max)
        {
            return new Random().NextDouble() * (max - min) + min;
        }
    }

    class Listas
    {
        public static readonly List<string> nombres = new List<string>(new string[] { "Emma", "Liam", "Olivia", "Noah", "Ava", "Mason", "Sophia", "Lucas", "Isabella", "Oliver", "Mia", "Ethan", "Charlotte", "Elijah", "Amelia", "Aiden", "Harper", "Logan", "Aria", "James", "Abigail", "Benjamin", "Ella", "Jackson", "Evelyn", "Jacob", "Avery", "Carter", "Emily", "Sebastian", "Madison", "Alexander", "Scarlett", "Michael", "Sofia", "Matthew", "Lily", "Jayden", "Mila", "Jack", "Riley", "Luke", "Layla", "Wyatt", "Chloe", "Daniel", "Ellie", "Gabriel", "Grace", "William", "Zoey", "Grayson", "Penelope", "Henry", "Aubrey", "Owen", "Elizabeth", "Levi", "Victoria", "Jaxon", "Hannah", "Lincoln", "Nora", "Adam", "Stella", "David", "Addison", "Isaiah", "Luna", "Joseph", "Natalie", "Julian", "Paisley", "Josiah", "Skylar", "Ryan", "Savannah", "Samuel", "Maya", "Eli", "Camila", "Dylan", "Hazel", "Nathan", "Lillian", "Joshua", "Lucy", "Isaac", "Bella", "John", "Brooklyn", "Caleb", "Audrey", "Andrew", "Aaliyah", "Hunter", "Leah", "Leo", "Zoe", "Muhammad" });
        public static readonly List<string> apellidos = new List<string>(new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Garcia", "Rodriguez", "Wilson", "Martinez", "Anderson", "Taylor", "Thomas", "Hernandez", "Moore", "Martin", "Jackson", "Thompson", "White", "Lopez", "Lee", "Gonzalez", "Harris", "Clark", "Lewis", "Robinson", "Walker", "Perez", "Hall", "Young", "Allen", "Sanchez", "Wright", "King", "Scott", "Green", "Baker", "Adams", "Nelson", "Hill", "Ramirez", "Campbell", "Mitchell", "Roberts", "Carter", "Phillips", "Evans", "Turner", "Torres", "Parker", "Collins", "Edwards", "Stewart", "Flores", "Morris", "Nguyen", "Murphy", "Rivera", "Cook", "Rogers", "Morgan", "Peterson", "Cooper", "Reed", "Bailey", "Bell", "Gomez", "Kelly", "Howard", "Ward", "Cox", "Diaz", "Richardson", "Wood", "Watson", "Brooks", "Bennett", "Gray", "James", "Reyes", "Cruz", "Hughes", "Price", "Myers", "Long", "Foster", "Sanders", "Ross", "Morales", "Powell", "Sullivan", "Russell", "Ortiz", "Jenkins", "Gutierrez", "Perry", "Butler", "Barnes", "Fisher" });
    }
}
