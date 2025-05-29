//-----------------------------------------------------------------------------------------------||
// Файл Test.cs
//-----------------------------------------------------------------------------------------------||

namespace LR2
{
    [Serializable]
    public class Test
    {
        /// <summary>
        /// 
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isPassed {  get; set; }

        public Test() { 
            SubjectName = string.Empty;
            isPassed = false;
        }

        public Test(string subjectName, bool isPassed)
        {
            SubjectName = subjectName;
            this.isPassed = isPassed;
        }

        public override string ToString()
        {
            return SubjectName + (isPassed ? " сдан." : " не сдан.");
        }
    }
}
