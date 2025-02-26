namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Result
    {
        public int ResultId { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int ScoreEnglish { get; set; }
        public int ScoreHebrew { get; set; }
        public int ScoreQuantity { get; set; }
        public DateTime CompletionDate { get; set; }

        public Result(int resultId, int studentId, int examId, int scoreEnglish,
                      int scoreHebrew, int scoreQuantity, DateTime completionDate)
        {
            ResultId = resultId;
            StudentId = studentId;
            ExamId = examId;
            ScoreEnglish = scoreEnglish;
            ScoreHebrew = scoreHebrew;
            ScoreQuantity = scoreQuantity;
            CompletionDate = completionDate;
        }
    }
}
