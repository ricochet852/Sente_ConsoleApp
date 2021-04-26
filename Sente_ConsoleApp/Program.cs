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

        static void Testowe()
        {
            Console.WriteLine("ruda");
        }

        static void Main()
        {
            Testowe();
            Console.WriteLine("Twórca : Patryk Agata");
            Console.WriteLine();

            while (true)
            {
                //Sprawdzenie czy wymagane pliki znajdują się w folderze aplikacji
                if (!File.Exists(file_piramida_nazwa) || !File.Exists(file_przelewy_nazwa))
                {
                    Console.WriteLine("Brak wymaganych plików w folderze aplikacji!");
                    Console.WriteLine("Wciśnij ENTER aby spróbować ponownie.");
                    Console.WriteLine("Wpisz 'exit' a następnie wciśnij ENTER aby wyjść.");

                    //Sprawdzanie czy ponowić działanie aplikacji czy wyjść
                    var readed_val = Console.ReadLine();
                    if (readed_val == "exit")
                    {
                        break;
                    }
                    continue;
                }

                //Odczyt danych z pliku piramida.xml oraz zaciągniecie ich do Listy "Piramida"
                Piramida = Functions.Piramida.Process_Piramida(file_piramida_nazwa);

                //Odczyt danych z pliku przelewy.xml oraz przeprocesowanie ich na przelewy
                Functions.Przelewy.Process_Przelewy(file_przelewy_nazwa, Piramida);

                //Wygenerowanie raportu
                Functions.Raport.Generate_Raport(Piramida);

                //Komunikat na koniec działania aplikacji
                Console.WriteLine();
                Console.WriteLine("Proces zakończony!");
                Console.WriteLine("Wciśnij ENTER aby ponowić.");
                Console.WriteLine("Wpisz 'exit' a następnie wciśnij ENTER aby wyjść.");

                //Sprawdzanie czy ponowić działanie aplikacji czy wyjść
                var readed_val_exit = Console.ReadLine();
                if (readed_val_exit == "exit")
                {
                    break;
                }
                continue;
            }
        }
    }
}
