using System.ComponentModel.DataAnnotations;

namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes
{



    public abstract class User
    {
        [Required(ErrorMessage = "נדרש להזין תעודת זהות")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "תעודת זהות חייבת להכיל 9 ספרות")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "תעודת זהות חייבת להכיל רק ספרות")]
        public string Id { get; set; }

        [Required(ErrorMessage = "נדרש להזין שם מלא")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "שם חייב להכיל בין 2 ל-50 תווים")]
        public string Name { get; set; }

        [Required(ErrorMessage = "נדרש להזין כתובת אימייל")]
        [EmailAddress(ErrorMessage = "כתובת אימייל לא תקינה")]
        public string Email { get; set; }

        [Required(ErrorMessage = "נדרש להזין סיסמה")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "הסיסמה חייבת להכיל לפחות 6 תווים")]
        public string Password { get; set; }

        [Required(ErrorMessage = "נדרש להזין מספר טלפון")]
        [RegularExpression(@"^0\d{8,9}$", ErrorMessage = "מספר טלפון לא תקין")]
        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }

        protected User(string id, string name, string email, string phoneNumber, DateTime registrationDate, UserRole role, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            RegistrationDate = registrationDate;
            Role = role;
            Password = password;
        }

        public User() { }
    }





    //public abstract class User
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }

    //    public string Password { get; set; }

    //    public string PhoneNumber { get; set; }
    //    public DateTime RegistrationDate { get; set; }

    //    public UserRole Role { get; set; }  // Add this property

       
    //    protected User(string id, string name, string email, string phoneNumber, DateTime registrationDate, UserRole role)
    //    {
    //        Id = id;
    //        Name = name;
    //        Email = email;
    //        PhoneNumber = phoneNumber;
    //        RegistrationDate = registrationDate;
    //        Role = role;
    //    }

    //    public User() {}

    //}
}
