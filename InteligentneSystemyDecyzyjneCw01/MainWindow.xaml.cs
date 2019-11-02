using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InteligentneSystemyDecyzyjneCw01
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("Miejsce 01");
            TDM();
            TFTDM();
            TFIDFTDM();
            //liczBinDoEdited();
            macierzRankingDokumentow();
        }

        // tablica dla zapytań, można wpisac wiele słów
        string[] enteredText = new string[] { "bazy", "danych" };
        #region Definicja tablic

        // Definiowanie dokumentów
        string[] dok01 = new string[] { "bazy danych", "bazy tekstowe", "bazy inne" };
        string[] dok02 = new string[] { "bazy danych", "przykłady", "zastosowania" };
        string[] dok03 = new string[] { "bazy danych", "minusy", "plusy" };
        string[] dok04 = new string[] { "składowanie danych" };

        // Operacja tworzenia bazy TDM Term Document Matrix
        string[] TDM01 = new string[] { "bazy", "relacyjne", "tekstowe", "inne" };
        string[] TDM02 = new string[] { "bazy", "danych", "przykłady", "zastosowania" };
        string[] TDM03 = new string[] { "bazy", "danych", "zalety", "wady" };
        string[] TDM04 = new string[] { "składowanie", "danych", "", "" };
        string[] TDM05 = new string[] { "bazy", "danych", "", "", "" }; // entered text

        string[] TFTDM01 = new string[] { "bazy", "relacyjne", "bazy", "tekstowe", "bazy", "inne" };
        string[] TFTDM02 = new string[] { "bazy", "danych", "przykłady", "zastosowania", "", "" };
        string[] TFTDM03 = new string[] { "bazy", "danych", "zalety", "bazy", "danych", "wady" };
        string[] TFTDM04 = new string[] { "składowanie", "danych", "", "", "", "" };
        string[] TFTDM05 = new string[] { "bazy", "danych", "", "", "", "" }; // entered text

        // tworzenie macierzy TDM dla wszystkich dokumentów
        string[] TDMAll = new string[] { "bazy", "danych", "tekstowe", "inne", "przykłady",
                                    "zastosowania", "zalety", "wady", "składowanie", "relacyjne", " " };
        string[] headDokument = new string[] { "Dok1", "Dok2", "Dok3", "Dok4"}; // tabela pomocnicza do generowania nagłówków w Ranking dokumentów

        #endregion

        #region Definicja zmiennych

        // tworzenie macierzy binarnej dla dok01
        int[] tempInt01 = new int[10];
        int[] tempInt02 = new int[10];
        int[] tempInt03 = new int[10];
        int[] tempInt04 = new int[10];
        int[] tempInt05 = new int[10];
        int[] binDok01 = new int[10];
        int[] binDok02 = new int[10];
        int[] binDok03 = new int[10];
        int[] binDok04 = new int[10];
        int[] binDok05 = new int[10];
        int[] binTFTDM01 = new int[10];
        int[] binTFTDM02 = new int[10];
        int[] binTFTDM03 = new int[10];
        int[] binTFTDM04 = new int[10];
        int[] binTFTDM05 = new int[10];
        int[] licznikwystapienDok = new int[10];
        int[,] licznikwystapienTDM = new int[10,10]; 
        int[,] licznikwystapienTFTDM = new int[10, 10];
        int[,] licznikwystapienTFIDFTDM = new int[10, 10];
        int[] licznikWystapienSumaTFIDFTDM = new int[10];
        int[] sumaKwadratowTDM = new int[10];
        int[] sumaKwadratowTFTDM = new int[10];
        double[,] sumaKwadratowTFIDFTDM = new double[10,10];
        double[] DsumaKwadratowTFIDFTDM = new double[10];
        
        double[] IDF = new double[10];
        double[,] TDMTFIDF = new double[10,10];
        // tablica dla wpisanej wartości
        int[] enteredTableTerms1 = new int[10];
        int[] enteredTableTerms2 = new int[10];
        int[] enteredTableTerms3 = new int[10];
        int[] enteredTableTerms4 = new int[10];
        int[] enteredTableTerms5 = new int[10];
        int[] sumaKwadratowEnteredtable1 = new int[10];  // nie wykorzystane, tylko jako temp
        int[] sumaKwadratowEnteredtable2 = new int[10];  // nie wykorzystane, tylko jako temp
        int[] sumaKwadratowEnteredtable3 = new int[10];  // nie wykorzystane, tylko jako temp
        int[] sumaKwadratowEnteredtable4 = new int[10];  // nie wykorzystane, tylko jako temp
        int[] sumaKwadratowEnteredtable5 = new int[10];  // nie wykorzystane, tylko jako temp
        int[] binEnteredTableTerms1 = new int[10];       // macierz z termami wpisanego wyrazu
        int[] binEnteredTableTerms2 = new int[10];       // macierz z termami wpisanego wyrazu
        int[] binEnteredTableTerms3 = new int[10];       // macierz z termami wpisanego wyrazu
        int[] binEnteredTableTerms4 = new int[10];       // macierz z termami wpisanego wyrazu
        int[] binEnteredTableTerms5 = new int[10];       // macierz z termami wpisanego wyrazu
        double dlugoscWektoraEntered;
        double dlugoscWektoraDok1;
        double dlugoscWektoraDok2;
        double dlugoscWektoraDok3;
        double dlugoscWektoraDok4;
        double dlugoscWektoraDok5;

        double[] czynnikNormalizujacy = new double[10]; 
        double[] czynnikNormalizujacyTFTDM = new double[10];
        double[] czynnikNormalizujacyTFIDFTDM = new double[10];
        double[] poNormalizacjiTDM = new double[10];
        double[] poNormalizacjiTFTDM = new double[10];
        double[] doGridTFITDM = new double[10];
        double[,] poNormalizacjiTFIDFTDM = new double[10, 10];
        //int[,] sumaKwadratowTDM = new int[10, 10];

        double[] answerDok01 = new double[10];
        double[] answerDok02 = new double[10];
        double[] answerDok03 = new double[10];

        #endregion

        #region Metoda TDM

        public void TDM()
        {
            // licznik wystąpień, liczący sumę kwadratów
            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tempInt01[i] = (string.Compare(TDM01[i], TDMAll[y]));
                    if (tempInt01[i] == 0)
                    {
                        binDok01[y] = 1;
                        sumaKwadratowTDM[0] += (int)Math.Pow(binDok01[y], 2);
                        licznikwystapienDok[0]++;
                    }
                    tempInt02[i] = (string.Compare(TDM02[i], TDMAll[y]));
                    if (tempInt02[i] == 0)
                    {
                        binDok02[y] = 1;
                        sumaKwadratowTDM[1] += (int)Math.Pow(binDok02[y], 2);
                        licznikwystapienDok[1]++;
                    }
                    tempInt03[i] = (string.Compare(TDM03[i], TDMAll[y]));
                    if (tempInt03[i] == 0)
                    {
                        binDok03[y] = 1;
                        sumaKwadratowTDM[2] += (int)Math.Pow(binDok03[y], 2);
                        licznikwystapienDok[2]++;
                    }
                    tempInt04[i] = (string.Compare(TDM04[i], TDMAll[y]));
                    if (tempInt04[i] == 0)
                    {
                        binDok04[y] = 1;
                        sumaKwadratowTDM[3] += (int)Math.Pow(binDok04[y], 2);
                        licznikwystapienDok[3]++;
                    }
                    tempInt05[i] = (string.Compare(TDM05[i], TDMAll[y]));
                    if (tempInt05[i] == 0)
                    {
                        binDok05[y] = 1;
                        sumaKwadratowTDM[4] += (int)Math.Pow(binDok05[y], 2);
                        licznikwystapienDok[4]++;
                    }
                    //Console.WriteLine(binDok01[i]);
                    //Console.WriteLine("Miejsce 02");
                }
            }

            // Czynnik normalizujący

            czynnikNormalizujacy[0] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTDM[0]), 2);
            czynnikNormalizujacy[1] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTDM[1]), 2);
            czynnikNormalizujacy[2] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTDM[2]), 2);
            czynnikNormalizujacy[3] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTDM[3]), 2);
            czynnikNormalizujacy[4] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTDM[4]), 2);

            // string do wyświetlenia macierzy TDM01
            string str01 = null;
            string str02 = null;
            string str03 = null;
            string str04 = null;
            string str05 = null;
            for (int i = 0; i < 10; i++)
            {
                str01 += binDok01[i];
                str02 += binDok02[i];
                str03 += binDok03[i];
                str04 += binDok04[i];
                str05 += binDok05[i];
                //Console.WriteLine("Miejsce 02");
            }
            // skupienie danych do wyswietlenia, mozna poprawic binDok01 - 04 ustawic jako tablica tablic, wtedy bedzie dzialalo for
            for (int i = 0; i < 10; i++)
            {
                (poNormalizacjiTDM[0]) = (czynnikNormalizujacy[0] * binDok01[0]+ (czynnikNormalizujacy[0] * binDok01[1]));
                (poNormalizacjiTDM[1]) = (czynnikNormalizujacy[1] * binDok02[0]+ (czynnikNormalizujacy[1] * binDok02[1]));
                (poNormalizacjiTDM[2]) = (czynnikNormalizujacy[2] * binDok03[0]+ (czynnikNormalizujacy[2] * binDok03[1]));
                (poNormalizacjiTDM[3]) = (czynnikNormalizujacy[3] * binDok04[0]+ (czynnikNormalizujacy[3] * binDok04[1]));
                (poNormalizacjiTDM[4]) = (czynnikNormalizujacy[4] * binDok05[0]+ (czynnikNormalizujacy[4] * binDok05[1]));
            }
            //Console.WriteLine("Str01 - " + str01);
            //Console.WriteLine("Str02 - " + str02);
            //Console.WriteLine("Str03 - " + str03);
            //Console.WriteLine("Str04 - " + str04);
            //Console.WriteLine("Str05 - " + str05);

            //Console.WriteLine("Licznik Wystapień 00 - " + licznikwystapienDok[0]);
            //Console.WriteLine("Licznik Wystapień 01 - " + licznikwystapienDok[1]);
            //Console.WriteLine("Licznik Wystapień 02 - " + licznikwystapienDok[2]);
            //Console.WriteLine("Licznik Wystapień 03 - " + licznikwystapienDok[3]);
            //Console.WriteLine("Licznik Wystapień 04 - " + licznikwystapienDok[4]);


            //Console.WriteLine("Suma kwadratów 01 - " + sumaKwadratowTDM[0]);
            //Console.WriteLine("Suma kwadratów 02 - " + sumaKwadratowTDM[1]);
            //Console.WriteLine("Suma kwadratów 03 - " + sumaKwadratowTDM[2]);
            //Console.WriteLine("Suma kwadratów 04 - " + sumaKwadratowTDM[3]);
            //Console.WriteLine("Suma kwadratów 05 - " + sumaKwadratowTDM[4]);

            //Console.WriteLine("Czynnik normalizujący 01 - " + czynnikNormalizujacy[0]);
            //Console.WriteLine("Czynnik normalizujący 02 - " + czynnikNormalizujacy[1]);
            //Console.WriteLine("Czynnik normalizujący 03 - " + czynnikNormalizujacy[2]);
            //Console.WriteLine("Czynnik normalizujący 04 - " + czynnikNormalizujacy[3]);
            //Console.WriteLine("Czynnik normalizujący 05 - " + czynnikNormalizujacy[4]);
        }

        #endregion

        #region Metoda TF TDM

        public void TFTDM()
        {
            // licznik wystąpień, liczący sumę kwadratów
            for (int i = 0; i < 6; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    tempInt01[i] = (string.Compare(TFTDM01[i], TDMAll[y]));
                    if (tempInt01[i] == 0)
                    {
                        binTFTDM01[y] = 1;
                        licznikwystapienTFTDM[0, y]++;
                        //Console.WriteLine("Licznik wystąpień i-" + i + " y-" + y + " wystąpienia-" + licznikwystapienTFTDM[0, y]);
                    }
                    tempInt02[i] = (string.Compare(TFTDM02[i], TDMAll[y]));
                    if (tempInt02[i] == 0)
                    {
                        binTFTDM02[y] = 1;
                        licznikwystapienTFTDM[1, y]++;
                    }
                    tempInt03[i] = (string.Compare(TFTDM03[i], TDMAll[y]));
                    if (tempInt03[i] == 0)
                    {
                        binTFTDM03[y] = 1;
                        licznikwystapienTFTDM[2, y]++;
                    }
                    tempInt04[i] = (string.Compare(TFTDM04[i], TDMAll[y]));
                    if (tempInt04[i] == 0)
                    {
                        binTFTDM04[y] = 1;
                        licznikwystapienTFTDM[3, y]++;
                    }
                    tempInt05[i] = (string.Compare(TFTDM05[i], TDMAll[y]));
                    if (tempInt05[i] == 0)
                    {
                        binTFTDM05[y] = 1;
                        licznikwystapienTFTDM[4, y]++;
                    }
                }
            }

            for (int i = 0; i < 10; i++)
            {
                sumaKwadratowTFTDM[0] += (int)Math.Pow(licznikwystapienTFTDM[0, i], 2);
                sumaKwadratowTFTDM[1] += (int)Math.Pow(licznikwystapienTFTDM[1, i], 2);
                sumaKwadratowTFTDM[2] += (int)Math.Pow(licznikwystapienTFTDM[2, i], 2);
                sumaKwadratowTFTDM[3] += (int)Math.Pow(licznikwystapienTFTDM[3, i], 2);
                sumaKwadratowTFTDM[4] += (int)Math.Pow(licznikwystapienTFTDM[4, i], 2);
            }

            // Czynnik normalizujący

            czynnikNormalizujacyTFTDM[0] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTFTDM[0]), 2);
            czynnikNormalizujacyTFTDM[1] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTFTDM[1]), 2);
            czynnikNormalizujacyTFTDM[2] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTFTDM[2]), 2);
            czynnikNormalizujacyTFTDM[3] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTFTDM[3]), 2);
            czynnikNormalizujacyTFTDM[4] = 1 / Math.Round(Math.Sqrt(sumaKwadratowTFTDM[4]), 2);

            // string do wyświetlenia macierzy TDM01
            string str01 = null;
            string str02 = null;
            string str03 = null;
            string str04 = null;
            string str05 = null;
            for (int i = 0; i < 10; i++)
            {
                str01 += licznikwystapienTFTDM[0, i];
                str02 += licznikwystapienTFTDM[1, i];
                str03 += licznikwystapienTFTDM[2, i];
                str04 += licznikwystapienTFTDM[3, i];
                str05 += licznikwystapienTFTDM[4, i];
                //Console.WriteLine("Miejsce 02");
            }
            // skupienie danych do wyswietlenia, mozna poprawic , wtedy bedzie dzialalo for
            for (int i = 0; i < 10; i++)
            {
                (poNormalizacjiTFTDM[0]) = ((czynnikNormalizujacyTFTDM[0]*licznikwystapienTFTDM[0,0]) + (czynnikNormalizujacyTFTDM[0]*licznikwystapienTFTDM[0,1]));
                (poNormalizacjiTFTDM[1]) = ((czynnikNormalizujacyTFTDM[1]*licznikwystapienTFTDM[1,0]) + (czynnikNormalizujacyTFTDM[1]*licznikwystapienTFTDM[1,1]));
                (poNormalizacjiTFTDM[2]) = ((czynnikNormalizujacyTFTDM[2]*licznikwystapienTFTDM[2,0]) + (czynnikNormalizujacyTFTDM[2]*licznikwystapienTFTDM[2,1]));
                (poNormalizacjiTFTDM[3]) = ((czynnikNormalizujacyTFTDM[3]*licznikwystapienTFTDM[3,0]) + (czynnikNormalizujacyTFTDM[3]*licznikwystapienTFTDM[3,1]));
                (poNormalizacjiTFTDM[4]) = ((czynnikNormalizujacyTFTDM[4]*licznikwystapienTFTDM[4,0]) + (czynnikNormalizujacyTFTDM[4]*licznikwystapienTFTDM[4,1]));
                //Console.WriteLine("Po Mormalizacji czynnik - " + czynnikNormalizujacyTFTDM[i] + "licznik-" + licznikwystapienTFTDM[0, i]);
            }

            //Console.WriteLine("Str01 - " + str01);
            //Console.WriteLine("Str02 - " + str02);
            //Console.WriteLine("Str03 - " + str03);
            //Console.WriteLine("Str04 - " + str04);
            //Console.WriteLine("Str05 - " + str05);

            //Console.WriteLine("Licznik Wystapień TFTDM00 - " + licznikwystapienTFTDM[0,0]);
            //Console.WriteLine("Licznik Wystapień TFTDM01 - " + licznikwystapienTFTDM[0,1]);
            //Console.WriteLine("Licznik Wystapień TFTDM02 - " + licznikwystapienTFTDM[0,2]);
            //Console.WriteLine("Licznik Wystapień TFTDM03 - " + licznikwystapienTFTDM[0,3]);
            //Console.WriteLine("Licznik Wystapień TFTDM04 - " + licznikwystapienTFTDM[0,4]);


            //Console.WriteLine("Suma kwadratów TFTDM 01 - " + sumaKwadratowTFTDM[0]);
            //Console.WriteLine("Suma kwadratów TFTDM 02 - " + sumaKwadratowTFTDM[1]);
            //Console.WriteLine("Suma kwadratów TFTDM 03 - " + sumaKwadratowTFTDM[2]);
            //Console.WriteLine("Suma kwadratów TFTDM 04 - " + sumaKwadratowTFTDM[3]);
            //Console.WriteLine("Suma kwadratów TFTDM 05 - " + sumaKwadratowTFTDM[4]);

            //Console.WriteLine("Czynnik normalizujący 01 - " + czynnikNormalizujacyTFTDM[0]);
            //Console.WriteLine("Czynnik normalizujący 02 - " + czynnikNormalizujacyTFTDM[1]);
            //Console.WriteLine("Czynnik normalizujący 03 - " + czynnikNormalizujacyTFTDM[2]);
            //Console.WriteLine("Czynnik normalizujący 04 - " + czynnikNormalizujacyTFTDM[3]);
            //Console.WriteLine("Czynnik normalizujący 05 - " + czynnikNormalizujacyTFTDM[4]);
        }

        #endregion

        #region Metoda TFIDF TDM

        public void TFIDFTDM()
        {
            Console.WriteLine("//////////////////  TFIDFTDM ////");
            // licznik wystąpień, liczący sumę kwadratów
            for (int i = 0; i < 10; i++)
            {
                for (int y = 0; y < 6; y++)
                {
                    tempInt01[i] = (string.Compare(TFTDM01[y], TDMAll[i]));
                    if (tempInt01[i] == 0)
                    {
                        binTFTDM01[y] = 1;
                        licznikwystapienTFIDFTDM[0, i]++;
                        //Console.WriteLine("Licznik wystąpień i-" + i + " y-" + y + " wystąpienia-" + licznikwystapienTFIDFTDM[0, y]);
                    }
                    tempInt02[i] = (string.Compare(TFTDM02[y], TDMAll[i])); // tablica TFTDM pozostaje taka sama również dla TFIDFTDM
                    if (tempInt02[i] == 0)
                    {
                        binTFTDM02[y] = 1;
                        licznikwystapienTFIDFTDM[1, i]++;
                    }
                    tempInt03[i] = (string.Compare(TFTDM03[y], TDMAll[i]));
                    if (tempInt03[i] == 0)
                    {
                        binTFTDM03[y] = 1;
                        licznikwystapienTFIDFTDM[2, i] ++;
                    }
                    tempInt04[i] = (string.Compare(TFTDM04[y], TDMAll[i]));
                    if (tempInt04[i] == 0)
                    {
                        binTFTDM04[y] = 1;
                        licznikwystapienTFIDFTDM[3, i]++;
                    }
                    tempInt05[i] = (string.Compare(TFTDM05[y], TDMAll[i]));
                    if (tempInt05[i] == 0)
                    {
                        binTFTDM05[y] = 1;
                        licznikwystapienTFIDFTDM[4, i]++;
                    }
                }
            }
            // sumator wystąpień fraz we wszystkich zbiorach, zbiór wprowadzonych danych (05) nie jest brany pod uwagę
            for (int i = 0; i < 10; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (licznikwystapienTFIDFTDM[y, i] != 0)
                    {
                        licznikWystapienSumaTFIDFTDM[i]++;
                    }
                }
            }
            // Obliczanie TF, IDF, TFIDF-TDMTFIDF[i,y]
            for (int i = 0; i < 10; i++)
            {
                IDF[i] = (double)Math.Log(4 / (double)licznikWystapienSumaTFIDFTDM[i], 2);

                //Console.WriteLine("IDF  " + IDF[i]);
            }
            
            for (int i = 0; i < 5; i++) 
            {                
                for (int y = 0; y < 10; y++)
                {
                    TDMTFIDF[i,y] = IDF[y] * licznikwystapienTFIDFTDM[i, y];
                }                
            }
            // suma kwadratów
            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    sumaKwadratowTFIDFTDM[i, y] = IDF[y] * licznikwystapienTFIDFTDM[i, y];
                    DsumaKwadratowTFIDFTDM[i] += Math.Pow(sumaKwadratowTFIDFTDM[i, y], 2);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (licznikwystapienTFIDFTDM[i, y] != 0)
                    {
                        czynnikNormalizujacyTFIDFTDM[i] = 1 / Math.Round(Math.Sqrt(DsumaKwadratowTFIDFTDM[i] * licznikwystapienTFIDFTDM[i, y]), 2)  ;
                    }                    
                }                
            }

            for (int i = 0; i < 5; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    poNormalizacjiTFIDFTDM[i, y] += czynnikNormalizujacyTFIDFTDM[i] * sumaKwadratowTFIDFTDM[i, y];
                }
            }
                        
            // string do wyświetlenia macierzy TDM01
            string str01 = null;
            string str02 = null;
            string str03 = null;
            string str04 = null;
            string str05 = null;
            for (int i = 0; i < 10; i++)
            {
                str01 += licznikwystapienTFIDFTDM[0, i];
                str02 += licznikwystapienTFIDFTDM[1, i];
                str03 += licznikwystapienTFIDFTDM[2, i];
                str04 += licznikwystapienTFIDFTDM[3, i];
                str05 += licznikwystapienTFIDFTDM[4, i];
                //Console.WriteLine("Miejsce 02");
            }

            // skupienie danych do wyswietlenia, mozna poprawic , wtedy bedzie dzialalo for
            for (int i = 0; i < 10; i++)
            {
                (doGridTFITDM[0]) = (((poNormalizacjiTFIDFTDM[0, 0]) + (poNormalizacjiTFIDFTDM[0, 1])) *(czynnikNormalizujacyTFTDM[4]));
                (doGridTFITDM[1]) = (((poNormalizacjiTFIDFTDM[1, 0]) + (poNormalizacjiTFIDFTDM[1, 1]))*(czynnikNormalizujacyTFTDM[4]));
                (doGridTFITDM[2]) = (((poNormalizacjiTFIDFTDM[2, 0]) + (poNormalizacjiTFIDFTDM[2, 1]))*(czynnikNormalizujacyTFTDM[4]));
                (doGridTFITDM[3]) = (((poNormalizacjiTFIDFTDM[3, 0]) + (poNormalizacjiTFIDFTDM[3, 1]))*(czynnikNormalizujacyTFTDM[4]));
                (doGridTFITDM[4]) = (((poNormalizacjiTFIDFTDM[4, 0]) + (poNormalizacjiTFIDFTDM[4, 1]))* (czynnikNormalizujacyTFTDM[4]));
                Console.WriteLine("Po Nor - " + poNormalizacjiTFIDFTDM[0, 0] + poNormalizacjiTFIDFTDM[0, 1] + czynnikNormalizujacyTFTDM[4]);
            }

            Console.WriteLine("Str01 - " + str01);
            Console.WriteLine("Str02 - " + str02);
            Console.WriteLine("Str03 - " + str03);
            Console.WriteLine("Str04 - " + str04);
            Console.WriteLine("Str05 - " + str05);
            
            //for (int y = 0; y < 10; y++)
            //{
            //    Console.WriteLine("IDF  - " + IDF[y]);
            //}
            //for (int y = 0; y < 10; y++)
            //{
            //    Console.WriteLine("Licz wystąpień suma  - " + licznikWystapienSumaTFIDFTDM[y]);
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int y = 0; y < 10; y++)
            //    {
            //        Console.WriteLine("TDMTFIDF i-"+ i + " y-" +y +" " + TDMTFIDF[i, y]);
            //    }
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int y = 0; y < 10; y++)
            //    {
            //        Console.WriteLine("Suma kwadratów i-" + i + " y-" + y + " " + sumaKwadratowTFIDFTDM[i, y]);
            //    }
            //}
  
            //for (int i = 0; i < 5; i++)
            //{               
            //        Console.WriteLine("Suma kwadratów dla D i-" + i +  " " + DsumaKwadratowTFIDFTDM[i]);
            //}
            
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine("Czynnik normalizacji dla D i-" + i + " " + czynnikNormalizujacyTFIDFTDM[i]);
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    for (int y = 0; y < 10; y++)
            //    {
            //        if (poNormalizacjiTFIDFTDM[i, y] != 0)
            //        {
            //            Console.WriteLine("Po normalizacji i-" + i + " y-" + y + " " + poNormalizacjiTFIDFTDM[i, y]);
            //        }
                    
            //    }
            //}
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        /// <summary>
        /// Dla wersji - użytkownik podaje swój term
        /// </summary>
        //public void liczBinDoEdited()
        //{
        //    //licznik wystąpień, liczący sumę kwadratów
        //    for (int i = 0; i < 2; i++)
        //    {
        //        for (int y = 0; y < 4; y++)
        //        {
        //            enteredTableTerms1[y] = (string.Compare(enteredText[i], TDM01[y]));
        //            if (enteredTableTerms1[y] == 0)
        //            {
        //                binEnteredTableTerms1[y] = 1;
        //                sumaKwadratowEnteredtable1[0] += (int)Math.Pow(binEnteredTableTerms1[y], 2);
        //                licznikwystapienDok1[0]++;
        //            }
        //            enteredTableTerms2[y] = (string.Compare(enteredText[i], TDM02[y]));
        //            if (enteredTableTerms2[y] == 0)
        //            {
        //                binEnteredTableTerms2[y] = 1;
        //                sumaKwadratowEnteredtable2[0] += (int)Math.Pow(binEnteredTableTerms2[y], 2);
        //                licznikwystapienDok1[1]++;
        //            }
        //            enteredTableTerms3[y] = (string.Compare(enteredText[i], TDM03[y]));
        //            if (enteredTableTerms3[y] == 0)
        //            {
        //                binEnteredTableTerms3[y] = 1;
        //                sumaKwadratowEnteredtable3[0] += (int)Math.Pow(binEnteredTableTerms3[y], 2);
        //                licznikwystapienDok1[2]++;
        //            }
        //            enteredTableTerms4[y] = (string.Compare(enteredText[i], TDM04[y]));
        //            if (enteredTableTerms4[y] == 0)
        //            {
        //                binEnteredTableTerms4[y] = 1;
        //                sumaKwadratowEnteredtable4[0] += (int)Math.Pow(binEnteredTableTerms4[y], 2);
        //                licznikwystapienDok1[3]++;
        //            }
        //            //Console.WriteLine(binDok01[i]);
        //            //Console.WriteLine("Miejsce 02");
        //        }
        //    }

        //    //Czynnik normalizujący


        //    if (sumaKwadratowEnteredtable1[0] != 0)
        //    {
        //        czynnikNormalizujacy1[0] = 1 / Math.Round(Math.Sqrt(sumaKwadratowEnteredtable1[0]), 2);
        //    }
        //    else
        //    {
        //        czynnikNormalizujacy1[0] = 0;
        //    }
        //    if (sumaKwadratowEnteredtable2[0] != 0)
        //    {
        //        czynnikNormalizujacy1[1] = 1 / Math.Round(Math.Sqrt(sumaKwadratowEnteredtable2[0]), 2);
        //    }
        //    else
        //    {
        //        czynnikNormalizujacy1[1] = 0;
        //    }
        //    if (sumaKwadratowEnteredtable3[0] != 0)
        //    {
        //        czynnikNormalizujacy1[2] = 1 / Math.Round(Math.Sqrt(sumaKwadratowEnteredtable3[0]), 2);
        //    }
        //    else
        //    {
        //        czynnikNormalizujacy1[2] = 0;
        //    }

        //    if (sumaKwadratowEnteredtable4[0] != 0)
        //    {
        //        czynnikNormalizujacy1[3] = 1 / Math.Round(Math.Sqrt(sumaKwadratowEnteredtable4[0]), 2);
        //    }
        //    else
        //    {
        //        czynnikNormalizujacy1[3] = 0;
        //    }


        //    // string do wyświetlenia macierzy TDM01
        //    string str01 = null;
        //    string str02 = null;
        //    string str03 = null;
        //    string str04 = null;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        str01 += binEnteredTableTerms1[i];
        //        str02 += binEnteredTableTerms2[i];
        //        str03 += binEnteredTableTerms3[i];
        //        str04 += binEnteredTableTerms4[i];
        //        //Console.WriteLine("Miejsce 02");
        //    }
        //    Console.WriteLine("Str01 - " + str01);
        //    Console.WriteLine("Str02 - " + str02);
        //    Console.WriteLine("Str03 - " + str03);
        //    Console.WriteLine("Str04 - " + str04);

        //    Console.WriteLine("Licznik Wystapień 00 - " + licznikwystapienDok1[0]);
        //    Console.WriteLine("Licznik Wystapień 01 - " + licznikwystapienDok1[1]);
        //    Console.WriteLine("Licznik Wystapień 02 - " + licznikwystapienDok1[2]);
        //    Console.WriteLine("Licznik Wystapień 03 - " + licznikwystapienDok1[3]);


        //    Console.WriteLine("Suma kwadratów 01 - " + sumaKwadratowEnteredtable1[0]);
        //    Console.WriteLine("Suma kwadratów 02 - " + sumaKwadratowEnteredtable2[0]);
        //    Console.WriteLine("Suma kwadratów 03 - " + sumaKwadratowEnteredtable3[0]);
        //    Console.WriteLine("Suma kwadratów 04 - " + sumaKwadratowEnteredtable4[0]);

        //    Console.WriteLine("Czynnik normalizujący 01 - " + czynnikNormalizujacy1[0]);
        //    Console.WriteLine("Czynnik normalizujący 02 - " + czynnikNormalizujacy1[1]);
        //    Console.WriteLine("Czynnik normalizujący 03 - " + czynnikNormalizujacy1[2]);
        //    Console.WriteLine("Czynnik normalizujący 04 - " + czynnikNormalizujacy1[3]);
        //}

        #region Metoda wystawiająca dane do Ranking dokumentów
        public void macierzRankingDokumentow()
        {
            for (int i = 0; i < 4; i++)
            {                
                answerDok01[i] = poNormalizacjiTDM[i] * (poNormalizacjiTFIDFTDM[4, 0]);
                answerDok02[i] = poNormalizacjiTFTDM[i] * (poNormalizacjiTFIDFTDM[4, 1]);
                answerDok03[i] = doGridTFITDM[i];
                //answerDok04[i] =
            }
        }
        #endregion

        #region Data Grid TDM
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a List of objects.
            var items = new List<TDMList>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new TDMList(TDMAll[i].ToString(),
                    binDok01[i], (czynnikNormalizujacy[0] * binDok01[i]),
                    binDok02[i], (czynnikNormalizujacy[1] * binDok02[i]),
                    binDok03[i], (czynnikNormalizujacy[2] * binDok03[i]),
                    binDok04[i], (czynnikNormalizujacy[3] * binDok04[i]),
                    binDok05[i], (czynnikNormalizujacy[4] * binDok05[i])));
            }
            // ... Assign ItemsSource of DataGrid.
            var grid = sender as DataGrid;
            grid.ItemsSource = items;
        }
        #endregion

        #region Data Grid TFTDM
        private void DataGridTFTDM_Loaded(object sender, RoutedEventArgs e)
                {
                    // ... Create a List of objects.
                    var items = new List<TFTDMList>();
                    for (int i = 0; i < 10; i++)
                    {                
                        items.Add(new TFTDMList(TDMAll[i].ToString(),
                           licznikwystapienTFIDFTDM[0, i], Math.Round(czynnikNormalizujacyTFTDM[0]*licznikwystapienTFTDM[0,i], 3),
                           licznikwystapienTFIDFTDM[1, i], Math.Round(czynnikNormalizujacyTFTDM[1]*licznikwystapienTFTDM[1,i], 3),
                           licznikwystapienTFIDFTDM[2, i], Math.Round(czynnikNormalizujacyTFTDM[2]*licznikwystapienTFTDM[2,i], 3), 
                           licznikwystapienTFIDFTDM[3, i], Math.Round(czynnikNormalizujacyTFTDM[3]*licznikwystapienTFTDM[3,i], 3),
                           licznikwystapienTFIDFTDM[4, i], Math.Round(czynnikNormalizujacyTFTDM[4]*licznikwystapienTFTDM[4,i], 3)));

                    }
                    // ... Assign ItemsSource of DataGrid.
                    var grid = sender as DataGrid;
                    grid.ItemsSource = items;
                }
        #endregion      

        #region Data Grid TFIDFTDM

        private void DataGridTFIDFTDM_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a List of objects.
            var items = new List<TFIDFTDMList>();
            for (int i = 0; i < 10; i++)
            {
                items.Add(new TFIDFTDMList(TDMAll[i].ToString(),
                   licznikwystapienTFIDFTDM[0 , i], Math.Round(poNormalizacjiTFIDFTDM[0,i], 3),
                   licznikwystapienTFIDFTDM[1 , i], Math.Round(poNormalizacjiTFIDFTDM[1,i], 3),
                   licznikwystapienTFIDFTDM[2 , i], Math.Round(poNormalizacjiTFIDFTDM[2,i], 3),
                   licznikwystapienTFIDFTDM[3 , i], Math.Round(poNormalizacjiTFIDFTDM[3,i], 3),
                   licznikwystapienTFIDFTDM[4 , i], Math.Round(poNormalizacjiTFIDFTDM[4,i], 3)));
            }
            // ... Assign ItemsSource of DataGrid.
            var grid = sender as DataGrid;
            grid.ItemsSource = items;
        }
        #endregion

        #region Data Grid Ranking dokumentów

        private void RankingDokumentowList_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a List of objects.
            var items = new List<RankingDokumentowList>();
            for (int i = 0; i < 4; i++)
            {
                items.Add(new RankingDokumentowList(
                   headDokument[i],
                   Math.Round(answerDok01[i], 3),
                   Math.Round(answerDok02[i], 3),
                   Math.Round(answerDok03[i], 3)));
            }
            // ... Assign ItemsSource of DataGrid.
            var grid = sender as DataGrid;
            grid.ItemsSource = items;
        }
        #endregion
    }

    #region Klasa odpowiadająca za wyświetlenie Data Grid TDM

    class TDMList
    {
        public string Term { get; set; }
        public int Dok01 { get; set; }
        public double Dok1Par { get; set; }
        public int Dok02 { get; set; }
        public double Dok2Par { get; set; }
        public int Dok03 { get; set; }
        public double Dok3Par { get; set; }
        public int Dok04 { get; set; }
        public double Dok4Par { get; set; }
        public int Dok05 { get; set; }
        public double Dok5Par { get; set; }
        public TDMList(string term, int dok01, double dok1Par, int dok02, double dok2Par, int dok03, double dok3Par, int dok04, double dok4Par, int dok05, double dok5Par)
        {
            this.Term = term;
            this.Dok01 = dok01;
            this.Dok1Par = dok1Par;
            this.Dok02 = dok02;
            this.Dok2Par = dok2Par;
            this.Dok03 = dok03;
            this.Dok3Par = dok3Par;
            this.Dok04 = dok04;
            this.Dok4Par = dok4Par;
            this.Dok05 = dok05;
            this.Dok5Par = dok5Par;
        }        
    }
}
    #endregion

    #region Klasa odpowiadająca za wyświetlenie Data Grid TFTDM
    class TFTDMList
        {
            public string Term { get; set; }
            public int Dok01 { get; set; }
            public double Dok1Par { get; set; }
            public int Dok02 { get; set; }
            public double Dok2Par { get; set; }
            public int Dok03 { get; set; }
            public double Dok3Par { get; set; }
            public int Dok04 { get; set; }
            public double Dok4Par { get; set; }
            public int Dok05 { get; set; }
            public double Dok5Par { get; set; }

            public TFTDMList(string term, int dok01, double dok1Par, int dok02, double dok2Par, int dok03, double dok3Par, int dok04, double dok4Par, int dok05, double dok5Par)
            {
                this.Term = term;
                this.Dok01 = dok01;
                this.Dok1Par = dok1Par;
                this.Dok02 = dok02;
                this.Dok2Par = dok2Par;
                this.Dok03 = dok03;
                this.Dok3Par = dok3Par;
                this.Dok04 = dok04;
                this.Dok4Par = dok4Par;
                this.Dok05 = dok05;
                this.Dok5Par = dok5Par;
            }
        }
