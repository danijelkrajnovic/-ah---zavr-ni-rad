using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Šah___završni_rad
{
    public partial class Form1 : Form
    {
        public Dictionary<string, Dictionary<string, int>> figureDict = new Dictionary<string, Dictionary<string, int>>
        {
            {"b", new Dictionary<string, int>{{"pijun1", 1}, {"pijun2", 2}, {"pijun3", 3}, {"pijun4", 4}, {"pijun5", 5}, {"pijun6", 6}, {"pijun7", 7}, {"pijun8", 8}, {"top1", 9}, {"top2", 10}, {"konj1", 11}, {"konj2", 12}, {"laufer1", 13}, {"laufer2", 14}, {"kraljica", 15}, {"kralj", 16}}},
            {"c", new Dictionary<string, int>{{"pijun1", 101}, {"pijun2", 102}, {"pijun3", 103}, {"pijun4", 104}, {"pijun5", 105}, {"pijun6", 106}, {"pijun7", 107}, {"pijun8", 108}, {"top1", 109}, {"top2", 110}, {"konj1", 111}, {"konj2", 112}, {"laufer1", 113}, {"laufer2", 114}, {"kraljica", 115}, {"kralj", 116}}}
        };
     
        //tablice za evaluacijsku funkciju
        ///////////////////////////////////
        private const int vrijednostPijuna = 100;
        private const int vrijednostKonja = 320;
        private const int vrijednostLaufera = 330; 
        private const int vrijednostTopa = 500; 
        private const int vrijednostKraljice = 900; 
        private const int vrijednostKralja = 20000;


        int[,] pijun_bijeli = new int[8, 8] 
        { 
            {0,  0,  0,  0,  0,  0,  0,  0}, 
            {50, 50, 50, 50, 50, 50, 50, 50}, 
            {10, 10, 20, 30, 30, 20, 10, 10}, 
            {5,  5, 10, 25, 25, 10,  5,  5}, 
            {0,  0,  0, 20, 20,  0,  0,  0}, 
            {5, -5,-10,  0,  0,-10, -5,  5}, 
            {5, 10, 10,-20,-20, 10, 10,  5}, 
            {0,  0,  0,  0,  0,  0,  0,  0}
        };


        int[,] pijun_crni = new int[8, 8] 
        { 
            {0,  0,  0,  0,  0,  0,  0,  0}, 
            {5, 10, 10,-20,-20, 10, 10,  5}, 
            {5, -5,-10,  0,  0,-10, -5,  5}, 
            {0,  0,  0, 20, 20,  0,  0,  0}, 
            {5,  5, 10, 25, 25, 10,  5,  5}, 
            {10, 10, 20, 30, 30, 20, 10, 10},  
            {50, 50, 50, 50, 50, 50, 50, 50},  
            {0,  0,  0,  0,  0,  0,  0,  0} 
        };


        int[,] konj_bijeli = new int[8, 8] 
        {
            {-50,-40,-30,-30,-30,-30,-40,-50},
            {-40,-20,  0,  0,  0,  0,-20,-40},
            {-30,  0, 10, 15, 15, 10,  0,-30},
            {-30,  5, 15, 20, 20, 15,  5,-30},
            {-30,  0, 15, 20, 20, 15,  0,-30},
            {-30,  5, 10, 15, 15, 10,  5,-30},
            {-40,-20,  0,  5,  5,  0,-20,-40},
            {-50,-40,-30,-30,-30,-30,-40,-50}
        };

        int[,] konj_crni = new int[8, 8] 
        {
             {-50,-40,-30,-30,-30,-30,-40,-50}, 
             {-40,-20,  0,  5,  5,  0,-20,-40}, 
             {-30,  5, 10, 15, 15, 10,  5,-30}, 
             {-30,  0, 15, 20, 20, 15,  0,-30}, 
             {-30,  5, 15, 20, 20, 15,  5,-30}, 
             {-30,  0, 10, 15, 15, 10,  0,-30}, 
             {-40,-20,  0,  0,  0,  0,-20,-40}, 
             {-50,-40,-30,-30,-30,-30,-40,-50} 
        };


        int[,] laufer_bijeli = new int[8, 8] 
        {
            {-20,-10,-10,-10,-10,-10,-10,-20},
            {-10,  0,  0,  0,  0,  0,  0,-10},
            {-10,  0,  5, 10, 10,  5,  0,-10},
            {-10,  5,  5, 10, 10,  5,  5,-10},
            {-10,  0, 10, 10, 10, 10,  0,-10},
            {-10, 10, 10, 10, 10, 10, 10,-10},
            {-10,  5,  0,  0,  0,  0,  5,-10},
            {-20,-10,-10,-10,-10,-10,-10,-20}
        };

        
        int[,] laufer_crni = new int[8, 8] 
        {
             {-20,-10,-10,-10,-10,-10,-10,-20}, 
             {-10,  5,  0,  0,  0,  0,  5,-10}, 
             {-10, 10, 10, 10, 10, 10, 10,-10}, 
             {-10,  0, 10, 10, 10, 10,  0,-10}, 
             {-10,  5,  5, 10, 10,  5,  5,-10}, 
             {-10,  0,  5, 10, 10,  5,  0,-10}, 
             {-10,  0,  0,  0,  0,  0,  0,-10}, 
             {-20,-10,-10,-10,-10,-10,-10,-20}
        };



        int[,] top_bijeli = new int[8, 8] 
        {
            {0,  0,  0,  0,  0,  0,  0,  0},
            {5, 10, 10, 10, 10, 10, 10,  5},
            {-5,  0,  0,  0,  0,  0,  0, -5},
            {-5,  0,  0,  0,  0,  0,  0, -5},
            {-5,  0,  0,  0,  0,  0,  0, -5},
            {-5,  0,  0,  0,  0,  0,  0, -5},
            {-5,  0,  0,  0,  0,  0,  0, -5},
            {0,  0,  0,  5,  5,  0,  0,  0}
        };


        int[,] top_crni = new int[8, 8] 
        {
            {0,  0,  0,  5,  5,  0,  0,  0}, 
            {-5,  0,  0,  0,  0,  0,  0, -5}, 
            {-5,  0,  0,  0,  0,  0,  0, -5}, 
            {-5,  0,  0,  0,  0,  0,  0, -5}, 
            {-5,  0,  0,  0,  0,  0,  0, -5}, 
            {-5,  0,  0,  0,  0,  0,  0, -5}, 
            {5, 10, 10, 10, 10, 10, 10,  5}, 
            {0,  0,  0,  0,  0,  0,  0,  0}
        };


        int[,] kraljica_bijeli = new int[8, 8] 
        {
            {-20,-10,-10, -5, -5,-10,-10,-20},
            {-10,  0,  0,  0,  0,  0,  0,-10},
            {-10,  0,  5,  5,  5,  5,  0,-10},
            {-5,  0,  5,  5,  5,  5,  0, -5},
            {0,  0,  5,  5,  5,  5,  0, -5},
            {-10,  5,  5,  5,  5,  5,  0,-10},
            {-10,  0,  5,  0,  0,  0,  0,-10},
            {-20,-10,-10, -5, -5,-10,-10,-20}
        };


        int[,] kraljica_crni = new int[8, 8] 
        {
            {-20,-10,-10, -5, -5,-10,-10,-20}, 
            {-10,  0,  5,  0,  0,  0,  0,-10}, 
            {-10,  5,  5,  5,  5,  5,  0,-10}, 
            {0,  0,  5,  5,  5,  5,  0, -5}, 
            {-5,  0,  5,  5,  5,  5,  0, -5}, 
            {-10,  0,  5,  5,  5,  5,  0,-10}, 
            {-10,  0,  0,  0,  0,  0,  0,-10}, 
            {-20,-10,-10, -5, -5,-10,-10,-20}
        };


        int[,] kralj_bijeli_sredina_igre = new int[8, 8] 
        {
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-30,-40,-40,-50,-50,-40,-40,-30},
            {-20,-30,-30,-40,-40,-30,-30,-20},
            {-10,-20,-20,-20,-20,-20,-20,-10},
            {20, 20,  0,  0,  0,  0, 20, 20},
            {20, 30, 10,  0,  0, 10, 30, 20}
        };


        int[,] kralj_crni_sredina_igre = new int[8, 8] 
        {
            {20, 30, 10,  0,  0, 10, 30, 20}, 
            {20, 20,  0,  0,  0,  0, 20, 20}, 
            {-10,-20,-20,-20,-20,-20,-20,-10}, 
            {-20,-30,-30,-40,-40,-30,-30,-20}, 
            {-30,-40,-40,-50,-50,-40,-40,-30}, 
            {-30,-40,-40,-50,-50,-40,-40,-30}, 
            {-30,-40,-40,-50,-50,-40,-40,-30}, 
            {-30,-40,-40,-50,-50,-40,-40,-30},   

        };


        int[,] kralj_bijeli_kraj_igre = new int[8, 8] 
        {
            {-50,-40,-30,-20,-20,-30,-40,-50},
            {-30,-20,-10,  0,  0,-10,-20,-30},
            {-30,-10, 20, 30, 30, 20,-10,-30},
            {-30,-10, 30, 40, 40, 30,-10,-30},
            {-30,-10, 30, 40, 40, 30,-10,-30},
            {-30,-10, 20, 30, 30, 20,-10,-30},
            {-30,-30,  0,  0,  0,  0,-30,-30},
            {-50,-30,-30,-30,-30,-30,-30,-50}
        };


        int[,] kralj_crni_kraj_igre = new int[8, 8] 
        {
            {-50,-30,-30,-30,-30,-30,-30,-50}, 
            {-30,-30,  0,  0,  0,  0,-30,-30}, 
            {-30,-10, 20, 30, 30, 20,-10,-30}, 
            {-30,-10, 30, 40, 40, 30,-10,-30}, 
            {-30,-10, 30, 40, 40, 30,-10,-30}, 
            {-30,-10, 20, 30, 30, 20,-10,-30}, 
            {-30,-20,-10,  0,  0,-10,-20,-30}, 
            {-50,-40,-30,-20,-20,-30,-40,-50}
        };
        /////////////////////////////////////////////////////

        Cvor root = new Cvor();
        Cvor trenutniCvor = new Cvor();

        public const int brojDjeceKojuImaSvakiCvor = 150;
        bool prviKlik = false;
        int poljeFiguraHolder = 0;
        int dubinaPretrage = 4;
        int prednostSahaPredOstalimPotezima = 0;
        public bool racunaluJeSahMat = false;
        public bool igracuJeSahMat = false;
        bool igracuJeSah = false;
        Panel prviKlikHolder;
        Panel drugiKlikHolder;
        int drugiKlikHolderVrijednost = 0;
        int prviKlikHolderVrijednost = 0;
        Cvor svaMjestaNaKojaSeFiguraMozePomaknuti;
        public bool crneFigure = false;
        public bool bijeleFigure = false;
        bool igracJePrijeBioBijeli = false;
        int tmrPorukaBroj = 0;
        int brojBijelihPijuna = 0;
        int brojBijelihTopova = 0;
        int brojBijelihKonja = 0;
        int brojBijelihLaufera = 0;
        int brojBijelihKraljica = 0;
        int brojCrnihPijuna = 0;
        int brojCrnihTopova = 0;
        int brojCrnihKonja = 0;
        int brojCrnihLaufera = 0;
        int brojCrnihKraljica = 0;
        int tempBrojBijelihPijuna = 0;
        int tempBrojBijelihTopova = 0;
        int tempBrojBijelihKonja = 0;
        int tempBrojBijelihLaufera = 0;
        int tempBrojBijelihKraljica = 0;
        int tempBrojCrnihPijuna = 0;
        int tempBrojCrnihTopova = 0;
        int tempBrojCrnihKonja = 0;
        int tempBrojCrnihLaufera = 0;
        int tempBrojCrnihKraljica = 0;        
        public int brojPotezaIgrac = 0;
        public int brojPotezaRacunalo = 0;
        public DateTime pocetakIgre = new DateTime();
        public DateTime krajIgre = new DateTime();
        bool startJeKliknut = false;
        Color tmpColor = new Color();
        Color[] panelBoje = new Color[2];
        Panel[] tmpPanelBoje = new Panel[2];
        bool zakljucajIgraca = false;
        int[] rezultatiKorijenaThreadova = new int[300];
        int[] popis_sahova_djeca_roota = new int[300];
        int broj_sahova_djeca_roota = 0;


        public Form1()
        {
            InitializeComponent();
        }


        public void varijableNaPocetneVrijednosti()
        {
            prviKlik = false;
            poljeFiguraHolder = 0;
            prednostSahaPredOstalimPotezima = 0;
            txbPrednost.Text = prednostSahaPredOstalimPotezima.ToString();
            racunaluJeSahMat = false;
            igracuJeSahMat = false;
            igracuJeSah = false;
            drugiKlikHolderVrijednost = 0;
            prviKlikHolderVrijednost = 0;
            crneFigure = false;
            bijeleFigure = false;
            tmrPorukaBroj = 0;
            brojBijelihPijuna = 0;
            brojBijelihTopova = 0;
            brojBijelihKonja = 0;
            brojBijelihLaufera = 0;
            brojBijelihKraljica = 0;
            brojCrnihPijuna = 0;
            brojCrnihTopova = 0;
            brojCrnihKonja = 0;
            brojCrnihLaufera = 0;
            brojCrnihKraljica = 0;
            tempBrojBijelihPijuna = 0;
            tempBrojBijelihTopova = 0;
            tempBrojBijelihKonja = 0;
            tempBrojBijelihLaufera = 0;
            tempBrojBijelihKraljica = 0;
            tempBrojCrnihPijuna = 0;
            tempBrojCrnihTopova = 0;
            tempBrojCrnihKonja = 0;
            tempBrojCrnihLaufera = 0;
            tempBrojCrnihKraljica = 0;
            brojPotezaIgrac = 0;
            brojPotezaRacunalo = 0;
            startJeKliknut = false;
            pnlBijeliIgrac.BackColor = Color.Transparent;
            pnlCrniIgrac.BackColor = Color.Transparent;
            lblBrojCrnihPijuna.Text = "8/8";
            lblBrojCrnihTopova.Text = "2/2";
            lblBrojCrnihKonja.Text = "2/2";
            lblBrojCrnihLaufera.Text = "2/2";
            lblBrojCrnihKraljica.Text = "1/1";
            lblBrojBijelihPijuna.Text = "8/8";
            lblBrojBijelihTopova.Text = "2/2";
            lblBrojBijelihKonja.Text = "2/2";
            lblBrojBijelihLaufera.Text = "2/2";
            lblBrojBijelihKraljica.Text = "1/1";
            zakljucajIgraca = false;
            for (int ii = 0; ii < 300; ii++)
            {
                rezultatiKorijenaThreadova[ii] = -123456789;
            }
            broj_sahova_djeca_roota = 0;
            for (int i = 0; i < 300; i++)
            {
                popis_sahova_djeca_roota[i] = 0;
            }
        }


        private int rezultatZaFiguru(Cvor trenutniCvor_e, int prvaFigura, int zadnjaFigura, int vrijednostFigure, int[,] tablicaZaFiguru, char boja)
        {
            int rezultat = 0;

            for (int figura = prvaFigura; figura <= zadnjaFigura; figura++)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        if (trenutniCvor_e.ploca[ii, i] == figura)
                        {
                            if (boja == 'b')
                            {
                                rezultat = rezultat + vrijednostFigure;
                                rezultat = rezultat + tablicaZaFiguru[ii, i];
                            }
                            if (boja == 'c')
                            {
                                rezultat = rezultat - vrijednostFigure;
                            }
                        }
                    }
                }
            }

            return rezultat;
        }


        private int Evaluacijska_funkcija_bijeli(ref Cvor trenutniCvor_e)
        {
            int rezultat = 0;

            //zbroji vrijednost svih bijelih figura
            ///////////////////////////////////////////////////////////////
            //bijeli pijuni 
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["pijun1"], (byte)figureDict["b"]["pijun8"], vrijednostPijuna, pijun_bijeli, 'b');
            //bijeli topovi
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["top1"], (byte)figureDict["b"]["top2"], vrijednostTopa, top_bijeli, 'b');
            //bijeli lauferi
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["laufer1"], (byte)figureDict["b"]["laufer2"], vrijednostLaufera, laufer_bijeli, 'b');
            //bijeli konji
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["konj1"], (byte)figureDict["b"]["konj2"], vrijednostKonja, konj_bijeli, 'b');
            //bijeli kraljica
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["kraljica"], (byte)figureDict["b"]["kraljica"], vrijednostKraljice, kraljica_bijeli, 'b');
            //bijeli kralj
            if (brojBijelihKraljica == 0 && brojCrnihKraljica == 0)
            {
                rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["kralj"], (byte)figureDict["b"]["kralj"], vrijednostKralja, kralj_bijeli_kraj_igre, 'b');
            }
            else
            {
                rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["b"]["kralj"], (byte)figureDict["b"]["kralj"], vrijednostKralja, kralj_bijeli_sredina_igre, 'b');
            }

            //zbroji vrijednost svih crnih figura
            ///////////////////////////////////////////////////////////////
            //crni pijuni 
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["pijun1"], (byte)figureDict["c"]["pijun8"], vrijednostPijuna, pijun_crni, 'c');
            //crni topovi
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["top1"], (byte)figureDict["c"]["top2"], vrijednostTopa, top_crni, 'c');
            //crni lauferi
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["laufer1"], (byte)figureDict["c"]["laufer2"], vrijednostLaufera, laufer_crni, 'c');
            //crni konji
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["konj1"], (byte)figureDict["c"]["konj2"], vrijednostKonja, konj_crni, 'c');
            //crni kraljica
            rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["kraljica"], (byte)figureDict["c"]["kraljica"], vrijednostKraljice, kraljica_crni, 'c');
            //crni kralj
            if (brojBijelihKraljica == 0 && brojCrnihKraljica == 0)
            {
                rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["kralj"], (byte)figureDict["c"]["kralj"], vrijednostKralja, kralj_crni_kraj_igre, 'c');
            }
            else
            {
                rezultat += rezultatZaFiguru(trenutniCvor_e, (byte)figureDict["c"]["kralj"], (byte)figureDict["c"]["kralj"], vrijednostKralja, kralj_crni_sredina_igre, 'c');
            }
            
            return rezultat;
        }


        private string invertBoja(string boja)
        {
            if (boja == "b")
                return "c";
            if (boja == "c")
                return "b";

            return "error";    
        }

        private void pomakniPijuna(byte figura, ref Cvor trenutniCvor_f, string boja)
        {
            int x = 0;
            int y = 0;
            bool pronadjenaFigura = false;

            //i = y
            //ii = x
            //panel = qxy
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {   
                    if(trenutniCvor_f.ploca[ii, i] == figura)
                    {
                        x = ii;
                        y = i;
                        pronadjenaFigura = true;
                    }

                }               
            }

            if (pronadjenaFigura)
            {
                //kopiraj plocu u temp ploca;
                int[,] tmpPloca = new int[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        tmpPloca[ii, i] = trenutniCvor_f.ploca[ii, i];
                    }
                }

                var poteziBijeli = new List<List<int>>()
                {                
                    new List<int>() { 0, -1 },
                    new List<int>() { 1, -1 },
                    new List<int>() { -1, -1 },
                    new List<int>() { 0, -2 }               
                };

                var poteziCrni = new List<List<int>>()
                {                
                    new List<int>() { 0, 1 },
                    new List<int>() { 1, 1 },
                    new List<int>() { -1, 1 },
                    new List<int>() { 0, 2 }               
                };

                var potezi = new List<List<int>>();

                if (boja == "b")
                {
                    foreach (var potez in poteziBijeli)
                    {
                        potezi.Add(potez);
                    }
                }
                else
                {
                    foreach (var potez in poteziCrni)
                    {
                        potezi.Add(potez);
                    }
                }

                int potezCount = 0;

                //potezi            
                foreach (var potez in potezi)
                {
                    potezCount++;

                    bool propusti = false;


                    if ((boja == "b" && y != 0) || (boja == "c" && y != 7))
                    {
                        if ((potezCount == 1) || (potezCount == 2 && x != 7) || (potezCount == 3 && x != 0) || (potezCount == 4 && boja == "b" && y == 6) || (potezCount == 4 && boja == "c" && y == 1))
                        {
                            propusti = true;   
                        }                        
                    }


                    if (propusti)
                    {
                        bool propusti1 = false;
                        bool postaviSahZastavica = false;

                        if (potezCount == 1 || (potezCount == 4 && trenutniCvor_f.ploca[x + potezi[0][0], y + potezi[0][1]] == 0))
                        {                                
                            if (trenutniCvor_f.ploca[x + potez[0], y + potez[1]] == 0 && pronadjenaFigura == true)
                            {
                                propusti1 = true;
                            }
                        }
                        if (potezCount == 2 || potezCount == 3)
                        {
                            if (trenutniCvor_f.ploca[x + potez[0], y + potez[1]] >= figureDict[invertBoja(boja)]["pijun1"] && trenutniCvor_f.ploca[x + potez[0], y + potez[1]] < figureDict[invertBoja(boja)]["kralj"] && pronadjenaFigura == true)
                            {
                                propusti1 = true;
                            }
                            if (trenutniCvor_f.ploca[x + potez[0], y + potez[1]] == figureDict[invertBoja(boja)]["kralj"])
                            {
                                propusti1 = true;
                                postaviSahZastavica = true;
                            }
                        }


                        if (propusti1)
                        {
                            //stavi 0 na trenutno polje
                            trenutniCvor_f.ploca[x, y] = 0;

                            //stavi slijedeceg na 1
                            trenutniCvor_f.ploca[x + potez[0], y + potez[1]] = (byte)figura;

                            //podesi cvoru djete++
                            trenutniCvor_f.slijedeceDjete++;
                            //napravi dijete
                            trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete] = new Cvor();
                            trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].roditelj = trenutniCvor_f;
                            //kopiraj plocu u novi cvor
                            for (int j = 0; j < 8; j++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].ploca[jj, j] = trenutniCvor_f.ploca[jj, j];
                                }
                            }

                            if (postaviSahZastavica)
                            {
                                //postavi zastavicu u čvoru da je šah
                                trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].sah = true;
                                trenutniCvor_f.sah = true;
                            }
                        }

                        //vrati plocu na staro
                        for (int i = 0; i < 8; i++)
                        {
                            for (int ii = 0; ii < 8; ii++)
                            {
                                trenutniCvor_f.ploca[ii, i] = (byte)tmpPloca[ii, i];
                            }
                        }                        
                    }
                }
            }
        }


        private void pomakniKraljicu(ref Cvor trenutniCvor_f, string boja)
        {
            int x = 0;
            int y = 0;
            bool pronadjenaFigura = false;


            //i = y
            //ii = x
            //panel = qxy
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (trenutniCvor_f.ploca[ii, i] == figureDict[boja]["kraljica"])
                    {
                        x = ii;
                        y = i;
                        pronadjenaFigura = true;
                    }                    
                }
            }


            if (pronadjenaFigura)
            {
                //kopiraj plocu u temp ploca;
                int[,] tmpPloca = new int[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        tmpPloca[ii, i] = trenutniCvor_f.ploca[ii, i];
                    }
                }


                //maknuti x i ostaviti samo brojeve/promjene u listi i onda x pisati kroz dolje kod
                var potezi = new List<List<int>>()
            {
                new List<int>() { 0, -1 },
                new List<int>() { 1, -1 },
                new List<int>() { 1, 0 },
                new List<int>() { 1, 1 },
                new List<int>() { 0, 1 },
                new List<int>() { -1, 1 },
                new List<int>() { -1, 0 },
                new List<int>() { -1, -1 }               
            };

                int potezCount = 0;


                //potezi - petlja
                foreach (var potez in potezi)
                {
                    int noviX = x;
                    int noviY = y;
                    
                    bool zastavica = false;

                    potezCount++;


                    if ((potezCount == 1 && y != 0) || (potezCount == 2 && y != 0 && x != 7) || (potezCount == 3 && x != 7) || (potezCount == 4 && y != 7 && x != 7) || (potezCount == 5 && y != 7) || (potezCount == 6 && y != 7 && x != 0) || (potezCount == 7 && x != 0) || (potezCount == 8 && y != 0 && x != 0))
                    {
                        zastavica = true;
                    }

                    if (zastavica)
                    {   
                        bool vrtiPetlju = true;

                        do
                        {
                            bool propusti = false;
                            bool postaviSahZastavica = false;

                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] != 0)
                            {
                                vrtiPetlju = false;

                                if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] >= figureDict[invertBoja(boja)]["pijun1"] && trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] < figureDict[invertBoja(boja)]["kralj"])
                                {
                                    propusti = true;
                                }
                                else if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] == figureDict[invertBoja(boja)]["kralj"])
                                {
                                    propusti = true;
                                    postaviSahZastavica = true;
                                }
                            }
                            else
                            {
                                propusti = true;
                            }


                            if (propusti)
                            {
                                //stavi 0 na proslo polje                            
                                trenutniCvor_f.ploca[noviX, noviY] = 0;

                                noviX = noviX + potez[0];
                                noviY = noviY + potez[1];

                                //napravi potez - stavi 15 na slijedece polje
                                trenutniCvor_f.ploca[noviX, noviY] = (byte)figureDict[boja]["kraljica"];

                                //podesi cvoru djete++
                                trenutniCvor_f.slijedeceDjete++;
                                //napravi dijete
                                Cvor noviCvor = new Cvor();
                                noviCvor.roditelj = trenutniCvor_f;
                                //kopiraj plocu u novi cvor
                                for (int j = 0; j < 8; j++)
                                {
                                    for (int jj = 0; jj < 8; jj++)
                                    {
                                        noviCvor.ploca[jj, j] = trenutniCvor_f.ploca[jj, j];
                                    }
                                }
                                //pridruzi novo djete trenutnom cvoru
                                trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete] = noviCvor;

                                if (postaviSahZastavica)
                                {
                                    //postavi zastavicu za šah
                                    trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].sah = true;
                                    trenutniCvor_f.sah = true;
                                }

                                //provjeri trenutno polje, da figura nije dosla do kraja ploce
                                if ((potezCount == 1 && noviY == 0) || (potezCount == 2 && noviY == 0 || noviX == 7) || (potezCount == 3 && noviX == 7) || (potezCount == 4 && noviY == 7 || noviX == 7) || (potezCount == 5 && noviY == 7) || (potezCount == 6 && noviY == 7 || noviX == 0) || (potezCount == 7 && noviX == 0) || (potezCount == 8 && noviY == 0 || noviX == 0))
                                {
                                    break;
                                }
                            }
                        } while (vrtiPetlju);                                                
                    }

                    //vrati na staro
                    for (int i = 0; i < 8; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            trenutniCvor_f.ploca[ii, i] = (byte)tmpPloca[ii, i];
                        }
                    }
                }
            }
        }


        private void pomakniKralja(ref Cvor trenutniCvor_f, string boja)
        {
            int x = 0;
            int y = 0;
            bool pronadjenaFigura = false;


            //i = y
            //ii = x
            //panel = qxy
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (trenutniCvor_f.ploca[ii, i] == figureDict[boja]["kralj"])
                    {
                        x = ii;
                        y = i;
                        pronadjenaFigura = true;
                    }
                }
            }


            if (pronadjenaFigura)
            {
                //kopiraj plocu u temp ploca;
                int[,] tmpPloca = new int[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        tmpPloca[ii, i] = trenutniCvor_f.ploca[ii, i];
                    }
                }


                var potezi = new List<List<int>>()
                {
                    new List<int>() { 0, -1 },
                    new List<int>() { 1, -1 },
                    new List<int>() { 1, 0 },
                    new List<int>() { 1, 1 },
                    new List<int>() { 0, 1 },
                    new List<int>() { -1, 1 },
                    new List<int>() { -1, 0 },
                    new List<int>() { -1, -1 }               
                };

                int potezCount = 0;

                //potezi - petlja
                foreach (var potez in potezi)
                {
                    int noviX = x;
                    int noviY = y;

                    bool zastavica = false;

                    potezCount++;

                    if ((potezCount == 1 && y != 0) || (potezCount == 2 && y != 0 && x != 7) || (potezCount == 3 && x != 7) || (potezCount == 4 && y != 7 && x != 7) || (potezCount == 5 && y != 7) || (potezCount == 6 && y != 7 && x != 0) || (potezCount == 7 && x != 0) || (potezCount == 8 && y != 0 && x != 0))
                    {
                        zastavica = true;
                    }


                    if (zastavica)
                    {
                        bool propusti = false;
                        bool postaviSahZastavica = false;

                        
                        if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] != 0)
                        {
                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] >= figureDict[invertBoja(boja)]["pijun1"] && trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] < figureDict[invertBoja(boja)]["kralj"] && pronadjenaFigura == true)
                            {
                                propusti = true;
                            }
                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] == figureDict[invertBoja(boja)]["kralj"])
                            {
                                propusti = true;
                                postaviSahZastavica = true;
                            }
                        }
                        else
                        {
                            propusti = true;
                        }

                        if (propusti)
                        {
                            //stavi 0 na proslo polje                            
                            trenutniCvor_f.ploca[noviX, noviY] = 0;

                            noviX = noviX + potez[0];
                            noviY = noviY + potez[1];

                            //napravi potez - stavi 15 na slijedece polje
                            trenutniCvor_f.ploca[noviX, noviY] = (byte)figureDict[boja]["kralj"];

                            //podesi cvoru djete++
                            trenutniCvor_f.slijedeceDjete++;
                            //napravi dijete
                            Cvor noviCvor = new Cvor();
                            noviCvor.roditelj = trenutniCvor_f;
                            //kopiraj plocu u novi cvor
                            for (int j = 0; j < 8; j++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    noviCvor.ploca[jj, j] = trenutniCvor_f.ploca[jj, j];
                                }
                            }
                            //pridruzi novo djete trenutnom cvoru
                            trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete] = noviCvor;

                            if (postaviSahZastavica)
                            {
                                //postavi zastavicu za šah
                                trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].sah = true;
                                trenutniCvor_f.sah = true;
                            }
                        }                                                
                    }

                    //vrati na staro
                    for (int i = 0; i < 8; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            trenutniCvor_f.ploca[ii, i] = (byte)tmpPloca[ii, i];
                        }
                    }
                }
            }
        }


        private void pomakniTop(byte figura, ref Cvor trenutniCvor_f, string boja)
        {
            int x = 0;
            int y = 0;
            bool pronadjenaFigura = false;


            //i = y
            //ii = x
            //panel = qxy
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (trenutniCvor_f.ploca[ii, i] == figura)
                    {
                        x = ii;
                        y = i;
                        pronadjenaFigura = true;
                    }

                }
            }

            if (pronadjenaFigura)
            {
                //kopiraj plocu u temp ploca;
                int[,] tmpPloca = new int[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        tmpPloca[ii, i] = trenutniCvor_f.ploca[ii, i];
                    }
                }


                var potezi = new List<List<int>>()
                {
                    new List<int>() { 0, -1 },
                    new List<int>() { 1, 0 },
                    new List<int>() { 0, 1 },
                    new List<int>() { -1, 0 }             
                };

                int potezCount = 0;


                //potezi - petlja
                foreach (var potez in potezi)
                {
                    int noviX = x;
                    int noviY = y;

                    potezCount++;

                    bool zastavica = false;

                    if ((potezCount == 1 && y != 0) || (potezCount == 2 && x != 7) || (potezCount == 3 && y != 7) || (potezCount == 4 && x != 0))
                    {
                        zastavica = true;
                    }

                    if (zastavica)
                    {
                        bool vrtiPetlju = true;

                        do
                        {
                            bool propusti = false;
                            bool postaviSahZastavica = false;

                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] != 0)
                            {
                                vrtiPetlju = false;

                                if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] >= figureDict[invertBoja(boja)]["pijun1"] && trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] < figureDict[invertBoja(boja)]["kralj"])
                                {
                                    propusti = true;
                                }
                                if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] == figureDict[invertBoja(boja)]["kralj"])
                                {
                                    propusti = true;
                                    postaviSahZastavica = true;
                                }
                            }
                            else
                            {
                                propusti = true;
                            }

                            if (propusti)
                            {
                                //stavi 0 na proslo polje                            
                                trenutniCvor_f.ploca[noviX, noviY] = 0;

                                noviX = noviX + potez[0];
                                noviY = noviY + potez[1];

                                //napravi potezCount - stavi 15 na slijedece polje
                                trenutniCvor_f.ploca[noviX, noviY] = figura;

                                //podesi cvoru djete++
                                trenutniCvor_f.slijedeceDjete++;
                                //napravi dijete
                                Cvor noviCvor = new Cvor();
                                noviCvor.roditelj = trenutniCvor_f;
                                //kopiraj plocu u novi cvor
                                for (int j = 0; j < 8; j++)
                                {
                                    for (int jj = 0; jj < 8; jj++)
                                    {
                                        noviCvor.ploca[jj, j] = trenutniCvor_f.ploca[jj, j];
                                    }
                                }
                                //pridruzi novo djete trenutnom cvoru
                                trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete] = noviCvor;

                                if (postaviSahZastavica)
                                {
                                    //postavi zastavicu za šah
                                    trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].sah = true;
                                    trenutniCvor_f.sah = true;
                                }

                                //provjeri trenutno polje, da figura nije dosla do kraja ploce
                                if ((potezCount == 1 && noviY == 0) || (potezCount == 2 && noviX == 7) || (potezCount == 3 && noviY == 7) || (potezCount == 4 && noviX == 0))
                                {
                                    break;
                                }
                            }
                        } while (vrtiPetlju);                                                
                    }


                    //vrati na staro
                    for (int i = 0; i < 8; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            trenutniCvor_f.ploca[ii, i] = (byte)tmpPloca[ii, i];
                        }
                    }                    
                }
            }
        }


        private void pomakniLaufera(byte figura, ref Cvor trenutniCvor_f, string boja)
        {
            int x = 0;
            int y = 0;
            bool pronadjenaFigura = false;

            
            //i = y
            //ii = x
            //panel = qxy
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (trenutniCvor_f.ploca[ii, i] == figura)
                    {
                        x = ii;
                        y = i;
                        pronadjenaFigura = true;
                    }

                }
            }

            if (pronadjenaFigura)
            {
                //kopiraj plocu u temp ploca;
                int[,] tmpPloca = new int[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        tmpPloca[ii, i] = trenutniCvor_f.ploca[ii, i];
                    }
                }



                var potezi = new List<List<int>>()
                {
                    new List<int>() { 1, -1 },
                    new List<int>() { 1, 1 },
                    new List<int>() { -1, 1 },
                    new List<int>() { -1, -1 }               
                };

                int potezCount = 0;


                //potezCounti - petlja
                foreach (var potez in potezi)
                {
                    int noviX = x;
                    int noviY = y;

                    potezCount++;

                    bool zastavica = false;

                    if ((potezCount == 1 && y != 0 && x != 7) || (potezCount == 2 && y != 7 && x != 7) || (potezCount == 3 && y != 7 && x != 0) || (potezCount == 4 && y != 0 && x != 0))
                    {
                        zastavica = true;
                    }


                    if (zastavica)
                    {
                        bool vrtiPetlju = true;

                        do
                        {
                            bool propusti = false;
                            bool postaviSahZastavica = false;

                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] != 0)
                            {
                                vrtiPetlju = false;

                                if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] >= figureDict[invertBoja(boja)]["pijun1"] && trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] < figureDict[invertBoja(boja)]["kralj"])
                                {
                                    propusti = true;
                                }
                                if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] == figureDict[invertBoja(boja)]["kralj"])
                                {
                                    propusti = true;
                                    postaviSahZastavica = true;
                                }
                            }
                            else
                            {
                                propusti = true;
                            }

                            if (propusti)
                            {
                                //stavi 0 na proslo polje                            
                                trenutniCvor_f.ploca[noviX, noviY] = 0;

                                noviX = noviX + potez[0];
                                noviY = noviY + potez[1];

                                //napravi potezCount - stavi 15 na slijedece polje
                                trenutniCvor_f.ploca[noviX, noviY] = figura;

                                //podesi cvoru djete++
                                trenutniCvor_f.slijedeceDjete++;
                                //napravi dijete
                                Cvor noviCvor = new Cvor();
                                noviCvor.roditelj = trenutniCvor_f;
                                //kopiraj plocu u novi cvor
                                for (int j = 0; j < 8; j++)
                                {
                                    for (int jj = 0; jj < 8; jj++)
                                    {
                                        noviCvor.ploca[jj, j] = trenutniCvor_f.ploca[jj, j];
                                    }
                                }
                                //pridruzi novo djete trenutnom cvoru
                                trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete] = noviCvor;

                                if (postaviSahZastavica)
                                {
                                    //postavi zastavicu za šah
                                    trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].sah = true;
                                    trenutniCvor_f.sah = true;
                                }

                                //provjeri trenutno polje, da figura nije dosla do kraja ploce                                    
                                if ((potezCount == 1 && noviY == 0 || noviX == 7) || (potezCount == 2 && noviY == 7 || noviX == 7) || (potezCount == 3 && noviY == 7 || noviX == 0) || (potezCount == 4 && noviY == 0 || noviX == 0))
                                {
                                    break;
                                }                                    
                            }
                        } while (vrtiPetlju);                        
                    }

                    //vrati na staro
                    for (int i = 0; i < 8; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            trenutniCvor_f.ploca[ii, i] = (byte)tmpPloca[ii, i];
                        }
                    }
                }
            }
        }


        private void pomakniKonja(byte figura, ref Cvor trenutniCvor_f, string boja)
        {
            int x = 0;
            int y = 0;
            bool pronadjenaFigura = false;

            
            //i = y
            //ii = x
            //panel = qxy
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (trenutniCvor_f.ploca[ii, i] == figura)
                    {
                        x = ii;
                        y = i;
                        pronadjenaFigura = true;
                    }

                }
            }

            if (pronadjenaFigura)
            {
                //kopiraj plocu u temp ploca;
                int[,] tmpPloca = new int[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        tmpPloca[ii, i] = trenutniCvor_f.ploca[ii, i];
                    }
                }


                var potezi = new List<List<int>>()
                {
                    new List<int>() { 1, -2 },
                    new List<int>() { 2, -1 },
                    new List<int>() { 2, 1 },
                    new List<int>() { 1, 2 },
                    new List<int>() { -1, 2 },
                    new List<int>() { -2, 1 },
                    new List<int>() { -2, -1 },
                    new List<int>() { -1, -2 }
                };


                int potezCount = 0;

                //potezi - petlja
                foreach (var potez in potezi)
                {
                    int noviX = x;
                    int noviY = y;

                    potezCount++;

                    bool zastavica = false;

                    if ((potezCount == 1 && y > 1 && x != 7) || (potezCount == 2 && y != 0 && x < 6) || (potezCount == 3 && y != 7 && x < 6) || (potezCount == 4 && y < 6 && x != 7) || (potezCount == 5 && y < 6 && x != 0) || (potezCount == 6 && y != 7 && x > 1) || (potezCount == 7 && y != 0 && x > 1) || (potezCount == 8 && y > 1 && x != 0))
                    {
                        zastavica = true;
                    }

                    if (zastavica)
                    {
                        bool propusti = false;
                        bool postaviSahZastavica = false;
                                                
                        if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] != 0)
                        {
                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] >= figureDict[invertBoja(boja)]["pijun1"] && trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] < figureDict[invertBoja(boja)]["kralj"] && pronadjenaFigura == true)
                            {
                                propusti = true;
                            }
                            if (trenutniCvor_f.ploca[noviX + potez[0], noviY + potez[1]] == figureDict[invertBoja(boja)]["kralj"])
                            {
                                propusti = true;
                                postaviSahZastavica = true;
                            }
                        }
                        else
                        {
                            propusti = true;
                        }

                        if (propusti)
                        {
                            //stavi 0 na proslo polje                            
                            trenutniCvor_f.ploca[noviX, noviY] = 0;

                            noviX = noviX + potez[0];
                            noviY = noviY + potez[1];

                            //stavi 15 na slijedece polje
                            trenutniCvor_f.ploca[noviX, noviY] = figura;

                            //podesi cvoru djete++
                            trenutniCvor_f.slijedeceDjete++;
                            //napravi dijete
                            Cvor noviCvor = new Cvor();
                            noviCvor.roditelj = trenutniCvor_f;
                            //kopiraj plocu u novi cvor
                            for (int j = 0; j < 8; j++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    noviCvor.ploca[jj, j] = trenutniCvor_f.ploca[jj, j];
                                }
                            }
                            //pridruzi novo djete trenutnom cvoru
                            trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete] = noviCvor;

                            if (postaviSahZastavica)
                            {
                                //postavi zastavicu za šah
                                trenutniCvor_f.djeca[trenutniCvor_f.slijedeceDjete].sah = true;
                                trenutniCvor_f.sah = true;
                            }
                        }                                                
                    }

                    //vrati na staro
                    for (int i = 0; i < 8; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            trenutniCvor_f.ploca[ii, i] = (byte)tmpPloca[ii, i];
                        }
                    }
                }
            }
        }


        private void povuciBijeleFigure(ref Cvor trenutniCvor2)
        {
            //bijeli je na redu:            
            pomakniPijuna((byte)figureDict["b"]["pijun1"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun2"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun3"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun4"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun5"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun6"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun7"], ref trenutniCvor2, "b");
            pomakniPijuna((byte)figureDict["b"]["pijun8"], ref trenutniCvor2, "b");
            pomakniTop((byte)figureDict["b"]["top1"], ref trenutniCvor2, "b");
            pomakniTop((byte)figureDict["b"]["top2"], ref trenutniCvor2, "b");
            pomakniKonja((byte)figureDict["b"]["konj1"], ref trenutniCvor2, "b");
            pomakniKonja((byte)figureDict["b"]["konj2"], ref trenutniCvor2, "b");
            pomakniLaufera((byte)figureDict["b"]["laufer1"], ref trenutniCvor2, "b");
            pomakniLaufera((byte)figureDict["b"]["laufer2"], ref trenutniCvor2, "b");
            pomakniKraljicu(ref trenutniCvor2, "b");
            pomakniKralja(ref trenutniCvor2, "b");
        }

        private void povuciCrneFigure(ref Cvor trenutniCvor2)
        {
            //crni je na redu:
            pomakniPijuna((byte)figureDict["c"]["pijun1"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun2"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun3"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun4"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun5"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun6"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun7"], ref trenutniCvor2, "c");
            pomakniPijuna((byte)figureDict["c"]["pijun8"], ref trenutniCvor2, "c");
            pomakniTop((byte)figureDict["c"]["top1"], ref trenutniCvor2, "c");
            pomakniTop((byte)figureDict["c"]["top2"], ref trenutniCvor2, "c");
            pomakniKonja((byte)figureDict["c"]["konj1"], ref trenutniCvor2, "c");
            pomakniKonja((byte)figureDict["c"]["konj2"], ref trenutniCvor2, "c");
            pomakniLaufera((byte)figureDict["c"]["laufer1"], ref trenutniCvor2, "c");
            pomakniLaufera((byte)figureDict["c"]["laufer2"], ref trenutniCvor2, "c");
            pomakniKraljicu(ref trenutniCvor2, "c");
            pomakniKralja(ref trenutniCvor2, "c");
        }
        
        private void napraviGranu(int dubina1, ref Cvor trenutniCvor1, int mjestoUPolju)
        {
            //inorder obilazak stabla sa minimax i alpha-beta i gradi stablo u isto vrijeme
            Cvor root1 = new Cvor();
            Cvor trenutniCvor2 = new Cvor();
            trenutniCvor2 = root1;
            for (int ii = 0; ii < 8; ii++)
            {
                for (int iii = 0; iii < 8; iii++)
                {
                    root1.ploca[iii, ii] = trenutniCvor1.ploca[iii, ii];
                }
            }
            //MessageBox.Show(root1.ploca[2, 2].ToString(), "", MessageBoxButtons.OK);
            bool vrti_petlju = true;
            int dubina_pretrage = 1;
            bool alpha_beta_podrezi = false;


            while (vrti_petlju == true)
            {
                if (alpha_beta_podrezi == false)
                {
                    while (dubina_pretrage < dubina1)
                    {
                        if (dubina_pretrage < dubina1 && trenutniCvor2.djeca[0] == null)
                        {
                            if (dubina_pretrage % 2 == 0)
                            {
                                //bijeli je na redu:
                                povuciBijeleFigure(ref trenutniCvor2);
                            }
                            else
                            {                                
                                //crni je na redu:
                                povuciCrneFigure(ref trenutniCvor2);
                            }
                        }

                        if (trenutniCvor2.djeca[trenutniCvor2.slijedeciCvorInorderObilazak] == null)
                        {
                            break;
                        }
                        if (trenutniCvor2.sah == true)
                        {
                            break;
                        }

                        if (trenutniCvor2.sah == false)
                        {
                            trenutniCvor2 = trenutniCvor2.djeca[trenutniCvor2.slijedeciCvorInorderObilazak];
                            dubina_pretrage++;
                            trenutniCvor2.roditelj.slijedeciCvorInorderObilazak++;
                            trenutniCvor2.alpha = trenutniCvor2.roditelj.alpha;
                            trenutniCvor2.beta = trenutniCvor2.roditelj.beta;
                        }

                    }

                    //evaluiraj
                    if (trenutniCvor2.djeca[0] == null || trenutniCvor2.sah == true)
                    {
                        trenutniCvor2.podatak = Evaluacijska_funkcija_bijeli(ref trenutniCvor2);
                        //kada je racunalo bijeli
                        if (trenutniCvor2.sah == true)
                        {
                            if (dubina_pretrage % 2 == 0)
                            {
                                if (dubina_pretrage == 2)
                                {
                                    bool tmpJeLiKraljPomaknut = daLiSeKraljPomako(trenutniCvor2.roditelj, trenutniCvor2);

                                    if (tmpJeLiKraljPomaknut == false)
                                    {
                                        //oznaci u kojim potezima djece roota racunalo moze dati sah
                                        popis_sahova_djeca_roota[mjestoUPolju] = 1;
                                        broj_sahova_djeca_roota++;
                                    }
                                }
                            }
                            else
                            {
                                //u min cvoru sam
                                //ja(bijeli) sam pod šahom
                                //ovako bijeli medju ostalim izbjegava šah
                                trenutniCvor2.podatak = trenutniCvor2.podatak - vrijednostKralja;
                            }
                        }
                    }
                    //dodijeli evaluiranu vrijednost alphi ili beti tog cvora
                    if (dubina_pretrage % 2 == 0)
                    {
                        //trenutni cvor je alpha
                        trenutniCvor2.alpha = trenutniCvor2.podatak;
                    }
                    else
                    {
                        //trenutni cvor je beta
                        trenutniCvor2.beta = trenutniCvor2.podatak;
                    }
                }
                alpha_beta_podrezi = false;

                //prenesi vrijednost na gornji cvor
                if (trenutniCvor2.roditelj.slijedeciCvorInorderObilazak == 1)
                {
                    //prvi povratak na taj cvor -> samo prenesi vrijednost
                    trenutniCvor2.roditelj.podatak = trenutniCvor2.podatak;
                }
                else
                {
                    //vec sam bio na tom cvoru -> usporedi vrijednosti -> minimax
                    if ((dubina_pretrage - 1) % 2 == 0)
                    {
                        //max cvor
                        if (trenutniCvor2.roditelj.podatak < trenutniCvor2.podatak)
                        {
                            trenutniCvor2.roditelj.podatak = trenutniCvor2.podatak;
                        }
                    }
                    else
                    {
                        //min cvor
                        if (trenutniCvor2.roditelj.podatak > trenutniCvor2.podatak)
                        {
                            trenutniCvor2.roditelj.podatak = trenutniCvor2.podatak;
                        }

                    }

                }

                //otidji na gornji cvor
                //ali prvo provjeri i prenesi alpha betu
                if (trenutniCvor2.roditelj.slijedeciCvorInorderObilazak == 1)
                {
                    //prvi put se vracam na taj cvor pa samo prenesi
                    if ((dubina_pretrage - 1) % 2 == 0)
                    {
                        //alpha
                        //roditelj na kojeg vracamo je alpha
                        trenutniCvor2.roditelj.alpha = trenutniCvor2.beta;
                    }
                    else
                    {
                        //beta
                        //roditelj na kojeg vracamo je beta
                        trenutniCvor2.roditelj.beta = trenutniCvor2.alpha;
                    }
                }
                else
                {
                    if ((dubina_pretrage - 1) % 2 == 0)
                    {
                        //alpha
                        //roditelj na kojeg vracamo je alpha
                        if (trenutniCvor2.roditelj.alpha < trenutniCvor2.beta)
                        {
                            trenutniCvor2.roditelj.alpha = trenutniCvor2.beta;
                        }
                    }
                    else
                    {
                        //beta
                        //roditelj na kojeg vracamo je beta
                        if (trenutniCvor2.roditelj.beta > trenutniCvor2.alpha)
                        {
                            trenutniCvor2.roditelj.beta = trenutniCvor2.alpha;
                        }

                    }
                }

                trenutniCvor2 = trenutniCvor2.roditelj;
                dubina_pretrage--;

                //provjeri da li treba podrezati granu sa alpha beta
                if (trenutniCvor2 != root1)
                {
                    if (dubina_pretrage % 2 == 0)
                    {
                        //trenutno sam na alpha cvoru
                        if (trenutniCvor2.alpha > trenutniCvor2.roditelj.beta)
                        {
                            alpha_beta_podrezi = true;
                        }
                    }
                    else
                    {
                        //trenutno sam na beta cvoru
                        if (trenutniCvor2.beta < trenutniCvor2.roditelj.alpha)
                        {
                            alpha_beta_podrezi = true;
                        }

                    }
                }


                if (trenutniCvor2 == root1)
                {
                    if (trenutniCvor2.djeca[trenutniCvor2.slijedeciCvorInorderObilazak] != null)
                    {
                        vrti_petlju = true;
                    }
                    else
                    {
                        vrti_petlju = false;
                    }
                }
            }

            rezultatiKorijenaThreadova[mjestoUPolju] = root1.podatak;


            //unisti stablo
            int tmpObrisiStablo = 0;
            while (root1.djeca[tmpObrisiStablo] != null)
            {
                root1.djeca[tmpObrisiStablo] = null;
            }
        }

        public void praznaMetoda()
        {

        }

        public void napraviStablo(int dubina)
        {
            zakljucajIgraca = true;

            if (crneFigure == true)
            {
                pnlBijeliIgrac.BackColor = Color.LightGreen;
                pnlCrniIgrac.BackColor = Color.Red;
                pnlCrniIgrac.Refresh();
                pnlBijeliIgrac.Refresh();
            }
            else
            {
                pnlCrniIgrac.BackColor = Color.LightGreen;
                pnlBijeliIgrac.BackColor = Color.Red;
                pnlCrniIgrac.Refresh();
                pnlBijeliIgrac.Refresh();
            }

            trenutniCvor = root;

            //kopiraj plocu u trenutni cvor - u ovome slucaju root
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                    trenutniCvor.ploca[ii, i] = byte.Parse(tmpPanel.Tag.ToString());
                }
            }

            //inorder obilazak stabla sa minimax i alpha-beta i gradi stablo u isto vrijeme
            trenutniCvor = root;
            bool vrti_petlju = true;
            int dubina_pretrage = 0;
            bool alpha_beta_podrezi = false;
            for (int i = 0; i < 300; i++)
            {
                popis_sahova_djeca_roota[i] = 0;
            }
            broj_sahova_djeca_roota = 0;            

            while (vrti_petlju == true)
            {
                if (alpha_beta_podrezi == false)
                {
                    while (dubina_pretrage < 2)
                    {
                        if (dubina_pretrage < 2 && trenutniCvor.djeca[0] == null)
                        {
                            if (dubina_pretrage % 2 == 0)
                            {
                                //bijeli je na redu:
                                povuciBijeleFigure(ref trenutniCvor);
                            }
                            else
                            {
                                //crni je na redu:
                                povuciCrneFigure(ref trenutniCvor);
                            }
                        }

                        if (trenutniCvor.djeca[trenutniCvor.slijedeciCvorInorderObilazak] == null)
                        {
                            break;
                        }
                        if (trenutniCvor.sah == true)
                        {
                            break;
                        }

                        trenutniCvor = trenutniCvor.djeca[trenutniCvor.slijedeciCvorInorderObilazak];
                        dubina_pretrage++;
                        trenutniCvor.roditelj.slijedeciCvorInorderObilazak++;
                        trenutniCvor.alpha = trenutniCvor.roditelj.alpha;
                        trenutniCvor.beta = trenutniCvor.roditelj.beta;
                        
                    }

                    //evaluiraj
                    if (trenutniCvor.djeca[0] == null || trenutniCvor.sah == true)
                    {
                        trenutniCvor.podatak = Evaluacijska_funkcija_bijeli(ref trenutniCvor);
                        //kada je racunalo bijeli
                        if (trenutniCvor.sah == true)
                        {
                            if (dubina_pretrage % 2 == 0)
                            {
                                if (dubina_pretrage == 2)
                                {
                                    bool tmpJeLiKraljPomaknut = daLiSeKraljPomako(trenutniCvor.roditelj, trenutniCvor);

                                    if (tmpJeLiKraljPomaknut == false)
                                    {
                                        //popis_sahova_djeca_roota[broj_sahova_djeca_roota] = trenutniCvor.roditelj.roditelj.slijedeciCvorInorderObilazak - 1;
                                        //broj_sahova_djeca_roota++;
                                    }
                                }
                            }
                            else
                            {
                                //u min cvoru sam
                                //ja(bijeli) sam pod šahom
                                //ovako bijeli izbjegava šah
                                trenutniCvor.podatak = trenutniCvor.podatak - vrijednostKralja;
                            }
                        }
                    }
                    //dodijeli evaluiranu vrijednost alphi ili beti tog cvora
                    if (dubina_pretrage % 2 == 0)
                    {
                        //trenutni cvor je alpha
                        trenutniCvor.alpha = trenutniCvor.podatak;
                    }
                    else
                    {
                        //trenutni cvor je beta
                        trenutniCvor.beta = trenutniCvor.podatak;
                    }
                }
                alpha_beta_podrezi = false;

                //prenesi vrijednost na gornji cvor
                if (trenutniCvor.roditelj.slijedeciCvorInorderObilazak == 1)
                {
                    //prvi povratak na taj cvor -> samo prenesi vrijednost
                    trenutniCvor.roditelj.podatak = trenutniCvor.podatak;
                }
                else
                {
                    //vec sam bio na tom cvoru -> usporedi vrijednosti -> minimax
                    if ((dubina_pretrage - 1) % 2 == 0)
                    {
                        //max cvor
                        if (trenutniCvor.roditelj.podatak < trenutniCvor.podatak)
                        {
                            trenutniCvor.roditelj.podatak = trenutniCvor.podatak;
                        }
                    }
                    else
                    {
                        //min cvor
                        if (trenutniCvor.roditelj.podatak > trenutniCvor.podatak)
                        {
                            trenutniCvor.roditelj.podatak = trenutniCvor.podatak;
                        }

                    }

                }

                //otidji na gornji cvor
                //ali prvo provjeri i prenesi alpha betu
                if (trenutniCvor.roditelj.slijedeciCvorInorderObilazak == 1)
                {
                    //prvi put se vracam na taj cvor pa samo prenesi
                    if ((dubina_pretrage - 1) % 2 == 0)
                    {
                        //alpha
                        //roditelj na kojeg vracamo je alpha
                        trenutniCvor.roditelj.alpha = trenutniCvor.beta;
                    }
                    else
                    {
                        //beta
                        //roditelj na kojeg vracamo je beta
                        trenutniCvor.roditelj.beta = trenutniCvor.alpha;
                    }
                }
                else
                {
                    if ((dubina_pretrage - 1) % 2 == 0)
                    {
                        //alpha
                        //roditelj na kojeg vracamo je alpha
                        if (trenutniCvor.roditelj.alpha < trenutniCvor.beta)
                        {
                            trenutniCvor.roditelj.alpha = trenutniCvor.beta;
                        }
                    }
                    else
                    {
                        //beta
                        //roditelj na kojeg vracamo je beta
                        if (trenutniCvor.roditelj.beta > trenutniCvor.alpha)
                        {
                            trenutniCvor.roditelj.beta = trenutniCvor.alpha;
                        }

                    }
                }

                trenutniCvor = trenutniCvor.roditelj;
                dubina_pretrage--;

                //provjeri da li treba podrezati granu sa alpha beta
                if (trenutniCvor != root)
                {
                    if (dubina_pretrage % 2 == 0)
                    {
                        //trenutno sam na alpha cvoru
                        if (trenutniCvor.alpha > trenutniCvor.roditelj.beta)
                        {
                            alpha_beta_podrezi = true;
                        }
                    }
                    else
                    {
                        //trenutno sam na beta cvoru
                        if (trenutniCvor.beta < trenutniCvor.roditelj.alpha)
                        {
                            alpha_beta_podrezi = true;
                        }

                    }
                }


                if (trenutniCvor == root)
                {
                    if (trenutniCvor.djeca[trenutniCvor.slijedeciCvorInorderObilazak] != null)
                    {
                        vrti_petlju = true;
                    }
                    else
                    {
                        vrti_petlju = false;
                    }
                }
            }

            //provjeri da li sva djeca roota imaju svoju djecu
            //minimalno potrebno da se vidi da li je racunalu sah - dakle min do 2. dubine
            int tmpProvjera = 0;
            bool postojiGreska = false;
            while (root.djeca[tmpProvjera] != null)
            {
                if (root.djeca[tmpProvjera].djeca[0] == null)
                {
                    postojiGreska = true;
                }
                tmpProvjera++;
            }
            if (postojiGreska == true)
            {
                MessageBox.Show("Greška u programu -> ne postoje djeca na 2. dubini -> računalo ne može provjeriti da li je pod šahom!", "Greška", MessageBoxButtons.OK);
            }


            //provjeri da li je šah mat tako da vidiš jesi li sva djeca roota pod šahom
            //ako jesu računalo je izgubilo odnosno računalu je šah mat
            ///////////////////////////////////////////////////////////////////////////
            racunaluJeSahMat = true;
            bool sahMatRacunaluPaPreskociZadnjiDio = true;
            int count_djeca = 0;
            trenutniCvor = root;
            bool prviKojiNijeSah = false;
            int prvoDjeteKojeNijePodSahom = 0;
            while (trenutniCvor.djeca[count_djeca] != null)
            {
                if (trenutniCvor.djeca[count_djeca].sah == false)
                {
                    if (prviKojiNijeSah == false)
                    {
                        prvoDjeteKojeNijePodSahom = count_djeca;
                        prviKojiNijeSah = true;
                    }
                    racunaluJeSahMat = false;
                    sahMatRacunaluPaPreskociZadnjiDio = false;
                }

                count_djeca++;
            }

            if (racunaluJeSahMat == true)
            {
                lblPoruka.Text = "Šah-mat, računalo je izgubilo";
                tmrPoruka.Start();
                Form3 zavrsniMeni = new Form3();
                zavrsniMeni.Owner = this;
                zavrsniMeni.ShowDialog();
                racunaluJeSahMat = false;
            }
            ///////////////////////////////////////////////////////////////////////////

            //
            //
            //
            //
            //

            //pogledaj koliko threadova je potrebno
            int brojThreadova = 0;
            while (root.djeca[brojThreadova] != null)
            {
                brojThreadova++;
            }
            int threadDjeca = brojThreadova;
            Thread[] tha = new Thread[brojThreadova];

            //pokreni threadove
            trenutniCvor = root;
            for (int i = 0; i < brojThreadova; i++)
            {
                if (root.djeca[i].sah == false)
                {
                    tha[i] = new Thread(() => napraviGranu(dubina, ref root.djeca[i], i));
                    tha[i].Start();
                    Thread.Sleep(100);
                }
                else
                {
                    tha[i] = new Thread(praznaMetoda);
                    tha[i].Start();
                }                
            }

            for (int i = 0; i < brojThreadova; i++)
            {
                tha[i].Join();
            }

                //
                //
                //
                //
                //


                //kopiraj potez iz cvora na plocu
                if (sahMatRacunaluPaPreskociZadnjiDio == false)
                {
                    if (broj_sahova_djeca_roota == 0)
                    {
                        
                        //prodji po polju rezultata
                        //pronadji najvecu vrijednost
                        //i zapisi redni broj rezultata u polju jer je to isto i redni
                        //broj cvora sa tim rezultatom
                        int cvorSaNajvecomVrijednoscu = 0;
                        int najvecaVrijednost = -123456789;
                        for (int i = 0; i < threadDjeca; i++)
                        {
                            if (rezultatiKorijenaThreadova[i] > najvecaVrijednost)
                            {
                                najvecaVrijednost = rezultatiKorijenaThreadova[i];
                                cvorSaNajvecomVrijednoscu = i;
                            }
                        }
                        for (int ii = 0; ii < 300; ii++)
                        {
                            rezultatiKorijenaThreadova[ii] = -123456789;
                        }

                        //pronadji sa kojeg i na koje mjesto je racunalo pomaklo figuru
                        tmpPanelBoje = odrediKojuFiguruJeRacunaloPomaklo(root.djeca[cvorSaNajvecomVrijednoscu]);


                        for (int i = 0; i < 8; i++)
                        {
                            for (int ii = 0; ii < 8; ii++)
                            {
                                string tmpNamePolje = "";
                                tmpNamePolje = "q" + ii.ToString() + i.ToString();

                                Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                                tmpPanel.Tag = root.djeca[cvorSaNajvecomVrijednoscu].ploca[ii, i].ToString();
                            }
                        }
                    }
                    else
                    {                        
                        //dodaj nagrađujuće bodove za šah i šahmat
                        for (int i = 0; i < 300; i++)
                        {
                            if (popis_sahova_djeca_roota[i] != 0)
                            {
                                //pridodaj bodove za sah pa da taj potez bude vise primamljiv
                                rezultatiKorijenaThreadova[i] += prednostSahaPredOstalimPotezima;

                                //provjeri ako je šah mat i pridodaj bodove ako je
                                funkcijaZaProvjeruSahMata("cvor", root.djeca[i]);
                                if (igracuJeSahMat == true)
                                {
                                    igracuJeSahMat = false;
                                    rezultatiKorijenaThreadova[i] += 10000000;
                                }
                            }
                        }

                        //prodji po polju rezultata
                        //pronadji najvecu vrijednost
                        //i zapisi redni broj rezultata u polju jer je to isto i redni
                        //broj cvora sa tim rezultatom
                        int cvorSaNajvecomVrijednoscu = 0;
                        int najvecaVrijednost = -123456789;
                        for (int i = 0; i < threadDjeca; i++)
                        {
                            if (rezultatiKorijenaThreadova[i] > najvecaVrijednost)
                            {
                                najvecaVrijednost = rezultatiKorijenaThreadova[i];
                                cvorSaNajvecomVrijednoscu = i;
                            }
                        }
                        for (int ii = 0; ii < 300; ii++)
                        {
                            rezultatiKorijenaThreadova[ii] = -123456789;
                        }

                        //pronadji sa kojeg i na koje mjesto je racunalo pomaklo figuru
                        tmpPanelBoje = odrediKojuFiguruJeRacunaloPomaklo(root.djeca[cvorSaNajvecomVrijednoscu]);


                        for (int i = 0; i < 8; i++)
                        {
                            for (int ii = 0; ii < 8; ii++)
                            {
                                string tmpNamePolje = "";
                                tmpNamePolje = "q" + ii.ToString() + i.ToString();

                                Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                                tmpPanel.Tag = root.djeca[cvorSaNajvecomVrijednoscu].ploca[ii, i].ToString();
                            }
                        }
                    }

                    //promocija pijuna
                    {
                        for (int pr = 0; pr < 8; pr++)
                        {
                            string tmpNamePolje = "";
                            tmpNamePolje = "q" + pr.ToString() + (0).ToString();

                            Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                            if (int.Parse(tmpPanel.Tag.ToString()) > 0 && int.Parse(tmpPanel.Tag.ToString()) < 9)
                            {
                                tmpPanel.Tag = "15";
                            }
                        }
                    }

                    funkcijaZaProvjeruSaha();

                    funkcijaZaProvjeruSahMata("ploca", null);

                    if (crneFigure == true)
                    {
                        pnlCrniIgrac.BackColor = Color.LightGreen;
                        pnlBijeliIgrac.BackColor = Color.Red;
                        pnlCrniIgrac.Refresh();
                        pnlBijeliIgrac.Refresh();
                    }
                    else
                    {
                        pnlBijeliIgrac.BackColor = Color.LightGreen;
                        pnlCrniIgrac.BackColor = Color.Red;
                        pnlCrniIgrac.Refresh();
                        pnlBijeliIgrac.Refresh();
                    }

                    prebrojiFigure();
                    grbBijeleFigureNaPloci.Refresh();
                    grbCrneFigureNaPloci.Refresh();

                    if (crneFigure == true)
                    {
                        if (brojCrnihPijuna < tempBrojCrnihPijuna)                        
                            prednostSahaPredOstalimPotezima += 6;                        
                        if (brojCrnihTopova < tempBrojCrnihTopova || brojCrnihKonja < tempBrojCrnihKonja || brojCrnihLaufera < tempBrojCrnihLaufera)
                            prednostSahaPredOstalimPotezima += 24;
                        if(brojCrnihKraljica < tempBrojCrnihKraljica)
                            prednostSahaPredOstalimPotezima += 48;                    
                    }

                    if (bijeleFigure == true)
                    {
                        if (brojBijelihPijuna < tempBrojBijelihPijuna)
                            prednostSahaPredOstalimPotezima += 6;
                        if (brojBijelihTopova < tempBrojBijelihTopova || brojBijelihKonja < tempBrojBijelihKonja || brojBijelihLaufera < tempBrojBijelihLaufera)
                            prednostSahaPredOstalimPotezima += 24;
                        if (brojBijelihKraljica < tempBrojBijelihKraljica)
                            prednostSahaPredOstalimPotezima += 48;
                    }

                    txbPrednost.Text = prednostSahaPredOstalimPotezima.ToString();

                    tempBrojBijelihPijuna = brojBijelihPijuna;
                    tempBrojBijelihTopova = brojBijelihTopova;
                    tempBrojBijelihKonja = brojBijelihKonja;
                    tempBrojBijelihLaufera = brojBijelihLaufera;
                    tempBrojBijelihKraljica = brojBijelihKraljica;
                    tempBrojCrnihPijuna = brojCrnihPijuna;
                    tempBrojCrnihTopova = brojCrnihTopova;
                    tempBrojCrnihKonja = brojCrnihKonja;
                    tempBrojCrnihLaufera = brojCrnihLaufera;
                    tempBrojCrnihKraljica = brojCrnihKraljica;

                    brojPotezaRacunalo++;

                    //promjeni boju pozadine na 1s pa vrati nazad
                    for (int b = 0; b < 2; b++)
                    {
                        panelBoje[b] = tmpPanelBoje[b].BackColor;
                        tmpPanelBoje[b].BackColor = Color.Red;
                    }
                    tmrBojaRacunalo.Start();

                }
        }

        private bool daLiSeKraljPomako(Cvor prijCv, Cvor trnCv)
        {
            int x = 0;
            int y = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    if (prijCv.ploca[ii, i] == 116)
                    {
                        x = ii;
                        y = i;                        
                    }
                }
            }

            if (trnCv.ploca[x, y] == 116)
            {
                return false;
            }

                return true;
        }

        private Panel[] odrediKojuFiguruJeRacunaloPomaklo(Cvor tmpCvor)
        {
            Panel[] panelPolje = new Panel[2];
            Cvor noviCvor = new Cvor();
            noviCvor = tmpCvor;
            int counterPolje = 0;

            
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;
                                                            
                    if (int.Parse(tmpPanel.Tag.ToString()) != noviCvor.ploca[ii, i])
                    {
                        panelPolje[counterPolje] = tmpPanel;
                        counterPolje++;
                    }
                }
            }

            return panelPolje;
        }

        private void funkcijaZaProvjeruSahMata(string izvor, Cvor tmpCvor)
        {
            //provjeri da li je igracu sah mat
            //ucitaj plocu u root, pokusaj pomaknuti sve crne figure
            //ako je svim crnim figurama sah, onda je sah mat
            ///////////////////////////////////////////////////////////////////////

            //prvo napravi novo stablo u kojem ces provjeravati
            //napravi root cvor
            Cvor root_igrac_sahmat = new Cvor();
            trenutniCvor = root_igrac_sahmat;

            //kopiraj plocu u trenutni cvor - u ovome slucaju root
            if (izvor == "ploca")
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        string tmpNamePolje = "";
                        tmpNamePolje = "q" + ii.ToString() + i.ToString();

                        Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                        trenutniCvor.ploca[ii, i] = byte.Parse(tmpPanel.Tag.ToString());
                    }
                }
            }

            if (izvor == "cvor")
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        trenutniCvor.ploca[ii, i] = tmpCvor.ploca[ii, i];
                    }
                }
            }

            //napravi stablo do dubine 2
            //crni je na redu:
            povuciCrneFigure(ref trenutniCvor);

            do
            {
                trenutniCvor = trenutniCvor.djeca[trenutniCvor.slijedeciCvorObliazak];

                //bijeli je na redu:
                povuciBijeleFigure(ref trenutniCvor);

                trenutniCvor = trenutniCvor.roditelj;
                trenutniCvor.slijedeciCvorObliazak++;
            } while (trenutniCvor.djeca[trenutniCvor.slijedeciCvorObliazak] != null);
            trenutniCvor.slijedeciCvorObliazak = 0;

            //provjeri je li šah mat -> jesu li sva djeca roota pod sahom            
            igracuJeSahMat = true;
            int count_djeca_igrac = 0;
            trenutniCvor = root_igrac_sahmat;
            while (trenutniCvor.djeca[count_djeca_igrac] != null)
            {
                if (trenutniCvor.djeca[count_djeca_igrac].sah == false)
                {
                    igracuJeSahMat = false;
                }
                count_djeca_igrac++;
            }


            //uništi stablo za provjeru je li igraču šah mat
            count_djeca_igrac = 0;
            while (trenutniCvor.djeca[count_djeca_igrac] != null)
            {
                trenutniCvor.djeca[count_djeca_igrac] = null;
                count_djeca_igrac++;
            }

            trenutniCvor = root;
            //stablo za provjeru igračevog šah mata je uništeno
            ///////////////////////////////////////////////////////////////////////
        }
        
        
        public void funkcijaZaProvjeruSaha()
        {
            //provjeri da li je igraču šah
            //////////////////////////////////////////////////////
            Cvor root_igrac_sah = new Cvor();
            trenutniCvor = root_igrac_sah;

            //kopiraj plocu u trenutni cvor - u ovome slucaju root
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                    trenutniCvor.ploca[ii, i] = byte.Parse(tmpPanel.Tag.ToString());
                }
            }

            //povuci sve bijele poteze
            povuciBijeleFigure(ref trenutniCvor);

            //ako je koji potez pojeo crnoga kralja onda je šah igraču
            //odnosno ako je šah u rootu pnda je šah igraču

            igracuJeSah = false;
            if (root_igrac_sah.sah == true)
            {
                igracuJeSah = true;
            }

            //uništi stablo za provjeru šaha igraču
            int count_provjera_saha = 0;
            while (trenutniCvor.djeca[count_provjera_saha] != null)
            {
                trenutniCvor.djeca[count_provjera_saha] = null;
                count_provjera_saha++;
            }


            trenutniCvor = root;
            //stablo za brovjeru saha je uništeno
            //////////////////////////////////////////////////////
        }
        
        public bool igracuSahMatPoruka()
        {
            if (igracuJeSahMat == true)
            {
                lblPoruka.Text = "Šah-mat, igrač je izgubio";
                tmrPoruka.Start();
                Form3 zavrsniMeni = new Form3();
                zavrsniMeni.Owner = this;
                zavrsniMeni.ShowDialog();
                igracuJeSahMat = false;
                return true;
            }
            return false;
        }


        public void igracuJeSahPoruka()
        {
            if (igracuJeSah == true)
            {
                igracuJeSah = false;
                lblPoruka.Text = "Igraču je šah";
                tmrPoruka.Start();
            }
        }


        public void unistiStablo()
        {
            root.slijedeceDjete = 0;
            while (root.djeca[root.slijedeceDjete] != null)
            {
                root.djeca[root.slijedeceDjete] = null;
                root.slijedeceDjete++;
            }

            root.slijedeciCvorObliazak = 0;
            root.alpha = -2147483648; //kao -beskonacno
            root.beta = 2147483647; //kao +beskonacno
            root.slijedeceDjete = -1;
            root.ime = 255;
            root.sah = false;
            root.slijedeciCvorInorderObilazak = 0;

            for (int i = 0; i < brojDjeceKojuImaSvakiCvor; i++)
            {
                root.djeca[i] = null;
            }

            for (int ii = 0; ii < 8; ii++)
            {
                for (int iii = 0; iii < 8; iii++)
                {
                    root.ploca[iii, ii] = 0;
                }
            }
        }


        public void PlocaRefreshRacunaloJeCrni()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {

                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;


                    if (tmpPanel.Tag.ToString() == "0")
                    {
                        tmpPanel.BackgroundImage = null;
                    }

                    //bijele figure
                    /////////////////
                    if (tmpPanel.Tag.ToString() == "1" || tmpPanel.Tag.ToString() == "2"
                        || tmpPanel.Tag.ToString() == "3" || tmpPanel.Tag.ToString() == "4"
                        || tmpPanel.Tag.ToString() == "5" || tmpPanel.Tag.ToString() == "6"
                        || tmpPanel.Tag.ToString() == "7" || tmpPanel.Tag.ToString() == "8")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.pijun_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "9" || tmpPanel.Tag.ToString() == "10")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.top_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "11")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj1_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "12")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj2_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "13" || tmpPanel.Tag.ToString() == "14")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.laufer_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "15")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kraljica_crna;
                    }

                    if (tmpPanel.Tag.ToString() == "16")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kralj_crni;
                    }



                    //crne figure
                    /////////////////
                    if (tmpPanel.Tag.ToString() == "101" || tmpPanel.Tag.ToString() == "102"
                        || tmpPanel.Tag.ToString() == "103" || tmpPanel.Tag.ToString() == "104"
                        || tmpPanel.Tag.ToString() == "105" || tmpPanel.Tag.ToString() == "106"
                        || tmpPanel.Tag.ToString() == "107" || tmpPanel.Tag.ToString() == "108")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.pijun_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "109" || tmpPanel.Tag.ToString() == "110")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.top_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "111")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj1_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "112")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj2_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "113" || tmpPanel.Tag.ToString() == "114")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.laufer_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "115")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kraljica_bijela;
                    }

                    if (tmpPanel.Tag.ToString() == "116")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kralj_bijeli;
                    }
                }
            }
        }
        
        public void PlocaRefresh()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {

                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;


                    if (tmpPanel.Tag.ToString() == "0")
                    {
                        tmpPanel.BackgroundImage = null;
                    }

                    //bijele figure
                    /////////////////
                    if (tmpPanel.Tag.ToString() == "1" || tmpPanel.Tag.ToString() == "2"
                        || tmpPanel.Tag.ToString() == "3" || tmpPanel.Tag.ToString() == "4"
                        || tmpPanel.Tag.ToString() == "5" || tmpPanel.Tag.ToString() == "6"
                        || tmpPanel.Tag.ToString() == "7" || tmpPanel.Tag.ToString() == "8")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.pijun_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "9" || tmpPanel.Tag.ToString() == "10")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.top_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "11")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj1_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "12")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj2_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "13" || tmpPanel.Tag.ToString() == "14")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.laufer_bijeli;
                    }

                    if (tmpPanel.Tag.ToString() == "15")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kraljica_bijela;
                    }

                    if (tmpPanel.Tag.ToString() == "16")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kralj_bijeli;
                    }

                    //crne figure
                    /////////////////
                    if (tmpPanel.Tag.ToString() == "101" || tmpPanel.Tag.ToString() == "102"
                        || tmpPanel.Tag.ToString() == "103" || tmpPanel.Tag.ToString() == "104"
                        || tmpPanel.Tag.ToString() == "105" || tmpPanel.Tag.ToString() == "106"
                        || tmpPanel.Tag.ToString() == "107" || tmpPanel.Tag.ToString() == "108")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.pijun_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "109" || tmpPanel.Tag.ToString() == "110")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.top_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "111")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj1_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "112")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.konj2_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "113" || tmpPanel.Tag.ToString() == "114")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.laufer_crni;
                    }

                    if (tmpPanel.Tag.ToString() == "115")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kraljica_crna;
                    }

                    if (tmpPanel.Tag.ToString() == "116")
                    {
                        tmpPanel.BackgroundImage = Properties.Resources.kralj_crni;
                    }
                }
            }
        }


        private void KlikNaFiguru(object sender, MouseEventArgs e)
        {
            if (zakljucajIgraca == false)
            {
                if (prviKlik == false)
                {
                    svaMjestaNaKojaSeFiguraMozePomaknuti = new Cvor();
                    trenutniCvor = svaMjestaNaKojaSeFiguraMozePomaknuti;

                    //kopiraj plocu u trenutni cvor - u ovome slucaju root
                    for (int i = 0; i < 8; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            string tmpNamePolje = "";
                            tmpNamePolje = "q" + ii.ToString() + i.ToString();

                            Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                            trenutniCvor.ploca[ii, i] = byte.Parse(tmpPanel.Tag.ToString());
                        }
                    }
                }
            }

            if (prviKlik == true)
            {
                prviKlik = false;
                prviKlikHolder.BackColor = tmpColor;
                drugiKlikHolder = ((Panel)sender);
                drugiKlikHolderVrijednost = int.Parse(drugiKlikHolder.Tag.ToString());

                //provjeri da li se figura moze staviti na to mjesto
                //izvuci x i y iz imena polja
                string tmpImePolja = drugiKlikHolder.Name;
                int xKoor = int.Parse(tmpImePolja[1].ToString());
                int yKoor = int.Parse(tmpImePolja[2].ToString());

                int tmpCountDjeca = 0;
                bool mozeSeStaviti = false;
                while (svaMjestaNaKojaSeFiguraMozePomaknuti.djeca[tmpCountDjeca] != null)
                {
                    if (svaMjestaNaKojaSeFiguraMozePomaknuti.djeca[tmpCountDjeca].ploca[xKoor, yKoor] == (byte)poljeFiguraHolder)
                    {
                        mozeSeStaviti = true;
                    }

                    tmpCountDjeca++;
                }

                //unisti stablo za provjeru
                tmpCountDjeca = 0;
                while (svaMjestaNaKojaSeFiguraMozePomaknuti.djeca[tmpCountDjeca] != null)
                {
                    svaMjestaNaKojaSeFiguraMozePomaknuti.djeca[tmpCountDjeca] = null;
                    tmpCountDjeca++;
                }

                trenutniCvor = root;
                /////////////////////////////////////////////////////

                ((Panel)sender).Tag = poljeFiguraHolder.ToString();
                poljeFiguraHolder = 0;


                funkcijaZaProvjeruSaha();
                if (igracuJeSah == true || mozeSeStaviti == false)
                {
                    //javi da je šah i vrati potez
                    if (mozeSeStaviti == false)
                    {
                        lblPoruka.Text = "Taj potez je zabranjen";
                        tmrPoruka.Start();
                    }
                    else
                    {
                        igracuJeSahPoruka();
                    }

                    drugiKlikHolder.Tag = drugiKlikHolderVrijednost.ToString();
                    prviKlikHolder.Tag = prviKlikHolderVrijednost.ToString();

                }
                else
                {
                    //potez je povucen
                    prebrojiFigure();
                    grbBijeleFigureNaPloci.Refresh();
                    grbCrneFigureNaPloci.Refresh();

                    brojPotezaIgrac++;

                    if (crneFigure == true)
                    {
                        PlocaRefresh();
                    }
                    if (bijeleFigure == true)
                    {
                        PlocaRefreshRacunaloJeCrni();
                    }
                    grbPloca.Refresh();

                    //promocija pijuna                    
                    for (int p = 0; p < 8; p++)
                    {
                        string tmpNamePolje = "";
                        tmpNamePolje = "q" + p.ToString() + (7).ToString();

                        Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                        if (int.Parse(tmpPanel.Tag.ToString()) > 100 && int.Parse(tmpPanel.Tag.ToString()) < 109)
                        {
                            Form4 promocijaPijuna = new Form4();
                            promocijaPijuna.Owner = this;
                            promocijaPijuna.pppx = p;
                            promocijaPijuna.pppy = 7;
                            promocijaPijuna.ShowDialog();

                        }
                    }

                    //racunalo povlaci potez
                    napraviStablo(dubinaPretrage);
                    unistiStablo();

                    if (crneFigure == true)
                    {
                        PlocaRefresh();
                    }
                    if (bijeleFigure == true)
                    {
                        PlocaRefreshRacunaloJeCrni();
                    }

                    bool tmpSahMat = igracuSahMatPoruka();
                    if (tmpSahMat == false)
                    {
                        igracuJeSahPoruka();
                    }
                }

            }
            else
            {
                if (zakljucajIgraca == false)
                {
                    poljeFiguraHolder = int.Parse(((Panel)sender).Tag.ToString());

                    if (poljeFiguraHolder != 0 && poljeFiguraHolder > 100 && poljeFiguraHolder <= 116 && startJeKliknut == true)
                    {
                        if (poljeFiguraHolder == 101 || poljeFiguraHolder == 102 ||
                            poljeFiguraHolder == 103 || poljeFiguraHolder == 104 ||
                            poljeFiguraHolder == 105 || poljeFiguraHolder == 106 ||
                            poljeFiguraHolder == 107 || poljeFiguraHolder == 108)
                        {
                            pomakniPijuna((byte)poljeFiguraHolder, ref trenutniCvor, "c");
                        }

                        if (poljeFiguraHolder == 109 || poljeFiguraHolder == 110)
                        {
                            pomakniTop((byte)poljeFiguraHolder, ref trenutniCvor, "c");
                        }

                        if (poljeFiguraHolder == 111 || poljeFiguraHolder == 112)
                        {
                            pomakniKonja((byte)poljeFiguraHolder, ref trenutniCvor, "c");
                        }

                        if (poljeFiguraHolder == 113 || poljeFiguraHolder == 114)
                        {
                            pomakniLaufera((byte)poljeFiguraHolder, ref trenutniCvor, "c");
                        }

                        if (poljeFiguraHolder == 115)
                        {
                            pomakniKraljicu(ref trenutniCvor, "c");
                        }

                        if (poljeFiguraHolder == 116)
                        {
                            pomakniKralja(ref trenutniCvor, "c");
                        }

                        prviKlik = true;
                        prviKlikHolder = ((Panel)sender);
                        prviKlikHolderVrijednost = int.Parse(prviKlikHolder.Tag.ToString());
                        ((Panel)sender).Tag = "0";

                        tmpColor = ((Panel)sender).BackColor;
                        ((Panel)sender).BackColor = Color.Blue;
                    }
                    else
                    {
                        poljeFiguraHolder = 0;
                        prviKlik = false;
                    }
                }
            }
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int ii = 0; ii < 300; ii++)
            {
                rezultatiKorijenaThreadova[ii] = -123456789;
            }

            for (int i = 0; i < 300; i++)
            {
                popis_sahova_djeca_roota[i] = 0;
            }

            txbDubina.Text = dubinaPretrage.ToString();
            txbPrednost.Text = prednostSahaPredOstalimPotezima.ToString();
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            startJeKliknut = true;
            pocetakIgre = DateTime.Now;
            
            if (bijeleFigure == true)
            {
                pnlCrniIgrac.BackColor = Color.Red;
                pnlBijeliIgrac.BackColor = Color.LightGreen;
                pnlCrniIgrac.Refresh();
                pnlBijeliIgrac.Refresh();
            }
            
            lblPoruka.Text = "Igra je započela";
            tmrPoruka.Start();
            
            /*
             * bijeli ---> racunalo
             * 
             *1, 2, 3, 4, 5, 6, 7, 8 ---> pijuni
             *9, 10                  ---> topovi
             *11, 12                 ---> konji
             *13, 14                 ---> lauferi
             *15                     ---> kraljica
             *16                     ---> kralj
             */

            /*
             * crni --->neki drugi igrac
             * 
             *101, 102, 103, 104, 105, 106, 107, 108 ---> pijuni
             *109, 110                               ---> topovi
             *111, 112                               ---> konji
             *113, 114                               ---> lauferi
             *115                                    ---> kraljica
             *116                                    ---> kralj
             */

            if (crneFigure == true)
            {
                napraviStablo(dubinaPretrage);
                unistiStablo();

                if (crneFigure == true)
                {
                    PlocaRefresh();
                }
                if (bijeleFigure == true)
                {
                    PlocaRefreshRacunaloJeCrni();
                }

                bool tmpSahMat = igracuSahMatPoruka();
                if (tmpSahMat == false)
                {
                    igracuJeSahPoruka();
                }
            }            
        }

        private void btnPrimjeni_Click(object sender, EventArgs e)
        {
            dubinaPretrage = int.Parse(txbDubina.Text);
            prednostSahaPredOstalimPotezima = int.Parse(txbPrednost.Text);
        }

        private void btnTrenutneVrijednosti_Click(object sender, EventArgs e)
        {
            txbDubina.Text = dubinaPretrage.ToString();
            txbPrednost.Text = prednostSahaPredOstalimPotezima.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Form2 startIzbornik = new Form2();
            startIzbornik.Owner = this;
            startIzbornik.StartPosition = FormStartPosition.CenterParent;
            startIzbornik.ShowDialog();
        }


        public void pripremiPlocu()
        {            
            if (crneFigure == true)
            {                
                if (igracJePrijeBioBijeli == true && crneFigure == true)
                {
                    //preokreni plocu
                    for (int i = 7; i >= 0; i--)
                    {
                        for (int ii = 7; ii >= 0; ii--)
                        {
                            //i -> y
                            //ii -> x
                            string tmpNamePolje = "";
                            tmpNamePolje = "q" + ii.ToString() + i.ToString();

                            Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                            int tempx = (ii - 7) * (-1);
                            int tempy = (i - 7) * (-1);

                            tmpPanel.Name = "q" + tempx.ToString() + tempy.ToString();
                        }
                    }
                }
                

                posloziFigure();
                PlocaRefresh();
                grbPloca.Refresh();

                igracJePrijeBioBijeli = false;
            }

            if (bijeleFigure == true)
            {
                //preokreni plocu
                for (int i = 0; i < 8; i++)
                {
                    for (int ii = 0; ii < 8; ii++)
                    {
                        //i -> y
                        //ii -> x
                        string tmpNamePolje = "";
                        tmpNamePolje = "q" + ii.ToString() + i.ToString();

                        Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                        int tempx = (ii - 7) * (-1);
                        int tempy = (i - 7) * (-1);
                        
                        tmpPanel.Name = "q" + tempx.ToString() + tempy.ToString();                        
                    }
                }
                

                posloziFigure();
                PlocaRefreshRacunaloJeCrni();
                grbPloca.Refresh();

                igracJePrijeBioBijeli = true;
            }
        }


        private void posloziFigure()
        {
            //prebrisi plocu -> makni sve figure
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                    tmpPanel.Tag = "0";
                }
            }


                if (crneFigure == true)
                {
                    //bijele figure
                    //pijuni
                    for (int ii = 0; ii < 8; ii++)
                    {
                        string tmpNamePolje = "";
                        tmpNamePolje = "q" + ii.ToString() + (6).ToString();

                        Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                        tmpPanel.Tag = (ii + 1).ToString();
                    }

                    // topovi
                    Panel tmpPanel1 = grbPloca.Controls.Find("q07", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "9";
                    tmpPanel1 = grbPloca.Controls.Find("q77", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "10";

                    //konji
                    tmpPanel1 = grbPloca.Controls.Find("q17", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "11";
                    tmpPanel1 = grbPloca.Controls.Find("q67", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "12";

                    //lauferi
                    tmpPanel1 = grbPloca.Controls.Find("q27", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "13";
                    tmpPanel1 = grbPloca.Controls.Find("q57", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "14";

                    //kraljica
                    tmpPanel1 = grbPloca.Controls.Find("q37", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "15";

                    //kralj
                    tmpPanel1 = grbPloca.Controls.Find("q47", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "16";



                    //crne figure
                    //pijuni
                    for (int ii = 0; ii < 8; ii++)
                    {
                        string tmpNamePolje = "";
                        tmpNamePolje = "q" + ii.ToString() + (1).ToString();

                        Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                        tmpPanel.Tag = (ii + 1 + 100).ToString();
                    }

                    //topovi
                    tmpPanel1 = grbPloca.Controls.Find("q00", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "109";
                    tmpPanel1 = grbPloca.Controls.Find("q70", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "110";

                    //konji
                    tmpPanel1 = grbPloca.Controls.Find("q10", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "111";
                    tmpPanel1 = grbPloca.Controls.Find("q60", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "112";

                    //lauferi
                    tmpPanel1 = grbPloca.Controls.Find("q20", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "113";
                    tmpPanel1 = grbPloca.Controls.Find("q50", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "114";

                    //kraljica
                    tmpPanel1 = grbPloca.Controls.Find("q30", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "115";

                    //kralj
                    tmpPanel1 = grbPloca.Controls.Find("q40", true).FirstOrDefault() as Panel;
                    tmpPanel1.Tag = "116";

                }


            if (bijeleFigure == true)
            {
                //bijele figure
                //pijuni
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + (6).ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                    tmpPanel.Tag = (ii + 1).ToString();
                }

                // topovi
                Panel tmpPanel1 = grbPloca.Controls.Find("q77", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "9";
                tmpPanel1 = grbPloca.Controls.Find("q07", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "10";

                //konji
                tmpPanel1 = grbPloca.Controls.Find("q67", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "11";
                tmpPanel1 = grbPloca.Controls.Find("q17", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "12";

                //lauferi
                tmpPanel1 = grbPloca.Controls.Find("q57", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "13";
                tmpPanel1 = grbPloca.Controls.Find("q27", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "14";

                //kraljica
                tmpPanel1 = grbPloca.Controls.Find("q47", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "15";

                //kralj
                tmpPanel1 = grbPloca.Controls.Find("q37", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "16";



                //crne figure
                //pijuni
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + (1).ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                    tmpPanel.Tag = (ii + 1 + 100).ToString();
                }

                //topovi
                tmpPanel1 = grbPloca.Controls.Find("q70", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "109";
                tmpPanel1 = grbPloca.Controls.Find("q00", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "110";

                //konji
                tmpPanel1 = grbPloca.Controls.Find("q60", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "111";
                tmpPanel1 = grbPloca.Controls.Find("q10", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "112";

                //lauferi
                tmpPanel1 = grbPloca.Controls.Find("q50", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "113";
                tmpPanel1 = grbPloca.Controls.Find("q20", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "114";

                //kraljica
                tmpPanel1 = grbPloca.Controls.Find("q40", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "115";

                //kralj
                tmpPanel1 = grbPloca.Controls.Find("q30", true).FirstOrDefault() as Panel;
                tmpPanel1.Tag = "116";

            }
        }

        private void tmrPoruka_Tick(object sender, EventArgs e)
        {
            tmrPorukaBroj++;
            
            if (tmrPorukaBroj == 5)
            {                
                tmrPorukaBroj = 0;
                lblPoruka.Text = "";
                grbPoruka.Refresh();
                tmrPoruka.Stop();
            }
        }


        private void prebrojiFigure()
        {
            brojBijelihPijuna = 0;
            brojBijelihTopova = 0;
            brojBijelihKonja = 0;
            brojBijelihLaufera = 0;
            brojBijelihKraljica = 0;

            brojCrnihPijuna = 0;
            brojCrnihTopova = 0;
            brojCrnihKonja = 0;
            brojCrnihLaufera = 0;
            brojCrnihKraljica = 0;

            //i->y
            //ii->x
            for (int i = 0; i < 8; i++)
            {
                for (int ii = 0; ii < 8; ii++)
                {
                    string tmpNamePolje = "";
                    tmpNamePolje = "q" + ii.ToString() + i.ToString();

                    Panel tmpPanel = grbPloca.Controls.Find(tmpNamePolje, true).FirstOrDefault() as Panel;

                    
                    for (int bc = 0; bc <= 1; bc++)
                    {
                        string color = "";
                        string color1 = "";
                        if (crneFigure == true)
                        {
                            if (bc == 0)
                            {
                                color = "b";
                                color1 = "b";
                            }
                            if (bc == 1)
                            {
                                color = "c";
                                color1 = "c";
                            }                                
                        }
                        if (bijeleFigure == true)
                        {
                            if (bc == 0)
                            {
                                color = "b";
                                color1 = "c";
                            }
                            if (bc == 1)
                            {
                                color = "c";
                                color1 = "b";
                            }    
                        }

                        if (int.Parse(tmpPanel.Tag.ToString()) >= figureDict[color]["pijun1"] && int.Parse(tmpPanel.Tag.ToString()) <= figureDict[color]["pijun8"])
                        {
                            if (color1 == "b")
                                brojBijelihPijuna++;
                            if (color1 == "c")
                                brojCrnihPijuna++;
                        }

                        if (int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["top1"] || int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["top2"])
                        {
                            if (color1 == "b")
                                brojBijelihTopova++;
                            if (color1 == "c")
                                brojCrnihTopova++;
                        }

                        if (int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["konj1"] || int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["konj2"])
                        {
                            if (color1 == "b")
                                brojBijelihKonja++;
                            if (color1 == "c")
                                brojCrnihKonja++;
                        }

                        if (int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["laufer1"] || int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["laufer2"])
                        {
                            if (color1 == "b")
                                brojBijelihLaufera++;
                            if (color1 == "c")
                                brojCrnihLaufera++;
                        }

                        if (int.Parse(tmpPanel.Tag.ToString()) == figureDict[color]["kraljica"])
                        {
                            if (color1 == "b")
                                brojBijelihKraljica++;
                            if (color1 == "c")
                                brojCrnihKraljica++;
                        }
                    }
                    
                    //postabi labele
                    lblBrojBijelihPijuna.Text = brojBijelihPijuna.ToString() + "/8";
                    lblBrojBijelihTopova.Text = brojBijelihTopova.ToString() + "/2";
                    lblBrojBijelihKonja.Text = brojBijelihKonja.ToString() + "/2";
                    lblBrojBijelihLaufera.Text = brojBijelihLaufera.ToString() + "/2";
                    lblBrojBijelihKraljica.Text = brojBijelihKraljica.ToString() + "/1";

                    lblBrojCrnihPijuna.Text = brojCrnihPijuna.ToString() + "/8";
                    lblBrojCrnihTopova.Text = brojCrnihTopova.ToString() + "/2";
                    lblBrojCrnihKonja.Text = brojCrnihKonja.ToString() + "/2";
                    lblBrojCrnihLaufera.Text = brojCrnihLaufera.ToString() + "/2";
                    lblBrojCrnihKraljica.Text = brojCrnihKraljica.ToString() + "/1";
                }
            }
        }

        private void tmrBojaRacunalo_Tick(object sender, EventArgs e)
        {
            for (int b = 0; b < 2; b++)
            {
                tmpPanelBoje[b].BackColor = panelBoje[b];
            }
            zakljucajIgraca = false;
            tmrBojaRacunalo.Stop();
        }
    }



    class Cvor
    {
        public byte ime;
        public int podatak;
        //uvijek staviti broj djece za jedan više od navećeg broja poteza da može izać iz petlje
        public const byte brojDjece = 150;
        public Cvor[] djeca = new Cvor[brojDjece];
        public Cvor roditelj;
        public sbyte slijedeceDjete;
        public int alpha;
        public int beta;
        public byte slijedeciCvorObliazak;
        public byte[,] ploca = new byte[8, 8];
        public bool sah;
        public byte slijedeciCvorInorderObilazak;


        public Cvor()
        {
            slijedeciCvorObliazak = 0;
            alpha = -2147483648; //kao -beskonacno
            beta = 2147483647; //kao +beskonacno
            slijedeceDjete = -1;
            ime = 255;
            sah = false;
            slijedeciCvorInorderObilazak = 0;
            podatak = 0;

            for (int i = 0; i < brojDjece; i++)
            {
                djeca[i] = null;
            }

            for (int ii = 0; ii < 8; ii++)
            {
                for (int iii = 0; iii < 8; iii++)
                {
                    ploca[iii, ii] = 0;
                }
            }
        }
    }    
}
