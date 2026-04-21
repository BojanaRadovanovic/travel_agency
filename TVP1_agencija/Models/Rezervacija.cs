using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP1_agencija.Models
{
    [Serializable]

    public class Rezervacija
    {
        public int idK;
        public int idI;
        public double ukupnaCena;
        public int brRezervisanihMesta;
        public DateTime datumRezervacije;

    }
}
