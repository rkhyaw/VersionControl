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
