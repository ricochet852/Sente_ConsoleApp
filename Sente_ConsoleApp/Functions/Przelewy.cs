using Sente_ConsoleApp.Models;

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sente_ConsoleApp.Functions
{
    public static class Przelewy
    {
        #region Przelewy

        public static void Process_Przelewy(string xmlfile_path, List<Uczestnik_Model> piramida)
        {
            var xmlfile_przelewy = XElement.Load(xmlfile_path);

            foreach (var przelew_raw in xmlfile_przelewy.Elements("przelew"))
            {
                var przelew = Get_Przelew(przelew_raw);
                var path = Find_User(przelew.Od, piramida);
                Generate_Prowizje(path, przelew);
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

        public static void Generate_Prowizje(List<Uczestnik_Model> path, Przelew_Model przelew)
        {
            for (int i = 0; i < path.Count; i++)
            {
                var uczestnik = path[i];
                if (i == path.Count - 1)
                {
                    uczestnik.Prowizja += przelew.Kwota;
                    return;
                }
                double prowizja = Math.Round(przelew.Kwota / 2);
                uczestnik.Prowizja += prowizja;
                przelew.Kwota -= prowizja;
            }
        }

        #endregion

        private static List<Uczestnik_Model> Find_User(uint id, List<Uczestnik_Model> piramida)
        {
            var list = new List<Uczestnik_Model>();

            var i = Find_User_ext(piramida, id, list);
            if (i == null)
            {
                return null;
            }

            list.Add(i);
            list.Reverse();

            return list;
        }

        private static Uczestnik_Model Find_User_ext(List<Uczestnik_Model> uczestnicy, uint id, List<Uczestnik_Model> path)
        {
            foreach (var uczestnik in uczestnicy)
            {
                if (uczestnik.Id == id)
                {
                    return null;
                }

                if (uczestnik.List_Podwladni.Count != 0)
                {
                    var i = Find_User_ext(uczestnik.List_Podwladni, id, path);
                    if (i != null)
                    {
                        path.Add(i);
                    }
                    return uczestnik;
                }
            }

            return null;
        }
    }
}
