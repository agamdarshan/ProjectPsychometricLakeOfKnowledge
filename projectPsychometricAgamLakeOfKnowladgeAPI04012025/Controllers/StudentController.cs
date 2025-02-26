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
    public class StudentController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public StudentController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all students
        // GET: api/Student
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = new List<Student>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var student = new Student(
                        reader.GetString("id"),
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("phone_number"),
                        reader.GetDateTime("registration_date"),
                        reader.GetInt32("progress_level_english"),
                        reader.GetInt32("progress_level_hebrew"),
                        reader.GetInt32("progress_level_quantity"),
                        reader.GetString("password"),
                        reader.GetBoolean("extra_time"),
                        reader.GetBoolean("larger_font"),
                        reader.GetBoolean("background_adjustment")
                    );
                    students.Add(student);
                }

                reader.Close();
            }

            return Ok(students);
        }

        // ✅ 2. Get students registered after a specific date
        // GET: api/Student/RegisteredAfter/{date}
        [HttpGet("RegisteredAfter/{date}")]
        public IActionResult GetStudentsRegisteredAfter(DateTime date)
        {
            var students = new List<Student>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student WHERE registration_date > @date";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetInt32("progress_level_english"),
                            reader.GetInt32("progress_level_hebrew"),
                            reader.GetInt32("progress_level_quantity"),
                            reader.GetString("password"),
                            reader.GetBoolean("extra_time"),
                            reader.GetBoolean("larger_font"),
                            reader.GetBoolean("background_adjustment")
                        );
                        students.Add(student);
                    }
                }
            }

            return Ok(students);
        }

        // ✅ 3. Get students with English progress level above a specified value
        // GET: api/Student/ProgressEnglishAbove/{level}
        [HttpGet("ProgressEnglishAbove/{level}")]
        public IActionResult GetStudentsWithProgressEnglishAbove(int level)
        {
            var students = new List<Student>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student WHERE progress_level_english > @level";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@level", level);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetInt32("progress_level_english"),
                            reader.GetInt32("progress_level_hebrew"),
                            reader.GetInt32("progress_level_quantity"),
                            reader.GetString("password"),
                            reader.GetBoolean("extra_time"),
                            reader.GetBoolean("larger_font"),
                            reader.GetBoolean("background_adjustment")
                        );
                        students.Add(student);
                    }
                }
            }

            return Ok(students);
        }

        // ✅ 4. Get students who have at least one special adjustment
        // GET: api/Student/WithAdjustments
        [HttpGet("WithAdjustments")]
        public IActionResult GetStudentsWithAdjustments()
        {
            var students = new List<Student>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student " +
                               "WHERE extra_time = true OR larger_font = true OR background_adjustment = true";
                MySqlCommand command = new MySqlCommand(query, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetInt32("progress_level_english"),
                            reader.GetInt32("progress_level_hebrew"),
                            reader.GetInt32("progress_level_quantity"),
                            reader.GetString("password"),
                            reader.GetBoolean("extra_time"),
                            reader.GetBoolean("larger_font"),
                            reader.GetBoolean("background_adjustment")
                        );
                        students.Add(student);
                    }
                }
            }

            return Ok(students);
        }

        // ✅ 5. Get students with a combined progress level across all categories above a specific total
        // GET: api/Student/TotalProgressAbove/{total}
        [HttpGet("TotalProgressAbove/{total}")]
        public IActionResult GetStudentsWithTotalProgressAbove(int total)
        {
            var students = new List<Student>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student " +
                               "WHERE (progress_level_english + progress_level_hebrew + progress_level_quantity) > @total";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@total", total);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetInt32("progress_level_english"),
                            reader.GetInt32("progress_level_hebrew"),
                            reader.GetInt32("progress_level_quantity"),
                            reader.GetString("password"),
                            reader.GetBoolean("extra_time"),
                            reader.GetBoolean("larger_font"),
                            reader.GetBoolean("background_adjustment")
                        );
                        students.Add(student);
                    }
                }
            }

            return Ok(students);
        }

        // ✅ Get a student by ID
        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public IActionResult GetStudentById(string id)
        {
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student WHERE id = @StudentId";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@StudentId", id);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var student = new Student(
                        reader.GetString("id"),
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("phone_number"),
                        reader.GetDateTime("registration_date"),
                        reader.GetInt32("progress_level_english"),
                        reader.GetInt32("progress_level_hebrew"),
                        reader.GetInt32("progress_level_quantity"),
                        reader.GetString("password"),
                        reader.GetBoolean("extra_time"),
                        reader.GetBoolean("larger_font"),
                        reader.GetBoolean("background_adjustment")
                    );

                    reader.Close();
                    return Ok(student);
                }

                reader.Close();
                return NotFound($"Student with ID {id} not found.");
            }
        }

        // ✅ Authenticate student by email and password
        // GET: api/Student/Login?email=example@example.com&password=12345
        [HttpGet("Login")]
        public IActionResult AuthenticateStudent(string email, string password)
        {
            Student student = null;

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, " +
                               "progress_level_hebrew, progress_level_quantity, extra_time, larger_font, " +
                               "background_adjustment, password FROM student " +
                               "WHERE email = @Email AND password = @Password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        student = new Student(
                            reader.GetString("id"),
                            reader.GetString("name"),
                            reader.GetString("email"),
                            reader.GetString("phone_number"),
                            reader.GetDateTime("registration_date"),
                            reader.GetInt32("progress_level_english"),
                            reader.GetInt32("progress_level_hebrew"),
                            reader.GetInt32("progress_level_quantity"),
                            reader.GetString("password"),
                            reader.GetBoolean("extra_time"),
                            reader.GetBoolean("larger_font"),
                            reader.GetBoolean("background_adjustment")
                        );
                    }
                }
            }

            if (student == null)
                return Unauthorized();

            return Ok(student);
        }

        // ✅ Add a new student
        // POST: api/Student
        [HttpPost]
        public IActionResult AddStudent([FromBody] Student newStudent)
        {
            if (newStudent == null)
            {
                return BadRequest("Invalid student data.");
            }

            // Check if the password is provided
            if (string.IsNullOrEmpty(newStudent.Password))
            {
                return BadRequest("Password is required.");
            }

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First, check if a student with this ID or email already exists
                string checkQuery = "SELECT COUNT(*) FROM student WHERE id = @id OR email = @email";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", newStudent.Id);
                checkCommand.Parameters.AddWithValue("@email", newStudent.Email);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount > 0)
                {
                    return BadRequest("A student with this ID or email already exists.");
                }

                // If no existing student found, proceed with insertion
                string query = @"
                    INSERT INTO student (id, name, email, phone_number, registration_date, 
                                       progress_level_english, progress_level_hebrew, progress_level_quantity, 
                                       extra_time, larger_font, background_adjustment, password)
                    VALUES (@id, @name, @email, @phone_number, @registration_date, 
                           @progress_level_english, @progress_level_hebrew, @progress_level_quantity, 
                           @extra_time, @larger_font, @background_adjustment, @password)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", newStudent.Id);
                command.Parameters.AddWithValue("@name", newStudent.Name);
                command.Parameters.AddWithValue("@email", newStudent.Email);
                command.Parameters.AddWithValue("@phone_number", newStudent.PhoneNumber);
                command.Parameters.AddWithValue("@registration_date", newStudent.RegistrationDate);
                command.Parameters.AddWithValue("@progress_level_english", newStudent.ProgressLevelEnglish);
                command.Parameters.AddWithValue("@progress_level_hebrew", newStudent.ProgressLevelHebrew);
                command.Parameters.AddWithValue("@progress_level_quantity", newStudent.ProgressLevelQuantity);
                command.Parameters.AddWithValue("@extra_time", newStudent.ExtraTime);
                command.Parameters.AddWithValue("@larger_font", newStudent.LargerFont);
                command.Parameters.AddWithValue("@background_adjustment", newStudent.BackgroundAdjustment);
                command.Parameters.AddWithValue("@password", newStudent.Password);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Student added successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while adding the student.");
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

        // Update a student
        // PUT: api/Student/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(string id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || id != updatedStudent.Id)
            {
                return BadRequest("Invalid student data or ID mismatch.");
            }

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First check if the student exists
                string checkQuery = "SELECT COUNT(*) FROM student WHERE id = @id";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount == 0)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                // Update the student
                string query = @"
                    UPDATE student 
                    SET name = @name, 
                        email = @email, 
                        phone_number = @phone_number, 
                        progress_level_english = @progress_level_english, 
                        progress_level_hebrew = @progress_level_hebrew, 
                        progress_level_quantity = @progress_level_quantity,
                        extra_time = @extra_time,
                        larger_font = @larger_font,
                        background_adjustment = @background_adjustment,
                        password = @password
                    WHERE id = @id";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", updatedStudent.Name);
                command.Parameters.AddWithValue("@email", updatedStudent.Email);
                command.Parameters.AddWithValue("@phone_number", updatedStudent.PhoneNumber);
                command.Parameters.AddWithValue("@progress_level_english", updatedStudent.ProgressLevelEnglish);
                command.Parameters.AddWithValue("@progress_level_hebrew", updatedStudent.ProgressLevelHebrew);
                command.Parameters.AddWithValue("@progress_level_quantity", updatedStudent.ProgressLevelQuantity);
                command.Parameters.AddWithValue("@extra_time", updatedStudent.ExtraTime);
                command.Parameters.AddWithValue("@larger_font", updatedStudent.LargerFont);
                command.Parameters.AddWithValue("@background_adjustment", updatedStudent.BackgroundAdjustment);
                command.Parameters.AddWithValue("@password", updatedStudent.Password);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Student updated successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while updating the student.");
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

        // Delete a student
        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(string id)
        {
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First check if the student exists
                string checkQuery = "SELECT COUNT(*) FROM student WHERE id = @id";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);

                int existingCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (existingCount == 0)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                // Delete the student
                string query = "DELETE FROM student WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok("Student deleted successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while deleting the student.");
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




