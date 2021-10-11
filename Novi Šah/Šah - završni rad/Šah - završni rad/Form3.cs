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
    public partial class Form3 : Form
    {
        TimeSpan trajanjeIgre = new TimeSpan();
        DateTime pIgre = new DateTime();
        DateTime kIgre = new DateTime();
        bool rSahMat = false;
        bool iSahMat = false;
        int bpRacunalo = 0;
        int bpIgrac = 0;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ((Form1)this.Owner).krajIgre = DateTime.Now;
            pIgre = ((Form1)this.Owner).pocetakIgre;
            kIgre = ((Form1)this.Owner).krajIgre;
            trajanjeIgre = kIgre.Subtract(pIgre);

            rSahMat = ((Form1)this.Owner).racunaluJeSahMat;
            iSahMat = ((Form1)this.Owner).igracuJeSahMat;

            bpRacunalo = ((Form1)this.Owner).brojPotezaRacunalo;
            bpIgrac = ((Form1)this.Owner).brojPotezaIgrac;

            
            if (rSahMat == true)
            {
                lblPobjednik.Text = "Igrač 1 je pobijedio!";
            }

            if (iSahMat == true)
            {
                lblPobjednik.Text = "Računalo je pobijedilo!";
            }

            lblTrajanjeIgre.Text = trajanjeIgre.ToString(@"hh\:mm\:ss");
            lblBrojPotezaIgrac.Text = bpIgrac.ToString();
            lblBrojPotezaRacunalo.Text = bpRacunalo.ToString();
            lblUkupnoPoteza.Text = (bpRacunalo + bpIgrac).ToString();
        }

        private void btnDa_Click(object sender, EventArgs e)
        {
            pnlNovaIgra.Visible = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            ((Form1)this.Owner).Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ((Form1)this.Owner).varijableNaPocetneVrijednosti();

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
    }
}
