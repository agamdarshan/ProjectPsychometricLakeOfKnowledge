using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
using System.Collections.Generic;
using System;

namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public GuideController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all guides
        // GET: api/Guide
        [HttpGet]
        public IActionResult GetGuides()
        {
            var guides = new List<Guide>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, " +
                               "expertise_quantity, expertise_hebrew, role, password FROM guide";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Determine the user's role based on the database value
                    string roleString = reader.GetString("role");
                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        ? UserRole.Admin
                        : UserRole.Guide;

                    // Create the guide object with the role and other data, including password
                    var guide = new Guide(
                        reader.GetString("id"),
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("phone_number"),
                        reader.GetDateTime("registration_date"),
                        reader.GetBoolean("expertise_english"),
                        reader.GetBoolean("expertise_quantity"),
                        reader.GetBoolean("expertise_hebrew"),
                        role,
                        reader.GetString("password")
                    );
                    guides.Add(guide);
                }

                reader.Close();
            }

            return Ok(guides);
        }

        // ✅ 2. Get guides with expertise in English
        // GET: api/Guide/WithEnglishExpertise
        [HttpGet("WithEnglishExpertise")]
        public IActionResult GetGuidesWithEnglishExpertise()
        {
            var guides = new List<Guide>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, " +
                               "expertise_quantity, expertise_hebrew, role, password FROM guide WHERE expertise_english = TRUE";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Determine the user's role based on the database value
                    string roleString = reader.GetString("role");
                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        ? UserRole.Admin
                        : UserRole.Guide;

                    // Create the guide object with the role and other data, including password
                    var guide = new Guide(
                        reader.GetString("id"),
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("phone_number"),
                        reader.GetDateTime("registration_date"),
                        reader.GetBoolean("expertise_english"),
                        reader.GetBoolean("expertise_quantity"),
                        reader.GetBoolean("expertise_hebrew"),
                        role,
                        reader.GetString("password")
                    );
                    guides.Add(guide);
                }

                reader.Close();
            }

            return Ok(guides);
        }

        // ✅ 3. Get guides registered in the last year
        // GET: api/Guide/Recent
        [HttpGet("Recent")]
        public IActionResult GetRecentGuides()
        {
            var guides = new List<Guide>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, " +
                               "expertise_quantity, expertise_hebrew, role, password FROM guide " +
                               "WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR)";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Determine the user's role based on the database value
                    string roleString = reader.GetString("role");
                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        ? UserRole.Admin
                        : UserRole.Guide;

                    // Create the guide object with the role and other data, including password
                    var guide = new Guide(
                        reader.GetString("id"),
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("phone_number"),
                        reader.GetDateTime("registration_date"),
                        reader.GetBoolean("expertise_english"),
                        reader.GetBoolean("expertise_quantity"),
                        reader.GetBoolean("expertise_hebrew"),
                        role,
                        reader.GetString("password")
                    );
                    guides.Add(guide);
                }

                reader.Close();
            }

            return Ok(guides);
        }

        // ✅ 4. Get guides with more than one expertise
        // GET: api/Guide/WithMultipleExpertise
        [HttpGet("WithMultipleExpertise")]
        public IActionResult GetGuidesWithMultipleExpertise()
        {
            var guides = new List<Guide>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, " +
                               "expertise_quantity, expertise_hebrew, role, password FROM guide " +
                               "WHERE (expertise_english + expertise_quantity + expertise_hebrew) > 1";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Determine the user's role based on the database value
                    string roleString = reader.GetString("role");
                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        ? UserRole.Admin
                        : UserRole.Guide;

                    // Create the guide object with the role and other data, including password
                    var guide = new Guide(
                        reader.GetString("id"),
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("phone_number"),
                        reader.GetDateTime("registration_date"),
                        reader.GetBoolean("expertise_english"),
                        reader.GetBoolean("expertise_quantity"),
                        reader.GetBoolean("expertise_hebrew"),
                        role,
                        reader.GetString("password")
                    );
                    guides.Add(guide);
                }

                reader.Close();
            }

            return Ok(guides);
        }

        // ✅ 5. Get a specific guide by ID
        // GET: api/Guide/{id}
        [HttpGet("{id}")]
        public IActionResult GetGuideById(string id)
        {
            Guide guide = null;

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, " +
                               "expertise_quantity, expertise_hebrew, role, password FROM guide WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Determine the user's role based on the database value
                        string roleString = reader.GetString("role");
                        UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                            ? UserRole.Admin
                            : UserRole.Guide;

                        // Create the guide object with the role and other data, including password
                        guide = new Guide(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetBoolean("expertise_english"),
                            reader.GetBoolean("expertise_quantity"),
                            reader.GetBoolean("expertise_hebrew"),
                            role,
                            reader.GetString("password")
                        );
                    }
                }
            }

            if (guide == null)
                return NotFound();

            return Ok(guide);
        }

        // ✅ Authenticate guide by email and password
        // GET: api/Guide/Login?email=example@example.com&password=12345
        [HttpGet("Login")]
        public IActionResult AuthenticateGuide(string email, string password)
        {
            Guide guide = null;

            // Establish a connection to the database
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // Define the SQL query to search for a guide by email and password
                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, " +
                               "expertise_quantity, expertise_hebrew, role, password FROM guide " +
                               "WHERE email = @Email AND password = @Password";
                MySqlCommand command = new MySqlCommand(query, connection);

                // Adding parameters to prevent SQL injection
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                // Execute the query and read the result
                using (var reader = command.ExecuteReader())
                {
                    // Check if the query returned a result
                    if (reader.Read())
                    {
                        // Retrieve the role from the database and map it to the UserRole enum
                        string roleString = reader.GetString("role");
                        UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                            ? UserRole.Admin
                            : UserRole.Guide;

                        // Initialize the Guide object with data from the database, including password
                        guide = new Guide(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetBoolean("expertise_english"),
                            reader.GetBoolean("expertise_quantity"),
                            reader.GetBoolean("expertise_hebrew"),
                            role,
                            reader.GetString("password")
                        );
                    }
                }
            }

            // If no guide was found, return Unauthorized response
            if (guide == null)
                return Unauthorized(); // Unauthorized if credentials are incorrect or no guide found

            // Return the guide object with the status of Ok
            return Ok(guide); // Return the guide with status 200 OK
        }

        // ✅ 3. Add a new guide
        // POST: api/Guide
        [HttpPost]
        public IActionResult AddGuide([FromBody] Guide newGuide)
        {
            if (newGuide == null)
            {
                return BadRequest("Invalid guide data.");
            }

            // Check if the password is provided
            if (string.IsNullOrEmpty(newGuide.Password))
            {
                return BadRequest("Password is required.");
            }

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First, check if a guide with this ID or email already exists
                string checkQuery = "SELECT COUNT(*) FROM guide WHERE id = @id OR email = @email";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", newGuide.Id);
                checkCommand.Parameters.AddWithValue("@email", newGuide.Email);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount > 0)
                {
                    return BadRequest("A guide with this ID or email already exists.");
                }

                // If no existing guide found, proceed with insertion
                string query = @"
                    INSERT INTO guide (id, name, email, phone_number, registration_date, expertise_english, 
                                     expertise_quantity, expertise_hebrew, role, password)
                    VALUES (@id, @name, @email, @phone_number, @registration_date, @expertise_english, 
                           @expertise_quantity, @expertise_hebrew, @role, @password)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", newGuide.Id);
                command.Parameters.AddWithValue("@name", newGuide.Name);
                command.Parameters.AddWithValue("@email", newGuide.Email);
                command.Parameters.AddWithValue("@phone_number", newGuide.PhoneNumber);
                command.Parameters.AddWithValue("@registration_date", newGuide.RegistrationDate);
                command.Parameters.AddWithValue("@expertise_english", newGuide.ExpertiseEnglish);
                command.Parameters.AddWithValue("@expertise_quantity", newGuide.ExpertiseQuantity);
                command.Parameters.AddWithValue("@expertise_hebrew", newGuide.ExpertiseHebrew);
                command.Parameters.AddWithValue("@role", newGuide.Role.ToString());
                command.Parameters.AddWithValue("@password", newGuide.Password);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Guide added successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while adding the guide.");
                    }
                }
                catch (MySqlException ex)
                {
                    return StatusCode(500, $"Database error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }

        // Update a guide
        // PUT: api/Guide/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateGuide(string id, [FromBody] Guide updatedGuide)
        {
            if (updatedGuide == null || id != updatedGuide.Id)
            {
                return BadRequest("Invalid guide data or ID mismatch.");
            }

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First check if the guide exists
                string checkQuery = "SELECT COUNT(*) FROM guide WHERE id = @id";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount == 0)
                {
                    return NotFound($"Guide with ID {id} not found.");
                }

                // Update the guide
                string query = @"
                    UPDATE guide 
                    SET name = @name, 
                        email = @email, 
                        phone_number = @phone_number, 
                        expertise_english = @expertise_english, 
                        expertise_quantity = @expertise_quantity, 
                        expertise_hebrew = @expertise_hebrew,
                        password = @password
                    WHERE id = @id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", updatedGuide.Name);
                command.Parameters.AddWithValue("@email", updatedGuide.Email);
                command.Parameters.AddWithValue("@phone_number", updatedGuide.PhoneNumber);
                command.Parameters.AddWithValue("@expertise_english", updatedGuide.ExpertiseEnglish);
                command.Parameters.AddWithValue("@expertise_quantity", updatedGuide.ExpertiseQuantity);
                command.Parameters.AddWithValue("@expertise_hebrew", updatedGuide.ExpertiseHebrew);
                command.Parameters.AddWithValue("@password", updatedGuide.Password);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Guide updated successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while updating the guide.");
                    }
                }
                catch (MySqlException ex)
                {
                    return StatusCode(500, $"Database error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }

        // Delete a guide
        // DELETE: api/Guide/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteGuide(string id)
        {
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First check if the guide exists
                string checkQuery = "SELECT COUNT(*) FROM guide WHERE id = @id";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount == 0)
                {
                    return NotFound($"Guide with ID {id} not found.");
                }

                // Delete the guide
                string query = "DELETE FROM guide WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Guide deleted successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while deleting the guide.");
                    }
                }
                catch (MySqlException ex)
                {
                    return StatusCode(500, $"Database error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
        }
    }
}








