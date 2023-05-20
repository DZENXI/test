using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _5_laboratory;

namespace ConsoleApp13
{
  
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1- MyFrac, 2 - Student");
            int x = int.Parse(Console.ReadLine());
            switch(x)
            {
                case 1:
                    Myfrackmainpart();
                    break;
                    case 2:
                    Block2();
                    break;

            }
        }

            static double DoubleValue(MyFrac f)
        {
            return (double)f.nom / f.denom;
        }
        static void ToStringWithIntegerPart(MyFrac f)
        {
            string p = Convert.ToString(Math.Truncate((double)f.nom / f.denom));


            if (f.nom > 0)
            {
                f.nom -= f.denom * int.Parse(p);
                if (f.nom == 0)
                {
                    Console.WriteLine($"+{p}");
                }
                else
                    Console.WriteLine($"{p}+{f.nom}/{f.denom}");
            }
            else
            {
                f.nom += f.denom * int.Parse(p);
                if (f.nom == 0)
                {
                    Console.WriteLine($"-{p}");
                }
                else
                    Console.WriteLine($"{p}-{f.nom}/{f.denom}");
            }
        }
        static MyFrac Plus(MyFrac f1, MyFrac f2)
        {
            return new MyFrac(f1.nom * f2.denom + f1.denom * f2.nom,
            f1.denom * f2.denom);
        }

        static MyFrac Minus(MyFrac f1, MyFrac f2)
        {
            return new MyFrac(f1.nom * f2.denom - f1.denom * f2.nom,
           f1.denom * f2.denom);
        }

        static MyFrac Multiply(MyFrac f1, MyFrac f2)
        {
            return new MyFrac(f1.nom * f2.nom, f1.denom * f2.denom);
        }

        static MyFrac Divide(MyFrac f1, MyFrac f2)
        {
            MyFrac temp = new MyFrac(f2.denom, f2.nom);
            return Multiply(f1, temp);
        }

        static void Input(ref string symbol, ref string[] firstfrac, ref string[] secondfrac, ref bool zero)
        {
            Console.WriteLine("Введiть дрiб,якщо дрiб має цiлу частину дробову частину запишiть в дужках.\nTакож укажiть арефметичну операцiю мiж дробами.");
            string[] data = Console.ReadLine().Trim().Split('(', ' ', ')', '/');

            for (int i = 0; i < data.Length; i++)//пошук символа
            {
                if ((data[i].Length == 0) || (data[i].Length == 1))
                {
                    if ((data[i] == "+") || (data[i] == "-") || (data[i] == "*"))
                    {
                        symbol = data[i];
                        break;
                    }
                    if (data[i] == "")
                    {
                        symbol = "/";
                    }
                }
            }
            if (symbol == "/")
            {
                data[data.Length / 2] = "/";
            }
            List<string> list = new List<string>();
            string[] variable = firstfrac;
            // розбиття дробу по елементно
            int counter = 0, cell = 0; long firstnumber = 0, secondnumber = 0, full_part = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != "")
                {
                    list.Add(data[i]);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {

                if ((list[i]) != symbol)
                {
                    if (counter != 2)
                    {
                        variable[cell] = list[i];
                        counter++;
                        cell++;
                    }
                    else
                    {
                        full_part = long.Parse(variable[0]);
                        variable[0] = variable[1];
                        variable[1] = list[i];
                    }
                }
                else
                {

                    firstnumber = full_part;
                    full_part = 0;
                    counter = 0; cell = 0;
                    variable = secondfrac;
                }
            }
            if (full_part != 0)
            {
                secondnumber = full_part;
            }

            if (long.Parse(firstfrac[1]) == 0 || long.Parse(secondfrac[1]) == 0)//перевірка
            {
                Console.WriteLine("Знаменник дорiвнює нулю");
                zero = true;
                return;
            }
            if (firstnumber != 0)//якщо дріб має цілу частину
            {
                firstfrac[0] = Convert.ToString((firstnumber * long.Parse(firstfrac[1])) + long.Parse(firstfrac[0]));
            }
            if (secondnumber != 0)
            {
                secondfrac[0] = Convert.ToString((secondnumber * long.Parse(secondfrac[1])) + long.Parse(secondfrac[0]));
            }
        }
        static MyFrac CalcSum1(int n)
        {
            MyFrac first = new MyFrac(1, 2);
            for (int j = 2; j <= n; j++)
            {
                MyFrac flex = new MyFrac(1, j * (j + 1));
                first = Plus(first, flex);
            }
            return first;
        }
        static MyFrac CalcSum2(int n)
        {
            //(1–1/4)*(1–1/9)*(1–1/16)*...*(1–1/n2).
            MyFrac res = new MyFrac(3, 4);
            for (int j = 3; j <= n; j++)
            {
                MyFrac add = new MyFrac(j * j - 1, j * j);
                res = Multiply(res, add);
            }
            return res;
        }
        static void Myfrackmainpart()
        {
            {
                string symbol = ""; bool zero = false;
                string[] firstfrac = new string[2],
                    secondfrac = new string[2];
                Input(ref symbol, ref firstfrac, ref secondfrac, ref zero);
                if (zero)
                {
                    return;
                }
                MyFrac f1 = new MyFrac(long.Parse(firstfrac[0]), long.Parse(firstfrac[1]));
                MyFrac f2 = new MyFrac(long.Parse(secondfrac[0]), long.Parse(secondfrac[1]));
                MyFrac result = new MyFrac();
                switch (symbol)
                {
                    case "*":
                        if ((firstfrac[0] == "0") || (secondfrac[0] == "0"))
                        {
                            Console.WriteLine("Erorr404");
                            return;
                        }
                        result = Multiply(f1, f2);
                        break;
                    case "+":
                        result = Plus(f1, f2);
                        break;
                    case "-":
                        result = Minus(f1, f2);
                        break;
                    case "/":
                        if ((firstfrac[0] == "0") || (secondfrac[0] == "0"))
                        {
                            Console.WriteLine("Erorr404");
                            return;
                        }
                        result = Divide(f1, f2);
                        break;
                }
                Console.WriteLine(DoubleValue(result));
                Console.WriteLine(result.ToString());
                if (result.nom == result.denom)
                {
                    Console.WriteLine("Цiла частина винесена наперед");
                    Console.WriteLine(result.nom);
                }
                if (result.nom > result.denom)
                {
                    Console.WriteLine("Цiла частина винесена наперед");

                    ToStringWithIntegerPart(result);
                }

                Console.WriteLine("Введiть число яке дорiвнюватиме n");
                int n = int.Parse(Console.ReadLine());
                Console.WriteLine("Оберiть метод обчислення  CalcSum1: 1 , CalcSum2: 2");
                int x = int.Parse(Console.ReadLine());
                while (x != 0)
                {
                    switch (x)
                    {

                        case 1:
                            result = CalcSum1(n);
                            Console.WriteLine(DoubleValue(result));
                            Console.WriteLine(result.ToString());
                            //Console.WriteLine("Перевiрка");
                            //MyFrac kt = new MyFrac(0, 1); //n / (n + 1)
                            //MyFrac rum = new MyFrac(n, (n + 1));
                            //kt = Plus(kt, rum);
                            //Console.WriteLine(kt);
                            Console.WriteLine();
                            break;
                        case 2:
                            result = CalcSum2(n);
                            Console.WriteLine(DoubleValue(result));
                            Console.WriteLine(result.ToString());
                            //Console.WriteLine("Перевiрка");
                            //MyFrac secondtest = new MyFrac(0, 1); // (n + 1) / (2*n).
                            //MyFrac test = new MyFrac(n + 1, 2 * n);
                            //kt = Plus(secondtest, test);
                            //Console.WriteLine(kt);
                            Console.WriteLine();
                            break;
                    }

                    Console.WriteLine("Оберiть метод обчислення  CalcSum1: 1 , CalcSum2: 2 \n Для завершенняпрограми: 0");
                    x = int.Parse(Console.ReadLine());
                }
            }
        }
        //-------------------------------------------------------
        static Student[] ReadData(string fileName)
        {
            List<Student> list = new List<Student>();
            using (StreamReader stream = new StreamReader(fileName, Encoding.UTF8))
            {
                while (!stream.EndOfStream)
                {
                    list.Add(new Student(stream.ReadLine()));
                }
            }
            return list.ToArray();
        }
        static void runMenu(Student[] studs)
        {
            DateTime currentDate = DateTime.Today;
            for (int i = 0; i < studs.Length; i++)
            {
                DateTime date = DateTime.ParseExact(studs[i].dateOfBirth, "dd.MM.yyyy", null);
                TimeSpan difference = currentDate - date;
                double ageInYears = difference.TotalDays / 365.25;
                if ((studs[i].sex == 'M') && (ageInYears >= 18))
                {
                    Console.WriteLine($"{studs[i].surName} {studs[i].firstName} {studs[i].patronymic}");
                }
            }
        }
        static void Block2()
        {

            Student[] studs = ReadData(@"data.txt");
            runMenu(studs);
        }
    }
}

