using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP1_agencija.Models
{
    [Serializable]

    public class Izlet
    {
        public int id;
        public string mesto;
        public string drzava;
        public double cena;
        public int popust;
        public int brojDana;
        public int ukupnoMesta;
        public DateTime datum;

    }
}