//using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
//using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
//using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
//using System.Collections.Generic;
//using Microsoft.AspNetCore.Identity.Data;

//namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GuideController : ControllerBase
//    {
//        private readonly DatabaseService _databaseService;

//        public GuideController(DatabaseService databaseService)
//        {
//            _databaseService = databaseService;
//        }

//        // ✅ 1. Get all guides
//        // GET: api/Guide
//        [HttpGet]
//        public IActionResult GetGuides()
//        {
//            var guides = new List<Guide>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT id, name, email, phone_number, registration_date, expertise_english, expertise_quantity, expertise_hebrew, role FROM guide";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                MySqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    // Determine the user's role based on the database value
//                    string roleString = reader.GetString("role");
//                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
//                        ? UserRole.Admin
//                        : UserRole.Guide;

//                    // Create the guide object with the role and other data
//                    var guide = new Guide(
//                        reader.GetString("id"),
//                        reader.GetString("name"),
//                        reader.GetString("email"),
//                        reader.GetString("phone_number"),
//                        reader.GetDateTime("registration_date"),
//                        reader.GetBoolean("expertise_english"),
//                        reader.GetBoolean("expertise_quantity"),
//                        reader.GetBoolean("expertise_hebrew"),
//                        role
//                    );
//                    guides.Add(guide);
//                }

