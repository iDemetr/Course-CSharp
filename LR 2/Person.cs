//-----------------------------------------------------------------------------------------------||
// Файл Person.cs
//-----------------------------------------------------------------------------------------------||

namespace LR2
{
    [Serializable]
    public class Person : IDateAndCopy
    {
        /// <summary>
        /// Имя
        /// </summary>
        protected string _Forename;
        /// <summary>
        /// Фамилия
        /// </summary>
        protected string _Surname;
        /// <summary>
        /// День рождения
        /// </summary>
        protected DateTime _BirthData;

        public string Forename { 
            set { _Forename = value; }
            get { return _Forename; } }
        public string Surname { 
            set { _Surname = value; }
            get { return _Surname; } }
        public DateTime BirthData {
            get { return _BirthData; }
            set { _BirthData = value; } 
        }
        public DateTime Date
        {
            get => BirthData;
            set { }
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Person()
        {
            _Forename = "";
            _Surname = "";
            _BirthData = DateTime.MinValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="birthData"></param>
        public Person(string forename, string surname, DateTime birthData)
        {
            _Forename = forename;
            _Surname = surname;
            _BirthData = birthData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _Forename + " " + _Surname + " " + _BirthData.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string ToShortString()
        {
            return _Forename + " " + _Surname + " ";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Person tmp = obj as Person;
            return tmp != null &&
                this._Forename == tmp._Forename &&
                this._Surname == tmp._Surname &&
                this._BirthData == tmp._BirthData;
        }
        
        public static bool operator ==(Person p1, Person p2)
        {
            if(p1 is null || p2 is null)
                return false;
            
            return  p1.Equals(p2);
        }
        public static bool operator !=(Person p1, Person p2)
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

        public object DeepCopy()
        {
            return new Person(Forename, Surname, BirthData);
        }
    }
}
