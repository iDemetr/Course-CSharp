using LR2;
using System.Security.Cryptography;

using Exam = LR3.Exam;

namespace LR5
{
    internal class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            var student = getStudentRND();
            var copyStudent = student.DeepCopy();

            Console.WriteLine("\n1. ---------------------------------------------------------\n");
            Console.WriteLine(student.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");
            Console.WriteLine(copyStudent.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            
            Console.WriteLine("\n2. ---------------------------------------------------------\n");
            
            Student newstudent = null;
            
            Console.WriteLine("Введите имя файла для сериализации:");
            var path = Console.ReadLine();
            if (!File.Exists(path)) {
                Console.WriteLine(path + " не существует и будет создан.");
                Console.WriteLine("Сериализация объекта...");
                student.Save(path);
            }
            else
            {
                newstudent = new Student();
                Console.WriteLine("Десериализация объекта...");
                newstudent.Load(path);
                Console.WriteLine(newstudent.ToString());
            }
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n4. ---------------------------------------------------------\n");
            if(newstudent != null)
            {
                newstudent.AddFromConsole();
                newstudent.Save(path);
                Console.WriteLine(newstudent.ToString());
            }
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n5. ---------------------------------------------------------\n");
            var newStudent2 = new Student();
            Student.Load(path, newStudent2);
            newStudent2.AddFromConsole();
            Student.Save(path, newStudent2);
            Console.WriteLine(newStudent2.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");
        }

        #region --- Рандомайзеры ---

        static Student getStudentRND()
        {
            var student = new Student(new Person("asdas", "asdasda",DateTime.Now), Education.Bachelor, 1000);
            student.AddExams(new List<Exam>{
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND()))
            });
            student.Education = (LR2.Education)RandomNumberGenerator.GetInt32(0, 3);

            return student;
        }

        static int getRatRND()
        {
            return RandomNumberGenerator.GetInt32(0, 6);
        }

        static int getDateRND()
        {
            return RandomNumberGenerator.GetInt32(0, int.MaxValue);
        }

        static string getStringRND()
        {
            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

    }
}
