using System;
using System.Linq;
using System.IO; // Para leer ficheros

namespace Hangman
{
    class Program
    {   

        static void Main(string[] args)
        {
            // Declaro la variable de tipo StreamReader para abrir el fichero secuencial
            StreamReader rf;
            int vidas = 6;
            bool continua = true; // Variable para saber si continúa el juego
            char SiNo; // Variable para saber si el jugador quiere seguir jugando o ya no
            //String Path = "C:\\Users\\odett\\Documents\\David\\Palabras.txt";
            String Path = "Palabras.txt";
            rf = File.OpenText(Path);
            // Cuenta cada linea del fichero y lo guarda en la variable count
            int count = File.ReadAllLines(Path).Length;
            // Lee las lineas del fichero y las guarda en el arreglo "words"
            string[] words = new string[count];
            int numItem = 0; // Numeración de los elementos del arreglo
            while (rf.EndOfStream != true)
            {
                string linea = rf.ReadLine();
                words[numItem] = linea;
                numItem++;
            }
            rf.Close();
            // Inincia el ciclo del juego mientras "continua" es True, sino sale del ciclo
            while (continua == true)
            {
                // Genera un número aleatorio
                var seed = Environment.TickCount;
                Random r = new Random(seed);
                // Con el número aleatorio se selecciona una palabra al azar
                string randpalabra = words[r.Next(0, count)];
                // Crea un arreglo de caracteres que representa la palabra a adivinar
                // Si la palabra es "planta" se separa en |p| |l| |a| |n| |t| |a|
                char[] arrayLetras = new char[randpalabra.Length];
                for (int i = 0; i < randpalabra.Length; i++)
                {
                    arrayLetras[i] = randpalabra[i];
                }
                // Arreglo inicial vacio porque aún no se adivina la palabra
                char[] iniTextArr = new char[randpalabra.Length];
                // Arreglo actualizado que representa los espacios adivinados por el jugador
                char[] ActTextArr = new char[randpalabra.Length];
                // Arreglo que representa una versión anterior de los espacios adivinadas por el jugador
                char[] AntTextArr = new char[randpalabra.Length];
                Console.WriteLine("Inicio\n");
                // Crea los espacios que muestran el tamaño de la palabra a adivinar
                for (int i = 0; i < randpalabra.Length; i++)
                {
                    Console.Write("_ ");
                    iniTextArr[i] = ' ';
                }
                char letra; // Variable de la letra ingresada por el jugador
                Console.WriteLine("\nEscribe la letra");
                letra = Console.ReadLine()[0];
                letra = Char.ToLower(letra);
                // Buscar en la palabra la letra ingresada por el jugador
                for (int i = 0; i < randpalabra.Length; i++)
                {
                    // Si se encuentra la letra la escibe, sino pone un guion _
                    if (arrayLetras[i] == letra)
                    {
                        Console.Write(letra + " ");
                        ActTextArr[i] = letra;
                    }
                    else
                    {
                        Console.Write("_ ");
                        ActTextArr[i] = ' ';
                    }
                }
                // Si las siguientes palabras son iguales quiere decir que no acerto a ninguna letra,
                // por lo tanto se pierde una vida
                if (Enumerable.SequenceEqual(ActTextArr, iniTextArr))
                {
                    vidas -= 1;
                    Hangman(vidas); // Dibujo del hombre
                }
                // El arreglo AntTextArr es actualizado con los valores del arreglo ActTextArr
                Array.Copy(ActTextArr, AntTextArr, randpalabra.Length);
                // Mientras haya vidas
                while (vidas > 0)
                {
                    Console.WriteLine("\n");
                    letra = Console.ReadLine()[0];
                    letra = Char.ToLower(letra);
                    for (int i = 0; i < randpalabra.Length; i++)
                    {
                        // Escribe la letra del arreglo AntTextArr si pertenece a la palabra a adivinar
                        if (AntTextArr[i] == arrayLetras[i])
                        {
                            Console.Write(AntTextArr[i] + " ");
                            // Actualiza el arreglo ActTextArr
                            ActTextArr[i] = AntTextArr[i];
                        }
                        // Pero si la letra ingresada también pertenece a la palabra a adivinar
                        else if (arrayLetras[i] == letra)
                        {
                            //Escribe la nueva letra también
                            Console.Write(letra + " ");
                            ActTextArr[i] = letra;
                        }
                        else
                        {
                            Console.Write("_ ");
                            ActTextArr[i] = ' ';
                        }
                    }
                    // Si las siguientes palabras son iguales quiere decir que no acerto a ninguna letra,
                    // por lo tanto se pierde una vida
                    if (Enumerable.SequenceEqual(ActTextArr, AntTextArr))
                    {
                        vidas -= 1;
                        Hangman(vidas); // Dibujo del hombre
                    }
                    // Si la palabra actual es igual a la que se debe adivinar
                    if (Enumerable.SequenceEqual(ActTextArr, arrayLetras))
                    {
                        Console.WriteLine("\n\nYa ganaste!!!\n");
                        Console.WriteLine("¿Deseas seguir jugando?");
                        Console.WriteLine("Presiona S en caso de 'Sí' o N en caso de 'No'");
                        SiNo = Console.ReadLine()[0];
                        while (Char.ToLower(SiNo) != 's' && Char.ToLower(SiNo) != 'n')
                        {
                            Console.WriteLine("¿Deseas seguir jugando?");
                            Console.WriteLine("Presiona S en caso de 'Sí' o N en caso de 'No'");
                            SiNo = Console.ReadLine()[0];
                        }
                        if (Char.ToLower(SiNo) == 's')
                        {
                            Console.Clear();
                            vidas = 6;
                            break;
                        }
                        if (Char.ToLower(SiNo) == 'n')
                        {
                            continua = false;
                            break;
                        }
                    }
                    // Copia ActTextArr que representa la palabra actual a AntTextArr
                    Array.Copy(ActTextArr, AntTextArr, randpalabra.Length);
                    // Letrero cuando pierdes
                    if (vidas <= 0)
                    {
                        Console.WriteLine("\n\nPerdiste!!");
                        Console.WriteLine("La palabra era: " + randpalabra);
                        continua = false;
                        break;
                    }
                }
            }
            Console.WriteLine("Gracias por haber jugado. FIN");
            Console.ReadKey(); // Para que no se cierre la consola
        }
        static void Hangman(int life)
        {
            if (life == 0)
            {
                Console.WriteLine("\n");
                Console.WriteLine("      00     ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("   0      0  ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("      00     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("    111111   ");
                Console.WriteLine("   1  11  1  ");
                Console.WriteLine("  1   11   1 ");
                Console.WriteLine(" 1    11    1");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("    //  \\\\   ");
                Console.WriteLine("   //    \\\\  ");
                Console.WriteLine("  //      \\\\ ");
            }
            if (life == 1)
            {
                Console.WriteLine("\n");
                Console.WriteLine("      00     ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("   0      0  ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("      00     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("    111111   ");
                Console.WriteLine("   1  11  1  ");
                Console.WriteLine("  1   11   1 ");
                Console.WriteLine(" 1    11    1");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("    //       ");
                Console.WriteLine("   //        ");
                Console.WriteLine("  //         ");
            }
            if (life == 2)
            {
                Console.WriteLine("\n");
                Console.WriteLine("      00     ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("   0      0  ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("      00     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("    111111   ");
                Console.WriteLine("   1  11  1  ");
                Console.WriteLine("  1   11   1 ");
                Console.WriteLine(" 1    11    1");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
            }
            if (life == 3)
            {
                Console.WriteLine("\n");
                Console.WriteLine("      00     ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("   0      0  ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("      00     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("    1111     ");
                Console.WriteLine("   1  11     ");
                Console.WriteLine("  1   11     ");
                Console.WriteLine(" 1    11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
            }
            if (life == 4)
            {
                Console.WriteLine("\n");
                Console.WriteLine("      00     ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("   0      0  ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("      00     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
                Console.WriteLine("      11     ");
            }
            if (life == 5)
            {
                Console.WriteLine("\n");
                Console.WriteLine("      00     ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("   0      0  ");
                Console.WriteLine("    0    0   ");
                Console.WriteLine("      00     ");
            }
        }
    }
}
