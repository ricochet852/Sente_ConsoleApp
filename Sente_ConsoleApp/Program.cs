using Sente_ConsoleApp.Models;

using System;
using System.Collections.Generic;
using System.IO;

namespace Sente_ConsoleApp
{
    class Program
    {
        #region props

        /// <summary>
        /// Nazwa pliku XML danych wejściowych dla piramidy
        /// </summary>
        static string file_piramida_nazwa = "piramida.xml";
        /// <summary>
        /// Nazwa pliku XML danych wejściowych dla przelewów
        /// </summary>
        static string file_przelewy_nazwa = "przelewy.xml";
        /// <summary>
        /// Pełna Lista Uczestników piramidy
        /// </summary>
        static List<Uczestnik_Model> Piramida = new List<Uczestnik_Model>();

        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("Twórca : Patryk Agata");
            Console.WriteLine();

            //Sprawdzenie czy wymagane pliki znajdują się w folderze aplikacji
            if (!File.Exists(file_piramida_nazwa) || !File.Exists(file_przelewy_nazwa))
            {
                Console.WriteLine("Brak wymaganych plików w folderze aplikacji!");
            }

            //Odczyt danych z pliku piramida.xml oraz zaciągniecie ich do Listy "Piramida"
            Piramida = Functions.Piramida.Process_Piramida(file_piramida_nazwa);

            //Odczyt danych z pliku przelewy.xml oraz przeprocesowanie ich na przelewy
            Functions.Przelewy.Process_Przelewy(file_przelewy_nazwa, Piramida);

            //Wygenerowanie raportu
            Functions.Raport.Generate_Raport(Piramida);

            Console.ReadKey();
        }
    }
}
