using Sente_ConsoleApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sente_ConsoleApp.Functions
{
    public static class Przelewy
    {
        /// <summary>
        /// Główna funkcja do procesowania pliku xml
        /// Odczy danych z pliku oraz rozpoczęcie ich przetwarzania
        /// </summary>
        /// <param name="xmlfile_path"></param>
        /// <param name="piramida"></param>
        public static void Process_Przelewy(string xmlfile_path, List<Uczestnik_Model> piramida)
        {
            var xmlfile_przelewy = XElement.Load(xmlfile_path);

            foreach (var przelew_raw in xmlfile_przelewy.Elements("przelew"))
            {
                var przelew = Get_Przelew(przelew_raw);
                if (przelew == null)
                {
                    continue;
                }
                var pracownik = piramida.Where(x => x.Id == przelew.Od).FirstOrDefault();
                if (pracownik == null)
                {
                    Console.WriteLine("Przelew nie może zostać zrealizowany!");
                    Console.WriteLine($"Przelew : Od : {przelew.Od}, Kwota : {przelew.Kwota}!");
                    Console.WriteLine("Brak pracownika o podanym id");
                    Console.WriteLine("======================= !!! =======================");
                    Console.WriteLine();
                    continue;
                }
                Generate_Prowizje(pracownik, przelew);
            }
        }

        /// <summary>
        /// Odczyt danych do przelewu z pliku xml oraz przełożenie ich na model wykorzystany w późniejszym etapie
        /// </summary>
        /// <param name="przelew_raw"></param>
        /// <returns></returns>
        public static Przelew_Model Get_Przelew(XElement przelew_raw)
        {
            if (!uint.TryParse(przelew_raw.Attribute("od").Value, out uint przelew_od))
            {
                Console.WriteLine("Przelew nie może zostać zrealizowany!");
                Console.WriteLine("Błąd podczas konwersji atrybutu 'od' ");
                Console.WriteLine($"Przelew : Od : { przelew_raw.Attribute("od").Value }, Kwota : { przelew_raw.Attribute("kwota").Value } ");
                Console.WriteLine("======================= !!! =======================");
                Console.WriteLine();
                return null;
            }
            if (!uint.TryParse(przelew_raw.Attribute("kwota").Value, out uint przelew_kwota))
            {
                Console.WriteLine("Przelew nie może zostać zrealizowany!");
                Console.WriteLine("Błąd podczas konwersji atrybutu 'kwota' ");
                Console.WriteLine($"Przelew : Od : { przelew_raw.Attribute("od").Value }, Kwota : { przelew_raw.Attribute("kwota").Value } ");
                Console.WriteLine("======================= !!! =======================");
                Console.WriteLine();
                return null;
            }

            return new Przelew_Model
            {
                Od = przelew_od,
                Kwota = przelew_kwota
            };
        }

        /// <summary>
        /// Generowanie prowizji zaczynając od 1 stopnia piramidy a kończąc na przełożonym uczestnika który wpłacił kwotę
        /// </summary>
        /// <param name="pracownik"></param>
        /// <param name="przelew"></param>
        /// <returns></returns>
        public static double Generate_Prowizje(Uczestnik_Model pracownik, Przelew_Model przelew)
        {
            var przelozony = pracownik.Przelozony;
            double prowizja = przelew.Kwota;
            if (przelozony != null)
            {
                prowizja = Generate_Prowizje(przelozony, przelew);
            }
            if (przelew.Od == pracownik.Id)
            {
                if (pracownik.Poziom_Piramidy == 0)
                {
                    pracownik.Prowizja += prowizja;
                    return 0;
                }
                pracownik.Przelozony.Prowizja += prowizja;
                return 0;
            }
            pracownik.Prowizja += Math.Round(prowizja / 2, 0);
            prowizja -= Math.Round(prowizja / 2, 0);
            return prowizja;
        }
    }
}
