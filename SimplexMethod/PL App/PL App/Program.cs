using System.Data.Common;

namespace PL_App
{
    class Method_Simplex
    {
        public static void Display()
        {
            ConsoleColor Red = ConsoleColor.Red;
            ConsoleColor Green = ConsoleColor.DarkGreen;
            ConsoleColor Blue = ConsoleColor.Blue;
            Console.ForegroundColor = Red;
            // Console.WriteLine("      == La Methode du SIMPLEX ==      ");
            string Word = @"
  _              __  __     _   _            _             _           ___ ___ __  __ ___ _    _____  __
 | |   __ _     |  \/  |___| |_| |_  ___  __| |___      __| |_  _     / __|_ _|  \/  | _ \ |  | __\ \/ /
 | |__/ _` |    | |\/| / -_)  _| ' \/ _ \/ _` / -_)    / _` | || |    \__ \| || |\/| |  _/ |__| _| >  < 
 |____\__,_|    |_|  |_\___|\__|_||_\___/\__,_\___|    \__,_|\_,_|    |___/___|_|  |_|_| |____|___/_/\_\                                                                                           
";
            Console.WriteLine(Word);
            Console.WriteLine(" ");
            Console.ResetColor();
            Console.WriteLine("*** Entrer La Fonction Objectif est Le Contraintes Sur La Form Canonique ***");
            Console.WriteLine(" ");
            Console.ForegroundColor = Green;
            Console.WriteLine("----------- Information --------------");
            Console.WriteLine(" ");
            Console.WriteLine("-- La Fonction Objectif: ");

            Console.WriteLine("Exemple: 20X1+30X2+0t1+0t2+0t3 ");
            Console.WriteLine("Entrez Comme ça : X1= 20 | X2= 30 | X3=t1= 0 | X4=t2= 0 | X5=t3= 0");
            Console.WriteLine(" ");
            Console.WriteLine("-- Le Contraintes: ");
            Console.WriteLine("Exemple: X1+3X2+t1+0+0 = 18 ");
            Console.WriteLine("Entrez Comme ça : X1= 1 | X2= 3 | X3=t1= 1 | X4=t2= 0 | X5=t3= 0 | X6= 18 ... ");
            Console.WriteLine(" ");
            Console.WriteLine("Nomber  de  Variable Total: 7 (X+t+C) ");

            Console.WriteLine("-------------------------------------- ");
            Console.ResetColor();
            Console.ForegroundColor = Blue;
            //Console.WriteLine("******* le Contraintes ******* ");
            Console.ResetColor();
            Console.WriteLine(" ");
        }
        public static void DisplayResult()
        {
            ConsoleColor Green = ConsoleColor.Green;
            ConsoleColor RED = ConsoleColor.Red;
            Console.WriteLine(" ");
            Console.WriteLine("---------------------------");
            string F = @"
  _____ ___ _   _ ____  _   _ 
 |  ___|_ _| \ | / ___|| | | |
 | |_   | ||  \| \___ \| |_| |
 |  _|  | || |\  |___) |  _  |
 |_|   |___|_| \_|____/|_| |_| 
";
            // Console.WriteLine("---------- FINSH ---------");
            Console.WriteLine(F);
            Console.ForegroundColor = RED;
            Console.WriteLine(" ");
            Console.WriteLine("****** On a Arrete ******");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("-- Parce que les coff des Variable (X) dons la Ligne * DELTA * Sont <= 0 alore on a ARRETE ");
            Console.ForegroundColor = Green;
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("--- Le Resultat ---");
            Console.WriteLine(" ");
            Console.WriteLine("Variable HB: ");
            Console.ResetColor();
        }
        //------------------------------------------------------------------
        //Fonction Calcule Max Pivot(Column)
        public static double Max(int Row, int Column, double[,] tabSimplex)
        {
            double MaxPivot = tabSimplex[Row - 1, 0];
            int Position = 0;
            for (int i = 0; i < Column; i++)
            {
                //Console.WriteLine(tabSimplex[Row - 1, i]);

                if (MaxPivot < tabSimplex[Row - 1, i])
                {
                    MaxPivot = tabSimplex[Row - 1, i];
                    Position = i;
                }

            }
            ConsoleColor Green = ConsoleColor.DarkGreen;
            Console.ForegroundColor = Green;
            Console.WriteLine("----------- Max ------------");
            Console.WriteLine("Le Max:" + MaxPivot);
            Console.WriteLine("La Position:" + Position);
            Console.WriteLine("-----------------------");
            Console.ResetColor();
            return Position;
        }
        //Fonction Calcule Min Pivot(Row)
        public static double Min(int Row, int Column, double Position, double[,] tabSimplex)
        {
            int PivotPosition = 0;
            double MaxPosition = Max(Row, Column, tabSimplex);
            double FirstDivision = tabSimplex[0, Column - 1] / tabSimplex[0, Convert.ToInt32(Position)];
            double ColumnDivision = 0;
            for (int i = 0; i < Row - 1; i++)
            {
                Console.WriteLine("La Valeur de La Colonne PIVOT:" + tabSimplex[i, Column - 1]);
                if (FirstDivision < 0)
                {

                    FirstDivision = tabSimplex[1, Column - 1] / tabSimplex[1, Convert.ToInt32(Position)];

                }
                else if (tabSimplex[i, Convert.ToInt32(MaxPosition)] < 0)
                {
                    continue;
                }
                else
                {
                    ColumnDivision = tabSimplex[i, Column - 1] / tabSimplex[i, Convert.ToInt32(Position)];
                    Console.WriteLine("La Resultat de Division:  " + ColumnDivision);
                    if (ColumnDivision < FirstDivision)
                    {
                        FirstDivision = ColumnDivision;
                        PivotPosition = i;
                    }
                }

            }
            ConsoleColor Blue = ConsoleColor.Blue;
            Console.ForegroundColor = Blue;
            Console.WriteLine("----------- Min ------------");
            Console.WriteLine("Le Min:" + FirstDivision);
            Console.WriteLine("La Position:" + PivotPosition);
            Console.WriteLine("-----------------------");
            Console.ResetColor();
            return PivotPosition;
        }
        //Fonction Calcule les Tableau 2,3,...
        public static void TableCalcuation(int Row, int Column, double MaxPivotPosition, double MinPivotPosition, double[,] tabSimplex, double[,] TabSimplexDraft, double[] ArrayDraft, double Var, int Nomber_X)
        {

            MaxPivotPosition = Max(Row, Column, tabSimplex);
            MinPivotPosition = Min(Row, Column, MaxPivotPosition, tabSimplex);
            //double[] MinPivot=new double[Row];
            //int o = 0;
            //MinPivot[o] = MinPivotPosition;
            //o++;

            Console.WriteLine("-----------Division La Colonne PIVOT------------");
            for (int i = 0; i < Column; i++)
            {
                //TabSimplexDraft:جدول جانبي من اجل حساب    
                TabSimplexDraft[Convert.ToInt32(MinPivotPosition), i] = tabSimplex[Convert.ToInt32(MinPivotPosition), i] / tabSimplex[Convert.ToInt32(MinPivotPosition), Convert.ToInt32(MaxPivotPosition)];
                ArrayDraft[i] = tabSimplex[Convert.ToInt32(MinPivotPosition), i] / tabSimplex[Convert.ToInt32(MinPivotPosition), Convert.ToInt32(MaxPivotPosition)];
                Console.WriteLine("Resultat de Division La Colonne PIVOT:" + TabSimplexDraft[Convert.ToInt32(MinPivotPosition), i]);

            }
            Var = 0;
            //ArrayDraft2:جدول جانبي من اجل حساب
            double[] ArrayDraft2 = new double[Column];

            for (int i = 0; i < Row; i++)
            {

                if (tabSimplex[i, Convert.ToInt32(MaxPivotPosition)] == tabSimplex[Convert.ToInt32(MinPivotPosition), Convert.ToInt32(MaxPivotPosition)])
                {
                    continue;
                }
                else if (tabSimplex[i, Convert.ToInt32(MaxPivotPosition)] == 0)
                {
                    for (int j = 0; j < Column; j++)
                    {

                        ArrayDraft2[j] = tabSimplex[i, j];
                        TabSimplexDraft[i, j] = ArrayDraft2[j];
                    }
                    continue;
                }
                else
                {
                    Var = tabSimplex[i, Convert.ToInt32(MaxPivotPosition)] * (-1);
                    Console.WriteLine("------------");
                    Console.WriteLine("La Valeur Pour Calculer les Elements du Tableau:" + Var);
                }
                for (int j = 0; j < Column; j++)
                {

                    TabSimplexDraft[i, j] = (Var * ArrayDraft[j]) + tabSimplex[i, j];

                }
            }
            int o=2;

            Console.WriteLine(" ");
            Console.WriteLine("--------- TABLEAU "+(o)+" ----------");
            Console.WriteLine(" ");
            o++;
            for (int i = 0; i < Row; i++)
            {
                if (i == Row - 1)
                {
                    Console.WriteLine(" ");
                    Console.Write("DELTA * [" + i + "] :");
                }
                else
                    Console.Write("La Ligne[" + i + "] :");
                for (int j = 0; j < Column; j++)
                {
                    tabSimplex[i, j] = TabSimplexDraft[i, j];
                    if (i == Row - 1)
                    {
                        Console.Write("   |" + Math.Round(tabSimplex[i, j], 3) + "| *    ");
                    }
                    else
                    {
                        Console.Write("   |" + Math.Round(tabSimplex[i, j], 3) + "| *  ");
                    }
                }
                Console.WriteLine(" ");
            }
            bool Finsh = false;
            for (int i = 0; i < Column; i++)
            {
                if (tabSimplex[Row - 1, i] > 0)
                {
                    Finsh = true;
                }

            }
            for (int j = 0; j < Column; j++)
            {
                if (Finsh == true)
                {
                    TableCalcuation(Row, Column, MaxPivotPosition, MinPivotPosition, tabSimplex, TabSimplexDraft, ArrayDraft, Var, Nomber_X);

                }
                else
                {

                    //Console.Write("[" + j + "]  : ");
                    //Console.WriteLine(tabSimplex[Row - 1, j]);
                }
            }
            //دالة من اجل طباعة شكل نتيجة
            DisplayResult();
            ConsoleColor Green = ConsoleColor.Green;

            Console.ForegroundColor = Green;
            for (int i = 0; i < Column - 1; i++)
            {
                //Console.WriteLine("M: " + MinPivot[i]);
                if (tabSimplex[Row - 1, i] < 0)
                {
                    Console.WriteLine("T:" + Convert.ToInt64(tabSimplex[Row - 1, i]));
                }
            }
            if (tabSimplex[Row - 1, 1] == 0)
            {
                for (int j = 0; j < Row; j++)
                {
                    for (int k = 0; k < Nomber_X; k++)
                    {
                        if (tabSimplex[j, k] == 1)
                        {
                            Console.WriteLine("X:" + Convert.ToInt64(tabSimplex[j, Column - 1]));
                        }
                    }
                }
            }
            Console.WriteLine(" ");
            Console.WriteLine("Z:" + Convert.ToInt64(tabSimplex[Row - 1, Column - 1]) * -1);
            Console.WriteLine("-------------------");
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            ConsoleColor Red = ConsoleColor.Red;
            //دالة من اجل طباعة شكل مقدمة
            Display();

            int column;
            int row;
            Console.Write("Entrer Nomber  de  Contraintes Total + la Fonction Objectif : ");
            row = Convert.ToInt32(Console.ReadLine());
            Console.Write("Entrer Nomber  de  Variable Total : ");
            column = Convert.ToInt32(Console.ReadLine());

            double[,] tabSimplex = new double[row, column];
            //create and fill the Table
            Console.WriteLine(" ");
            Console.WriteLine("Taille du Tableau : " + tabSimplex.Length);
            Console.WriteLine(" ");
            bool Test = false;
            //Nomber_X: حساب عددهم من اجل اخراج نتائج في الاخير
            int Nomber_X = 0;
            for (int i = 0; i < row; i++)
            {
                if (i == row - 1)
                {
                    Console.WriteLine("--Entrer les Coefficient Variable de La Fonction Objectif : ");
                    Test = true;
                }
                else
                    Console.WriteLine("--Entrer les Coefficient Variable de Contraintes : " + (i + 1));
                for (int j = 0; j < column; j++)
                {
                    Console.Write("\n     |X" + (j + 1) + "|");
                    //Console.Write("\n |t" + i + ":|");
                    tabSimplex[i, j] = Convert.ToDouble(Console.ReadLine());
                    if (Test == true)
                    {
                        if (tabSimplex[i, j] > 0)
                            Nomber_X = Nomber_X + 1;
                    }
                }
            }
            Console.WriteLine("---------Nomber X----------" + Nomber_X);
            Console.WriteLine("-------------------");
            /////-----------TAB 0-----------/////
            double MaxPosition, MinPosition;
            MaxPosition = Max(row, column, tabSimplex);
            MinPosition = Min(row, column, MaxPosition, tabSimplex);
            Console.ForegroundColor = Red;
            Console.WriteLine("-----------PIVOT------------");
            Console.WriteLine("La Valeur de PIVOT :" + tabSimplex[Convert.ToInt32(MinPosition), Convert.ToInt32(MaxPosition)]);
            Console.WriteLine("-----------------------");
            Console.ResetColor();
            /////-----------TAB 1-----------/////
            double[,] TabSimplexDraft = new double[row, column];
            double[] ArrayDraft = new double[column];
            double[] ArrayDraft2 = new double[column];
            // double LinePivotDivition;
            Console.WriteLine("-----------Division La Colonne PIVOT------------");
            for (int i = 0; i < column; i++)
            {

                TabSimplexDraft[Convert.ToInt32(MinPosition), i] = tabSimplex[Convert.ToInt32(MinPosition), i] / tabSimplex[Convert.ToInt32(MinPosition), Convert.ToInt32(MaxPosition)];
                ArrayDraft[i] = tabSimplex[Convert.ToInt32(MinPosition), i] / tabSimplex[Convert.ToInt32(MinPosition), Convert.ToInt32(MaxPosition)];
                Console.WriteLine("Resultat de Division La Colonne PIVOT:" + TabSimplexDraft[Convert.ToInt32(MinPosition), i]);


            }

            //Console.WriteLine("-------------------");
            //Console.WriteLine("-----------fill table Draft------------");
            double Var = 0;

            for (int i = 0; i < row; i++)
            {

                if (tabSimplex[i, Convert.ToInt32(MaxPosition)] == tabSimplex[Convert.ToInt32(MinPosition), Convert.ToInt32(MaxPosition)])
                {

                    continue;

                }
                else if (tabSimplex[i, Convert.ToInt32(MaxPosition)] == 0)
                {
                    for (int j = 0; j < column; j++)
                    {

                        ArrayDraft2[j] = tabSimplex[i, j];
                        TabSimplexDraft[i, j] = ArrayDraft2[j];
                    }
                    continue;
                }
                else
                {

                    Var = tabSimplex[i, Convert.ToInt32(MaxPosition)] * (-1);
                    Console.WriteLine("------------");
                    Console.WriteLine("La Valeur Pour Calculer les Elements du Tableau:" + Var);
                }

                for (int j = 0; j < column; j++)
                {
                    TabSimplexDraft[i, j] = (Var * ArrayDraft[j]) + tabSimplex[i, j];


                }
            }
            Console.WriteLine(" ");
            Console.WriteLine("--------- TABLEAU 1 ----------");
            Console.WriteLine(" ");
            for (int i = 0; i < row; i++)
            {
                if (i == row - 1)
                {
                    Console.WriteLine(" ");
                    Console.Write("DELTA * [" + i + "] :");
                }
                else
                    Console.Write("La Ligne[" + i + "] :");
                for (int j = 0; j < column; j++)
                {
                    tabSimplex[i, j] = TabSimplexDraft[i, j];
                    if (i == row - 1)
                    {
                        Console.Write("   |" + Math.Round(tabSimplex[i, j], 3) + "| *    ");
                    }
                    else
                    {
                        
                        Console.Write("   |" + Math.Round(tabSimplex[i, j], 3) + "| *  ");
                    }


                }
                Console.WriteLine(" ");
            }
            TableCalcuation(row, column, MaxPosition, MinPosition, tabSimplex, TabSimplexDraft, ArrayDraft, Var, Nomber_X);





        }
    }
}