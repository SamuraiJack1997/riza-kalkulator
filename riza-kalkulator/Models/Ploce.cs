using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace riza_kalkulator.Models
{
    public class Ploce
    {
        public int ID { get; set; }
        public string nazivPloce { get; set; }
        public double cena { get; set; }
        public double vrednost_otiska { get; set; }

        public Ploce(int ID, string nazivPloce, double cena, double vrednost_otiska)
        {
            this.ID = ID;
            this.nazivPloce = nazivPloce;
            this.cena = cena;
            this.vrednost_otiska = vrednost_otiska;
        }
    }
}
