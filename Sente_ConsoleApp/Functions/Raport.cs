using Sente_ConsoleApp.Models;

using System;
using System.Collections.Generic;

namespace Sente_ConsoleApp.Functions
{
    public static class Raport
    {
        /// <summary>
        /// Wypisanie z piramidy wymaganych danych
        /// </summary>
        /// <param name="piramida"></param>
        public static void Generate_Raport(List<Uczestnik_Model> piramida)
        {
            foreach (var uczestnik in piramida)
            {
                Console.WriteLine($"{uczestnik.Id} {uczestnik.Poziom_Piramidy} {uczestnik.Ilosc_Podwladnych_bez_Podwladnych()} {uczestnik.Prowizja}");
            }
        }
    }
}
