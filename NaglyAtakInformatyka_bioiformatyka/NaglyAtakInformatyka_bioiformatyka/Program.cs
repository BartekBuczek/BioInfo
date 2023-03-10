using System;
using System.Reflection.Emit;
using System.Windows.Media.Imaging;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;
using Bio.Core;
using Bio.Phylogenetics;
using Bio.Algorithms.Translation;
using Npgsql;
using Microsoft.AspNetCore.Hosting;
using JetBrains.Annotations;

namespace BioInfa
{
    public static class BudowaBialka
    {
        public static string Test_Dlugosci(string rnax)
        {
            string gotowe = "";
            if (rnax.Length % 3 == 0)
            {
                gotowe = rnax;
            }
            else if (rnax.Length % 3 == 1)
            {
                gotowe = "Nieprawidłowość: Podałeś 1 znak za dużo";
            }
            else
            {
                gotowe = "Nieprawidłowość: Podałeś 2 znak za dużo";
            }
            return gotowe;
        }
        public static int Walidacja(string RNACODE)
        {
            string Test = RNACODE.ToLower();
            int t = 0;
            for (int i = 0; i < Test.Length; i++)
            {
                switch (Test[i])
                {
                    case 'a':
                    case 'u':
                    case 'g':
                    case 'c':
                        break;
                    case 't':
                    case ' ':
                        t = 1;
                        break;
                    default:
                        t = 2;
                        break;
                }
            }
            return t;
        }
        public static string Podmianka(string RNACODE)
        {

            string dnat = "";
            RNACODE = RNACODE.ToLower();

            for (int i = 0; i < RNACODE.Length; i++)
            {
                switch (RNACODE[i])
                {
                    case 't':
                        dnat += 'u';
                        break;
                    case ' ':
                        break;
                    default:
                        dnat += RNACODE[i];
                        break;
                }
            }
            Console.WriteLine(dnat.Length);

            return dnat;
        }
        //public static string Usuwanie(string RNACODE)
        //{
        //    string dnat = RNACODE;
        //    string gotowyKod = "";
        //    for(int i = 0; i < RNACODE.Length; i++)
        //    {
        //        switch (RNACODE[i])
        //        {
        //            case ' ':
        //                dnat.Length -= 1;
        //                break;
        //        }
        //    }

        //}