////using Microsoft.AspNetCore.Mvc;
////using MySql.Data.MySqlClient;
////using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
////using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
////using System.Collections.Generic;

////namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class StudentController : ControllerBase
////    {
////        private readonly DatabaseService _databaseService;

////        public StudentController(DatabaseService databaseService)
////        {
////            _databaseService = databaseService;
////        }

////        // GET: api/Student
////        [HttpGet]
////        public IActionResult GetStudents()
////        {
////            var students = new List<Student>();

////            using (MySqlConnection connection = _databaseService.GetConnection())
////            {
////                connection.Open();
////                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, progress_level_hebrew, progress_level_quantity, extra_time, larger_font, background_adjustment FROM student";
////                MySqlCommand command = new MySqlCommand(query, connection);
////                MySqlDataReader reader = command.ExecuteReader();

////                while (reader.Read())
////                {
////                    var student = new Student(
////                        reader.GetInt32(0),
////                        reader.GetString(1),
////                        reader.GetString(2),
////                        reader.GetString(3),
////                        reader.GetDateTime(4),
////                        reader.GetInt32(5),
////                        reader.GetInt32(6),
////                        reader.GetInt32(7),
////                        reader.GetBoolean(8),
////                        reader.GetBoolean(9),
////                        reader.GetBoolean(10)
////                    );
////                    students.Add(student);
////                }

