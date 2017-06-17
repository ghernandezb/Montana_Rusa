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
            MontanaRusa montanaRusa = new MontanaRusa(1, 1, 1.5f, 3, 5);

            bool opcionMenu = true;
            int numeroMenu = -1;
            int valorInicialMenu = 0;
            int valorFinalMenu = 6;
            Random ran = new Random();

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
                    Console.WriteLine("La atraccion termino.");
                    montanaRusa.FechaSalida = null;

                    // Inicio de bajada de gente
                    foreach (Carrito carrito in montanaRusa.Carritos)
                    {
                        foreach (Asiento asiento in carrito.Asientos)
                        {
                            if (asiento.Persona != null)
                            {
                                int volveraSubir = ran.Next(0, 100);

                                if (volveraSubir <= 20)
                                {
                                    montanaRusa.ListaEspera.Add(asiento.Persona);
                                    Console.WriteLine(asiento.Persona.Nombre + " ha ingresado nuevamente a la fila.");
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    Console.WriteLine(asiento.Persona.Nombre + " ha salido de la atraccion.");
                                }
                            }
                            asiento.Persona = null;
                        }
                    }
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

                            if (numeroIngresados > 0)
                            {
                                if (montanaRusa.FechaEspera == null)
                                {
                                    montanaRusa.FechaEspera = DateTime.Now;
                                }

                                if (montanaRusa.ObtenerCampoDisponible() == null)
                                {
                                    Console.WriteLine("Todos los carritos estan llenos.");
                                    Console.WriteLine("");
                                }
                                else
                                {
                                    Console.WriteLine("Carritos aun no llenos, esperando mas pasajeros.");
                                    Console.WriteLine("");
                                }
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
                        bool listo;

                        foreach (Carrito carrito in montanaRusa.Carritos)
                        {
                            foreach (Asiento asiento in carrito.Asientos)
                            {
                                asiento.Asegurado = ran.Next(0, 100) < 99;
                            }
                        }

                        var listaDeChequeo = montanaRusa.Chequear(out listo);
                        var numeroPersonasAsustadas = 0;
                        var numeroPersonasEnojadas = 0;
                        var numeroAsientosNoAsegurados = 0;

                        foreach (var item in listaDeChequeo)
                        {
                            switch (item)
                            {
                                case ListaChequeo.PersonaEnojada:
                                    numeroPersonasEnojadas++;
                                    break;
                                case ListaChequeo.PersonaAsustada:
                                    numeroPersonasAsustadas++;
                                    break;
                                case ListaChequeo.AsientoNoAsegurado:
                                    numeroAsientosNoAsegurados++;
                                    break;
                            }
                        }

                        if (listaDeChequeo.Contains(ListaChequeo.AtraccionEnCurso))
                        {
                            Console.WriteLine("No se puede iniciar atraccion que ya esta en curso.");
                        }

                        if (listaDeChequeo.Contains(ListaChequeo.TiempoDeEsperaNoFinalizado))
                        {
                            Console.WriteLine("No se puede iniciar atraccion que esta en espera.");
                        }

                        if (numeroAsientosNoAsegurados > 0)
                        {
                            Console.WriteLine("No se puede iniciar por que hay " + numeroAsientosNoAsegurados + " asiento(s) no asegurado(s).");
                        }

                        if (numeroPersonasAsustadas > 0)
                        {
                            Console.WriteLine("No se puede iniciar por que hay " + numeroPersonasAsustadas + " persona(s) asustada(s).");
                        }

                        if (numeroPersonasEnojadas > 0)
                        {
                            Console.WriteLine("No se puede iniciar por que hay " + numeroPersonasEnojadas + " persona(s) enojada(s).");
                        }

                        if (listaDeChequeo.Contains(ListaChequeo.AtraccionVacia))
                        {
                            Console.WriteLine("No se puede iniciar atraccion ya que se encuentra vacia o falta gente.");
                        }

                        if (listo)
                        {
                            montanaRusa.FechaEspera = null;
                            montanaRusa.FechaSalida = DateTime.Now;
                            Console.WriteLine("La atraccion se encuentra en curso.");
                        }

                        break;

                    case 3:
                        // Refrescar lista de espera
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
                        foreach (Carrito carrito in montanaRusa.Carritos)
                        {
                            foreach (Asiento asiento in carrito.Asientos)
                            {
                                if (!asiento.Asegurado)
                                {
                                    asiento.Asegurado = true;
                                    Console.WriteLine("Un asiento se ha asegurado.");
                                    Console.WriteLine("");
                                }
                            }
                        }
                        break;

                    case 5:
                        // bajar gente asustada
                        foreach (Carrito carrito in montanaRusa.Carritos)
                        {
                            foreach (Asiento asiento in carrito.Asientos)
                            {
                                if (asiento.Persona !=null && asiento.Persona.EstadoAnimo == PersonaEstadosAnimos.Asustado)
                                {
                                    string tempName = asiento.Persona.Nombre;
                                    asiento.Persona = null;
                                    asiento.Asegurado = false;
                                    Console.WriteLine(tempName + " ha salido de la atraccion pues se encontraba asustad@.");
                                    Console.WriteLine("");
                                }
                            }
                        }
                        break;

                    case 6:
                        // Bajar gente violenta
                        foreach (Carrito carrito in montanaRusa.Carritos)
                        {
                            foreach (Asiento asiento in carrito.Asientos)
                            {
                                if (asiento.Persona != null && asiento.Persona.EstadoAnimo == PersonaEstadosAnimos.Enojado)
                                {
                                    string tempName = asiento.Persona.Nombre;
                                    asiento.Persona = null;
                                    asiento.Asegurado = false;
                                    Console.WriteLine(tempName + " ha salido de la atraccion pues se encontraba violent@.");
                                    Console.WriteLine("");
                                }
                            }
                        }
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
