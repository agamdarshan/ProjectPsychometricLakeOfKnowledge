

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;

namespace BlazorWebAppLakeOfKnowledge04012025.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("API");
        }

        // Fetch all guides, properly deserializing into List<Guide>
        public async Task<List<Guide>> GetDataAsyncAPIGetAllGuides()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Guide>>("api/Guide", new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Ensures JSON keys match class properties even if case differs
                });

                return response ?? new List<Guide>(); // Return empty list if null
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching guides: {ex.Message}");
                return new List<Guide>(); // Handle error gracefully
            }
        }

        // Fetch all students, properly deserializing into List<Student>
        public async Task<List<Student>> GetDataAsyncAPIGetAllStudents()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Student>>("api/Student", new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return response ?? new List<Student>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching students: {ex.Message}");
                return new List<Student>();
            }
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Student>($"api/Student/{id}", new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return response;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching student by ID: {ex.Message}");
                return null; // Handle error gracefully
            }
        }

        public async Task<Guide> AuthenticateGuideOrAdminAsync(string email, string password)
        {
            try
            {
                // Create the URL with query parameters
                var response = await _httpClient.GetAsync($"api/Guide/Login?email={email}&password={password}");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to a Guide object
                    var guide = await response.Content.ReadFromJsonAsync<Guide>();
                    return guide; // Return the guide or admin object if found

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during guide authentication: {ex.Message}");
            }

            return null; // Return null if authentication fails
        }

        //public async Task<string> AddGuideAsync(Guide newGuide)
        //{
        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("api/Guide", newGuide);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // If the guide is successfully added, return a success message
        //            return "Guide added successfully.";
        //        }
        //        else
        //        {
        //            // If there was an error with the POST request
        //            return "Failed to add guide.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Error adding guide: {ex.Message}");
        //        return "An error occurred while adding the guide.";
        //    }
        //}

        public async Task<string> AddStudentAsync(Student newStudent)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Student", newStudent);

                // Read the response content regardless of status code
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseContent ?? "Student added successfully.";
                }
                else
                {
                    // Return the specific error message from the API if available
                    Console.WriteLine($"API Error: {response.StatusCode} - {responseContent}");
                    return responseContent ?? $"Failed to add student. Status: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Network error adding student: {ex.Message}");
                return "חיבור לשרת נכשל. אנא בדוק את חיבור האינטרנט שלך ונסה שנית.";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding student: {ex.Message}");
                return "אירעה שגיאה בלתי צפויה בעת הוספת הסטודנט.";
            }
        }


        public async Task<string> AddGuideAsync(Guide newGuide)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Guide", newGuide);

                // Read the response content regardless of status code
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseContent ?? "Guide added successfully.";
                }
                else
                {
                    // Return the specific error message from the API if available
                    return responseContent ?? $"Failed to add guide. Status: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.Error.WriteLine($"Network error adding guide: {ex.Message}");
                return "חיבור לשרת נכשל. אנא בדוק את חיבור האינטרנט שלך ונסה שנית.";
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding guide: {ex.Message}");
                return "אירעה שגיאה בלתי צפויה בעת הוספת המדריך.";
            }
        }




        public async Task<Student> AuthenticateStudentAsync(string email, string password)
        {
            try
            {
                // Create the URL with query parameters
                var response = await _httpClient.GetAsync($"api/Student/Login?email={email}&password={password}");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to a Student object
                    var student = await response.Content.ReadFromJsonAsync<Student>();
                    return student; // Return the student object if found
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during student authentication: {ex.Message}");
            }

            return null; // Return null if authentication fails
        }


        //public async Task<bool> AuthenticateStudentAsync(string email, string password)
        //{
        //    try
        //    {
        //        var credentials = new { Email = email, Password = password };
        //        var response = await _httpClient.PostAsJsonAsync("api/Student/Login", credentials);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"API Response: {result}"); // Log response content
        //            if (result.Contains("Student")) // Make sure the response is what you expect
        //            {
        //                return true;
        //            }
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Error during student authentication: {ex.Message}");
        //        return false;
        //    }
        //}



        //// Authenticate as a student
        //public async Task<string> AuthenticateStudentAsync(string email, string password)
        //{
        //    try
        //    {
        //        var credentials = new { Email = email, Password = password };

        //        var response = await _httpClient.PostAsJsonAsync("api/Student/Login", credentials);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return "Student"; // Return "Student" if authentication is successful
        //        }

        //        return "Failed"; // Return "Failed" if authentication fails
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine($"Error during student authentication: {ex.Message}");
        //        return "Failed"; // Return "Failed" in case of an error
        //    }
        //}

        // Authenticate as a guide
        public async Task<string> AuthenticateGuideAsync(string email, string password)
        {
            try
            {
                var credentials = new { Email = email, Password = password };

                var response = await _httpClient.PostAsJsonAsync("api/Guide/Login", credentials);

                if (response.IsSuccessStatusCode)
                {
                    return "Guide"; // Return "Guide" if authentication is successful
                }

                return "Failed"; // Return "Failed" if authentication fails
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during guide authentication: {ex.Message}");
                return "Failed"; // Return "Failed" in case of an error
            }
        }

        // Get all reviews
        public async Task<List<Review>> GetReviewsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Review>>("api/Review", new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return response ?? new List<Review>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching reviews: {ex.Message}");
                return new List<Review>();
            }
        }

        // Add a new review
        public async Task<string> AddReviewAsync(Review review)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Review", review);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return "Review added successfully.";
                }
                else
                {
                    return $"Failed to add review: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error adding review: {ex.Message}");
                return $"An error occurred: {ex.Message}";
            }
        }

        // Delete a review
        public async Task<string> DeleteReviewAsync(int reviewId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Review/{reviewId}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return "Review deleted successfully.";
                }
                else
                {
                    return $"Failed to delete review: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting review: {ex.Message}");
                return $"An error occurred: {ex.Message}";
            }
        }

        // Get a specific question by ID
        public async Task<string> GetQuestionByIdAsync(int questionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Question/{questionId}");
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {
                    return $"Failed to retrieve question: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error retrieving question: {ex.Message}");
                return $"An error occurred: {ex.Message}";
            }
        }

        // Update an existing question
        public async Task<string> UpdateQuestionAsync(Question updatedQuestion)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Question/{updatedQuestion.QuestionId}", updatedQuestion);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return responseContent;
                }
                else
                {
                    return $"Failed to update question: {responseContent}";
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error updating question: {ex.Message}");
                return $"An error occurred: {ex.Message}";
            }
        }




    }
}

