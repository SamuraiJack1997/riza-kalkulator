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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Font = iTextSharp.text.Font;

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
        public Papiri izabranPapir1;
        public double kolicinaPapir1 = 1;
        public double sumaPapir1 = 0;

        public double izabranaVrednostPapir2 = 0;
        public Papiri izabranPapir2;
        public double kolicinaPapir2 = 1;
        public double sumaPapir2 = 0;

        public double izabranaVrednostPapir3 = 0;
        public Papiri izabranPapir3;
        public double kolicinaPapir3 = 1;
        public double sumaPapir3 = 0;

        public double izabranaVrednostPapir4 = 0;
        public Papiri izabranPapir4;
        public double kolicinaPapir4 = 1;
        public double sumaPapir4 = 0;

        public double vrednostPripreme = 0;

        public double izabranaVrednostPloce = 0;
        public Ploce  izabranaPloca;
        public double kolicinaPloca = 1;
        public double sumaPloca = 0;

        public double izabranaVrednostPlastike = 0;
        public Plastike izabranaPlastika;
        public double kolicinaPlastike = 1;
        public double sumaPlastike = 0;

        public double vrednostPripremePoPloci = 0;
        public double vrednostPranjaMasina = 0;

        public double sumaOtisaka = 0;
        public double kolicinaOtisaka = 1;

        public double vrednostSivenja = 0;
        public double kolicinaSivenja = 1;
        public double sumaSivenja = 0;

        public double vrednostToniranja = 0;
        public double vrednostFederPoveza = 0;
        public double vrednostStancovanja = 0;
        public double vrednostRicovanja = 0;
        public double vrednostNumeracije = 0;
        public double vrednostSecenjaPapira = 0;
        public double vrednostLepljenjaForzeca = 0;
        public double vrednostCantragovanja = 0;
        public double vrednostLKPT = 0;
        public double vrednostKoricenja = 0;
        public double vrednostIzradeKorica = 0;
        public double vrednostSecenjaZica = 0;
        public double vrednostPakovanja = 0;
        public double vrednostJahaca = 0;
        public double vrednostHeftanja = 0;
        public double vrednostSavijanjaPapira = 0;
        public double vrednostKasiranja = 0;
        public double vrednostDigitale = 0;
        public double vrednostPerforacije = 0;
        public double vrednostBusenjaRupa = 0;
        public double vrednostBigovanja = 0;
        public double vrednostZlatotiska = 0;
        public double vrednostKlisea = 0;
        public double vrednostIzradeKesa = 0;
        public double vrednostIzradeKutija = 0;
        public double vrednostLajmovanja = 0;
        public double vrednostBrojanjaListova = 0;

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

            for (int i = 0; i < countPapiri; i++)
            {
                comboBox1.Items.Add(nizPapira[i].nazivPapira);
                comboBox2.Items.Add(nizPapira[i].nazivPapira);
                comboBox3.Items.Add(nizPapira[i].nazivPapira);
                comboBox4.Items.Add(nizPapira[i].nazivPapira);
            }
            for (int i = 0; i < countPloce; i++)
            {
                comboBox5.Items.Add(nizPloca[i].nazivPloce);
            }
            for (int i = 0; i < countPlastike; i++)
            {
                comboBox6.Items.Add(nizPlastika[i].nazivPlastike);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult result = MessageBox.Show("Da li ste sigurni da želite da izadjete?", "Riža kalkulator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

        }

        public void izracunaj()
        {
            suma = 0.0;
            if (textBox1.Text == "")
            {
                sumaPapir1 = 0.0;
            }
            if (textBox2.Text == "")
            {
                sumaPapir2 = 0.0;
            }
            if (textBox3.Text == "")
            {
                sumaPapir3 = 0.0;
            }
            if (textBox4.Text == "")
            {
                sumaPapir4 = 0.0;
            }
            if (textBox26.Text == "")
            {
                sumaSivenja = 0.0;
            }
            if (textBox12.Text == "")
            {
                sumaPlastike = 0.0;
            }
            if (textBox7.Text == "")
            {
                sumaPloca = 0.0;
            }
            if (textBox10.Text == "")
            {
                sumaOtisaka = 0.0;
            }

            suma += sumaPapir1
                 + sumaPapir2
                 + sumaPapir3
                 + sumaPapir4
                 + vrednostPripreme
                 + sumaPloca
                 + vrednostPripremePoPloci
                 + sumaOtisaka
                 + vrednostPranjaMasina
                 + vrednostToniranja
                 + vrednostFederPoveza
                 + sumaPlastike
                 + sumaSivenja
                 + vrednostBrojanjaListova
                 + vrednostStancovanja
                 + vrednostRicovanja
                 + vrednostNumeracije
                 + vrednostSecenjaPapira
                 + vrednostLepljenjaForzeca
                 + vrednostCantragovanja
                 + vrednostLKPT
                 + vrednostKoricenja
                 + vrednostIzradeKorica
                 + vrednostSecenjaZica
                 + vrednostPakovanja
                 + vrednostJahaca
                 + vrednostHeftanja
                 + vrednostSavijanjaPapira
                 + vrednostKasiranja
                 + vrednostDigitale
                 + vrednostPerforacije
                 + vrednostBigovanja
                 + vrednostZlatotiska
                 + vrednostKlisea
                 + vrednostIzradeKesa
                 + vrednostIzradeKutija
                 + vrednostLajmovanja
                 + vrednostBusenjaRupa;

            textBox5.Text = suma.ToString("0.00") + " rsd.";
            button4.Enabled = true;
        }

        //SUMA
        private void button2_Click(object sender, EventArgs e)
        {
            

        izracunaj();
            

        }
        //ODABIR PAPIRA 
        public Papiri odabirPapira(ComboBox comboBox,Label labela)
        {
            Papiri izabranPapir=null;
            double izabranaVrednostPapir=0;
            double sumaPapir=0;
            double kolicinaPapir = 1;

            for (int i = 0; i < countPapiri; i++)
            {
                if (comboBox.SelectedItem.Equals(nizPapira[i].nazivPapira))
                {
                    izabranPapir = nizPapira[i];
                    izabranaVrednostPapir = nizPapira[i].ukupnaVrednost;
                    break;
                }
            }

            sumaPapir = izabranaVrednostPapir * kolicinaPapir;

            labela.Text = sumaPapir.ToString("0.00");
            labela.Text += " rsd.";
            return izabranPapir;
        }
        //KOLICINA PAPIRA
        public (double,double) kolicinaPapira(ComboBox comboBox, Label labela, TextBox textBox)
        {
            double izabranaVrednostPapir = 0;
            double sumaPapir = 0;
            double kolicinaPapir = 1;
            int vrednost;
            bool success = Int32.TryParse(textBox.Text, out vrednost);
            if (success)
            {
                kolicinaPapir = vrednost;
            }
            else
            {
                if (textBox.Text != "")
                    MessageBox.Show("Nedozvoljen unos.");

                textBox.Text = "";
                sumaPapir = 0;
                labela.Text = "0.00 rsd.";
            }


            if (comboBox.SelectedItem != null)
            {
                for (int i = 0; i < countPapiri; i++)
                {
                    if (comboBox.SelectedItem.Equals(nizPapira[i].nazivPapira))
                    {
                        izabranaVrednostPapir = nizPapira[i].ukupnaVrednost;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Papir nije izabran.");
            }


            sumaPapir = izabranaVrednostPapir * kolicinaPapir;

            labela.Text = sumaPapir.ToString("0.00");
            labela.Text += " rsd.";

            return (sumaPapir,kolicinaPapir);
        }
        //UNOS VREDNOSTI

        public double unosVrednosti(TextBox textBox,Label labela)
        {
            double unesenaVrednost=0, vrednost;
            bool success = Double.TryParse(textBox.Text, out vrednost);
            if (success)
            {
                unesenaVrednost = vrednost;
            }
            else
            {
                if (textBox.Text != "")
                    MessageBox.Show("Nedozvoljen unos.");

                textBox.Text = "";
                labela.Text = "0.00 rsd.";
            }

            labela.Text = unesenaVrednost.ToString("0.00");
            labela.Text += " rsd.";

            return unesenaVrednost;
        }

        //PAPIR 1
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            izabranPapir1=odabirPapira(comboBox1,label3);
            izabranaVrednostPapir1 = izabranPapir1.ukupnaVrednost;
            textBox1.Text = "1";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (sumaPapir1,kolicinaPapir1) = kolicinaPapira(comboBox1, label3, textBox1);
            
        }
        //PAPIR 2
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox2.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                textBox2.Enabled = false;
                textBox2.Text = "";
                sumaPapir2 = 0;
                label7.Text = "0.00 rsd.";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            izabranPapir2=odabirPapira(comboBox2, label7);
            izabranaVrednostPapir2 = izabranPapir2.ukupnaVrednost;
            textBox2.Text = "1";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            (sumaPapir2,kolicinaPapir2) = kolicinaPapira(comboBox2, label7, textBox2);
        }

        //PAPIR 3
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                comboBox3.Enabled = true;
                textBox3.Enabled = true;
            }
            else
            {
                comboBox3.Enabled = false;
                textBox3.Enabled = false;
                textBox3.Text = "";
                sumaPapir3 = 0;
                label10.Text = "0.00 rsd.";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            izabranPapir3=odabirPapira(comboBox3, label10);
            izabranaVrednostPapir3 = izabranPapir3.ukupnaVrednost;
            textBox3.Text = "1";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            (sumaPapir3,kolicinaPapir3) = kolicinaPapira(comboBox3, label10, textBox3);
        }
        //PAPIR 4
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                comboBox4.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                comboBox4.Enabled = false;
                textBox4.Enabled = false;
                textBox4.Text = "";
                sumaPapir4 = 0;
                label14.Text = "0.00 rsd.";
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            izabranPapir4=odabirPapira(comboBox4, label14);
            izabranaVrednostPapir4 = izabranPapir4.ukupnaVrednost;
            textBox4.Text = "1";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            (sumaPapir4,kolicinaPapir4) = kolicinaPapira(comboBox4, label14, textBox4);
        }

        //PRIPREMA
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox6.Enabled = true;
            }
            else
            {
                textBox6.Enabled = false;
                textBox6.Text = "";
                label20.Text = "0.00 rsd.";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            vrednostPripreme = unosVrednosti(textBox6, label20);
        }

        //PLOCA
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                comboBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox8.Enabled = true;
                textBox7.Enabled = true;
                if (!checkBox6.Checked)
                {
                    vrednostPripremePoPloci = 0.0;
                }
                if (!checkBox8.Checked)
                {
                    sumaOtisaka = 0.0;
                }
            }
            else
            {
                comboBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox6.Checked = false;
                checkBox8.Enabled = false;
                checkBox8.Checked = false;
                textBox7.Enabled = false;
                textBox7.Text = "";
                sumaPloca = 0.0;
                label22.Text = "0.00 rsd.";
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < countPloce; i++)
            {
                if (comboBox5.SelectedItem.Equals(nizPloca[i].nazivPloce))
                {
                    izabranaPloca = nizPloca[i];
                    izabranaVrednostPloce = nizPloca[i].cena;
                    break;
                }
            }
            
            sumaPloca = izabranaVrednostPloce * kolicinaPloca;

            textBox7.Text = "1";
            label32.Text = "Količina*("+izabranaPloca.nazivPloce+": "+izabranaPloca.vrednost_otiska+"):";
            label22.Text = sumaPloca.ToString("0.00");
            label22.Text += " rsd.";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int vrednost;
            bool success = Int32.TryParse(textBox7.Text, out vrednost);
            if (success)
            {
                kolicinaPloca = vrednost;
            }
            else
            {
                if (textBox7.Text != "")
                    MessageBox.Show("Nedozvoljen unos.");

                textBox7.Text = "";
                label22.Text = "0.00 rsd.";
            }


            if (comboBox5.SelectedItem != null)
            {
                for (int i = 0; i < countPloce; i++)
                {
                    if (comboBox5.SelectedItem.Equals(nizPloca[i].nazivPloce))
                    {
                        izabranaPloca = nizPloca[i];
                        izabranaVrednostPloce = nizPloca[i].cena;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Ploča nije izabrana.");
            }

            sumaPloca = izabranaVrednostPloce * kolicinaPloca;

            label22.Text = sumaPloca.ToString("0.00");
            label22.Text += " rsd.";
        }
        
        //PRIPREMA PO PLOCI
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox8.Enabled = false;
                vrednostPripremePoPloci = sumaPloca;
                textBox8.Text = vrednostPripremePoPloci.ToString("0.00");
                label25.Text = vrednostPripremePoPloci.ToString("0.00") + " rsd.";
            }
            else
            {
                vrednostPripremePoPloci = 0.00;
                textBox8.Enabled = false;
                textBox8.Text = "";
                label25.Text = "0.00 rsd.";
            }
        }

        //PRANJE MASINA
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                textBox9.Enabled = true;
            }
            else
            {
                textBox9.Enabled = false;
                textBox9.Text = "";
                label28.Text = "0.00 rsd.";
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            vrednostPranjaMasina = unosVrednosti(textBox9, label28);
        }

        //OTISCI
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                textBox10.Enabled = true;
                textBox10.Text = "1";
            }
            else
            {
                sumaOtisaka = 0.0;
                textBox10.Enabled = false;
                textBox10.Text = "";
                label31.Text = "0.00 rsd.";
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            int vrednost;
            bool success = Int32.TryParse(textBox10.Text, out vrednost);
            if (success)
            {
                kolicinaOtisaka = vrednost;
            }
            else
            {
                if (textBox10.Text != "")
                    MessageBox.Show("Nedozvoljen unos.");

                textBox10.Text = "";
                label31.Text = "0.00 rsd.";
            }

            if (!(izabranaPloca == null))
            {
                sumaOtisaka = izabranaPloca.vrednost_otiska * kolicinaOtisaka;
            }
            else
            {
                sumaOtisaka = 0;
            }
            

            label31.Text = sumaOtisaka.ToString("0.00");
            label31.Text += " rsd.";
        }

        //FEDER POVEZ
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                textBox11.Enabled = true;
            }
            else
            {
                textBox11.Enabled = false;
                textBox11.Text = "";
                label34.Text = "0.00 rsd.";
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            vrednostFederPoveza = unosVrednosti(textBox11, label34);
        }

        //IZBOR PLASTIKE
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                comboBox6.Enabled = true;
                textBox12.Enabled = true;

            }
            else
            {
                comboBox6.Enabled = false;
                textBox12.Enabled = false;
                textBox12.Text = "";
                label37.Text = "0.00 rsd.";
            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < countPlastike; i++)
            {
                if (comboBox6.SelectedItem.Equals(nizPlastika[i].nazivPlastike))
                {
                    izabranaPlastika = nizPlastika[i];
                    izabranaVrednostPlastike = nizPlastika[i].cena;
                    break;
                }
            }

            sumaPlastike = izabranaVrednostPlastike * kolicinaPlastike;
            textBox12.Text = "1";
            label38.Text = "Količina*("+ izabranaVrednostPlastike +" rsd.):";
            label37.Text = sumaPlastike.ToString("0.00");
            label37.Text += " rsd.";
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            int vrednost;
            bool success = Int32.TryParse(textBox12.Text, out vrednost);
            if (success)
            {
                kolicinaPlastike = vrednost;
            }
            else
            {
                if (textBox12.Text != "")
                    MessageBox.Show("Nedozvoljen unos.");

                textBox12.Text = "";
                label37.Text = "0.00 rsd.";
            }


            if (comboBox6.SelectedItem != null)
            {
                for (int i = 0; i < countPlastike; i++)
                {
                    if (comboBox6.SelectedItem.Equals(nizPlastika[i].nazivPlastike))
                    {
                        izabranaPlastika = nizPlastika[i];
                        izabranaVrednostPlastike = nizPlastika[i].cena;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Plastika nije izabrana.");
            }


            sumaPlastike = izabranaVrednostPlastike * kolicinaPlastike;

            label37.Text = sumaPlastike.ToString("0.00");
            label37.Text += " rsd.";
        }

        //SIVENJE
        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked)
            {
                textBox26.Enabled = true;
                textBox26.Text = "1";
                label80.Text="Količina*("+vrednostSivenja+"):";
            }
            else
            {
                textBox26.Enabled = false;
                textBox26.Text = "";
                label79.Text = "0.00 rsd.";
            }
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            int vrednost;
            bool success = Int32.TryParse(textBox26.Text, out vrednost);
            if (success)
            {
                kolicinaSivenja = vrednost;
            }
            else
            {
                if (textBox26.Text != "")
                    MessageBox.Show("Nedozvoljen unos.");

                textBox26.Text = "";
                label79.Text = "0.00 rsd.";
            }

            sumaSivenja = vrednostSivenja * kolicinaSivenja;

            label79.Text = sumaSivenja.ToString("0.00");
            label79.Text += " rsd.";
        }

        //TONIRANJE
        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox25.Checked)
            {
                textBox27.Enabled = true;
            }
            else
            {
                textBox27.Enabled = false;
                textBox27.Text = "";
                label82.Text = "0.00 rsd.";
            }
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            vrednostToniranja = unosVrednosti(textBox27, label82);
        }

        //STANCOVANJE
        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                textBox15.Enabled = true;
            }
            else
            {
                textBox15.Enabled = false;
                textBox15.Text = "";
                label46.Text = "0.00 rsd.";
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            vrednostStancovanja = unosVrednosti(textBox15, label46);
        }
        //RICOVANJE
        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                textBox14.Enabled = true;
            }
            else
            {
                textBox14.Enabled = false;
                textBox14.Text = "";
                label43.Text = "0.00 rsd.";
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            vrednostRicovanja = unosVrednosti(textBox14, label43);
        }
        //NUMERACIJA
        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                textBox16.Enabled = true;
            }
            else
            {
                textBox16.Enabled = false;
                textBox16.Text = "";
                label49.Text = "0.00 rsd.";
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            vrednostNumeracije = unosVrednosti(textBox16, label49);
        }
        //SECENJE PAPIRA
        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                textBox17.Enabled = true;
            }
            else
            {
                textBox17.Enabled = false;
                textBox17.Text = "";
                label52.Text = "0.00 rsd.";
            }
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            vrednostSecenjaPapira = unosVrednosti(textBox17, label52);
        }
        //LEPLJENJE FORZECA
        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                textBox18.Enabled = true;
            }
            else
            {
                textBox18.Enabled = false;
                textBox18.Text = "";
                label55.Text = "0.00 rsd.";
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            vrednostLepljenjaForzeca = unosVrednosti(textBox18, label55);
        }
        //CANTRAGOVANJE
        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                textBox19.Enabled = true;
            }
            else
            {
                textBox19.Enabled = false;
                textBox19.Text = "";
                label58.Text = "0.00 rsd.";
            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            vrednostCantragovanja = unosVrednosti(textBox19, label58);
        }
        //LKPT
        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                textBox21.Enabled = true;
            }
            else
            {
                textBox21.Enabled = false;
                textBox21.Text = "";
                label64.Text = "0.00 rsd.";
            }
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            vrednostLKPT = unosVrednosti(textBox21, label64);
        }
        //KORICENJE
        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked)
            {
                textBox22.Enabled = true;
            }
            else
            {
                textBox22.Enabled = false;
                textBox22.Text = "";
                label67.Text = "0.00 rsd.";
            }
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            vrednostKoricenja = unosVrednosti(textBox22, label67);
        }
        //IZRADA KORICA
        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                textBox23.Enabled = true;
            }
            else
            {
                textBox23.Enabled = false;
                textBox23.Text = "";
                label70.Text = "0.00 rsd.";
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            vrednostIzradeKorica = unosVrednosti(textBox23, label70);
        }
        //SECENJE ZICA
        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                textBox24.Enabled = true;
            }
            else
            {
                textBox24.Enabled = false;
                textBox24.Text = "";
                label73.Text = "0.00 rsd.";
            }
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            vrednostSecenjaZica = unosVrednosti(textBox24, label73);
        }
        //PAKOVANJE
        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
            {
                textBox25.Enabled = true;
            }
            else
            {
                textBox25.Enabled = false;
                textBox25.Text = "";
                label76.Text = "0.00 rsd.";
            }
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            vrednostPakovanja = unosVrednosti(textBox25, label76);
        }
        //JAHACI
        private void checkBox29_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox29.Checked)
            {
                textBox31.Enabled = true;
            }
            else
            {
                textBox31.Enabled = false;
                textBox31.Text = "";
                label94.Text = "0.00 rsd.";
            }
        }

        private void textBox31_TextChanged(object sender, EventArgs e)
        {
            vrednostJahaca = unosVrednosti(textBox31, label94);
        }
        //HEFTANJE
        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked)
            {
                textBox20.Enabled = true;
            }
            else
            {
                textBox20.Enabled = false;
                textBox20.Text = "";
                label61.Text = "0.00 rsd.";
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            vrednostHeftanja = unosVrednosti(textBox20, label61);
        }
        //SAVIJANJE PAPIRA
        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                textBox13.Enabled = true;
            }
            else
            {
                textBox13.Enabled = false;
                textBox13.Text = "";
                label40.Text = "0.00 rsd.";
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            vrednostSavijanjaPapira = unosVrednosti(textBox13, label40);
        }
        //KASIRANJE
        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked)
            {
                textBox28.Enabled = true;
            }
            else
            {
                textBox28.Enabled = false;
                textBox28.Text = "";
                label85.Text = "0.00 rsd.";
            }
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {
            vrednostKasiranja = unosVrednosti(textBox28, label85);
        }
        //DIGITALA
        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox27.Checked)
            {
                textBox29.Enabled = true;
            }
            else
            {
                textBox29.Enabled = false;
                textBox29.Text = "";
                label88.Text = "0.00 rsd.";
            }
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
            vrednostDigitale = unosVrednosti(textBox29, label88);
        }
        //PERFORACIJA
        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox28.Checked)
            {
                textBox30.Enabled = true;
            }
            else
            {
                textBox30.Enabled = false;
                textBox30.Text = "";
                label91.Text = "0.00 rsd.";
            }
        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            vrednostPerforacije = unosVrednosti(textBox30, label91);
        }

        //BUSENJE RUPA
        private void checkBox36_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox36.Checked)
            {
                textBox39.Enabled = true;
            }
            else
            {
                textBox39.Enabled = false;
                textBox39.Text = "";
                label116.Text = "0.00 rsd.";
            }
        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {
            vrednostBusenjaRupa = unosVrednosti(textBox39, label116);
        }
        //BIGOVANJE
        private void checkBox30_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox30.Checked)
            {
                textBox33.Enabled = true;
            }
            else
            {
                textBox33.Enabled = false;
                textBox33.Text = "";
                label98.Text = "0.00 rsd.";
            }
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {
            vrednostBigovanja = unosVrednosti(textBox33, label98);
        }
        //ZLATOTISAK
        private void checkBox35_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox35.Checked)
            {
                textBox38.Enabled = true;
            }
            else
            {
                textBox38.Enabled = false;
                textBox38.Text = "";
                label113.Text = "0.00 rsd.";
            }
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            vrednostZlatotiska = unosVrednosti(textBox38, label113);
        }
        //KLISE
        private void checkBox34_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox34.Checked)
            {
                textBox37.Enabled = true;
            }
            else
            {
                textBox37.Enabled = false;
                textBox37.Text = "";
                label110.Text = "0.00 rsd.";
            }
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            vrednostKlisea = unosVrednosti(textBox37, label110);
        }
        //IZRADA KESA
        private void checkBox33_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox33.Checked)
            {
                textBox36.Enabled = true;
            }
            else
            {
                textBox36.Enabled = false;
                textBox36.Text = "";
                label107.Text = "0.00 rsd.";
            }
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            vrednostIzradeKesa = unosVrednosti(textBox36, label107);
        }
        //IZRADA KUTIJA
        private void checkBox31_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox31.Checked)
            {
                textBox35.Enabled = true;
            }
            else
            {
                textBox35.Enabled = false;
                textBox35.Text = "";
                label104.Text = "0.00 rsd.";
            }
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            vrednostIzradeKutija = unosVrednosti(textBox35, label104);
        }
        //LAJMOVANJE
        private void checkBox32_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox32.Checked)
            {
                textBox34.Enabled = true;
            }
            else
            {
                textBox34.Enabled = false;
                textBox34.Text = "";
                label101.Text = "0.00 rsd.";
            }
        }

        private void textBox34_TextChanged(object sender, EventArgs e)
        {
            vrednostLajmovanja = unosVrednosti(textBox34, label101);
        }
        //BROJANJE LISTOVA
        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox37.Checked)
            {
                textBox32.Enabled = true;
            }
            else
            {
                textBox32.Enabled = false;
                textBox32.Text = "";
                label118.Text = "0.00 rsd.";
            }
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            vrednostBrojanjaListova = unosVrednosti(textBox32, label118);
        }
        //PDF
        private void button4_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog save = new SaveFileDialog() { Filter="PDF file|*.pdf", ValidateNames = true })
            {
                if(save.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4,25,25,15,15);
                    try
                    {
                        PdfWriter.GetInstance(document, new FileStream(save.FileName, FileMode.Create));

                        Font mainFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, 11);
                        Font naslovFont = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, 13);
                        document.Open();

                        PdfPTable table = new PdfPTable(3);

                        PdfPCell naslov = new PdfPCell(new Phrase("Štamparija Riža - Ponuda", naslovFont));
                        PdfPCell naziv = new PdfPCell(new Phrase("Naziv", mainFont));
                        PdfPCell kolicina = new PdfPCell(new Phrase("Količina", mainFont));
                        PdfPCell cena = new PdfPCell(new Phrase("Cena", mainFont));

                        naslov.Colspan = 3;

                        naslov.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        naziv.HorizontalAlignment = 1;
                        kolicina.HorizontalAlignment = 1;
                        cena.HorizontalAlignment = 1;
                        table.AddCell(naslov);
                        table.AddCell(naziv);
                        table.AddCell(kolicina);
                        table.AddCell(cena);

                        String oznakaKolicine = "\u221A";

                        if (sumaPapir1 > 0)
                        {
                            naziv = new PdfPCell(new Phrase(izabranPapir1.nazivPapira, mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaPapir1.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaPapir1.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }
                        if (sumaPapir4 > 0)
                        {
                            naziv = new PdfPCell(new Phrase(izabranPapir4.nazivPapira, mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaPapir4.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaPapir4.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }
                        if (sumaPapir2 > 0)
                        {
                            naziv = new PdfPCell(new Phrase(izabranPapir2.nazivPapira, mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaPapir2.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaPapir2.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }
                        if (sumaPapir3 > 0)
                        {
                            naziv = new PdfPCell(new Phrase(izabranPapir3.nazivPapira, mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaPapir3.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaPapir3.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostPripreme > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Priprema", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostPripreme.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (sumaPloca > 0)
                        {
                            naziv = new PdfPCell(new Phrase(izabranaPloca.nazivPloce, mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaPloca.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaPloca.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostPripremePoPloci > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Priprema po ploči", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostPripreme.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (sumaOtisaka > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Otisci", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaOtisaka.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaOtisaka.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostPranjaMasina > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Pranje mašina", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostPranjaMasina.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostToniranja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Toniranje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostToniranja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostFederPoveza > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Feder povez", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostFederPoveza.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (sumaPlastike > 0)
                        {
                            naziv = new PdfPCell(new Phrase(izabranaPlastika.nazivPlastike, mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaPlastike.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaPlastike.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostBusenjaRupa > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Bušenje rupa(za kalendare i rek. blokove)", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostBusenjaRupa.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            cena.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(cena);
                        }

                        if (sumaSivenja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Šivenje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(kolicinaSivenja.ToString(), mainFont));
                            kolicina.HorizontalAlignment = 1;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(sumaSivenja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostStancovanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Štancovanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostStancovanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostRicovanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Ricovanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostRicovanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostNumeracije > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Numeracija", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostNumeracije.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostSecenjaPapira > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Sečenje papira", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostSecenjaPapira.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostLepljenjaForzeca > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Lepljenje forzeca", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostLepljenjaForzeca.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostCantragovanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Cantragovanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostCantragovanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostLKPT > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Lepljenje,kapital i pokazne trake", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostLKPT.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            cena.VerticalAlignment = Element.ALIGN_MIDDLE;
                            table.AddCell(cena);
                        }

                        if (vrednostKoricenja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Koričenje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostKoricenja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostIzradeKorica > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Izrada korica", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostIzradeKorica.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostSecenjaZica > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Sečenje žica", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostSecenjaZica.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostPakovanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Pakovanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostPakovanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostBrojanjaListova > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Brojanje listova", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostBrojanjaListova.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostJahaca > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Jahači", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostJahaca.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostHeftanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Heftanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostHeftanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostSavijanjaPapira > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Savijanje papira", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostSavijanjaPapira.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostKasiranja> 0)
                        {
                            naziv = new PdfPCell(new Phrase("Kasiranje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostKasiranja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostDigitale > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Digitala", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostDigitale.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostPerforacije> 0)
                        {
                            naziv = new PdfPCell(new Phrase("Perforacija", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostPerforacije.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostBigovanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Bigovanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostBigovanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostZlatotiska > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Zlatotisak", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostZlatotiska.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostKlisea > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Kliše", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostKlisea.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostIzradeKesa > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Izrada kesa", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostIzradeKesa.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostIzradeKutija > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Izrada kutija", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostIzradeKutija.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        if (vrednostLajmovanja > 0)
                        {
                            naziv = new PdfPCell(new Phrase("Lajmovanje", mainFont));
                            naziv.HorizontalAlignment = 0;
                            table.AddCell(naziv);

                            kolicina = new PdfPCell(new Phrase(oznakaKolicine, mainFont));
                            kolicina.HorizontalAlignment = 1;
                            kolicina.BackgroundColor = BaseColor.LIGHT_GRAY;
                            table.AddCell(kolicina);

                            cena = new PdfPCell(new Phrase(vrednostLajmovanja.ToString("0.00") + " rsd.", mainFont));
                            cena.HorizontalAlignment = 2;
                            table.AddCell(cena);
                        }

                        document.Add(table);
                        
                        String footer1 = "";
                        footer1 += "\n________________________________________________________________________________________";
                        footer1 += "\n\n                                                                                                                      Ukupna cena: " + suma.ToString("0.00") + " rsd.";
                        footer1 += "\n________________________________________________________________________________________\n\n";
                        footer1 += "\n  Izdavač ponude: _____________________                                           Primio: _____________________";
                        footer1 += "\n\n                 Datum: "+DateTime.Now.ToString("dd/MM/yyyy")+" god.";
                        var footer_para1 = new Paragraph(footer1, mainFont);
                        document.Add(footer_para1);

                        MessageBox.Show("Uspešno čuvanje PDF-a.", "Riža kalkulator", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Greška prilikom čuvanja PDF-a!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    finally
                    {
                        document.Close();
                    }
                }
            }
        }

        
    }
}
