using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVP1_agencija.Models
{
    public enum vrsta { Admin, Klijent };

    [Serializable]
    public class Korisnik
    {
        public int id;
        public string ime;
        public string prezime;
        public string korisnickoIme;
        public string lozinka;
        public vrsta vrstaKorisnika;
    }
}
