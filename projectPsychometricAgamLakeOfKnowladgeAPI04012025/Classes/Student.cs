namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{
    public class Student : User
    {

        public int ProgressLevelEnglish { get; set; }
        public int ProgressLevelHebrew { get; set; }
        public int ProgressLevelQuantity { get; set; }
        public bool ExtraTime { get; set; }
        public bool LargerFont { get; set; }
        public bool BackgroundAdjustment { get; set; }

        public Student() { }

        public Student(string id, string name, string email, string phoneNumber, DateTime registrationDate,
                   int progressLevelEnglish, int progressLevelHebrew, int progressLevelQuantity, string password,
                   bool extraTime, bool largerFont, bool backgroundAdjustment)
        : base(id, name, email, phoneNumber, registrationDate, UserRole.Student,password) // Default to Student role
        {
            ProgressLevelEnglish = progressLevelEnglish;
            ProgressLevelHebrew = progressLevelHebrew;
            ProgressLevelQuantity = progressLevelQuantity;
            ExtraTime = extraTime;
            LargerFont = largerFont;
            BackgroundAdjustment = backgroundAdjustment;
        }

       // public Student(string id, string name, string email, string phoneNumber, DateTime registrationDate,
       //           int progressLevelEnglish, int progressLevelHebrew, int progressLevelQuantity,
       //           bool extraTime, bool largerFont, bool backgroundAdjustment)
       //: base(id, name, email, phoneNumber, registrationDate, UserRole.Student, password) // Default to Student role
       // {
       //     ProgressLevelEnglish = progressLevelEnglish;
       //     ProgressLevelHebrew = progressLevelHebrew;
       //     ProgressLevelQuantity = progressLevelQuantity;
       //     ExtraTime = extraTime;
       //     LargerFont = largerFont;
       //     BackgroundAdjustment = backgroundAdjustment;
       // }

    }
}
