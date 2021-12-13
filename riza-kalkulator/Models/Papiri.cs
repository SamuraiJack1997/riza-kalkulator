using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace riza_kalkulator.Models
{
    public class Papiri
    {
        public int ID { get; set; }
        public string nazivPapira { get; set; }
        public double vrednostFormule { get; set; }
        public double cena { get; set; }
        public double ukupnaVrednost { get; set; }

        public Papiri(int ID, string nazivPapira, double vrednostFormule, double cena, double ukupnaVrednost)
        {
            this.ID = ID;
            this.nazivPapira = nazivPapira;
            this.vrednostFormule = vrednostFormule;
            this.cena = cena;
            this.ukupnaVrednost = ukupnaVrednost;
        }
    }
}
