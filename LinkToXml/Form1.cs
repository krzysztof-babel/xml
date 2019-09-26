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
            XDocument xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Parametry aplikacji"),
                new XElement("opcje",
                    new XElement("okno",
                        new XAttribute("nazwa", this.Text),
                        new XElement("pozycja",
                            new XElement("X", this.Left),
                            new XElement("Y", this.Top)
                        ),
                        new XElement("wielkość",
                            new XElement("Szer", this.Width),
                            new XElement("Wys", this.Height)
                        )
                    )
                )
            );

            xml.Save("Ustawienia.xml");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                XDocument xml = XDocument.Load("Ustawienia.xml");

                //odczytanie tytułu okna
                this.Text = xml.Root.Element("okno").Attribute("nazwa").Value;

                //odczytanie pozycji i wielkości
                XElement pozycja = xml.Root.Element("okno").Element("pozycja");
                this.Left = int.Parse(pozycja.Element("X").Value);
                this.Top = int.Parse(pozycja.Element("Y").Value);

                XElement wielkość = xml.Root.Element("okno").Element("wielkość");
                this.Width = int.Parse(wielkość.Element("Szer").Value);
                this.Height = int.Parse(wielkość.Element("Wys").Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Błąd podczas odczytywania pliku XML:\n" + exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XDocument xml = XDocument.Load("Ustawienia.xml");

            //wersja XML
            string wersja = xml.Declaration.Version;
            MessageBox.Show("Wersja XML: " + wersja);

            //odczytanie nazwy głównego elementu
            string nazwaElementuGlownego = xml.Root.Name.LocalName;
            MessageBox.Show("Nazwa elementu głównego: " + nazwaElementuGlownego);

            //kolekcja podelementów ze wszystkich poziomów drzewa
            IEnumerable<XElement> wszystkiePodelementy = xml.Root.Descendants();
            string s = "Wszystkie podelementy:\n";
            foreach (XElement podelement in wszystkiePodelementy) s += podelement.Name + "\n";
            MessageBox.Show(s);

            //kolekcja podelementów elementu okno
            IEnumerable<XElement> podelementyOkno = xml.Root.Element("okno").Elements();
            s = "Podelementy elementu okno:\n";
            foreach (XElement podelement in podelementyOkno) s += podelement.Name + "\n";
            MessageBox.Show(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pobieranie danych
            XDocument xml = XDocument.Load("ListaOsob.xml");
            IEnumerable<Osoba> listaOsobPelnoletnich = from osoba in xml.Descendants("Osoba")
                where int.Parse(osoba.Element("Wiek").Value) >= 18
                orderby osoba.Element("Imię").Value
                select
                new Osoba()
                {
                    Id = int.Parse(osoba.Attribute("Id").Value),
                    Imię = osoba.Element("Imię").Value,
                    Nazwisko = osoba.Element("Nazwisko").Value,
                    NumerTelefonu = int.Parse(osoba.Element("NumerTelefonu").Value),
                    Wiek = int.Parse(osoba.Element("Wiek").Value)
                };

            //wyświetlenie danych
            string s = "Lista osób pełnoletnich: \n";
            foreach (Osoba osoba in )

        }
    }
}
