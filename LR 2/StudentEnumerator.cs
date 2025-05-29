//-----------------------------------------------------------------------------------------------||
// Файл StudentEnumerator.cs
//-----------------------------------------------------------------------------------------------||

using System.Collections;

namespace LR2
{
    /// <summary>
    /// По доп требованиям:
    /// Реализовать интерфейс System.Collections.IEnumerable для перебора
    /// названий всех предметов(объектов типа string), которые есть как в списке
    /// зачетов, так и в списке экзаменов(пересечение). Для этого определить
    /// вспомогательный класс StudentEnumerator, реализующий интерфейс
    /// System.Collections.IEnumerator.
    /// </summary>
    public class StudentEnumerator : IEnumerator
    {
        Student _student;

        int position = -1;

        public object Current => (_student.ClosedExams[position] as Exam).NameSubject;

        public StudentEnumerator(Student student)
        {
            _student = student;
        }

        public bool MoveNext()
        {
            // Поиск индексов
            while (++position < _student.ClosedExams.Count)
            {
                Exam exam = _student.ClosedExams[position] as Exam;
                if (exam != null)
                    foreach (Test subject in _student.Subjects)
                    {
                        if (subject.SubjectName == exam.NameSubject)
                        {
                            return true;
                        }
                    }
            }

            return false;
        }

        public void Reset() => position = -1;
    }
}
