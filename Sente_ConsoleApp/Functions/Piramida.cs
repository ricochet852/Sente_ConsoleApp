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
        public static List<Uczestnik_Model> Process_Piramida(string xmlfile_path)
        {
            var xmlfile = XElement.Load(xmlfile_path);

            return Get_Lista_Uczestnikow(xmlfile).OrderBy(o => o.Id).ToList();
        }

        public static List<Uczestnik_Model> Get_Lista_Uczestnikow(XElement xmlfile)
        {
            var piramida = new List<Uczestnik_Model>();
            foreach (var uczestnik in xmlfile.Elements("uczestnik"))
            {
                uint poziom_piramidy = 0;
                var uczestnik_add = new Uczestnik_Model
                {
                    Id = uint.Parse(uczestnik.Attribute("id").Value),
                    Poziom_Piramidy = poziom_piramidy
                };
                Get_Uczestnik(uczestnik, uczestnik_add, poziom_piramidy + 1, piramida);
                piramida.Add(uczestnik_add);
            }

            return piramida;
        }

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
