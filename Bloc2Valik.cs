using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using _5_laboratory;

namespace struct_lab_student
{
    partial class Program
    {
        static Student[] ReadData(string fileName)
        {
            List<Student> list = new List<Student>();
            using (StreamReader stream = new StreamReader(fileName,Encoding.UTF8))
            {
                while(!stream.EndOfStream) 
                {
                    list.Add(new Student(stream.ReadLine()));
                }
            }
            return list.ToArray();
        }
        static void runMenu(Student[] studs)
        {
            DateTime currentDate = DateTime.Today;          
            for (int i = 0;i < studs.Length;i++)
            {
                DateTime date = DateTime.ParseExact(studs[i].dateOfBirth, "dd.MM.yyyy", null);
                TimeSpan difference = currentDate - date;
                double ageInYears = difference.TotalDays / 365.25;
                if ((studs[i].sex== 'M') && (ageInYears >= 18))
                {
                    Console.WriteLine($"{studs[i].surName} {studs[i].firstName} {studs[i].patronymic}");
                }
            }
        }
        static void Main(string[] args)
        {
             
            Student[] studs = ReadData(@"data.txt");
            runMenu(studs);
        }
    }  
}