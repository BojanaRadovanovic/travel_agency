using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TVP1_agencija.Models;
using TVP1_agencija.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace TVP1_agencija
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            Services.Data.UcitajSve();
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
           
            string kIme = txtIme.Text.Trim();
            string lozinka = txtLozinka.Text.Trim();
          
            var korisnik = Services.Data.Korisnici.FirstOrDefault(k => k.korisnickoIme == kIme && k.lozinka == lozinka);
            if (korisnik == null)
            {
                MessageBox.Show("Pogrešno korisničko ime ili lozinka!");
                return;
            }

            
            if (korisnik.vrstaKorisnika == vrsta.Admin)
            {
                Admin adminForm = new Admin();
                adminForm.Show();
                this.Hide();
                MessageBox.Show("Ulogovani ste kao ADMIN.");
            }
            else
            {

                Klijent klijentForm = new Klijent(korisnik.id);
                klijentForm.Show();
                this.Hide();
                MessageBox.Show("Ulogovani ste kao KLIJENT.");
            }
            

        }

        private void btnRegistracija_Click(object sender, EventArgs e)
        {
            txtIme.Visible = false;
            txtLozinka.Visible = false;
            btnPrijava.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            btnRegistracija.Visible = false;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            btnReg.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;

        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            
            string ime = textBox1.Text.Trim();
            string prezime = textBox2.Text.Trim();
            string korisnickoIme = textBox3.Text.Trim();
            string lozinka = textBox4.Text;
            string ponovljenaLozinka = textBox5.Text;
            
            if (string.IsNullOrEmpty(ime) || string.IsNullOrEmpty(prezime) ||
                string.IsNullOrEmpty(korisnickoIme) || string.IsNullOrEmpty(lozinka) ||
                string.IsNullOrEmpty(ponovljenaLozinka))
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            }
            if (lozinka != ponovljenaLozinka)
            {
                MessageBox.Show("Lozinka i ponovljena lozinka se ne poklapaju!");
                return;
            }
            if (Services.Data.Korisnici.Any(k => k.korisnickoIme == korisnickoIme))
            {
                MessageBox.Show("Korisničko ime već postoji, izaberite drugo!");
                return;
            }
           
            int noviId = 1;
            if (Services.Data.Korisnici.Count > 0)
                noviId = Services.Data.Korisnici.Max(k => k.id) + 1;
            
            Korisnik noviKorisnik = new Korisnik
            {
                id = noviId,
                ime = ime,
                prezime = prezime,
                korisnickoIme = korisnickoIme,
                lozinka = lozinka,
                vrstaKorisnika = vrsta.Klijent
            };
            
            Services.Data.Korisnici.Add(noviKorisnik);
            Services.Data.SacuvajKorisnike();
            MessageBox.Show("Registracija uspešna!");

            
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            btnReg.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            txtIme.Visible = true;
            txtLozinka.Visible = true;
            btnPrijava.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            btnRegistracija.Visible = true;
        }
    }
}

