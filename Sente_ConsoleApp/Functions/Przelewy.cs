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
        public static void Process_Przelewy(string xmlfile_path, List<Uczestnik_Model> piramida)
        {
            var xmlfile_przelewy = XElement.Load(xmlfile_path);

            foreach (var przelew_raw in xmlfile_przelewy.Elements("przelew"))
            {
                var przelew = Get_Przelew(przelew_raw);
                var pracownik = piramida.Where(x => x.Id == przelew.Od).FirstOrDefault();
                Generate_Prowizje(pracownik, przelew);
            }
        }

        public static Przelew_Model Get_Przelew(XElement przelew)
        {
            return new Przelew_Model
            {
                Od = uint.Parse(przelew.Attribute("od").Value),
                Kwota = double.Parse(przelew.Attribute("kwota").Value)
            };
        }

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
