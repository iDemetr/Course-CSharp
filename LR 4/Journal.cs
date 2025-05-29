//-----------------------------------------------------------------------------------------------||
// Файл Journal.cs
//-----------------------------------------------------------------------------------------------||

using System.Text;

namespace LR4
{
    internal class Journal<TKey>
    {
        List<JournalEntry> journalEntries = new List<JournalEntry>();

        /// <summary>
        /// Обработчик событий
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public void StudentsChangedHandler(object source, StudentsChangedEventArgs<TKey> args)
        {
            var tmp = new JournalEntry(args.NameCollection, args.Action, args.PropertyStudent, args.Key.ToString());
            journalEntries.Add(tmp);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in journalEntries)
            {
                stringBuilder.AppendLine(" - " + item.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
