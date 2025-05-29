//-----------------------------------------------------------------------------------------------||
// Файл Program.cs
//-----------------------------------------------------------------------------------------------||

namespace LR1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HelloUser();

            Console.WriteLine(" Введите кол-во строк и столбцов массива, через\',\':");
            var input = Console.ReadLine();
            var tmp = input.Split(',');

            int nRow = int.Parse(tmp[0]);
            int nCol = int.Parse(tmp[1]);

            //int nRow = 7;
            //int nCol = 3;

            #region --- Init arrays ---

            int AllElements = nRow * nCol;

            Student[] Array = new Student[AllElements];
            for (int i = 0; i < AllElements; i++)
            {
                Array[i] = new Student();
            }

            Student[,] Array2D = new Student[nCol, nRow];
            for (int i = 0; i < nCol; i++)
            {
                for (int j = 0; j < nRow; j++)
                {
                    Array2D[i, j] = new Student();
                }
            }

            Student[][] Array2DStep = new Student[nCol][];
            for (int i = 0; i < nCol; i++)
            {
                Array2DStep[i] = new Student[nRow];
                for (int j = 0; j < nRow; j++)
                {
                    Array2DStep[i][j] = new Student();
                }
            }

            #endregion

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            //------------------------------------------------------------------------------------- ||
            Console.WriteLine("\n Диагностика времени выполнения доступа к эл-там массива c " +
                "изменением номера группы студента: \n");
            //------------------------------------------------------------------------------------- ||

            Student student0;

            stopwatch.Reset();
            stopwatch.Start();
            foreach (Student student in Array)
            {
                student.NumberGroup = 1;
            }
            stopwatch.Stop();
            Console.WriteLine(" Время выполнения foreach Array: " + stopwatch.ElapsedTicks + " Ticks.");

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < AllElements; i++)
            {
                Array[i].NumberGroup = 1;                
            }
            stopwatch.Stop();
            Console.WriteLine(" Время выполнения for Array: " + stopwatch.ElapsedTicks + " Ticks.\n");

            //------------------------------------------------------------------------------------- ||

            stopwatch.Reset();
            stopwatch.Start();
            foreach (Student student in Array2D)
            {
                student.NumberGroup = 1;
            }
            stopwatch.Stop();
            Console.WriteLine(" Время выполнения foreach Array2D: " + stopwatch.ElapsedTicks + " Ticks.");
            
            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < nCol; i++)
            {
                for (int j = 0; j < nRow; j++)
                {
                    Array2D[i,j].NumberGroup = 1;
                }
            }
            stopwatch.Stop();
            Console.WriteLine(" Время выполнения for Array2D: " + stopwatch.ElapsedTicks + " Ticks.\n");

            //------------------------------------------------------------------------------------- ||

            stopwatch.Reset();
            stopwatch.Start();
            foreach (var arr in Array2DStep)
            {
                foreach (Student student in arr)
                {
                    student.NumberGroup = 1;
                }
            }
            stopwatch.Stop();
            Console.WriteLine(" Время выполнения foreach Array2DStep: " + stopwatch.ElapsedTicks + " Ticks.");

            stopwatch.Reset();
            stopwatch.Start();
            for (int i = 0; i < nCol; i++)
            {
                for (int j = 0; j < nRow; j++)
                {
                    Array2DStep[i][j].NumberGroup = 1;
                }
            }
            stopwatch.Stop();
            Console.WriteLine(" Время выполнения for Array2DStep: " + stopwatch.ElapsedTicks + " Ticks.");

            //------------------------------------------------------------------------------------- ||

            Console.ReadKey();
        }

        static void HelloUser()
        {
            Console.WriteLine(" Example working: \n");

            var stud = new Student();
            Console.WriteLine(" ToShortString: " + stud.ToShortString() + "\n");
            Console.WriteLine(" isSpecialist: " + stud[Education.Specialist]);
            Console.WriteLine(" isBachelor: " + stud[Education.Bachelor]);
            Console.WriteLine(" isSecondEducation: " + stud[Education.SecondEducation]);

            stud.Info = new Person("name", "surname", DateTime.Now);
            stud.NumberGroup = 1;
            stud.Education = Education.Specialist;

            Console.WriteLine("\n ToString: " + stud.ToString() + "\n");

            stud.AddExams([new Exam(), new Exam("Subject1", 4, DateTime.Today)]);

            Console.WriteLine(" ToString after added exams: \n " + stud.ToString() + "\n\n");
        }
    }
}
