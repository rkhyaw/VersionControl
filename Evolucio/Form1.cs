using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace Evolucio
{
    public partial class Form1 : Form
    {
        GameController gc=new GameController();
        GameArea ga;
        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;

        public Form1()
        {
            InitializeComponent();

            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);
            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }

            gc.Start();

            //gc.AddPlayer();
            //gc.Start(true);

            gc.GameOver += Gc_GameOver;
        }

        private void Gc_GameOver(object sender)
        {
            generation++;
            lblGen.Text = string.Format("{0}. generáció", generation);
            var playerList = (from x in gc.GetCurrentPlayers() orderby x.GetFitness() descending select x).ToList();
            var topPlayers = playerList.Take(populationSize / 2).ToList();

            gc.ResetCurrentLevel();

            foreach (var p in topPlayers)
            {
                var b = p.Brain.Clone();
                if (generation % 3 == 0)
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b);

                if (generation % 3 == 0)
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b.Mutate());
            }
        }
    }
}
