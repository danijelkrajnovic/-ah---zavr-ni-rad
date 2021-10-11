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
    public partial class Form4 : Form
    {
        public int pppx = 0; //ppp -> polozaj pijuna za promociju
        public int pppy = 0;


        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (((Form1)this.Owner).crneFigure == true)
            {
                pcbKraljicaBijela.Visible = false;
                pcbKonjBijeli.Visible = false;
                pcbTopBijeli.Visible = false;
                pcbLauferBijeli.Visible = false;

                pcbKraljicaCrna.Visible = true;
                pcbKonjCrni.Visible = true;
                pcbTopCrni.Visible = true;
                pcbLauferCrni.Visible = true;
            }

            if (((Form1)this.Owner).bijeleFigure == true)
            {
                pcbKraljicaBijela.Visible = true;
                pcbKonjBijeli.Visible = true;
                pcbTopBijeli.Visible = true;
                pcbLauferBijeli.Visible = true;

                pcbKraljicaCrna.Visible = false;
                pcbKonjCrni.Visible = false;
                pcbTopCrni.Visible = false;
                pcbLauferCrni.Visible = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string tmpNamePolje = "";
            tmpNamePolje = "q" + pppx.ToString() + pppy.ToString();

            Panel tmpPanel = ((Form1)this.Owner).grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

            if (rbKraljica.Checked == true)
            {
                tmpPanel.Tag = "115";                
            }

            if (rbKonj.Checked == true)
            {
                tmpPanel.Tag = "111";
            }

            if (rbTop.Checked == true)
            {
                tmpPanel.Tag = "109";
            }

            if (rbLaufer.Checked == true)
            {
                tmpPanel.Tag = "113";
            }

            if (((Form1)this.Owner).crneFigure == true)
            {
                ((Form1)this.Owner).PlocaRefresh();
            }
            if (((Form1)this.Owner).bijeleFigure == true)
            {
                ((Form1)this.Owner).PlocaRefreshRacunaloJeCrni();
            }

            this.Close();
        }
    }
}
