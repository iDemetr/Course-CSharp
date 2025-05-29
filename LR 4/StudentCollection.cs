//-----------------------------------------------------------------------------------------------||
// Файл StudentCollection.cs
//-----------------------------------------------------------------------------------------------||

using LR2;
using System.Text;

namespace LR4
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="st"></param>
    /// <returns></returns>
    delegate TKey KeySelector<TKey>(Student st);

    internal class StudentCollection<TKey>
    {
        #region --- Переменные ---

        internal double MaxAvgRating { 
            get {
                if (Dictionary.Count == 0)
                    return 0;
                else
                    return Dictionary.Max(kvp=>kvp.Value.AvgRating);
            } 
        }

        /// <summary>
        /// Выполняет группировку элементов коллекции 
        /// Dictionary<TKey, Student> в зависимости от формы обучения студента с 
        /// помощью метода Group класса System.Linq.Enumerable.
        /// </summary>
        internal IEnumerable<IGrouping<Education, KeyValuePair<TKey, Student>>> group
        {
            get
            {
                return Dictionary.GroupBy((kvp) => { return kvp.Value.Education; });
            }
        }

        /// <summary>
        /// Кеш инициализированных студентов
        /// </summary>
        Dictionary<TKey, Student> Dictionary;
        
        /// <summary>
        /// Делегат формирования ключей словаря
        /// </summary>
        KeySelector<TKey> keySelector;

        /// <summary>
        /// Имя коллекции
        /// </summary>
        string NameCollection;

        #endregion

        #region --- Events and Delegates ---

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public delegate void StudentsChangedHandler<TKey>(object source, StudentsChangedEventArgs<TKey> args);

        /// <summary>
        /// 
        /// </summary>
        public event StudentsChangedHandler<TKey> StudentsChanged;

        #endregion

        public StudentCollection(string name, KeySelector<TKey> deleg)
        {
            keySelector = deleg;
            NameCollection = name;
            Dictionary = new Dictionary<TKey, Student>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        void Student_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var key = Dictionary.FirstOrDefault(x => x.Value == sender as Student).Key;
            if (key != null)
                StudentsChanged(this, new StudentsChangedEventArgs<TKey>(NameCollection, EAction.Property, e.PropertyName, key));
        }


        /// <summary>
        /// Возвращает подмножество элементов коллекции 
        /// Dictionary<TKey, Student> с заданной формой обучения; 
        /// для формирования подмножества использовать метод Where класса System.Linq.Enumerable;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education value)
        {
            return Dictionary.Where((kvp)=>kvp.Value.Education == value);
        }

        /// <summary>
        /// Для добавления некоторого числа элементов типа Student для инициализации коллекции 
        /// по умолчанию;
        /// </summary>
        public void AddDefaults()
        {
            for (int i = 0; i < 1; i++) {
                var st = new Student();
                var key = keySelector.Invoke(st);
                if(Dictionary.TryAdd(key, st))
                {
                    StudentsChanged(this, new StudentsChangedEventArgs<TKey>(NameCollection, EAction.Add, "", key));
                }

                st.PropertyChanged += Student_PropertyChanged;
            }
        }

        /// <summary>
        /// Для добавления элементов в коллекцию Dictionary<TKey, Student>
        /// </summary>
        /// <param name="students"></param>
        public void AddStudents(params Student[] students)
        {
            foreach (var student in students) { 
                var key = keySelector.Invoke(student);
                if(Dictionary.TryAdd(key, student)) 
                {
                    StudentsChanged(this, new StudentsChangedEventArgs<TKey>(NameCollection, EAction.Add, "", key));
                }

                student.PropertyChanged += Student_PropertyChanged;
            }
        }

        /// <summary>
        /// Для удаления элемента со значением st из словаря Dictionary<TKey, Student>; 
        /// если в словаре нет элемента st, метод возвращает значение false;
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public bool Remove(Student st)
        {
            if (Dictionary.ContainsValue(st))
            {
                var key = Dictionary.FirstOrDefault(x => x.Value == st).Key;
                Dictionary.Remove(key);
                StudentsChanged(this, new StudentsChangedEventArgs<TKey>(NameCollection, EAction.Remove, "", key));

                st.PropertyChanged -= Student_PropertyChanged;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Формирует строку, содержащую информацию обо всех элементах
        /// коллекции Dictionary<TKey, Student>, в том числе значения всех полей
        /// класса Student, включая список зачетов и экзаменов;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var kvp in Dictionary)
            {
                stringBuilder.AppendLine("||----------------------------------------------------------------||");
                stringBuilder.AppendLine($" {kvp.Key} - {kvp.Value.ToString()}");
                stringBuilder.AppendLine("||----------------------------------------------------------------||");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Формирует строку c информацией обо всех элементах коллекции Dictionary<TKey, Student>, состоящую из
        /// значений всех полей, среднего балла, числа зачетов и экзаменов для
        /// каждого элемента Student, но без списка зачетов и экзаменов.
        /// </summary>
        /// <returns></returns>
        public virtual string ToShortString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var kvp in Dictionary)
            {
                stringBuilder.AppendLine("||----------------------------------------------------------------||");
                stringBuilder.AppendLine($" {kvp.Key} - {kvp.Value.ToShortString()} " +
                    $"Exams: {kvp.Value.ClosedExams.Count()} Tests: {kvp.Value.Subjects.Count()}");
                stringBuilder.AppendLine("||----------------------------------------------------------------||");
            }

            return stringBuilder.ToString();
        }

    }
}