        public static void Budowa(string rnaCode, char[,] tab)
        {
            string rna;
            rna = rnaCode;
            rna = rna.ToLower();
            // n - długość stringa
            int n = rna.Length;
            //rnattp -rna tabela trójkami przesunięcie
            string[,] rnattp = new string[3, n / 3];

            int[] l = new int[3];
            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < n / 3; j++)
                {



                    Przesuniecie(i, j, n, l);

                    string t = Convert.ToString(rna[l[0]]) + Convert.ToString(rna[l[1]]) + Convert.ToString(rna[l[2]]);

                    rnattp[i, j] = t;

                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < n / 3; j++)
                {
                    Console.Write(rnattp[i, j] + " ");
                }
            }
            //r - rozwiązanie
            char[,] r = new char[3, n / 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < n / 3; j++)
                {
                    switch (rnattp[i, j])
                    {
                        case "aaa":
                        case "aag":
                            r[i, j] = 'K';
                            break;
                        case "aac":
                        case "aau":
                            r[i, j] = 'N';
                            break;
                        case "aga":
                        case "agg":
                            r[i, j] = 'R';
                            break;
                        case "agc":
                        case "agu":
                            r[i, j] = 'S';
                            break;
                        case "aca":
                        case "acg":
                        case "acc":
                        case "acu":
                            r[i, j] = 'T';
                            break;
                        case "aua":
                            r[i, j] = 'I';
                            break;
                        case "aug":
                            r[i, j] = 'M';
                            break;
                        case "auc":
                        case "auu":
                            r[i, j] = 'I';
                            break;
                        case "caa":
                        case "cag":
                            r[i, j] = 'Q';
                            break;
                        case "cac":
                        case "cau":
                            r[i, j] = 'H';
                            break;
                        case "cga":
                        case "cgg":
                        case "cgc":
                        case "cgu":
                            r[i, j] = 'R';
                            break;
                        case "cca":
                        case "ccg":
                        case "ccc":
                        case "ccu":
                            r[i, j] = 'P';
                            break;
                        case "cua":
                        case "cug":
                        case "cuc":
                        case "cuu":
                            r[i, j] = 'L';
                            break;
                        case "uaa":
                        case "uag":
                            r[i, j] = 'X';
                            break;
                        case "uac":
                        case "uau":
                            r[i, j] = 'Y';
                            break;
                        case "uga":
                            r[i, j] = 'X';
                            break;
                        case "ugg":
                            r[i, j] = 'W';
                            break;
                        case "ugc":
                        case "ugu":
                            r[i, j] = 'C';
                            break;
                        case "uca":
                        case "ucg":
                        case "ucc":
                        case "ucu":
                            r[i, j] = 'S';
                            break;
                        case "uua":
                        case "uug":
                            r[i, j] = 'L';
                            break;
                        case "uuc":
                        case "uuu":
                            r[i, j] = 'F';
                            break;
                        case "gaa":
                        case "gag":
                            r[i, j] = 'E';
                            break;
                        case "gac":
                        case "gau":
                            r[i, j] = 'D';
                            break;
                        case "gga":
                        case "ggg":
                        case "ggc":
                        case "ggu":
                            r[i, j] = 'G';
                            break;
                        case "gca":
                        case "gcg":
                        case "gcc":
                        case "gcu":
                            r[i, j] = 'A';
                            break;
                        case "gua":
                        case "gug":
                        case "guc":
                        case "guu":
                            r[i, j] = 'V';
                            break;
                    }
                }

            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < n / 3; j++)
                {
                    tab[i, j] = r[i, j];
                }

            }

            //string protein = null;
            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < n / 3; j++)
            //    {
            //       Console.Write(r[i, j] + " ");
            //        if (r[i, j] == 'M')
            //        {
            //            protein = r[i, j].ToString();
            //            for (int k = 0; k < n / 3; k++)
            //            {
            //                if (r[i, k] == 'X')
            //                {
            //                    for (int index = 0; index < n / 3; index++)
            //                    {
            //                        protein = r[i, index].ToString();
            //                    }
            //                }
            //            }


            //        }
            //    }
            //}
            //Console.WriteLine("Przy tym kodzie RNA oragnizm będzie wytwarzał niżej wymienione białka:");
            //Console.WriteLine();


            //for (int i = 0; i < 3; i++)
            //{
            //    for (int j = 0; j < n / 3; j++)
            //    {
            //        Console.WriteLine("przesuniecie {0}", i);
            //        switch (r[i, j])
            //        {
            //            case 'M':
            //                Console.WriteLine("Methonine START");
            //                break;
            //            case 'R':
            //                Console.WriteLine("Arginine");
            //                break;
            //            case 'H':
            //                Console.WriteLine("Histidine");
            //                break;
            //            case 'K':
            //                Console.WriteLine("Lysine");
            //                break;
            //            case 'D':
            //                Console.WriteLine("Aspartic Acid");
            //                break;
            //            case 'E':
            //                Console.WriteLine("Glutamic Acid");
            //                break;
            //            case 'S':
            //                Console.WriteLine("Serine");
            //                break;
            //            case 'T':
            //                Console.WriteLine("Threonine");
            //                break;
            //            case 'N':
            //                Console.WriteLine("Asparagine");
            //                break;
            //            case 'Q':
            //                Console.WriteLine("Glutamine");
            //                break;
            //            case 'C':
            //                Console.WriteLine("Cysteine");
            //                break;
            //            case 'U':
            //                Console.WriteLine("Selenocysteine");
            //                break;
            //            case 'G':
            //                Console.WriteLine("Glycine");
            //                break;
            //            case 'P':
            //                Console.WriteLine("Proline");
            //                break;
            //            case 'A':
            //                Console.WriteLine("Alanine");
            //                break;
            //            case 'V':
            //                Console.WriteLine("Valine");
            //                break;
            //            case 'I':
            //                Console.WriteLine("Isoleucine");
            //                break;
            //            case 'L':
            //                Console.WriteLine("Leucine");
            //                break;
            //            case 'F':
            //                Console.WriteLine("Phenylalanine");
            //                break;
            //            case 'Y':
            //                Console.WriteLine("Tyrosine");
            //                break;
            //            case 'W':
            //                Console.WriteLine("Tryptophan");
            //                break;
            //            case 'X':
            //                Console.WriteLine("STOP");
            //                break;
            //        }
            //    }
        }
        public static string NajdluzszeBialko(int najdluzsze_bialko, string rna)
        {
            int start = 0, end, protein;
            string gotowe = "";
            for (int j = 0; j < rna.Length; j++)
            {
                if (rna[j] == 'M')
                {
                    start = j + 1;
                    for (int k = j; k < rna.Length; k++)
                    {
                        bool loop = false;
                        if (rna[k] == 'X')
                        {
                            end = k + 1;
                            protein = end - start;

                            if (najdluzsze_bialko == protein && !loop)
                            {
                                for (int index = j; index <= k; index++)
                                {
                                    gotowe += rna[index];
                                }
                                goto koniec;
                            }
                            else
                            {
                                start = 0; end = 0;
                            }
                        }
                    }
                }
            }
        koniec: return gotowe;
        }


        static void Przesuniecie(int i, int j, int n, int[] l)
        {

            switch (i)
            {
                case 0:
                    l[0] = j * 3;
                    l[1] = j * 3 + 1;
                    l[2] = j * 3 + 2;
                    break;
                case 1:
                    l[0] = (j + 1) * 3 - 2;
                    l[1] = (j + 1) * 3 - 1;
                    l[2] = (j + 1) * 3;
                    if (l[2] >= n)
                    {
                        l[2] = l[2] - n;
                    }
                    break;
                case 2:
                    l[0] = (j + 1) * 3 - 1;
                    l[1] = (j + 1) * 3;
                    l[2] = (j + 1) * 3 + 1;
                    if (l[1] >= n)
                    {
                        l[1] = l[1] - n;
                        l[2] = l[2] - n;
                    }
                    else if (l[2] >= n)
                    {
                        l[2] = l[2] - n;
                    }
                    break;
            }

        }

    }
}