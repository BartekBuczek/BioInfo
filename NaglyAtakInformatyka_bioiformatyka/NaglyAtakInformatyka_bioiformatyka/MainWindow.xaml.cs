using System;
using System.Windows;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;
using System.Security;
using System.Text;
using System.Reflection.Emit;
using System.Windows.Media.Media3D;
using Bio;
using Bio.IO;
using System.Runtime.InteropServices;

namespace BioInfa
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

            string rnax = Input.Text;
            string err = "";
            string najdluzsze = "";
            Output1.Content = BudowaBialka.Walidacja(rnax);
            if (BudowaBialka.Walidacja(rnax) == 0)
            {
                char[,] r = new char[3, rnax.Length / 3];
                BudowaBialka.Budowa(rnax, r);

                string[] o = new string[3];
                int start
 = 0; int stop = 0;
                int bialko = 0;
                int[] bialka = new int[1000];
                int najdluzsze_bialko = 0;
                int[] end = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    bool loop = false;
                    end[i] = 0;
                    for (int j = end[i]; j < rnax.Length / 3; j++)
                    {
                        Console.WriteLine("end {0}", end);
                        loop = false;
                        if (r[i, j] == 'M')
                        {
                            start = j + 1;
                            for (int k = j; k < rnax.Length / 3; k++)
                            {
                                if (r[i, k] == 'X' && !loop)
                                {
                                    stop = k + 1;
                                    bialko++;
                                    loop = true;
                                    // Console.WriteLine("ilość białek {0}", bialko);
                                    for (int index = j; index <= k; index++)
                                    {
                                        o[i] += Convert.ToString(r[i, index]);
                                        Console.WriteLine("Powstajace bialko {0}", o[i]);
                                    }
                                    end[i] = k;
                                    //Console.WriteLine("Start {0}, Stop {1}", start, stop);
                                    bialka[bialko] = stop - start;
                                }
                            }
                        }

                    }
                    //Console.WriteLine("ilość białek {0}", bialko);
                    najdluzsze_bialko = bialka[1];
                    for (int index = 2; index <= bialko; index++)
                    {
                        if (najdluzsze_bialko <= bialka[index])
                        {
                            najdluzsze_bialko = bialka[index];
                        }
                    }
                    najdluzsze += o[i];
                    Console.WriteLine("Najdluzsze {0}", najdluzsze);
                }
                najdluzsze = BudowaBialka.NajdluzszeBialko(najdluzsze_bialko, najdluzsze);
                Output1.Content = o[0];
                Output2.Content = o[1];
                Output3.Content = o[2];
                // Console.WriteLine(najdluzsze);
            }
            else if (BudowaBialka.Walidacja(rnax) == 1)
            {
                rnax = BudowaBialka.Podmianka(rnax);
                //Output1.Content = rnax;
                char[,] r = new char[3, rnax.Length / 3];
                BudowaBialka.Budowa(rnax, r);

                string[] o = new string[3];
                int start = 0;
                int stop = 0;
                int bialko = 0;
                int[] bialka = new int[1000];
                int najdluzsze_bialko = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < rnax.Length / 3; j++)
                    {
                        bool loop = false;
                        if (r[i, j] == 'M')
                        {
                            start = j + 1;
                            for (int k = j; k < rnax.Length / 3; k++)
                            {
                                if (r[i, k] == 'X' && !loop)
                                {
                                    stop = k + 1;
                                    bialko++;
                                    loop = true;
                                    // Console.WriteLine("ilość białek {0}", bialko);
                                    for (int index = j; index <= k; index++)
                                    {
                                        o[i] += Convert.ToString(r[i, index]);
                                        Console.WriteLine("Powstajace bialko {0}", o[i]);
                                    }
                                    //Console.WriteLine("Start {0}, Stop {1}", start, stop);
                                    bialka[bialko] = stop - start;
                                }
                            }
                        }

                    }
                    //Console.WriteLine("ilość białek {0}", bialko);
                    najdluzsze_bialko = bialka[1];
                    for (int index = 2; index <= bialko; index++)
                    {
                        if (najdluzsze_bialko <= bialka[index])
                        {
                            najdluzsze_bialko = bialka[index];
                        }
                    }
                    najdluzsze += o[i];
                    Console.WriteLine("Najdluzsze {0}", najdluzsze);
                }
                najdluzsze = BudowaBialka.NajdluzszeBialko(najdluzsze_bialko, najdluzsze);
                Output1.Content = o[0];
                Output2.Content = najdluzsze;
                Output3.Content = o[2];
            }
            else
            {
                Output1.Content = "Błąd we wprowadzonej składni!!";
            }
            // Obliczenie pozycji atomów i wiązań w białkowej strukturze
        }
        void Reset_Click(object sender, RoutedEventArgs e)
        {
            Input.Text = "";
        }
        void Choose_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog File = new OpenFileDialog()
            {
                FileName = "Select a text file",
                Filter = "Text files (*.txt)|*.txt",
                Title = "Open text file"
            };

            try
            {
                File.ShowDialog();
                StreamReader sr = new StreamReader(File.FileName);
                string Line = sr.ReadLine();
                StringBuilder ans = new StringBuilder();

                while (Line != null)
                {
                    Console.WriteLine(Line);
                    ans.Append(Line);
                    Line = sr.ReadLine();
                }
                sr.Close();
                Input.Text = ans.ToString();
            }
            catch (SecurityException ex)
            {
                string msg = ex.Message;
                MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Output1.Content = "File succesfully opened";
            }
        }

    }
}
//AAAUGAACGAAAAUCUGUUCGCUUCAUUCAUUGCCCCCACAAUCCUAGGCCUACCC
//GUCAGAGUAAUGUCAAAGUGCUUACAGUGCAGGUAGUGAUAUAUAGAACCUACUGCAGUGAAGGCACUUGUAGCAUUAUAGUGACAA