#endregion

    #region Klasa odpowiadająca za wyświetlenie Data Grid TFIDFTDM
class TFIDFTDMList
{
    public string Term { get; set; }
    public int Dok01 { get; set; }
    public double Dok1Par { get; set; }
    public int Dok02 { get; set; }
    public double Dok2Par { get; set; }
    public int Dok03 { get; set; }
    public double Dok3Par { get; set; }
    public int Dok04 { get; set; }
    public double Dok4Par { get; set; }
    public int Dok05 { get; set; }
    public double Dok5Par { get; set; }

    public TFIDFTDMList(string term, int dok01, double dok1Par, int dok02, double dok2Par, int dok03, double dok3Par, int dok04, double dok4Par, int dok05, double dok5Par)
    {
        this.Term = term;
        this.Dok01 = dok01;
        this.Dok1Par = dok1Par;
        this.Dok02 = dok02;
        this.Dok2Par = dok2Par;
        this.Dok03 = dok03;
        this.Dok3Par = dok3Par;
        this.Dok04 = dok04;
        this.Dok4Par = dok4Par;
        this.Dok05 = dok05;
        this.Dok5Par = dok5Par;
    }
}
#endregion

    #region Klasa odpowiadająca za wyświetlenie Data Grid Ranking Dokumentów
    class RankingDokumentowList
    {
        public string Nr_Dok { get; set; }
        public double _TDM { get; set; }
        public double _TFTDM { get; set; }
        public double _TFITDM { get; set; }
        public RankingDokumentowList(string term, double TDM, double TFTDM, double TFITDM)
        {
            this.Nr_Dok = term;
            this._TDM = TDM;
            this._TFTDM = TFTDM;
            this._TFITDM = TFITDM;
        }
    }
#endregion

