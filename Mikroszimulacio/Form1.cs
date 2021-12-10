using Mikroszimulacio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroszimulacio
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbabilities> BirthProbabilities = new List<BirthProbabilities>();
        List<DeathProbabilities> DeathProbabilities = new List<DeathProbabilities>();

        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"D:\temp\nép-teszt.csv");
            BirthProbabilities = GetBirthProbabilities(@"D:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"D:\Temp\halál.csv");

            for (int year = 2005; year <= 2024; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    SzimulaciosLepes(Population[i], year);
                }

                int ferfiakszama = (from x in Population where x.Gender == Gender.Male select x).Count();
                int nokszama = (from x in Population where x.Gender == Gender.Female select x).Count();

                Console.WriteLine(string.Format("Év: {0} Férfiak: {1} Nők: {2}", year, ferfiakszama, nokszama));
            }

        }

        private void SzimulaciosLepes(Person p, int year)
        {
            if (!p.IsAlive) return;
            int kor = year - p.BirthYear;
            double halalozasvalsz = (from x in DeathProbabilities where x.Gender == p.Gender && x.Age == kor select x.P).FirstOrDefault();
            double vltln = rng.NextDouble();
            if (vltln <= halalozasvalsz) p.IsAlive = false;

            if(p.IsAlive&&p.Gender==Gender.Female)
            {
                double szuletesvalsz = (from x in BirthProbabilities where x.Age == kor select x.P).FirstOrDefault();
                vltln = rng.NextDouble();
                if (vltln <= szuletesvalsz)
                {
                    Person baba = new Person();
                    baba.BirthYear = year;
                    baba.NbrOfChildren = 0;
                    baba.Gender = (Gender)rng.Next(1, 3);
                    Population.Add(baba);
                }
            }
        }

        private List<BirthProbabilities> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbabilities> result = new List<BirthProbabilities>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    result.Add(new BirthProbabilities()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }
            return result;
        }

        private List<DeathProbabilities> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbabilities> result = new List<DeathProbabilities>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    result.Add(new DeathProbabilities()
                    {
                        Age = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }
            return result;
        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> result = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    result.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }
            return result;
        }
    }
}