//                reader.Close();
//            }

//            return Ok(guides);
//        }

//        // ✅ 2. Get guides with expertise in English
//        // GET: api/Guide/WithEnglishExpertise
//        [HttpGet("WithEnglishExpertise")]
//        public IActionResult GetGuidesWithEnglishExpertise()
//        {
//            var guides = new List<Guide>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM guide WHERE expertise_english = TRUE";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                MySqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    // Determine the user's role based on the database value
//                    string roleString = reader.GetString("role");
//                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
//                        ? UserRole.Admin
//                        : UserRole.Guide;

//                    // Create the guide object with the role and other data
//                    var guide = new Guide(
//                        reader.GetString("id"),
//                        reader.GetString("name"),
//                        reader.GetString("email"),
//                        reader.GetString("phone_number"),
//                        reader.GetDateTime("registration_date"),
//                        reader.GetBoolean("expertise_english"),
//                        reader.GetBoolean("expertise_quantity"),
//                        reader.GetBoolean("expertise_hebrew"),
//                        role
//                    );
//                    guides.Add(guide);
//                }

//                reader.Close();
//            }

//            return Ok(guides);
//        }

//        // ✅ 3. Get guides registered in the last year
//        // GET: api/Guide/Recent
//        [HttpGet("Recent")]
//        public IActionResult GetRecentGuides()
//        {
//            var guides = new List<Guide>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM guide WHERE registration_date >= DATE_SUB(CURDATE(), INTERVAL 1 YEAR)";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                MySqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    // Determine the user's role based on the database value
//                    string roleString = reader.GetString("role");
//                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
//                        ? UserRole.Admin
//                        : UserRole.Guide;

