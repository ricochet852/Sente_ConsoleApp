using Sente_ConsoleApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sente_ConsoleApp.Functions
{
    public static class Piramida
    {
        /// <summary>
        /// Główna metoda rozpoczynająca odczyt pliku piramida.xml
        /// </summary>
        /// <param name="xmlfile_path"></param>
        /// <returns>
        /// zwraca pełną piramidę uczestników
        /// </returns>
        public static List<Uczestnik_Model> Process_Piramida(string xmlfile_path)
        {
            var xmlfile = XElement.Load(xmlfile_path);

            return Get_Lista_Uczestnikow(xmlfile).OrderBy(o => o.Id).ToList();
        }

        /// <summary>
        /// Rozpoczęcie procesowania pliku xml
        /// Stworzenie listy do zwrócenia a następnie generowanie uczestników
        /// </summary>
        /// <param name="xmlfile"></param>
        /// <returns></returns>
        public static List<Uczestnik_Model> Get_Lista_Uczestnikow(XElement xmlfile)
        {
            var piramida = new List<Uczestnik_Model>();
            foreach (var uczestnik in xmlfile.Elements("uczestnik"))
            {
                uint poziom_piramidy = 0;
                if(!uint.TryParse(uczestnik.Attribute("id").Value, out uint uczestnik_id))
                {
                    Console.WriteLine("Błąd podczas odczytu uczestnika!");
                    Console.WriteLine("Błąd podczas konwersji atrybutu 'id' ");
                    Console.WriteLine($"Uczestnik : id : { uczestnik.Attribute("id").Value } ");
                    Console.WriteLine("======================= !!! =======================");
                    Console.WriteLine();
                    return null;
                }

                var uczestnik_add = new Uczestnik_Model
                {
                    Id = uczestnik_id,
                    Poziom_Piramidy = poziom_piramidy
                };
                Get_Uczestnik(uczestnik, uczestnik_add, poziom_piramidy + 1, piramida);
                piramida.Add(uczestnik_add);
            }

            return piramida;
        }

        /// <summary>
        /// generowanie uczestników których są podwładnymi przynajmniej jednego uczestnika
        /// </summary>
        /// <param name="uczestnik_element"></param>
        /// <param name="uczestnik_przelozony"></param>
        /// <param name="poziom_piramidy"></param>
        /// <param name="piramida"></param>
        static void Get_Uczestnik(XElement uczestnik_element, Uczestnik_Model uczestnik_przelozony, uint poziom_piramidy, List<Uczestnik_Model> piramida)
        {
            uczestnik_przelozony.List_Podwladni = new List<Uczestnik_Model>();

            foreach (var uczestnik in uczestnik_element.Elements("uczestnik"))
            {
                var uczestnik_add = new Uczestnik_Model
                {
                    Id = uint.Parse(uczestnik.Attribute("id").Value),
                    Przelozony = uczestnik_przelozony,
                    Poziom_Piramidy = poziom_piramidy
                };
                Get_Uczestnik(uczestnik, uczestnik_add, poziom_piramidy + 1, piramida);
                uczestnik_przelozony.List_Podwladni.Add(uczestnik_add);
                piramida.Add(uczestnik_add);
            }
        }
    }
}
