using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LinkToXml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //XDocument xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("Parametry aplikacji"), new XElement("opcje", new XAttribute("nazwa", this.Text), new XElement("pozycja", new XElement ("X", this.Left), new XElement("Y", this.Top)), new XElement("wielkość", new XElement))
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                XDocument xml = XDocument.Load("Ustawienia.xml");
                //odczytywanie tytułu okna
                this.Text = xml.Root.Element("okno").Attribute("nazwa").Value;
                //odczytanie pozycji i wielkosci
                XElement pozycja = xml.Root.Element("okno").Element("pozycja");
                this.Left = int.Parse(pozycja.Element("X").Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Błąd podczas");
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xml = XDocument.Load("Ustawienia.xml");
            //wersja XML

            string wersja = xml.Declaration.Version;
            MessageBox.Show("Wersja XML:" + wersja);

            //odczytanie nazwy glownego elementu
            string nazwaElementuGlownego = xml.Root.Name.LocalName;
            Message.Box("Nazwa elementu głównego: " + nazwaElementuGlownego);

            //kolekcja podelementów ze wszystkich poziomow drzewa
            IEnumerable<XElement> wszystkiePodelementy = xml.Root.Descendants();
            string s = "Wszystkie podelementy: \n";
            foreach (XElement podelement in wszystkiePodelementy) s += podelement.Name + "\n";
            MessageBox.Show(s);

            //kolekcja podelemetow elementu okno
            IEnumerable<XElement> podelementyOkno = xml.Root.Element("okno").Elements();
            s = "Podelementy elementu okno: \n";

            foreach (XElement podelement in podelementyOkno) s += podelement.Name + "\n";
            MessageBox.Show(s);
        }
    }
}
