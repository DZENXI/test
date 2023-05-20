using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lb5_1_8;

namespace lb5_1_8
{
    class start
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Виберіть, яку задачу ви хочете виконати:");
            Console.WriteLine("1 - Pasha");
            Console.WriteLine("2 - Valic");

            string choice = Console.ReadLine();

            IRunnable runnable;

            switch (choice)
            {
                case "1":
                    runnable = new Program();
                    break;

                case "2":
                    runnable = new ProgramV();
                    break;

                default:
                    Console.WriteLine("Некоректний вибір. Будь ласка, введіть 1 або 2.");
                    return;
            }

            runnable.Run();

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
