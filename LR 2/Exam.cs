//-----------------------------------------------------------------------------------------------||
// Файл Exam.cs
//-----------------------------------------------------------------------------------------------||

namespace LR2
{
    public class Exam : IDateAndCopy
    {
        #region --- Параметры ---

        /// <summary>
        /// Название предмета
        /// </summary>
        public string NameSubject { get; set; }

        /// <summary>
        /// Оценка
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Дата экзамена
        /// </summary>
        public DateTime DateExam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date
        {
            get { return DateTime.MinValue; }//throw new NotImplementedException(); 
            set { }//throw new NotImplementedException(); 
        }

        #endregion

        #region --- Конструкторы ---

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Exam()
        {
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
        public Exam(string nameSubject, int rating, DateTime dateExam)
        {
            NameSubject = nameSubject;
            Rating = rating;
            DateExam = dateExam;
        }

        #endregion

        #region --- Virtuals ---

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return NameSubject + " " + Rating + ", Date:" + DateExam.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Exam tmp = obj as Exam;
            return tmp != null &&
                this.NameSubject == tmp.NameSubject &&
                this.Rating == tmp.Rating &&
                this.DateExam.Equals(tmp.DateExam);
        }

        public static bool operator ==(Exam p1, Exam p2)
        {
            if (p1 is null || p2 is null)
                return false;

            return p1.Equals(p2);
        }
        public static bool operator !=(Exam p1, Exam p2)
        {
            return !(p1 == p2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        public object DeepCopy()
        {
            return new Exam(NameSubject, Rating, DateExam);
        }
    }
}
