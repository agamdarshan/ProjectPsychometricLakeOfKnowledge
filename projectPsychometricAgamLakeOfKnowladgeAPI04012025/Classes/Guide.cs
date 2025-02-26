namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{

    public class Guide : User
    {
        public bool ExpertiseEnglish { get; set; }
        public bool ExpertiseQuantity { get; set; }
        public bool ExpertiseHebrew { get; set; }

        public Guide() { }

        public Guide(string id, string name, string email, string phoneNumber, DateTime registrationDate,
                     bool expertiseEnglish, bool expertiseQuantity, bool expertiseHebrew, UserRole role, string password)
        : base(id, name, email, phoneNumber, registrationDate, role, password)
        {
            ExpertiseEnglish = expertiseEnglish;
            ExpertiseQuantity = expertiseQuantity;
            ExpertiseHebrew = expertiseHebrew;
        }
    }

    //public class Guide : User
    //{
    //    public bool ExpertiseEnglish { get; set; }
    //    public bool ExpertiseQuantity { get; set; }
    //    public bool ExpertiseHebrew { get; set; }

    //    public Guide() { }


    //    public Guide(string id, string name, string email, string phoneNumber, DateTime registrationDate,
    //                 bool expertiseEnglish, bool expertiseQuantity, bool expertiseHebrew, UserRole role)
    //    : base(id, name, email, phoneNumber, registrationDate, role) // Pass the role to the base class
    //    {
    //        ExpertiseEnglish = expertiseEnglish;
    //        ExpertiseQuantity = expertiseQuantity;
    //        ExpertiseHebrew = expertiseHebrew;
    //    }
    //}
}