//                    // Create the guide object with the role and other data
//                    var guide = new Guide(
//                        reader.GetString("id"),
//                        reader.GetString("name"),
//                        reader.GetString("email"),
//                        reader.GetString("phone_number"),
//                        reader.GetDateTime("registration_date"),
//                        reader.GetBoolean("expertise_english"),
//                        reader.GetBoolean("expertise_quantity"),
//                        reader.GetBoolean("expertise_hebrew"),
//                        role
//                    );
//                    guides.Add(guide);
//                }

//                reader.Close();
//            }

//            return Ok(guides);
//        }

//        // ✅ 4. Get guides with more than one expertise
//        // GET: api/Guide/WithMultipleExpertise
//        [HttpGet("WithMultipleExpertise")]
//        public IActionResult GetGuidesWithMultipleExpertise()
//        {
//            var guides = new List<Guide>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM guide WHERE (expertise_english + expertise_quantity + expertise_hebrew) > 1";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                MySqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    // Determine the user's role based on the database value
//                    string roleString = reader.GetString("role");
//                    UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
//                        ? UserRole.Admin
//                        : UserRole.Guide;

//                    // Create the guide object with the role and other data
//                    var guide = new Guide(
//                        reader.GetString("id"),
//                        reader.GetString("name"),
//                        reader.GetString("email"),
//                        reader.GetString("phone_number"),
//                        reader.GetDateTime("registration_date"),
//                        reader.GetBoolean("expertise_english"),
//                        reader.GetBoolean("expertise_quantity"),
//                        reader.GetBoolean("expertise_hebrew"),
//                        role
//                    );
//                    guides.Add(guide);
//                }

//                reader.Close();
//            }

//            return Ok(guides);
//        }

//        // ✅ 5. Get a specific guide by ID
//        // GET: api/Guide/{id}
//        [HttpGet("{id}")]
//        public IActionResult GetGuideById(int id)
//        {
//            Guide guide = null;

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM guide WHERE id = @id";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@id", id);

//                using (var reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        // Determine the user's role based on the database value
//                        string roleString = reader.GetString("role");
//                        UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
//                            ? UserRole.Admin
//                            : UserRole.Guide;

//                        // Create the guide object with the role and other data
//                        guide = new Guide(
//                            reader.GetString("id"),
//                            reader.GetString("name"),
//                            reader.GetString("email"),
//                            reader.GetString("phone_number"),
//                            reader.GetDateTime("registration_date"),
//                            reader.GetBoolean("expertise_english"),
//                            reader.GetBoolean("expertise_quantity"),
//                            reader.GetBoolean("expertise_hebrew"),
//                            role
//                        );
//                    }
//                }
//            }