////                reader.Close();
////            }

////            return Ok(students);
////        }
////    }
////}


//using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
//using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
//using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
//using System.Collections.Generic;

//namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentController : ControllerBase
//    {
//        private readonly DatabaseService _databaseService;

//        public StudentController(DatabaseService databaseService)
//        {
//            _databaseService = databaseService;
//        }

//        // ✅ 1. Get all students
//        // GET: api/Student
//        [HttpGet]
//        public IActionResult GetStudents()
//        {
//            var students = new List<Student>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT id, name, email, phone_number, registration_date, progress_level_english, progress_level_hebrew, progress_level_quantity, extra_time, larger_font, background_adjustment FROM student";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                MySqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    var student = new Student(
//                        reader.GetString(0),
//                        reader.GetString(1),
//                        reader.GetString(2),
//                        reader.GetString(3),
//                        reader.GetDateTime(4),
//                        reader.GetInt32(5),
//                        reader.GetInt32(6),
//                        reader.GetInt32(7),
//                        reader.GetBoolean(8),
//                        reader.GetBoolean(9),
//                        reader.GetBoolean(10)
//                    );
//                    students.Add(student);
//                }

//                reader.Close();
//            }

//            return Ok(students);
//        }

//        // ✅ 2. Get students registered after a specific date
//        // GET: api/Student/RegisteredAfter/{date}
//        [HttpGet("RegisteredAfter/{date}")]
//        public IActionResult GetStudentsRegisteredAfter(DateTime date)
//        {
//            var students = new List<Student>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM student WHERE registration_date > @date";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@date", date);

//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var student = new Student(
//                            reader.GetString("id"),
//                            reader.GetString("name"),
//                            reader.GetString("email"),
//                            reader.GetString("phone_number"),
//                            reader.GetDateTime("registration_date"),
//                            reader.GetInt32("progress_level_english"),
//                            reader.GetInt32("progress_level_hebrew"),
//                            reader.GetInt32("progress_level_quantity"),
//                            reader.GetBoolean("extra_time"),
//                            reader.GetBoolean("larger_font"),
//                            reader.GetBoolean("background_adjustment")
//                        );
//                        students.Add(student);
//                    }
//                }
//            }

//            return Ok(students);
//        }

//        // ✅ 3. Get students with English progress level above a specified value
//        // GET: api/Student/ProgressEnglishAbove/{level}
//        [HttpGet("ProgressEnglishAbove/{level}")]
//        public IActionResult GetStudentsWithProgressEnglishAbove(int level)
//        {
//            var students = new List<Student>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM student WHERE progress_level_english > @level";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@level", level);

//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var student = new Student(
//                            reader.GetString("id"),
//                            reader.GetString("name"),
//                            reader.GetString("email"),
//                            reader.GetString("phone_number"),
//                            reader.GetDateTime("registration_date"),
//                            reader.GetInt32("progress_level_english"),
//                            reader.GetInt32("progress_level_hebrew"),
//                            reader.GetInt32("progress_level_quantity"),
//                            reader.GetBoolean("extra_time"),
//                            reader.GetBoolean("larger_font"),
//                            reader.GetBoolean("background_adjustment")
//                        );
//                        students.Add(student);
//                    }
//                }
//            }

