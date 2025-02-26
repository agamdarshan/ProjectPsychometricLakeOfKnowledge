namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Question
    {
        // Properties
        public int QuestionId { get; set; } // Unique identifier for the question (Primary Key)
        public int QuestionnaireId { get; set; } // Foreign Key linking to the Questionnaire
        public string GuideId { get; set; } // Foreign Key linking to the Guide who created the question
        public string Text { get; set; } // Text or content of the question
        public string DifficultyLevel { get; set; } // Difficulty level of the question
        public string OptionA { get; set; } // First answer option
        public string OptionB { get; set; } // Second answer option
        public string OptionC { get; set; } // Third answer option
        public string OptionD { get; set; } // Fourth answer option
        public string CorrectOption { get; set; } // Correct answer (e.g., "A" for OptionA)

        // Constructor
        public Question(
            int questionId,
            int questionnaireId,
            string guideId,
            string text,
            string difficultyLevel,
            string optionA,
            string optionB,
            string optionC,
            string optionD,
            string correctOption)
        {
            QuestionId = questionId;
            QuestionnaireId = questionnaireId;
            GuideId = guideId;
            Text = text;
            DifficultyLevel = difficultyLevel;
            OptionA = optionA;
            OptionB = optionB;
            OptionC = optionC;
            OptionD = optionD;
            CorrectOption = correctOption;
        }
    }
}
