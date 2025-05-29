//-----------------------------------------------------------------------------------------------||
// Файл Student.cs
//-----------------------------------------------------------------------------------------------||

using LR2;
using Exam = LR3.Exam;

using AutoMapper;
using System.Text.Json;
using System.Xml.Serialization;

namespace LR5
{
    [Serializable]    
    public class Student : LR4.Student 
    {
        #region --- Static ---

        /// <summary>
        /// для сохранения объекта в файле с помощью сериализации;
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public bool Save(string filename, Student obj)
        {
            return obj.Save(filename);
        }

        /// <summary>
        /// для восстановления объекта из файла с помощью десериализации.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public bool Load(string filename, Student obj)
        {
            return obj.Load(filename);
        }

        #endregion

        public Student() : base() { }

        public Student(Person p, Education e, int n) : base(p, e, n) { }

        #region --- LR 5 ---

        /// <summary>
        /// для сохранения данных объекта в файле с помощью сериализации
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Save(string filename)
        {
            try
            {
                // Реализация №1
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true                               // Для читаемого форматирования                                                              
                };

                using (Stream stream = new FileStream(filename, FileMode.Create))
                {
                    JsonSerializer.Serialize(stream, this, options);
                }

                // Реализация №2
                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student));
                //using (Stream stream = new FileStream(filename, FileMode.Create))
                //{
                //    xmlSerializer.Serialize(stream, this);                    
                //}
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// для инициализации объекта данными из файла с помощью десериализации;
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool Load(string filename)
        {
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open))
                {
                    if (stream.Length == 0)
                    {
                        throw new InvalidOperationException("Файл пуст.");
                    }

                    var tmp = JsonSerializer.Deserialize<Student>(stream);

                    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student));
                    //var tmp = xmlSerializer.Deserialize(stream) as Student;
                    
                    // Настройка AutoMapper
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Student, Student>());
                    IMapper mapper = config.CreateMapper();

                    // Создание копии объекта
                    var copyObject = mapper.Map<Student, Student>(tmp, this);
                }
                //var tmp = new Student((Person)info.DeepCopy(), education, numberGroup);
                //tmp.closedExams = new List<Exam>();
                //tmp.subjects = new List<Test>();

                //foreach (Exam exam in closedExams)
                //{
                //    tmp.closedExams.Add(exam.DeepCopy() as Exam);
                //}

                //foreach (Test subject in subjects)
                //{
                //    tmp.subjects.Add(subject);
                //}

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// для добавления в один из списков класса нового элемента, данные для которого вводятся с консоли;
        /// </summary>
        /// <returns></returns>
        public bool AddFromConsole()
        {
            try
            {
                Console.WriteLine("Введите экзамен: [Названия дисциплины], [Оценка], [Дата экзамена] без использования []");
                var input = Console.ReadLine();

                var paramss = input.Split(", ");
                if (paramss.Count() < 3)
                    throw new Exception("Введены не все данные!");

                for (int i = 0; i < paramss.Count(); i++)
                {
                    paramss[i] = paramss[i].Trim('[', ']');
                }

                var exam = new Exam(paramss[0], int.Parse(paramss[1]), DateTime.Parse(paramss[2]));

                closedExams.Add(exam);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Нельзя прегрузить, ошибка CS0506
        /// Копирование объекта через сериализцию
        /// </summary>
        /// <returns></returns>
        public Student DeepCopy()
        {
            // Сериализация вызывающего объекта в MemoryStream
            // Реализация №1
            using (var memoryStream = new MemoryStream())
            {
                // По какой-то приниче, если класс наследуется от интерфейса IEnumerable - ошибка:
                // Неизлечимая ошибка  - System.NullReferenceException: "Object reference not set to an instance of an object."
                JsonSerializer.Serialize(memoryStream, this);

                // Перемещение указателя в начало потока перед десериализацией
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Десериализация потока обратно в объект
                return JsonSerializer.Deserialize<Student>(memoryStream);
            }

            // Реализация №2
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Student));
            // Сериализация вызывающего объекта в MemoryStream
            //using (var memoryStream = new MemoryStream())
            //{
            //    xmlSerializer.Serialize(memoryStream, this);
            //    memoryStream.Seek(0, SeekOrigin.Begin);
            //    // Десериализация потока обратно в объект
            //    return xmlSerializer.Deserialize(memoryStream) as Student;
            //}
        }

        #endregion

    }
}
