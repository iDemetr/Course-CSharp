//-----------------------------------------------------------------------------------------------||
// Файл StudentCollection.cs
//-----------------------------------------------------------------------------------------------||

using LR2;
using System.Text;

namespace LR3
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

        internal StudentCollection(KeySelector<TKey> deleg)
        {
            keySelector = deleg;

            Dictionary = new Dictionary<TKey, Student>();
        }

        /// <summary>
        /// Возвращает подмножество элементов коллекции 
        /// Dictionary<TKey, Student> с заданной формой обучения; 
        /// для формирования подмножества использовать метод Where класса System.Linq.Enumerable;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal IEnumerable<KeyValuePair<TKey, Student>> EducationForm(Education value)
        {
            return Dictionary.Where((kvp)=>kvp.Value.Education == value);
        }

        /// <summary>
        /// Для добавления некоторого числа элементов типа Student для инициализации коллекции 
        /// по умолчанию;
        /// </summary>
        internal void AddDefaults()
        {
            for (int i = 0; i < 1; i++) {
                var st = new Student();
                var key = keySelector.Invoke(st);
                Dictionary.TryAdd(key, st);
            }
        }

        /// <summary>
        /// Для добавления элементов в коллекцию Dictionary<TKey, Student>
        /// </summary>
        /// <param name="students"></param>
        internal void AddStudents(params Student[] students)
        {
            foreach (var student in students) { 
                var key = keySelector.Invoke(student);
                Dictionary.TryAdd(key, student);
            }
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
