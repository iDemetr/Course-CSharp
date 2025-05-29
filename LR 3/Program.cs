//-----------------------------------------------------------------------------------------------||
// Файл Program.cs
//-----------------------------------------------------------------------------------------------||

using LR2;
using System.Security.Cryptography;

namespace LR3
{
    internal class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            T1();

            T2_3();

            T4();

            Console.ReadKey();
        }

        static void T1()
        {
            var student = new Student();

            student.AddExams(new List<Exam>{
                    new Exam("subject0", 1, DateTime.Now),
                    new Exam("subject2", 2, DateTime.Now),
                    new Exam("subject1", 5, DateTime.MinValue),
                    new Exam("subject3", 3, DateTime.Now),
                    new Exam("subject4", 4, DateTime.MaxValue)
            });

            Console.WriteLine("\n1. ---------------------------------------------------------\n");
            student.SortExamByName();
            Console.WriteLine(" Sort by Name: " + student.ToString());
            student.SortExamByRating();
            Console.WriteLine(" Sort by Rating: " + student.ToString());
            student.SortExamByDate();
            Console.WriteLine(" Sort by Date: " + student.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");
        }

        static void T2_3()
        {
            int countElem = 0;
            var StudentCollection = new StudentCollection<string>((Student st) => { return st.Forename + countElem++; });
            StudentCollection.AddDefaults();
            StudentCollection.AddStudents([
                getStudentRND(),
                getStudentRND(),
                getStudentRND(),
                getStudentRND(),
                getStudentRND()
            ]);

            Console.WriteLine("\n2. ---------------------------------------------------------\n");
            Console.WriteLine(StudentCollection.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n3. ---------------------------------------------------------\n");
            Console.WriteLine( " Max AVG Rating: " + StudentCollection.MaxAvgRating);
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            var coolect = StudentCollection.EducationForm(Education.Bachelor);

            Console.WriteLine(" Students - Bachelor:");

            foreach (var kvp in coolect)
            {
                Console.WriteLine(" - " + $" {kvp.Key} - {kvp.Value.ToShortString()}");
            }

            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine(" Grouped students:");

            foreach (var group in StudentCollection.group)
            {
                Console.WriteLine(" " + group.Key.ToString() + ":");
                foreach (var kvp in group)
                {
                    Console.WriteLine(" - " + $" {kvp.Key} - {kvp.Value.ToShortString()}");
                }
            }

            Console.WriteLine("\n   ---------------------------------------------------------\n");
        }

        static void T4()
        {
            Console.WriteLine(" Введите число элементов:");

            int count = 0;
            while (count < 1)
            {
                try
                {
                    count = int.Parse(Console.ReadLine());                    
                }
                catch (Exception)
                {
                    Console.WriteLine(" Ошибка! Введите положительное число элементов:");
                }
            }

            GenerateElement<Person,Student> initDelegate = (int num) =>
            {
                var key = new Person(num.ToString(), "", DateTime.MinValue);
                var value = new Student(key, Education.Bachelor,0);

                return KeyValuePair.Create(key, value);
            };

            var TestCollection = new TestCollection<Person, Student>(initDelegate, count);

            TestCollection.TestListKey();
            TestCollection.TestListString();
            TestCollection.TestDictionary();
            TestCollection.TestDictionaryString();
        }

        #region --- Рандомайзеры ---

        static Student getStudentRND()
        {
            var student = new Student();
            student.AddExams(new List<Exam>{
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND())),
                    new Exam(getStringRND(), getRatRND(), DateTime.FromFileTime(getDateRND()))
            });
            student.Education = (Education)RandomNumberGenerator.GetInt32(0, 3);

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
