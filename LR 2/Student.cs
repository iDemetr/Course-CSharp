//-----------------------------------------------------------------------------------------------||
// Файл Student.cs
//-----------------------------------------------------------------------------------------------||

using System.Collections;

namespace LR2
{
    public class Student : Person, IDateAndCopy, IEnumerable
    {
        #region --- Переменные ---

        Person info => this;

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
        ArrayList closedExams;

        /// <summary>
        /// 
        /// </summary>
        ArrayList subjects;

        #endregion

        #region --- Свойства ---

        /// <summary>
        /// Средний балл
        /// </summary>
        double AvgRating { 
            get {
                if (closedExams == null || closedExams.Count == 0) return 0;

                return (double)(closedExams.ToArray()?.Average((ex) => { return (ex as Exam)?.Rating ?? 0.0; }));
            } 
        }

        internal Education Education
        {
            get { return education; }
            set { education = value; }
        }        
        internal int NumberGroup 
        { 
            get { return numberGroup; } 
            set { 
                if (value <= 100 || value > 599)
                    throw new Exception("Недопустимый номер группы, границы значений от 101 до 599");
                numberGroup = value; 
            }
        }

        internal ArrayList ClosedExams 
        { 
            get { return closedExams; } 
            set { closedExams = value; }
        }

        internal ArrayList Subjects
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
            var iter = closedExams.GetEnumerator();
            while (iter.MoveNext())
            {
                if ((iter.Current as Exam).Rating > 2)
                    yield return iter.Current;
            }

            iter = Subjects.GetEnumerator();
            while(iter.MoveNext())
            {
                if ((iter.Current as Test).isPassed)
                    yield return iter.Current;
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

        /// <summary>
        /// По доп. требованиям
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new StudentEnumerator(this);
        }

        #endregion

        #region --- Конструкторы ---

        /// <summary>
        /// 
        /// </summary>
        public Student() {
        
            education = new ();
            numberGroup = 0;
            closedExams = new ArrayList();        
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
        }

        #endregion

        #region --- Virtuals ---

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
        internal void AddExams(ArrayList exams)
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
            tmp.closedExams = new ArrayList();
            tmp.subjects = new ArrayList();

            foreach (Exam exam in closedExams)
            {
                tmp.closedExams.Add(exam.DeepCopy());
            }

            foreach (Test subject in subjects)
            {
                tmp.subjects.Add(subject);
            }

            return tmp;
        }

    }
}
