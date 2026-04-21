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
    public partial class DodajRezervaciju : Form
    {
        
        int trenutniKorisnikId;

        public DodajRezervaciju(int korisnikId)
        {
            InitializeComponent();
            trenutniKorisnikId = korisnikId;
            var mesta = Data.Izleti.Select(i => i.mesto).ToList();
            cmdMesto.DataSource = mesta;
        }

        private void btnRezervisi_Click(object sender, EventArgs e)
        {
            
            if (!int.TryParse(brMesta.Text, out int brojMesta) || brojMesta <= 0)
            {
                MessageBox.Show("Unesite ispravan broj mesta!");
                return;
            }
            
            string unesenoMesto = txtMesto.Text.Trim();
            DateTime uneseniDatum = datum.Value.Date;
            Izlet izabraniIzlet = Data.Izleti.FirstOrDefault(i => i.mesto.Equals(unesenoMesto) && i.datum.Date == uneseniDatum);
            if (izabraniIzlet == null)
            {
                MessageBox.Show("Nema izleta za uneto mesto i datum!");
                return;
            }
            
            if (brojMesta > izabraniIzlet.ukupnoMesta)
            {
                MessageBox.Show("Nema dovoljno slobodnih mesta!");
                return;
            }
            
            bool vecImaRezervaciju = Data.Rezervacije.Any(r => r.idK == trenutniKorisnikId && r.datumRezervacije.Date == uneseniDatum);
            if (vecImaRezervaciju)
            {
                MessageBox.Show("Već imate rezervaciju za taj datum!");
                return;
            }
            
            var mojeRezervacije = Data.Rezervacije.Where(r => r.idK == trenutniKorisnikId).ToList();
            DateTime pocetakNovog = izabraniIzlet.datum;
            DateTime krajNovog = izabraniIzlet.datum.AddDays(izabraniIzlet.brojDana);
            foreach (var rez in mojeRezervacije)
            {
                Izlet stariIzlet = Data.Izleti.FirstOrDefault(i => i.id == rez.idI);
                if (stariIzlet != null)
                {
                    DateTime pocetakStarog = stariIzlet.datum;
                    DateTime krajStarog = stariIzlet.datum.AddDays(stariIzlet.brojDana);
                    if (pocetakNovog < krajStarog && krajNovog > pocetakStarog)
                    {
                        MessageBox.Show("Već imate rezervisan izlet koji se vremenski preklapa sa ovim!");
                        return;
                    }
                }
            }
          
            double cenaSaPopustom = izabraniIzlet.cena - (izabraniIzlet.cena * izabraniIzlet.popust / 100.0);
            double ukupnaCena = brojMesta * cenaSaPopustom;
            
            Rezervacija nova = new Rezervacija
            {
                idK = trenutniKorisnikId,
                idI = izabraniIzlet.id,
                brRezervisanihMesta = brojMesta,
                datumRezervacije = DateTime.Now,
                ukupnaCena = ukupnaCena
            };
            Data.Rezervacije.Add(nova);
            Data.SacuvajRezervacije();
            izabraniIzlet.ukupnoMesta -= brojMesta;
            Data.SacuvajIzlete();
            MessageBox.Show("Uspešno ste napravili rezervaciju!");
            
            this.Close();
            Klijent k = new Klijent(trenutniKorisnikId);
            k.Show();
        }
       
        private void cmdMesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmdMesto.SelectedItem == null)
                return;
            string izabranoMesto = cmdMesto.SelectedItem.ToString();
            var izletiZaMesto = Data.Izleti.Where(i => i.mesto.Equals(izabranoMesto)).ToList();
            mesto.Clear();
            foreach (var izlet in izletiZaMesto)
            {
                mesto.AppendText(
                    $"Mesto: {izlet.mesto}\r\n" +
                    $"Država: {izlet.drzava}\r\n" +
                    $"Cena: {izlet.cena} RSD\r\n" +
                    $"Popust: {izlet.popust}%\r\n" +
                    $"Broj dana: {izlet.brojDana}\r\n" +
                    $"Ukupno mesta: {izlet.ukupnoMesta}\r\n" +
                    $"Datum: {izlet.datum:d}\r\n" +
                    "-----------------------------\r\n"
                );
            }
        }


    }
}
