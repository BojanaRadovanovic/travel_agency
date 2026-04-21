using TVP1_agencija.Services;
using TVP1_agencija.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TVP1_agencija
{
    public partial class Klijent : Form
    {
        
        public Klijent(int idKorisnika)
        {
            InitializeComponent();
            trenutniKorisnikId = idKorisnika;
        }

        private int trenutniKorisnikId;
        
        private List<Rezervacija> _prikazaneRezervacije = new List<Rezervacija>();


        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            IzmenaRezervacije izR = new IzmenaRezervacije(trenutniKorisnikId);
            izR.Show();
            this.Close();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            DodajRezervaciju dodajR = new DodajRezervaciju(trenutniKorisnikId);
            dodajR.Show();
            this.Close();
        }

       
        private void Klijent_Load(object sender, EventArgs e)
        {
            OsveziListBox();
        }
        
        private void OsveziListBox()
        {
            lstIspis.Items.Clear();

           
            var stavke = Data.Rezervacije
                .Where(r => r.idK == trenutniKorisnikId)
                .Select(r => new { Rez = r, Izlet = Data.Izleti.FirstOrDefault(i => i.id == r.idI) })
                .Where(x => x.Izlet != null && x.Izlet.datum.Date >= DateTime.Today)
                .OrderBy(x => x.Izlet.datum)
                .ToList();

           
            _prikazaneRezervacije = stavke.Select(x => x.Rez).ToList();

            
            foreach (var x in stavke)
            {
                var i = x.Izlet;
                var r = x.Rez;
                lstIspis.Items.Add(
                    $"{i.drzava}-{i.mesto} cena: {i.cena} (popust: {i.popust}%) " +
                    $"za {r.brRezervisanihMesta} osoba, dana {i.datum:dd.MM.yyyy} -> {i.brojDana} dana"
                );
            }

            if (lstIspis.Items.Count == 0)
                lstIspis.Items.Add("Nema budućih rezervacija.");
        }

       
        private void btnObrisi_Click(object sender, EventArgs e)
        {
            if (lstIspis.SelectedIndex < 0)
            {
                MessageBox.Show("Izaberite rezervaciju za brisanje!");
                return;
            }

            
            var rez = _prikazaneRezervacije[lstIspis.SelectedIndex];

            
            var izlet = Data.Izleti.FirstOrDefault(i => i.id == rez.idI);
            if (izlet == null || izlet.datum.Date < DateTime.Today)
            {
                MessageBox.Show("Ne možete obrisati prošlu rezervaciju!");
                return;
            }

            var potvrda = MessageBox.Show("Da li zaista želite da otkažete rezervaciju?",
                                          "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (potvrda != DialogResult.Yes) return;

         
            izlet.ukupnoMesta += rez.brRezervisanihMesta;

            
            Data.Rezervacije.Remove(rez);
            Data.SacuvajRezervacije();
            Data.SacuvajIzlete();

            
            OsveziListBox();
            MessageBox.Show("Rezervacija uspešno obrisana!");
        }


        
        private void btnFiltriraj_Click(object sender, EventArgs e)
        {
            DateTime pocetak = pocetni.Value.Date;
            DateTime kraj = krajnji.Value.Date;
            if (pocetak > kraj)
            {
                MessageBox.Show("Početni datum mora biti pre krajnjeg!");
                return;
            }
            lstIspis.Items.Clear();
            var filtriraneRezervacije = Data.Rezervacije.Where(r => r.idK == trenutniKorisnikId).ToList();
            var rezultat = filtriraneRezervacije.Select(r => new { Rez = r, Izlet = Data.Izleti.FirstOrDefault(i => i.id == r.idI) })
                                                .Where(x => x.Izlet != null && x.Izlet.datum.Date >= pocetak && x.Izlet.datum.Date <= kraj).OrderBy(x => x.Izlet.datum)
                                                .ToList();
            if (rezultat.Count == 0)
            {
                lstIspis.Items.Add("Nema rezervacija u odabranom periodu.");
            }
            else
            {
                foreach (var r in rezultat)
                {
                    Izlet izlet = Data.Izleti.FirstOrDefault(i => i.id == r.Rez.idI);
                    if (izlet != null)
                    {
                        lstIspis.Items.Add($"{izlet.drzava}-{izlet.mesto} cena: {izlet.cena} (popust: {izlet.popust}%) " + $"za {r.Rez.brRezervisanihMesta} osoba, dana {izlet.datum.ToShortDateString()} -> {izlet.brojDana} dana");
                    }
                }
            }
        }
      
        private void btnOdjava_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Close();
        }

        
    }
}
