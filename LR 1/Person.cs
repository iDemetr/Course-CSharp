//-----------------------------------------------------------------------------------------------||
// Файл Person.cs
//-----------------------------------------------------------------------------------------------||

using System.Numerics;

namespace LR1
{
    internal class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        string _Forename;
        /// <summary>
        /// Фамилия
        /// </summary>
        string _Surname;
        /// <summary>
        /// День рождения
        /// </summary>
        DateTime _BirthData;

        internal string Forename { get { return _Forename; } }
        internal string Surname { get { return _Surname; } }
        internal DateTime BirthData {
            get { return _BirthData; }
            set { _BirthData = value; } 
        }


        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        internal Person()
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
        internal Person(string forename, string surname, DateTime birthData)
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
    }
}
