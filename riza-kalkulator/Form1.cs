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
        public double kolicinaPloca = 0;
        public double sumaPloca = 0;

        public double izabranaVrednostPlastike = 0;
        public Plastike izabranaPlastika;
        public double kolicinaPlastike = 0;
        public double sumaPlastike = 0;

        public double vrednostPripremePoPloci = 0;
        public double vrednostPranjaMasina = 0;

        public double sumaOtisaka = 0;
        public double kolicinaOtisaka = 0;

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

        //SUMA
        private void button2_Click(object sender, EventArgs e)
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

            suma += sumaPapir1
                 + sumaPapir2
                 + sumaPapir3
                 + sumaPapir4
                 + sumaPloca
                 + vrednostPripremePoPloci
                 + vrednostPranjaMasina
                 + sumaOtisaka
                 + vrednostFederPoveza
                 + sumaPlastike;

            textBox5.Text = suma.ToString("0.00") + " rsd.";

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
                textBox7.Enabled = true;
            }
            else
            {
                comboBox5.Enabled = false;
                textBox7.Enabled = false;
                textBox7.Text = "";
                sumaPloca = 0;
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
                textBox8.Text = sumaPloca.ToString("0.00");
                label25.Text = sumaPloca.ToString("0.00")+" rsd.";
                vrednostPripremePoPloci = sumaPloca;
            }
            else
            {
                vrednostPripremePoPloci = 0;
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

            }
            else
            {
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
    }
}
