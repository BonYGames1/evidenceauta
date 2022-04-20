using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evidenceAua
{
    class Program
    {
        static void Main(string[] args)
        {
            Databaze databaze = new Databaze();

            databaze.ImportPriStartu();

            string volba = "";

            do
            {
                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine("Moje supermenu: ");
                Console.WriteLine("");
                Console.WriteLine("1. Výpis všech aut");
                Console.WriteLine("2. Ruční vložení do db");
                Console.WriteLine("3. Import z textového souboru Import.txt");//domácí úkol dodělat
                Console.WriteLine("");
                Console.WriteLine("k - Konec programu");
                Console.WriteLine("************************");
                Console.WriteLine("");
                Console.WriteLine("Tvoje volba:");
                volba = Console.ReadLine();

                switch (volba)
                {
                    case "1":
                        databaze.VypisVseho();
                        break;
                    case "2":
                        databaze.RucniVlozeniAuta();
                        break;
                    case "3":
                        Console.WriteLine("Dodělat dú");
                        break;
                    case "k":         
                        break;
                    default:
                        Console.WriteLine("Špatná volba..");
                        Console.ReadKey();
                        break;
                }

            } while (volba != "k");

            databaze.UlozeniNaKonci();
        }
    }

    class Auto
    {
        public string Nazev { get; private set; }
        public string Model { get; private set; }
        public DateTime DatumVyroby { get; private set; }

        public Auto(string nazev, string model, DateTime datumVyroby)
        {
            Nazev = nazev;
            Model = model;
            DatumVyroby = datumVyroby;
        }
        public void Info()
        {
            Console.WriteLine("************************");
            Console.WriteLine("Značka a model auta: {0} {1}", Model, Nazev);
            Console.WriteLine("Datum vyroby: {0}", DatumVyroby);
            Console.WriteLine("************************");
        }
    }

    class Databaze
    {
        public List<Auto> listAut { get; private set; }

        public Databaze()
        {
            listAut = new List<Auto>();
        }

        public void Vlozit(Auto auto)
        {
            listAut.Add(auto);
        }

        public void Smazat(Auto auto)
        {
            listAut.Remove(auto);
        }
        
        public void ImportPriStartu()
        {
            StreamReader cteni = new StreamReader("Data.txt");

            string radek;

            while ((radek = cteni.ReadLine()) != null)
            {
                string[] poleHodnot = radek.Split(';');
                Vlozit(new Auto(poleHodnot[0], poleHodnot[1], new DateTime(int.Parse(poleHodnot[4]), int.Parse(poleHodnot[3]), int.Parse(poleHodnot[2]))));
            }

            cteni.Close();
        }

        public void UlozeniNaKonci()
        {
            StreamWriter zapis = new StreamWriter("Data.txt");

            foreach (Auto auto in listAut)
            {
                string radekZapis = auto.Nazev + ";" + auto.Model + ";" + auto.DatumVyroby.Year + ";" + auto.DatumVyroby.Month + ";" + auto.DatumVyroby.Day;

                zapis.WriteLine(radekZapis);
            }

            zapis.Close();
        }

        public void RucniVlozeniAuta()
        {
            Console.WriteLine("zadej údaje o autě: ");
            Console.WriteLine("Značka: ");
            string znacka = Console.ReadLine();
            Console.WriteLine("Model: ");
            string model = Console.ReadLine();
            Console.WriteLine("Rok výroby: ");
            int rok = int.Parse(Console.ReadLine());
            Console.WriteLine("Měsíc výroby: ");
            int mesic = int.Parse(Console.ReadLine());
            Console.WriteLine("Den výroby: ");
            int den = int.Parse(Console.ReadLine());

            Vlozit(new Auto(znacka, model, new DateTime(rok, mesic, den)));

            Console.WriteLine("Auto bylo vloženo, pokračuj libovolnou klávesou......");
            Console.ReadKey();
        }

        public void VypisVseho()
        {
            foreach (Auto auto in listAut)
            {
                auto.Info();
            }

            Console.WriteLine("Konec výpisu, pokračuj libovolnou klávesou......");
            Console.ReadKey();
        }
    }
}
