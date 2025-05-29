//-----------------------------------------------------------------------------------------------||
// Файл Student.cs
//-----------------------------------------------------------------------------------------------||

using System.ComponentModel;

using LR2;

namespace LR4
{
    [Serializable]
    public class Student : LR3.Student, INotifyPropertyChanged
    {
        #region --- Свойства ---

        public new Education Education
        {
            get { return education; }
            set
            {
                education = value;
                var args = new PropertyChangedEventArgs("education");
                PropertyChanged?.Invoke(this, args);
            }
        }
        public new int NumberGroup 
        { 
            get { return numberGroup; }
            set
            {
                //if (value <= 100 || value > 599)
                //    throw new Exception("Недопустимый номер группы, границы значений от 101 до 599");
                numberGroup = value;
                var args = new PropertyChangedEventArgs("numberGroup");
                PropertyChanged?.Invoke(this, args);
            }
        }

        #endregion

        #region --- События и делегаты ---

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region --- Конструкторы ---

        /// <summary>
        /// 
        /// </summary>
        public Student() : base() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="education"></param>
        /// <param name="numberGroup"></param>
        public Student(Person p, Education e, int n)  : base(p, e, n) { }

        #endregion
    }
}
