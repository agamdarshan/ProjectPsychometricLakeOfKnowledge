namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Practice
    {
        public int PracticeId { get; set; }
        public int StudentId { get; set; }
        public string Category { get; set; }
        public string DifficultyLevel { get; set; }
        public int NumQuestions { get; set; }
        public int Score { get; set; }
        public TimeSpan TimeSpent { get; set; }

        public Practice(int practiceId, int studentId, string category, string difficultyLevel,
                        int numQuestions, int score, TimeSpan timeSpent)
        {
            PracticeId = practiceId;
            StudentId = studentId;
            Category = category;
            DifficultyLevel = difficultyLevel;
            NumQuestions = numQuestions;
            Score = score;
            TimeSpent = timeSpent;
        }
    }
}
