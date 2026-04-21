using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVP1_agencija.Models;
using TVP1_agencija.Services;

namespace TVP1_agencija
{
    public partial class IzmenaRezervacije : Form
    {
       
        private int trenutniKorisnik;

        public IzmenaRezervacije(int idKorisnika)
        {
            InitializeComponent();
            trenutniKorisnik = idKorisnika;
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            int brojMesta;
            
            if (!int.TryParse(brMesta.Text.Trim(), out brojMesta) || brojMesta <= 0)
            {
                MessageBox.Show("Molimo unesite validan broj rezervisanih mesta!");
                return;
            }
           
            Izlet izlet = Data.Izleti.FirstOrDefault(i => i.mesto == mesto.Text.Trim() && i.datum.Date == datum.Value.Date);
            if (izlet == null)
            {
                MessageBox.Show("Ne možete menjati mesto ili datum izleta!");
                return;
            }
            
            Rezervacija rezervacija = Data.Rezervacije.FirstOrDefault(r => r.idK == trenutniKorisnik && r.idI == izlet.id);
            if (rezervacija == null)
            {
                MessageBox.Show("Rezervacija nije pronađena!");
                return;
            }
            
            if (mesto.Text.Trim() != izlet.mesto || datum.Value.Date != izlet.datum.Date)
            {
                MessageBox.Show("Možete menjati samo broj rezervisanih mesta!");
                return;
            }
            
            izlet.ukupnoMesta += rezervacija.brRezervisanihMesta;
            if (brojMesta > izlet.ukupnoMesta)
            {
                MessageBox.Show("Nema dovoljno slobodnih mesta za izabrani broj!");
                izlet.ukupnoMesta -= rezervacija.brRezervisanihMesta;
                return;
            }
            izlet.ukupnoMesta -= brojMesta;
            
            rezervacija.brRezervisanihMesta = brojMesta;
            rezervacija.datumRezervacije = DateTime.Now;
            rezervacija.ukupnaCena = izlet.cena * brojMesta * (1 - izlet.popust / 100.0);
            Data.SacuvajRezervacije();
            Data.SacuvajIzlete();
            MessageBox.Show("Rezervacija uspešno izmenjena!");
            this.Close();
            Klijent k = new Klijent(trenutniKorisnik);
            k.Show();
        }

       
    }
}
