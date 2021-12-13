using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace riza_kalkulator.Models
{
    public class Plastike
    {
        public int ID { get; set; }
        public string nazivPlastike { get; set; }
        public double cena { get; set; }

        public Plastike(int ID, string nazivPlastike, double cena)
        {
            this.ID = ID;
            this.nazivPlastike = nazivPlastike;
            this.cena = cena;
        }
    }
}
