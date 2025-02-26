namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string StudentId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Content { get; set; }

        // Default constructor for serialization
        public Review() { }

        // Constructor with all properties
        public Review(int reviewId, string studentId, DateTime reviewDate, string content)
        {
            ReviewId = reviewId;
            StudentId = studentId;
            ReviewDate = reviewDate;
            Content = content;
        }

        // Constructor without ID (for new reviews where ID will be auto-assigned)
        public Review(string studentId, DateTime reviewDate, string content)
        {
            StudentId = studentId;
            ReviewDate = reviewDate;
            Content = content;
        }
    }
}
