//-----------------------------------------------------------------------------------------------||
// Файл Student.cs
//-----------------------------------------------------------------------------------------------||

using System.Collections;
using System.Text;
using LR2;

namespace LR3
{
    [Serializable]
    public class Student : Person, IDateAndCopy //, IEnumerable
    {
        #region --- Переменные ---
        Person info => this;

        /// <summary>
        /// Форма обучения
        /// </summary>
        protected Education education;

        /// <summary>
        /// Номер группы
        /// </summary>
        protected int numberGroup;

        /// <summary>
        /// Закрытые экзамены
        /// </summary>
        protected List<Exam> closedExams;

        /// <summary>
        /// 
        /// </summary>
        protected List<Test> subjects;

        #endregion

        #region --- Свойства ---

        /// <summary>
        /// Средний балл
        /// </summary>
        public double AvgRating { 
            get {
                if (closedExams == null || closedExams.Count == 0) return 0;

                return (double)(closedExams.ToArray()?.Average((ex) => { return (ex as Exam)?.Rating ?? 0.0; }));
            }
            set { }
        }
        public Education Education
        {
            get { return education; }
            set { education = value; }
        }
        public int NumberGroup 
        { 
            get { return numberGroup; } 
            set { 
                if (value <= 100 || value > 599)
                    throw new Exception("Недопустимый номер группы, границы значений от 101 до 599");
                numberGroup = value; 
            }
        }
        public List<Exam> ClosedExams 
        { 
            get { return closedExams; } 
            set { closedExams = value; }
        }
        public List<Test> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        #endregion

        #region --- Итераторы ---

        /// <summary>
        /// Итератор для последовательного перебора всех элементов (объектов типа object) 
        /// из списков зачетов и экзаменов(объединение)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<object> GetSubjects()
        {
            foreach (Exam ex in closedExams)
            {
                yield return ex;
            }

            foreach (Test subject in subjects)
            {
                   yield return subject;
            }
        }

        /// <summary>
        /// Итератор c параметром для перебора экзаменов (объектов типа Exam) 
        /// с оценкой больше заданного значения.
        /// </summary>
        /// <param name="targetRating"></param>
        /// <returns></returns>
        public IEnumerable<Exam> GetExam(int targetRating)
        {            
            foreach (Exam ex in closedExams)
            {
                if (ex.Rating >= targetRating)
                    yield return ex;
            }
        }

        /// <summary>
        /// По доп требованиям:
        /// определить итератор для перебора сданных зачетов и экзаменов
        /// (объектов типа object), для этого определить метод, содержащий блок
        /// итератора и использующий оператор yield; сданный экзамен - экзамен с
        /// оценкой больше 2; 
        /// </summary>
        /// <param name="targetRating"></param>
        /// <returns></returns>
        public IEnumerable<object> GetPassedSubjectsAndExams()
        {
            var iterExam = closedExams.GetEnumerator();
            while (iterExam.MoveNext())
            {
                if ((iterExam.Current as Exam).Rating > 2)
                    yield return iterExam.Current;
            }

            var iterTest = Subjects.GetEnumerator();
            while(iterTest.MoveNext())
            {
                if ((iterTest.Current as Test).isPassed)
                    yield return iterTest.Current;
            }
        }

        /// <summary>
        /// По доп требованиям:
        /// определить итератор для перебора всех сданных зачетов (объектов
        /// типа Test), для которых сдан и экзамен, для этого определить метод,
        /// содержащий блок итератора и использующий оператор yield.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Test> GetPassedSubjects()
        {
            foreach (Exam exam in closedExams)
            {
                foreach (Test subject in Subjects)
                {
                    if (subject.SubjectName == exam.NameSubject && exam.Rating > 2 && subject.isPassed)
                    {
                        yield return subject;
                    }
                }
            }
        }

        ///// <summary>
        ///// По доп. требованиям
        ///// </summary>
        ///// <returns></returns>
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    //return null;
        //    return new StudentEnumerator(this);
        //}

        #endregion

        #region --- Конструкторы ---

        /// <summary>
        /// 
        /// </summary>
        public Student() : base() {                        
            education = new ();
            numberGroup = 0;
            closedExams = new List<Exam>();
            Subjects = new List<Test>();        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="education"></param>
        /// <param name="numberGroup"></param>
        public Student(Person person, Education education, int numberGroup )
        {
            base._BirthData = person.BirthData;
            base._Surname = person.Surname;
            base._Forename = person.Forename;

            this.education = education;
            this.numberGroup = numberGroup;

            closedExams = new List<Exam>();
            Subjects = new List<Test>();
        }

        #endregion

        #region --- Сортировщики ---

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal void SortExamByName()
        {            
            closedExams.Sort();         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal void SortExamByRating()
        {            
            closedExams.Sort(new Exam());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal void SortExamByDate()
        {
            closedExams.Sort(new CompareExam());
        }

        #endregion

        #region --- Virtuals ---

        public override string ToString()
        {
            StringBuilder exams = new StringBuilder();

            if (closedExams != null)
                foreach (var ex in ClosedExams)
                {
                    exams.AppendLine(" - " + ex.ToString());
                }

            StringBuilder tests = new StringBuilder();

            if (subjects != null)
                foreach (var ex in subjects)
                {
                    tests.AppendLine(" - " + ex.ToString());
                }

            return ToShortString() + "\n Exams:\n" + exams.ToString() + "\n Tests:\n" + tests.ToString();
        }
        public virtual string ToShortString()
        {
            return base.ToString() + " " + education.ToString() + " " + numberGroup + ", AvgRat: " + AvgRating;
        }

        public override bool Equals(object obj)
        {
            Student tmp = obj as Student;
            return tmp != null &&
                base.Equals(tmp) &&
                this.education == tmp.education &&
                this.closedExams == tmp.closedExams &&
                this.AvgRating == tmp.AvgRating &&
                this.subjects == tmp.subjects &&
                this.numberGroup == tmp.numberGroup;
        }

        public static bool operator ==(Student p1, Student p2)
        {
            if (p1 is null || p2 is null)
                return false;

            return p1.Equals(p2);
        }
        public static bool operator !=(Student p1, Student p2)
        {
            return !(p1 == p2);
        }

        public override int GetHashCode()
        {
            return info.GetHashCode() + education.GetHashCode() + numberGroup + closedExams.GetHashCode() + subjects.GetHashCode();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="education"></param>
        /// <returns></returns>
        internal bool this[Education education]
        {
            get => education == this.education;
        }

        /// <summary>
        /// Добавляет перечень экзаменов в коллекцию закрытых экзаменов
        /// </summary>
        /// <param name="exams">Закрытые экзамены</param>
        public void AddExams(List<Exam> exams)
        {
            if (closedExams != null)
            {
                if (exams != null)
                    closedExams.AddRange((exams).ToArray());
            }
            else
                closedExams = exams;
        }

        /// <summary>
        /// Нельзя прегрузить, ошибка CS0506
        /// </summary>
        /// <returns></returns>
        public new object DeepCopy()
        {
            var tmp = new Student((Person)info.DeepCopy(), education, numberGroup);
            tmp.closedExams = new List<Exam>();
            tmp.subjects = new List<Test>();

            foreach (Exam exam in closedExams)
            {
                tmp.closedExams.Add(exam.DeepCopy() as Exam);
            }

            foreach (Test subject in subjects)
            {
                tmp.subjects.Add(subject);
            }

            return tmp;
        }

    }
}