//            if (guide == null)
//                return NotFound();

//            return Ok(guide);
//        }

//        // ✅ Authenticate guide by email and password
//        // GET: api/Guide/Login?email=example@example.com&password=12345
//        [HttpGet("Login")]
//        public IActionResult AuthenticateGuide(string email, string password)
//        {
//            Guide guide = null;

//            // Establish a connection to the database
//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();

//                // Define the SQL query to search for a guide by email and password
//                string query = "SELECT * FROM guide WHERE email = @Email AND password = @Password";
//                MySqlCommand command = new MySqlCommand(query, connection);

//                // Adding parameters to prevent SQL injection
//                command.Parameters.AddWithValue("@Email", email);
//                command.Parameters.AddWithValue("@Password", password);

//                // Execute the query and read the result
//                using (var reader = command.ExecuteReader())
//                {
//                    // Check if the query returned a result
//                    if (reader.Read())
//                    {
//                        // Retrieve the role from the database and map it to the UserRole enum
//                        string roleString = reader.GetString("role");
//                        UserRole role = roleString.Equals("Admin", StringComparison.OrdinalIgnoreCase)
//                            ? UserRole.Admin
//                            : UserRole.Guide;

//                        // Initialize the Guide object with data from the database
//                        guide = new Guide(
//                            reader.GetString("id"), // ID of the guide
//                            reader.GetString("name"), // Name of the guide
//                            reader.GetString("email"), // Email of the guide
//                            reader.GetString("phone_number"), // Phone number of the guide
//                            reader.GetDateTime("registration_date"), // Registration date
//                            reader.GetBoolean("expertise_english"), // Expertise in English
//                            reader.GetBoolean("expertise_quantity"), // Expertise in Quantity
//                            reader.GetBoolean("expertise_hebrew"), // Expertise in Hebrew
//                            role // Role (Guide or Admin)
//                        );
//                    }
//                }
//            }

//            // If no guide was found, return Unauthorized response
//            if (guide == null)
//                return Unauthorized(); // Unauthorized if credentials are incorrect or no guide found

//            // Return the guide object with the status of Ok
//            return Ok(guide); // Return the guide with status 200 OK
//        }


//        // ✅ 3. Add a new guide
//        // POST: api/Guide
//        [HttpPost]
//        public IActionResult AddGuide([FromBody] Guide newGuide)
//        {
//            if (newGuide == null)
//            {
//                return BadRequest("Invalid guide data.");
//            }

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = @"
//            INSERT INTO guide (id, name, email, phone_number, registration_date, expertise_english, expertise_quantity, expertise_hebrew, role, password)
//            VALUES (@id, @name, @email, @phone_number, @registration_date, @expertise_english, @expertise_quantity, @expertise_hebrew, @role, @password)";

//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@id", newGuide.Id);
//                command.Parameters.AddWithValue("@name", newGuide.Name);
//                command.Parameters.AddWithValue("@email", newGuide.Email);
//                command.Parameters.AddWithValue("@phone_number", newGuide.PhoneNumber);
//                command.Parameters.AddWithValue("@registration_date", newGuide.RegistrationDate);
//                command.Parameters.AddWithValue("@expertise_english", newGuide.ExpertiseEnglish);
//                command.Parameters.AddWithValue("@expertise_quantity", newGuide.ExpertiseQuantity);
//                command.Parameters.AddWithValue("@expertise_hebrew", newGuide.ExpertiseHebrew);
//                command.Parameters.AddWithValue("@role", UserRole.Guide.ToString()); // Assuming 'Role' is an Enum, we convert it to string
//                command.Parameters.AddWithValue("@password", newGuide.Password);

//                int rowsAffected = command.ExecuteNonQuery();

//                if (rowsAffected > 0)
//                {
//                    return Ok("Guide added successfully.");
//                }
//                else
//                {
//                    return StatusCode(500, "An error occurred while adding the guide.");
//                }
//            }
//        }


//    }
//}
