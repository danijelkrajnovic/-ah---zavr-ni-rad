using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Šah___završni_rad
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (rbCrne.Checked == true)
            {
                ((Form1)this.Owner).crneFigure = true;
                ((Form1)this.Owner).bijeleFigure = false;
                ((Form1)this.Owner).lblCrniIgrac.Text = "Igrač 1";
                ((Form1)this.Owner).lblBijeliIgrac.Text = "Računalo";
            }

            if (rbBijele.Checked == true)
            {
                ((Form1)this.Owner).bijeleFigure = true;
                ((Form1)this.Owner).crneFigure = false;
                ((Form1)this.Owner).lblBijeliIgrac.Text = "Igrač 1";
                ((Form1)this.Owner).lblCrniIgrac.Text = "Računalo";
            }

            if (rbRandom.Checked == true)
            {
                Random rndBroj = new Random();
                int tmpBroj = rndBroj.Next(0, 1000);

                if (tmpBroj % 2 == 0)
                {
                    //crne figure = true
                    ((Form1)this.Owner).crneFigure = true;
                    ((Form1)this.Owner).bijeleFigure = false;
                    ((Form1)this.Owner).lblCrniIgrac.Text = "Igrač 1";
                    ((Form1)this.Owner).lblBijeliIgrac.Text = "Računalo";
                }
                else
                {
                    //bijele figure = true
                    ((Form1)this.Owner).bijeleFigure = true;
                    ((Form1)this.Owner).crneFigure = false;
                    ((Form1)this.Owner).lblBijeliIgrac.Text = "Igrač 1";
                    ((Form1)this.Owner).lblCrniIgrac.Text = "Računalo";
                }
            }


            ((Form1)this.Owner).pripremiPlocu();            

            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
