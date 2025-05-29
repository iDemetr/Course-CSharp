//-----------------------------------------------------------------------------------------------||
// Файл StudentsChangedEventArgs.cs
//-----------------------------------------------------------------------------------------------||

namespace LR4
{
    internal class StudentsChangedEventArgs<TKey> : System.EventArgs
    {
        public string NameCollection { get; set; }
        public string PropertyStudent {  get; set; }
        public EAction Action { get; set; }
        public TKey Key { get; set; }

        public StudentsChangedEventArgs(string nameCollection, EAction action, string property, TKey key)
        {
            NameCollection = nameCollection;
            PropertyStudent = property;
            Action = action;
            Key = key;
        }

        public override string ToString()
        {
            return NameCollection + " " + Action.ToString() + " " + PropertyStudent + " " + Key;
        }
    }
}
