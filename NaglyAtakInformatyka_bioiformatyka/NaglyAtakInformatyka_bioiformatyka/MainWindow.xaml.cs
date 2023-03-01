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
//using HelixToolkit.Wpf;
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
            
            int b = BudowaBialka.Walidacja(rnax);
            start:
            

            //Output1.Content = BudowaBialka.Walidacja(rnax);
            if (b == 0)
            {
                char[,] r = new char[3,rnax.Length/3];
                BudowaBialka.Budowa(rnax, r);


                string[] o = new string[3];

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < rnax.Length / 3; j++)
                    {
                        if (r[i, j] == 'M')
                        {
                            for (int k = 0; k < rnax.Length / 3; k++)
                            {
                                if (r[i, k] == 'X')
                                {
                                    for (int index = 0; index < rnax.Length / 3; index++)
                                    {
                                        o[i] += Convert.ToString(r[i, index]);
                                    }
                                }
                            }
                        }
                    }

                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < rnax.Length / 3; j++)
                    {
                        o[i] += r[i,j];
                    }
                    
                }

                Output1.Content = o[0];
                Output2.Content = o[1];
                Output3.Content = o[2];
            }
            else if(b == 1)
            {
                rnax = BudowaBialka.Podmianka(rnax);
                b = 0;
                goto start;
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
        public class Window2{
            
        }
    }
}
    //AAAUGAACGAAAAUCUGUUCGCUUCAUUCAUUGCCCCCACAAUCCUAGGCCUACCC
    //GUCAGAGUAAUGUCAAAGUGCUUACAGUGCAGGUAGUGAUAUAUAGAACCUACUGCAGUGAAGGCACUUGUAGCAUUAUAGUGACAA