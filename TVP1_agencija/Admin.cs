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
    public partial class Admin : Form
    {
        
        public Admin()
        {
            
            InitializeComponent();
            cmbVrsta.DataSource = Enum.GetValues(typeof(vrsta));
            OsveziPocetnu();
        }
        
        private void btnDodajIzlet_Click(object sender, EventArgs e)
        {
            if (Data.Izleti.Any(x => x.id == int.Parse(idI.Text)))
            {
                MessageBox.Show("Pogrešan ID, pokušajte ponovo!");
                return;
            }
            Izlet i = new Izlet
            {
                id = Data.NoviIdIzleta(),
                mesto = mesto.Text,
                drzava = drzava.Text,
                cena = double.Parse(cena.Text),
                popust = int.Parse(popust.Text),
                brojDana = int.Parse(brojDana.Text),
                ukupnoMesta = int.Parse(ukMesta.Text),
                datum = datumIzleta.Value
            };
            Data.Izleti.Add(i);
            Data.SacuvajIzlete();
            PrikaziIzlete();
            OsveziPocetnu();
        }
       
        private void btnIzmeniIzlet_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idI.Text);
            Izlet i = Data.Izleti.FirstOrDefault(x => x.id == id);
            if (i == null)
            {
                MessageBox.Show("Id izleta ne postoji u sistemu, pokušajte ponovo!");
            }
            else
            {
                i.mesto = mesto.Text;
                i.drzava = drzava.Text;
                i.cena = double.Parse(cena.Text);
                i.popust = int.Parse(popust.Text);
                i.brojDana = int.Parse(brojDana.Text);
                i.ukupnoMesta = int.Parse(ukMesta.Text);
                i.datum = datumIzleta.Value;
                PrikaziIzlete();
                OsveziPocetnu();
                Data.SacuvajIzlete();
            }
        }
       
        private void btnObrisiIzlet_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idI.Text);
            Izlet izlet = Data.Izleti.FirstOrDefault(x => x.id == id);
            if (izlet == null)
            {
                MessageBox.Show("Izlet sa tim ID-jem ne postoji!");
                return;
            }
            bool postojiRezervacija = Data.Rezervacije.Any(r => r.idI == id);
            if (postojiRezervacija)
            {
                MessageBox.Show("Ne možete obrisati izlet koji ima rezervacije!");
                return;
            }
            Data.Izleti.Remove(izlet);
            Data.SacuvajIzlete();
            PrikaziIzlete();
            OsveziPocetnu();
        }
      
        private void btnPrikaziI_Click(object sender, EventArgs e)
        {
            PrikaziIzlete();
        }
        private void PrikaziIzlete()
        {
            txtIspisIzleti.Clear();
            foreach (var i in Data.Izleti)
            {
                txtIspisIzleti.AppendText($"{i.id}: {i.mesto}, {i.drzava}\r\n" +
     $" Cena: {i.cena:0.00} RSD, Popust: {i.popust}%, Broj dana: {i.brojDana}, " +
     $"Slobodna mesta: {i.ukupnoMesta}, Datum: {i.datum:dd.MM.yyyy}\r\n" );

            }
        }
       
        private void btnDodajRez_Click(object sender, EventArgs e)
        {
            int idKorisnika, idI, brojRezMesta;
            if (!int.TryParse(idK.Text, out idKorisnika) ||
                !int.TryParse(idIzleta.Text, out idI) ||
                !int.TryParse(rezMesta.Text, out brojRezMesta))
            {
                MessageBox.Show("Sva polja moraju biti brojevi!");
                return;
            }
            Izlet izlet = Data.Izleti.FirstOrDefault(i => i.id == idI);
            if (izlet == null)
            {
                MessageBox.Show("Izlet ne postoji!");
                return;
            }
            if (brojRezMesta > izlet.ukupnoMesta)
            {
                MessageBox.Show("Nema dovoljno dostupnih mesta na izletu!");
                return;
            }
            double cenaSaPopustom = izlet.cena * (1 - izlet.popust / 100.0);
            double ukupnaCena = cenaSaPopustom * brojRezMesta;
            Rezervacija r = new Rezervacija
            {
                idK = idKorisnika,
                idI = idI,
                ukupnaCena = ukupnaCena,
                datumRezervacije = DateTime.Now,
                brRezervisanihMesta = brojRezMesta
            };
            Data.Rezervacije.Add(r);
            Data.SacuvajRezervacije();
            izlet.ukupnoMesta -= brojRezMesta;
            Data.SacuvajIzlete();
            PrikaziRezervacije();
            OsveziPocetnu();
        }
      
        private void btnIzmeniRez_Click(object sender, EventArgs e)
        {
            int idKorisnika = int.Parse(idK.Text);
            int idI = int.Parse(idIzleta.Text);
            int noviBrojMesta = int.Parse(rezMesta.Text);
            Rezervacija r = Data.Rezervacije.FirstOrDefault(x => x.idK == idKorisnika && x.idI == idI);
            Izlet izlet = Data.Izleti.FirstOrDefault(i => i.id == idI);
            if (r != null && izlet != null)
            {
                izlet.ukupnoMesta += r.brRezervisanihMesta;
                if (noviBrojMesta > izlet.ukupnoMesta)
                {
                    MessageBox.Show("Nema dovoljno slobodnih mesta!");
                    return;
                }
                r.brRezervisanihMesta = noviBrojMesta;
                izlet.ukupnoMesta -= noviBrojMesta;
                r.datumRezervacije = DateTime.Now;
                r.ukupnaCena = izlet.cena * noviBrojMesta * (1 - izlet.popust / 100.0);
                PrikaziRezervacije();
                OsveziPocetnu();
                Data.SacuvajRezervacije();
                Data.SacuvajIzlete();
            }
        }
        
        private void btnObrisiRez_Click(object sender, EventArgs e)
        {
            int idKorisnika = int.Parse(idK.Text);
            int idI = int.Parse(idIzleta.Text);
            Rezervacija r = Data.Rezervacije.FirstOrDefault(x => x.idK == idKorisnika && x.idI == idI);
            if (r != null)
            {
                Izlet izlet = Data.Izleti.FirstOrDefault(i => i.id == idI);
                if (izlet != null)
                {
                    izlet.ukupnoMesta += r.brRezervisanihMesta;
                    Data.SacuvajIzlete();
                }
                Data.Rezervacije.Remove(r);
                Data.SacuvajRezervacije();
                PrikaziRezervacije();
                OsveziPocetnu();
            }
        }
      
        private void btnPrikaziR_Click(object sender, EventArgs e)
        {
            PrikaziRezervacije();
        }
        private void PrikaziRezervacije()
        {
            txtIspisRez.Clear();
            foreach (var r in Data.Rezervacije)
            {
                txtIspisRez.AppendText( $"Korisnik {r.idK}:\r\n " +
    $"Izlet: {r.idI}, Cena: {r.ukupnaCena:0.00} RSD, Datum: {r.datumRezervacije:dd.MM.yyyy}\r\n");

            }
        }
       
        private void btnDodajK_Click(object sender, EventArgs e)
        {
            var k = new Korisnik
            {
                id = Data.NoviIdKorisnika(),
                ime = Ime.Text,
                prezime = prezime.Text,
                korisnickoIme = kIme.Text,
                lozinka = lozinka.Text,
                vrstaKorisnika = (vrsta)cmbVrsta.SelectedItem
            };
            Data.Korisnici.Add(k);
            Data.SacuvajKorisnike();
            PrikaziKorisnike();
            OsveziPocetnu();
        }
        
        private void btnIzmeniK_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idKorisnika.Text);
            Korisnik k = Data.Korisnici.FirstOrDefault(x => x.id == id);
            if (k != null)
            {
                k.ime = Ime.Text;
                k.prezime = prezime.Text;
                k.korisnickoIme = kIme.Text;
                k.lozinka = lozinka.Text;
                k.vrstaKorisnika = (vrsta)cmbVrsta.SelectedItem;
                Data.SacuvajKorisnike();
                PrikaziKorisnike();
                OsveziPocetnu();
            }
        }
        
        private void btnObrisiK_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idKorisnika.Text);
            Korisnik k = Data.Korisnici.FirstOrDefault(x => x.id == id);
            if (k != null)
            {
                bool imaRezervacija = Data.Rezervacije.Any(r => r.idK == id);
                if (imaRezervacija)
                {
                    MessageBox.Show("Ne možete obrisati korisnika koji ima rezervacije!");
                    return;
                }
                Data.Korisnici.Remove(k);
                Data.SacuvajKorisnike();
                PrikaziKorisnike();
                OsveziPocetnu();
            }
        }
       
        private void btnPrikaziK_Click(object sender, EventArgs e)
        {
            PrikaziKorisnike();
        }
        private void PrikaziKorisnike()
        {
            txtIspisK.Clear();
            foreach (var k in Data.Korisnici)
            {
                txtIspisK.AppendText($"{k.id}, Ime: {k.ime}, Prezime: {k.prezime},Korisnicko ime: {k.korisnickoIme}, {k.vrstaKorisnika}\r\n");
            }
        }
       
        private void OsveziPocetnu()
        {

            txtIzletiPocetna.Text = string.Join("\r\n", Data.Izleti.Select(i => $"{i.drzava} - {i.mesto}"));
            txtKorisnici.Text = string.Join("\r\n", Data.Korisnici.Select(k => $"{k.ime} {k.prezime} - {k.vrstaKorisnika}"));
            txtRezervacijePocetna.Text = string.Join("\r\n", Data.Rezervacije.Select(r => $"ID_korisnik: {r.idK} - ID_izlet: {r.idI}"));
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }



    }
}
