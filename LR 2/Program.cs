//-----------------------------------------------------------------------------------------------||
// Файл Program.cs
//-----------------------------------------------------------------------------------------------||

namespace LR2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Person("forename", "surname", DateTime.MinValue);
            var p2 = new Person("forename", "surname", DateTime.MinValue);

            Console.WriteLine("\n1. ---------------------------------------------------------\n");
            Console.WriteLine("     is Equals(p1,p2): " + p1.Equals(p2));
            Console.WriteLine("     is ==: " + (p1 == p2));
            Console.WriteLine("     GetHashCode p1: " + p1.GetHashCode());
            Console.WriteLine("     GetHashCode p2: " + p2.GetHashCode());
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            var student = new Student(p1, Education.Specialist, 407);
            student.AddExams(new System.Collections.ArrayList { 
                new Exam("subject1", 1, DateTime.Now),
                new Exam("subject5", 5, DateTime.Now),
                new Exam("subject4", 4, DateTime.Now),
                new Exam("subject3", 3, DateTime.Now)
            });

            student.Subjects = (new System.Collections.ArrayList {
                new Test("subject1", true),
                new Test("subject5", false),
                new Test("subject4", true),
                new Test("subject3", true),
                new Test("subject7", true)
            });

            Console.WriteLine("\n2. ---------------------------------------------------------\n");
            Console.WriteLine("   Student ToString: " + student.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n3. ---------------------------------------------------------\n");
            Console.WriteLine("   Student Person: " + (student as Person).ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            var studentCopy = student.DeepCopy() as Student;

            student.BirthData = DateTime.Now;
            student.Subjects.Add(new Test("subject2", false));
            student.AddExams(new System.Collections.ArrayList { new Exam("subject2", 2, DateTime.Now) });

            Console.WriteLine("\n4. ---------------------------------------------------------\n");
            Console.WriteLine("   Student ToString: " + student.ToString());
            Console.WriteLine("   StudentCopy ToString: " + studentCopy?.ToString());
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            try
            {
                student.NumberGroup = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n5. ---------------------------------------------------------\n");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("\n   ---------------------------------------------------------\n");                
            }


            Console.WriteLine("\n6. ---------------------------------------------------------\n");
            
            foreach (var item in student.GetSubjects())
            {                
                Console.WriteLine(" - " + item.ToString());
            }
            
            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n7. ---------------------------------------------------------\n");

            foreach (var item in student.GetExam(3))
            {
                Console.WriteLine(" - " + item.ToString());
            }

            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n8. ---------------------------------------------------------\n");

            foreach (var item in student)
            {
                Console.WriteLine(" - " + item.ToString());
            }

            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n9. ---------------------------------------------------------\n");

            foreach (var item in student.GetPassedSubjectsAndExams())
            {
                Console.WriteLine(" - " + item.ToString());
            }

            Console.WriteLine("\n   ---------------------------------------------------------\n");

            Console.WriteLine("\n10. ---------------------------------------------------------\n");

            foreach (var item in student.GetPassedSubjects())
            {
                Console.WriteLine(" - " + item.ToString());
            }

            Console.WriteLine("\n   ---------------------------------------------------------\n");
        }
    }
}
