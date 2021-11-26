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
using medal.Entities;

namespace medal
{
    public partial class Form1 : Form
    {
        List<OlympicResult> results = new List<OlympicResult>();
        public Form1()
        {
            InitializeComponent();

            Betolt("Summer_olympic_Medals.csv");

            ComboFeltolt();

            Osztalyozas();

            dataGridView1.DataSource = results;
        }

        private void Osztalyozas()
        {
            foreach (OlympicResult item in results)
            {
                item.Position = Helyezes(item);
            }
        }

        int Helyezes(OlympicResult res)
        {
            int counter = 0;
            var szurt = (from x in results where x.Year == res.Year && x.Country != res.Country select x);
            foreach (OlympicResult item in szurt)
            {
                if (item.Medals[0] > res.Medals[0] || (item.Medals[0] == res.Medals[0] && item.Medals[1] > res.Medals[1]) || (item.Medals[0] == res.Medals[0] && item.Medals[1] == res.Medals[1]&& item.Medals[2] > res.Medals[2]))
                    counter++;
            }

            return counter+1;
        }

        private void ComboFeltolt()
        {
            var years = (from x in results orderby x.Year select x.Year).Distinct();
            comboBox1.DataSource = years.ToList();
        }

        void Betolt(string fajlnev)
        {
            using (StreamReader sr=new StreamReader(fajlnev))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    string[] mezok = sor.Split(',');
                    OlympicResult olympicResult = new OlympicResult();
                    olympicResult.Year = int.Parse(mezok[0]);
                    olympicResult.Country = mezok[3];
                    int[] mtomb = new int[3];

                    mtomb[0]=int.Parse(mezok[5]);
                    mtomb[1] = int.Parse(mezok[6]);
                    mtomb[2] = int.Parse(mezok[7]);
                    olympicResult.Medals = mtomb;

                    results.Add(olympicResult);
                    
                }
            }
        }
    }
}
