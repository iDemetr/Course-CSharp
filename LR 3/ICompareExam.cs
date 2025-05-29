//-----------------------------------------------------------------------------------------------||
// Файл ICompareExam.cs
//-----------------------------------------------------------------------------------------------||

namespace LR3
{
    /// <summary>
    /// Для сравнения объектов типа Exam по дате экзамена
    /// </summary>
    public class CompareExam : IComparer<Exam>
    {
        public int Compare(Exam? x, Exam? y)
        {
            if (x.DateExam > y.DateExam) return -1;
            else if (x.DateExam < y.DateExam) return 1;
            else return 0;
        }
    }
}
