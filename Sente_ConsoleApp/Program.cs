using System;
using System.Collections.Generic;
using System.IO;

namespace Sente_ConsoleApp
{
    class Program
    {
        static string file_piramida_nazwa = "piramida.xml";
        static string file_przelewy_nazwa = "przelewy.xml";
        static List<Models.Uczestnik_Model> Piramida = new List<Models.Uczestnik_Model>();
        static void Main(string[] args)
        {
            Console.WriteLine("Twórca : Patryk Agata!");
            Console.WriteLine();

            if (!File.Exists(file_piramida_nazwa) || !File.Exists(file_przelewy_nazwa))
            {
                Console.WriteLine("Brak wymaganych plików w folderze aplikacji!");
            }

            Piramida = Functions.Piramida.Process_Piramida(file_piramida_nazwa);
            Functions.Przelewy.Process_Przelewy(file_przelewy_nazwa, Piramida);

            Console.ReadKey();
        }
    }
}
