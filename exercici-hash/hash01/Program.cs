    using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace hash01
{

    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                Console.WriteLine("Menú:");
                Console.WriteLine("1.- Calcular el hash d'un fitxer.");
                Console.WriteLine("2.- Verificar la integritat d'un fitxer.");
                Console.Write("Opció: ");
                int opcio = int.Parse(Console.ReadLine());

                if (opcio == 1)
                {
                    Console.Write("Entra la ruta del archivo: ");
                    string ruta = Console.ReadLine();

                    if (!File.Exists(ruta))
                    {
                        Console.WriteLine("El archivo no existe.");
                        return;
                    }

                    // Leer el contenido del archivo
                    string contenido = File.ReadAllText(ruta);

                    // Calcular el hash SHA-512
                    byte[] bytesIn = Encoding.UTF8.GetBytes(contenido);

                    using (SHA512Managed SHA512 = new SHA512Managed())
                    {
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);
                        string textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                        Console.WriteLine("Hash del archivo '{0}':", ruta);
                        Console.WriteLine(textOut);

                        // Guardar el hash en un archivo con la extensión .SHA
                        string hashFilePath = Path.ChangeExtension(ruta, "SHA");
                        File.WriteAllText(hashFilePath, textOut);
                        Console.WriteLine($"Hash guardat en {hashFilePath}");
                    }
                }
                else if (opcio == 2)
                {
                    Console.Write("Entra la ruta del fitxer: ");
                    string ruta = Console.ReadLine();

                    Console.Write("Entra la ruta del fitxer hash .SHA: ");
                    string hashFilePath = Console.ReadLine();

                    if (!File.Exists(ruta) || !File.Exists(hashFilePath))
                    {
                        Console.WriteLine("Un dels dos fitxers o els dos no existeixen.");
                        return;
                    }

                    // Leer el contenido del archivo
                    string contenido = File.ReadAllText(ruta);

                    // Calcular el hash SHA-512
                    byte[] bytesIn = Encoding.UTF8.GetBytes(contenido);

                    using (SHA512Managed SHA512 = new SHA512Managed())
                    {
                        byte[] hashResult = SHA512.ComputeHash(bytesIn);
                        string textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                        // Leer el hash almacenado en el archivo
                        string hashGuardado = File.ReadAllText(hashFilePath);

                        // Comparar el hash calculado con el hash almacenado
                        if (string.Equals(textOut, hashGuardado, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("La integridad del archivo es válida.");
                        }
                        else
                        {
                            Console.WriteLine("La integridad del archivo no es válida.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Opción no válida.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}