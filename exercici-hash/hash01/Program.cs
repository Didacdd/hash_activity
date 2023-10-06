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
                Console.Write("Entra la ruta del fitxer: ");
                String ruta = Console.ReadLine();

                if (!File.Exists(ruta))
                {
                    Console.WriteLine("El archivo no existe.");
                    return;
                }

                string contenido = File.ReadAllText(ruta);

                byte[] bytesIn = Encoding.UTF8.GetBytes(ruta);

                using (SHA512Managed SHA512 = new SHA512Managed())
                {
                    byte[] hashResult = SHA512.ComputeHash(bytesIn);
                    string textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);

                    Console.WriteLine("Hash del archivo '{0}':", ruta);
                    Console.WriteLine(textOut);

                    string hashArchivoRuta = Path.ChangeExtension(ruta, "SHA");
                    File.WriteAllText(hashArchivoRuta, textOut);
                    Console.WriteLine($"Hash guardado en {hashArchivoRuta}");
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