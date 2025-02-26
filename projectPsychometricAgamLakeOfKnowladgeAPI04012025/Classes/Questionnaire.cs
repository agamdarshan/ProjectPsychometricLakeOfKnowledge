namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        public int ExamId { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }

        public Questionnaire(int questionnaireId, int examId, string category, string title)
        {
            QuestionnaireId = questionnaireId;
            ExamId = examId;
            Category = category;
            Title = title;
        }
    }
}
