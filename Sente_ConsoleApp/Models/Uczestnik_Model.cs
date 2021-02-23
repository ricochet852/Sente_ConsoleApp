using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sente_ConsoleApp.Models
{
    class Uczestnik_Model
    {
        public int Id { get; set; }
        public int Poziom_Piramidy { get; set; }
        public double Prowizja { get; set; }
        public List<Uczestnik_Model> List_Podwladni { get; set; }

        #region functions

        public int Ilosc_Podwladnych_bez_Podwladnych()
        {
            var ilosc = List_Podwladni.Where(x => x.List_Podwladni.Count == 0).Count(0);

            foreach (var podwladny in List_Podwladni)
            {
                ilosc += podwladny.Ilosc_Podwladnych_bez_Podwladnych();
            }

            return ilosc;
        }

        #endregion
    }
}
