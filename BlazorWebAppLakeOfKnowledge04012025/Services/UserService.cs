using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;

namespace BlazorWebAppLakeOfKnowledge04012025.Services
{
    public class UserService
    {

        public string userId { get; set; }
        public string userName { get; set; }
        public UserRole role { get; set; }

        // Constructor with default values
        public UserService()
        {
            userId = "0";
            userName = "Guest";
            role = UserRole.Guest;
        }

        // Function to update user data
        public void UpdateUserData(string newUserId, string newUserName, UserRole newRole)
        {
            userId = newUserId;
            userName = newUserName;
            role = newRole;
        }

        // פונקציה לאיפוס הנתונים במקרה של יציאה מהמערכת
        public void ClearUser()
        {
            userId = "0";
            userName = "Guest";
            role = UserRole.Guest;
        }

    }
}
