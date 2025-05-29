//-----------------------------------------------------------------------------------------------||
// Файл Exam.cs
//-----------------------------------------------------------------------------------------------||

namespace LR1
{
    internal class Exam
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        internal string NameSubject{ get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        internal int Rating { get; set; }

        /// <summary>
        /// Дата экзамена
        /// </summary>
        internal DateTime DateExam { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        internal Exam() {
            NameSubject = "noData";
            Rating = 0;
            DateExam = DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameSubject"></param>
        /// <param name="rating"></param>
        /// <param name="dateExam"></param>
        internal Exam(string nameSubject, int rating, DateTime dateExam) { 
            NameSubject= nameSubject;
            Rating= rating; 
            DateExam= dateExam;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return NameSubject + " " + Rating + ", Date:" + DateExam.ToString();
        }
    }
}