//            return Ok(students);
//        }

//        // ✅ 4. Get students who have at least one special adjustment
//        // GET: api/Student/WithAdjustments
//        [HttpGet("WithAdjustments")]
//        public IActionResult GetStudentsWithAdjustments()
//        {
//            var students = new List<Student>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM student WHERE extra_time = true OR larger_font = true OR background_adjustment = true";
//                MySqlCommand command = new MySqlCommand(query, connection);

//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var student = new Student(
//                            reader.GetString("id"),
//                            reader.GetString("name"),
//                            reader.GetString("email"),
//                            reader.GetString("phone_number"),
//                            reader.GetDateTime("registration_date"),
//                            reader.GetInt32("progress_level_english"),
//                            reader.GetInt32("progress_level_hebrew"),
//                            reader.GetInt32("progress_level_quantity"),
//                            reader.GetBoolean("extra_time"),
//                            reader.GetBoolean("larger_font"),
//                            reader.GetBoolean("background_adjustment")
//                        );
//                        students.Add(student);
//                    }
//                }
//            }

//            return Ok(students);
//        }

//        // ✅ 5. Get students with a combined progress level across all categories above 200
//        // GET: api/Student/TotalProgressAbove/{total}
//        [HttpGet("TotalProgressAbove/{total}")]
//        public IActionResult GetStudentsWithTotalProgressAbove(int total)
//        {
//            var students = new List<Student>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM student WHERE (progress_level_english + progress_level_hebrew + progress_level_quantity) > @total";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@total", total);

//                using (var reader = command.ExecuteReader())
//                {
//                    while (reader.Read())
//                    {
//                        var student = new Student(
//                            reader.GetString("id"),
//                            reader.GetString("name"),
//                            reader.GetString("email"),
//                            reader.GetString("phone_number"),
//                            reader.GetDateTime("registration_date"),
//                            reader.GetInt32("progress_level_english"),
//                            reader.GetInt32("progress_level_hebrew"),
//                            reader.GetInt32("progress_level_quantity"),
//                            reader.GetBoolean("extra_time"),
//                            reader.GetBoolean("larger_font"),
//                            reader.GetBoolean("background_adjustment")
//                        );
//                        students.Add(student);
//                    }
//                }
//            }

//            return Ok(students);
//        }


//        // ✅  Get a student by ID
//        [HttpGet("{id}")]
//        public IActionResult GetStudentById(int id)
//        {
//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT id, name, email, phone_number, registration_date, " +
//                               "progress_level_english, progress_level_hebrew, progress_level_quantity, " +
//                               "extra_time, larger_font, background_adjustment FROM student WHERE id = @StudentId";

//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@StudentId", id);
//                MySqlDataReader reader = command.ExecuteReader();

//                if (reader.Read())
//                {
//                    var student = new Student(
//                        reader.GetString(0),
//                        reader.GetString(1),
//                        reader.GetString(2),
//                        reader.GetString(3),
//                        reader.GetDateTime(4),
//                        reader.GetInt32(5),
//                        reader.GetInt32(6),
//                        reader.GetInt32(7),
//                        reader.GetBoolean(8),
//                        reader.GetBoolean(9),
//                        reader.GetBoolean(10)
//                    );

//                    reader.Close();
//                    return Ok(student);
//                }

//                reader.Close();
//                return NotFound($"Student with ID {id} not found.");
//            }
//        }


//        // ✅ Authenticate student by email and password
//        // GET: api/Student/Login?email=example@example.com&password=12345
//        [HttpGet("Login")]
//        public IActionResult AuthenticateStudent(string email, string password)
//        {
//            Student student = null;

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT * FROM student WHERE email = @Email AND password = @Password";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                command.Parameters.AddWithValue("@Email", email);
//                command.Parameters.AddWithValue("@Password", password);

//                using (var reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        student = new Student(
//                            reader.GetString("id"),
//                            reader.GetString("name"),
//                            reader.GetString("email"),
//                            reader.GetString("phone_number"),
//                            reader.GetDateTime("registration_date"),
//                            reader.GetInt32("progress_level_english"),
//                            reader.GetInt32("progress_level_hebrew"),
//                            reader.GetInt32("progress_level_quantity"),
//                            reader.GetBoolean("extra_time"),
//                            reader.GetBoolean("larger_font"),
//                            reader.GetBoolean("background_adjustment")
//                        );
//                    }
//                }
//            }

//            if (student == null)
//                return Unauthorized();

//            return Ok(student);
//        }



//    }


//}

