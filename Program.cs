using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb5_1_8
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть номер блоку (1 або 2):");
            string blockNumber = Console.ReadLine();

            if (blockNumber == "1")
            {
                Console.WriteLine("Введіть час у форматі 'години:хвилини:секунди':");
                string inputTime = Console.ReadLine();

                if (TryParseTime(inputTime, out MyTime time))
                {
                    Console.WriteLine("Time: " + time.ToString());
                    Console.WriteLine("Час з початку доби в секундах: " + TimeSinceMidnight(time));
                    Console.WriteLine("Додати одну секунду: " + AddOneSecond(time).ToString());
                    Console.WriteLine("Додати одну хвилину: " + AddOneMinute(time).ToString());
                    Console.WriteLine("Додати одну годину: " + AddOneHour(time).ToString());
                    Console.WriteLine("Розклад: " + WhatLesson(time));
                    Console.WriteLine("Введіть кількість секунд для додавання:");
                    if (int.TryParse(Console.ReadLine(), out int secondsToAdd))
                    {
                        MyTime newTime = AddSeconds(time, secondsToAdd);
                        Console.WriteLine($"Додати {secondsToAdd} секунд: " + newTime.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Невірний формат кількості секунд.");
                    }

                    Console.WriteLine("Розклад: " + WhatLesson(time));
                }
                else
                {
                    Console.WriteLine("Невірний формат часу. Будь ласка, введіть час у форматі 'години:хвилини:секунди'.");
                }
            }
            else if (blockNumber == "2")
            {
                Student[] studs = ReadData(@"D:\PALM\lb5_time\lb5_1_8\data.txt");
                runMenu(studs);
            }
            else
            {
                Console.WriteLine("Невірний номер блоку.");
            }

            Console.ReadKey();
        }
        static int TimeSinceMidnight(MyTime t)
        {
            return t.hour * 3600 + t.minute * 60 + t.second;
        }

        static MyTime TimeSinceMidnight(int t)
        {
            t %= 86400;
            if (t < 0)
                t += 86400;

            int h = t / 3600;
            int m = (t / 60) % 60;
            int s = t % 60;

            return new MyTime(h, m, s);
        }
        static MyTime AddOneSecond(MyTime t)
        {
            int seconds = TimeSinceMidnight(t) + 1;
            return TimeSinceMidnight(seconds);
        }
        static MyTime AddOneMinute(MyTime t)
        {
            int minutes = TimeSinceMidnight(t) + 60;
            return TimeSinceMidnight(minutes);
        }

        static MyTime AddOneHour(MyTime t)
        {
            int hours = TimeSinceMidnight(t) + 3600;
            return TimeSinceMidnight(hours);
        }
        static MyTime AddSeconds(MyTime t, int s)
        {
            int totalSeconds = TimeSinceMidnight(t) + s;
            return TimeSinceMidnight(totalSeconds);
        }

        static int Difference(MyTime mt1, MyTime mt2)
        {
            int seconds1 = TimeSinceMidnight(mt1);
            int seconds2 = TimeSinceMidnight(mt2);
            return seconds1 - seconds2;
        }

        static string WhatLesson(MyTime mt)
        {
            int totalSeconds = TimeSinceMidnight(mt);

            if (totalSeconds < TimeSinceMidnight(new MyTime(8, 0, 0)))
                return "Пари ще не почалися";
            if (totalSeconds > TimeSinceMidnight(new MyTime(14, 20, 0)))
                return "Пари вже скiнчилися";

            int[,] lessons = new int[,]
            {
                { TimeSinceMidnight(new MyTime(8, 0, 0)), TimeSinceMidnight(new MyTime(9, 20, 0)) },
                { TimeSinceMidnight(new MyTime(9, 20, 0)), TimeSinceMidnight(new MyTime(9, 40, 0)) },
                { TimeSinceMidnight(new MyTime(9, 40, 0)), TimeSinceMidnight(new MyTime(11, 0, 0)) },
                { TimeSinceMidnight(new MyTime(11, 0, 0)), TimeSinceMidnight(new MyTime(11, 20, 0)) },
                { TimeSinceMidnight(new MyTime(11, 20, 0)), TimeSinceMidnight(new MyTime(12, 40, 0)) },
                { TimeSinceMidnight(new MyTime(12, 40, 0)), TimeSinceMidnight(new MyTime(13, 0, 0)) },
                { TimeSinceMidnight(new MyTime(13, 0, 0)), TimeSinceMidnight(new MyTime(14, 20, 0)) }
            };

            string[] lessonNames = { "Перша", "Перерва мiж першою i другою", "Друга", "Перерва мiж другою i третьою", "Третя", "Перерва мiж третью i четверою", "Четвера" };

            for (int i = 0; i < lessons.GetLength(0); i++)
            {
                int lessonStart = lessons[i, 0];
                int lessonEnd = lessons[i, 1];

                if (totalSeconds >= lessonStart && totalSeconds < lessonEnd)
                {
                    return $"{lessonNames[i]} пара";
                }
                else if (totalSeconds >= lessonEnd && i < lessons.GetLength(0) - 1)
                {

                    int breakStart = lessonEnd;
                    int breakEnd = lessons[i + 1, 0];

                    if (totalSeconds >= breakStart && totalSeconds < breakEnd)
                    {
                        return $"Перерва мiж {lessonNames[i]} і {lessonNames[i + 1]} парою";
                    }
                }
            }

            return "Поза розкладом пар";
        }
        static bool TryParseTime(string input, out MyTime time)
        {
            string[] parts = input.Split(':');

            if (parts.Length == 3 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes) && int.TryParse(parts[2], out int seconds))
            {
                time = new MyTime(hours, minutes, seconds);
                return true;
            }

            time = default(MyTime);
            return false;
        }
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
            int count = 0;

            for (int i = 0; i < studs.Length; i++)
            {
                DateTime date = DateTime.ParseExact(studs[i].dateOfBirth, "dd.MM.yyyy", null);
                TimeSpan difference = currentDate - date;
                double ageInYears = difference.TotalDays / 365.25;

                if (ageInYears < 16)
                {

                    Console.WriteLine($"Прізвище: {studs[i].surName}");
                    Console.WriteLine($"Ім'я: {studs[i].firstName}");
                    Console.WriteLine($"По батькові: {studs[i].patronymic}");
                    Console.WriteLine($"Стать: {studs[i].sex}");
                    Console.WriteLine($"Дата народження: {studs[i].dateOfBirth}");
                    Console.WriteLine($"Оцінка з математики: {studs[i].mathematicsMark}");
                    Console.WriteLine($"Оцінка з фізики: {studs[i].physicsMark}");
                    Console.WriteLine($"Оцінка з інформатики: {studs[i].informaticsMark}");
                    Console.WriteLine($"Стипендія: {studs[i].scholarship}");
                    Console.WriteLine();

                    count++;
                }
            }

            Console.WriteLine($"Кількість студентів молодших за 16 років: {count}");
        }
    }
}
