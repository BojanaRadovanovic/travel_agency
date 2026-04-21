using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Windows.Forms;
using TVP1_agencija.Models;
using System.Xml.Serialization;

namespace TVP1_agencija.Services
{
    public static class Data
    {
       
        public static List<Korisnik> Korisnici = new List<Korisnik>();
        public static List<Izlet> Izleti = new List<Izlet>();
        public static List<Rezervacija> Rezervacije = new List<Rezervacija>();

       
        private static string korisniciXml = "korisnici.xml";
        private static string izletiXml = "izleti.xml";
        private static string rezervacijeXml = "rezervacije.xml";

        
        public static int NoviIdKorisnika()
        {
            if (Korisnici.Count == 0)
                return 1;

            int maxId = Korisnici.Max(k => k.id);
            return maxId + 1;
        }
        public static int NoviIdIzleta()
        {
            if (Izleti.Count == 0)
                return 1;

            int maxId = Izleti.Max(i => i.id);
            return maxId + 1;
        }
        public static int NoviIdRezervacije()
        {
            if (Rezervacije.Count == 0)
                return 1;

            int maxId = Rezervacije.Max(r => r.idI + r.idK);
            return maxId + 1;
        }

     
        private static void Sacuvaj<T>(List<T> lista, string xmlFajl)
        {
            try
            {
                using (FileStream fs = new FileStream(xmlFajl, FileMode.Create))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<T>));
                    xml.Serialize(fs, lista);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri čuvanju: " + ex.Message);
            }
        }

        
        private static List<T> Ucitaj<T>(string xmlFajl)
        {
            try
            {
                using (FileStream fs = new FileStream(xmlFajl, FileMode.Open))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<T>));
                    return (List<T>)xml.Deserialize(fs);
                }
            }
            catch
            {
                return new List<T>();
            }
        }
        //


        public static void SacuvajKorisnike()
        {
            Sacuvaj(Korisnici, korisniciXml);
        }
        public static void SacuvajIzlete()
        {
            Sacuvaj(Izleti, izletiXml);
        }
        public static void SacuvajRezervacije()
        {
            Sacuvaj(Rezervacije, rezervacijeXml);
        }

       
        public static void UcitajSve()
        {
            if (File.Exists(korisniciXml))
                Korisnici = Ucitaj<Korisnik>(korisniciXml);
            else
                DodajPodrazumevanogAdmina();
            if (File.Exists(izletiXml))
            {
                Izleti = Ucitaj<Izlet>(izletiXml);
            }
            else
            {
                Izleti = new List<Izlet>();
            }
            if (File.Exists(rezervacijeXml))
            {
                Rezervacije = Ucitaj<Rezervacija>(rezervacijeXml);
            }
            else
            {
                Rezervacije = new List<Rezervacija>();
            }
        }

        
        private static void DodajPodrazumevanogAdmina()
        {
            Korisnik admin = new Korisnik
            {
                id = 1,
                ime = "Admin",
                prezime = "Admin",
                korisnickoIme = "admin",
                lozinka = "admin",
                vrstaKorisnika = vrsta.Admin
            };
            Korisnici.Add(admin);
            SacuvajKorisnike();
        }



    }
}
