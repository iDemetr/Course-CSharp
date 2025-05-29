//-----------------------------------------------------------------------------------------------||
// Файл Main.cs
//-----------------------------------------------------------------------------------------------||

using LR2;
using System.Security.Cryptography;

namespace LR4
{
    internal class Program
    {

        private static Random random = new Random();

        static void Main(string[] args)
        {
            int countElem = 0;
            var studentCollection1 = new StudentCollection<string>("studentCollection1", (Student st) => { return st.Forename + ++countElem; });
            var studentCollection2 = new StudentCollection<string>("studentCollection2", (Student st) => { return st.Forename + ++countElem; });   

            Journal<string> journal = new Journal<string>();

            studentCollection1.StudentsChanged += journal.StudentsChangedHandler;
            studentCollection2.StudentsChanged += journal.StudentsChangedHandler;

            Console.WriteLine("\n Рандомизация коллекций 1 и 2...");

            var toDel1 = getStudentRND();
            var toDel2 = getStudentRND();

            studentCollection1.AddDefaults();
            studentCollection1.AddStudents([
                getStudentRND(),
                toDel1
            ]);

            studentCollection2.AddDefaults();
            studentCollection2.AddStudents([
                getStudentRND(),
                toDel1,
                toDel2
            ]);

            Console.WriteLine("\n Изменение параметров элементов коллекций 1 и 2...");
            toDel1.Education = Education.Bachelor;
            toDel1.NumberGroup = 99;

            Console.WriteLine("\n Удаление элементов коллекций 1 и 2...");
            studentCollection1.Remove(toDel1);
            studentCollection2.Remove(toDel2);

            Console.WriteLine("\n Изменение параметров удаленного элемента коллекций 1 и 2...");
            toDel1.Education = Education.Specialist;
            toDel1.NumberGroup = 120;

            Console.WriteLine("\n Печать Journal:");
            Console.WriteLine(journal.ToString());
        }

        #region --- Рандомайзеры ---

        static Student getStudentRND()
        {
            var student = new Student();

            student.NumberGroup = RandomNumberGenerator.GetInt32(100, 200);
            student.Education = (Education)RandomNumberGenerator.GetInt32(0, 3);

            return student;
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
