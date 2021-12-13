using riza_kalkulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace riza_kalkulator
{
    public partial class Form1 : Form
    {

        //Database
        public string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = C:\\Baza\\Riza.accdb;Persist Security Info=True";
        public double suma = 0.0;
        public int countPapiri;
        public int countPloce;
        public int countPlastike;
        public Papiri[] nizPapira;
        public Plastike[] nizPlastika;
        public Ploce[] nizPloca;

        //Varijable
        public double izabranaVrednostPapir1 = 0;
        public double kolicinaPapir1 = 0;
        public double sumaPapir1 = 0;

        public double izabranaVrednostPapir2 = 0;
        public double kolicinaPapir2 = 0;
        public double sumaPapir2 = 0;

        public double vrednostPripreme = 0;

        public double izabranaVrednostPloce = 0;
        public double kolicinaPloca = 0;
        public double sumaPloca = 0;

        public double izabranaVrednostPlastike = 0;
        public double kolicinaPlastike = 0;
        public double sumaPlastike = 0;

        public double vrednostPranjaMasina = 0;
        public double vrednostPripremePoPloci = 0;

        public double vrednostDorade = 0;
        public double vrednostFederPoveza = 0;

        public double vrednostSivenja = 0;
        public double kolicinaSivenja = 0;
        public double sumaSivenja = 0;

        public double vrednostKorice = 0;
        public double vrednostNumeracije = 0;
        public double vrednostRicovanja = 0;
        public double vrednostStancovanja = 0;
        public double vrednostStancAlata = 0;

        //REGEX
        public string regexDouble = "^\\d*\\.?\\d+$";
        public string regexInt = "^[\\d ]*$";

        /// /////////////////////////////GET DATA/////////////////////////////
        public void getData()
        {
            OleDbConnection connection = new OleDbConnection(@connectionString);
            connection.Open();

            //SQL komande
            OleDbCommand getCountPapiri = new OleDbCommand("select count(*) from Papiri", connection);
            OleDbCommand getPapire = new OleDbCommand("select ID,naziv_papira,vrednost_formule,cena,ukupna_vrednost from Papiri", connection);

            OleDbCommand getCountPloce = new OleDbCommand("select count(*) from Ploce", connection);
            OleDbCommand getPloce = new OleDbCommand("select ID,naziv_ploce,cena,vrednost_otiska from Ploce", connection);

            OleDbCommand getCountPlastike = new OleDbCommand("select count(*) from Plastike", connection);
            OleDbCommand getPlastike = new OleDbCommand("select ID,naziv_plastike,cena from Plastike", connection);

            OleDbCommand getSivenje = new OleDbCommand("select vrednost_sivenja from Sivenje", connection);

            OleDbDataReader reader = null;

            //////////////////////GET SIVENJE////////////////////////
            reader = getSivenje.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    vrednostSivenja = Double.Parse(reader[0].ToString());

                    Console.WriteLine("DODATA VREDNOST SIVENJA:" + vrednostSivenja);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //////////////////////GET COUNT PAPIRI/////////////////////////
            reader = getCountPapiri.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    countPapiri = Int32.Parse(reader[0].ToString());

                    Console.WriteLine("BROJ PAPIRA:" + countPapiri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //////////////////////GET PAPIRI/////////////////////////
            nizPapira = new Papiri[countPapiri];

            reader = getPapire.ExecuteReader();
            int brojPapira = 0, id_papira;
            string naziv_papira;
            double vrednost_formule, cena, ukupna_vrednost_papira;

            while (reader.Read())
            {
                try
                {
                    id_papira = Int32.Parse(reader[0].ToString());
                    naziv_papira = reader[1].ToString();
                    vrednost_formule = Double.Parse(reader[2].ToString());
                    cena = Double.Parse(reader[3].ToString());
                    ukupna_vrednost_papira = Double.Parse(reader[4].ToString());

                    nizPapira[brojPapira] = new Papiri(id_papira, naziv_papira, vrednost_formule, cena, ukupna_vrednost_papira);

                    Console.WriteLine("DODAT PAPIR:"
                        + nizPapira[brojPapira].ID + " "
                        + nizPapira[brojPapira].nazivPapira + " "
                        + nizPapira[brojPapira].vrednostFormule + " "
                        + nizPapira[brojPapira].cena + "rsd. "
                        + nizPapira[brojPapira].ukupnaVrednost);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                brojPapira++;
            }
            //////////////////////GET COUNT PLOCE/////////////////////////
            reader = getCountPloce.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    countPloce = Int32.Parse(reader[0].ToString());
                    Console.WriteLine("BROJ PLOCA:" + countPloce);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //////////////////////GET PlOCE/////////////////////////
            nizPloca = new Ploce[countPloce];

            reader = getPloce.ExecuteReader();
            int brojPloca = 0, id_ploce;
            string naziv_ploce;
            double cena_ploce;
            double vrednost_otiska;

            while (reader.Read())
            {
                try
                {

                    id_ploce = Int32.Parse(reader[0].ToString());
                    naziv_ploce = reader[1].ToString();
                    cena_ploce = Double.Parse(reader[2].ToString());
                    vrednost_otiska = Double.Parse(reader[3].ToString());

                    nizPloca[brojPloca] = new Ploce(id_ploce, naziv_ploce, cena_ploce, vrednost_otiska);

                    Console.WriteLine("DODATA PLOCA:" + nizPloca[brojPloca].ID + " " + nizPloca[brojPloca].nazivPloce + " " + nizPloca[brojPloca].cena + " " + nizPloca[brojPloca].vrednost_otiska);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                brojPloca++;
            }
            //////////////////////GET COUNT PLASTIKE/////////////////////////
            reader = getCountPlastike.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    countPlastike = Int32.Parse(reader[0].ToString());
                    Console.WriteLine("BROJ PLASTIKA:" + countPlastike);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            //////////////////////GET PLASTIKE/////////////////////////
            nizPlastika = new Plastike[countPlastike];

            reader = getPlastike.ExecuteReader();
            int brojPlastika = 0, id_plastike;
            string naziv_plastike;
            double cena_plastike;

            while (reader.Read())
            {
                try
                {
                    id_plastike = Int32.Parse(reader[0].ToString());
                    naziv_plastike = reader[1].ToString();
                    cena_plastike = Double.Parse(reader[2].ToString());

                    nizPlastika[brojPlastika] = new Plastike(id_plastike, naziv_plastike, cena_plastike);

                    Console.WriteLine("DODATA PLASTIKA:" + nizPlastika[brojPlastika].ID + " " + nizPlastika[brojPlastika].nazivPlastike + " " + nizPlastika[brojPlastika].cena);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                brojPlastika++;
            }

            connection.Close();
        }

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getData();
        }
    }
}
