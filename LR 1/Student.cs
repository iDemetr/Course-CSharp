//-----------------------------------------------------------------------------------------------||
// Файл Student.cs
//-----------------------------------------------------------------------------------------------||

namespace LR1
{
    internal class Student
    {
        /// <summary>
        /// Данные студента
        /// </summary>
        Person info;

        /// <summary>
        /// Форма обучения
        /// </summary>
        Education education;
        
        /// <summary>
        /// Номер группы
        /// </summary>
        int numberGroup;

        /// <summary>
        /// Закрытые экзамены
        /// </summary>
        Exam[] closedExams;

        /// <summary>
        /// Средний балл
        /// </summary>
        double AvgRating { 
            get {
                if (closedExams == null || closedExams.Length == 0) return 0;

                return closedExams.Average((ex) => { return ex.Rating; });
            } 
        }

        internal Person Info 
        {  
            get { return info; } 
            set { info = value; }
        }
        internal Education Education
        {
            get { return education; }
            set { education = value; }
        }        
        internal int NumberGroup 
        { 
            get { return numberGroup; } 
            set { numberGroup = value; }
        }
        internal Exam[] ClosedExams 
        { 
            get { return closedExams; } 
            set { closedExams = value; }
        }        
        internal bool this[Education education]
        {
            get => education == this.education;
        }

        /// <summary>
        /// 
        /// </summary>
        internal Student() {
        
            info = new();
            education = new ();
            numberGroup = 0;
            closedExams = new Exam[0];        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="education"></param>
        /// <param name="numberGroup"></param>
        internal Student( Person info,  Education education, int numberGroup )
        {
            this.info = info;
            this.education = education;
            this.numberGroup = numberGroup;
        }

        /// <summary>
        /// Добавляет перечень экзаменов в коллекцию закрытых экзаменов
        /// </summary>
        /// <param name="exams">Закрытые экзамены</param>
        internal void AddExams(Exam[] exams)
        {
            if (closedExams != null)
            {
                if (exams != null)
                    closedExams = closedExams.Concat(exams).ToArray();
            }
            else
                closedExams = exams;
        }

        public override string ToString()
        {
            string exams = "";

            if (closedExams != null)
                foreach (var ex in ClosedExams)
                {
                    exams += " - " + ex.ToString() + "\n";
                }

            return ToShortString() + " Exams:\n" + exams;
        }

        public virtual string ToShortString()
        {
            return info?.ToString() + " " + education.ToString() + " " + numberGroup + ", AvgRat: " + AvgRating;
        }

    }
}
