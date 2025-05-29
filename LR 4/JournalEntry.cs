//-----------------------------------------------------------------------------------------------||
// Файл JournalEntry.cs
//-----------------------------------------------------------------------------------------------||

namespace LR4
{
    internal class JournalEntry
    {
        public string NameCollection { get; set; }
        public EAction Action { get; set; }
        public string Property { get; set; }  
        public string Key {  get; set; }

        public JournalEntry(string Name, EAction action, string prop, string key)
        {
            NameCollection = Name;
            Action = action;
            Property = prop;
            Key = key;
        }

        public override string ToString()
        {
            return NameCollection + " " + Action.ToString() + " " + Property + " " + Key;
        }
    }
}
