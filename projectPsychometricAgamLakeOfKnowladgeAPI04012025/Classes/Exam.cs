namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public Exam(int examId, string name, DateTime date)
        {
            ExamId = examId;
            Name = name;
            Date = date;
        }
    }
}
