//-----------------------------------------------------------------------------------------------||
// Файл TestCollection.cs
//-----------------------------------------------------------------------------------------------||

using System.Diagnostics;


namespace LR3
{
    delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);

    internal class TestCollection<TKey, TValue>
    {
        int SizeCollections;

        List<TKey> LKeys;
        List<string> LStrings;
        Dictionary<TKey, TValue> Dictionary;
        Dictionary<string, TValue> DictionarySTR;

        GenerateElement<TKey, TValue> InitDelegate;

        internal TestCollection(GenerateElement<TKey, TValue> deleg, int countElem)
        {
            InitDelegate = deleg;
            SizeCollections = countElem;

            LKeys = new List<TKey>(SizeCollections);
            for (int i =0; i< SizeCollections;i++)
            {
                LKeys.Add(InitDelegate.Invoke(i).Key);
            }

            LStrings = new List<string>(SizeCollections);
            for (int i = 0; i < SizeCollections; i++)
            {
                LStrings.Add(InitDelegate.Invoke(i).Key.ToString());
            }

            Dictionary = new Dictionary<TKey, TValue>(SizeCollections);
            for (int i = 0; i < SizeCollections; i++)
            {
                var kvp = InitDelegate.Invoke(i);
                Dictionary.Add(kvp.Key, kvp.Value);
            }

            DictionarySTR = new Dictionary<string, TValue>(SizeCollections);
            for (int i = 0; i < SizeCollections; i++)
            {
                var kvp = InitDelegate.Invoke(i);
                DictionarySTR.Add(kvp.Key.ToString(), kvp.Value);
            }
        }

        internal void TestListKey()
        {
            bool isFind;
            var watcher = new Stopwatch();

            Console.WriteLine("------------------------- TestListKey -------------------------");

            var tmp = InitDelegate(0);
            watcher.Start();
            isFind = LKeys.Contains(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains First:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections/2);
            watcher.Restart();
            isFind = LKeys.Contains(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains Middle:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections-1);
            watcher.Restart();
            isFind = LKeys.Contains(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains Last:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections);
            watcher.Restart();
            isFind = LKeys.Contains(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains Outsider:" + watcher.ElapsedTicks + " " + isFind);
        }

        internal void TestListString()
        {

            bool isFind;
            var watcher = new Stopwatch();

            Console.WriteLine("------------------------- TestListString -------------------------");

            var tmp = InitDelegate(0);
            watcher.Start();
            isFind = LStrings.Contains(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains First:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections / 2);
            watcher.Restart();
            isFind = LStrings.Contains(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains Middle:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections - 1);
            watcher.Restart();
            isFind = LStrings.Contains(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains Last:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections);
            watcher.Restart();
            isFind = LStrings.Contains(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains Outsider:" + watcher.ElapsedTicks + " " + isFind);
        }

        internal void TestDictionary()
        {
            bool isFind;
            var watcher = new Stopwatch();

            Console.WriteLine("------------------------- TestDictionary -------------------------");

            var tmp = InitDelegate(0);
            watcher.Start();
            isFind = Dictionary.ContainsKey(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains First:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections / 2);
            watcher.Restart();
            isFind = Dictionary.ContainsKey(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains Middle:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections - 1);
            watcher.Restart();
            isFind = Dictionary.ContainsKey(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains Last:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections);
            watcher.Restart();
            isFind = Dictionary.ContainsKey(tmp.Key);
            watcher.Stop();
            Console.WriteLine(" Ticks contains Outsider:" + watcher.ElapsedTicks + " " + isFind);
        }

        internal void TestDictionaryString()
        {
            bool isFind;
            var watcher = new Stopwatch();

            Console.WriteLine("------------------------- TestDictionaryString -------------------------");

            var tmp = InitDelegate(0);
            watcher.Start();
            isFind = DictionarySTR.ContainsKey(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains First:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections / 2);
            watcher.Restart();
            isFind = DictionarySTR.ContainsKey(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains Middle:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections - 1);
            watcher.Restart();
            isFind = DictionarySTR.ContainsKey(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains Last:" + watcher.ElapsedTicks + " " + isFind);

            tmp = InitDelegate(SizeCollections);
            watcher.Restart();
            isFind = DictionarySTR.ContainsKey(tmp.Key.ToString());
            watcher.Stop();
            Console.WriteLine(" Ticks contains Outsider:" + watcher.ElapsedTicks + " " + isFind);
        }
    }
}
