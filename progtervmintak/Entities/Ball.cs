using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace progtervmintak.Entities
{
    public class Ball : Label
    {
        public Ball()
        {
            AutoSize = false;
            Width = Height = 50;
            Paint += Ball_Paint;
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected void DrawImage(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(Color.Red), -50, 0, Width, Height);
        }

        public void MoveBall()
        {
            Left += 1;
        }
    }
}
