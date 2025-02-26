using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
using System.Collections.Generic;

namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public PracticeController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all practices
        // GET: api/Practice
        [HttpGet]
        public IActionResult GetPractices()
        {
            var practices = new List<Practice>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT practice_id, student_id, category, difficulty_level, num_questions, score, time_spent FROM practice";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var practice = new Practice(
                        reader.GetInt32(0),   // practice_id
                        reader.GetInt32(1),   // student_id
                        reader.GetString(2),  // category
                        reader.GetString(3),  // difficulty_level
                        reader.GetInt32(4),   // num_questions
                        reader.GetInt32(5),   // score
                        reader.GetTimeSpan(6) // time_spent
                    );
                    practices.Add(practice);
                }

                reader.Close();
            }

            return Ok(practices);
        }

        // ✅ 2. Get practices by student ID
        // GET: api/Practice/ByStudent/{studentId}
        [HttpGet("ByStudent/{studentId}")]
        public IActionResult GetPracticesByStudentId(int studentId)
        {
            var practices = new List<Practice>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM practice WHERE student_id = @studentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentId", studentId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var practice = new Practice(
                            reader.GetInt32("practice_id"),
                            reader.GetInt32("student_id"),
                            reader.GetString("category"),
                            reader.GetString("difficulty_level"),
                            reader.GetInt32("num_questions"),
                            reader.GetInt32("score"),
                            reader.GetTimeSpan("time_spent")
                        );
                        practices.Add(practice);
                    }
                }
            }

            return Ok(practices);
        }

        // ✅ 3. Get practices by category
        // GET: api/Practice/ByCategory/{category}
        [HttpGet("ByCategory/{category}")]
        public IActionResult GetPracticesByCategory(string category)
        {
            var practices = new List<Practice>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM practice WHERE category = @category";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@category", category);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var practice = new Practice(
                            reader.GetInt32("practice_id"),
                            reader.GetInt32("student_id"),
                            reader.GetString("category"),
                            reader.GetString("difficulty_level"),
                            reader.GetInt32("num_questions"),
                            reader.GetInt32("score"),
                            reader.GetTimeSpan("time_spent")
                        );
                        practices.Add(practice);
                    }
                }
            }

            return Ok(practices);
        }

        // ✅ 4. Get practices with score above a certain value
        // GET: api/Practice/AboveScore/{score}
        [HttpGet("AboveScore/{score}")]
        public IActionResult GetPracticesAboveScore(int score)
        {
            var practices = new List<Practice>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM practice WHERE score > @score";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@score", score);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var practice = new Practice(
                            reader.GetInt32("practice_id"),
                            reader.GetInt32("student_id"),
                            reader.GetString("category"),
                            reader.GetString("difficulty_level"),
                            reader.GetInt32("num_questions"),
                            reader.GetInt32("score"),
                            reader.GetTimeSpan("time_spent")
                        );
                        practices.Add(practice);
                    }
                }
            }

            return Ok(practices);
        }

        // ✅ 5. Get practices by difficulty level
        // GET: api/Practice/ByDifficulty/{difficultyLevel}
        [HttpGet("ByDifficulty/{difficultyLevel}")]
        public IActionResult GetPracticesByDifficulty(string difficultyLevel)
        {
            var practices = new List<Practice>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM practice WHERE difficulty_level = @difficultyLevel";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@difficultyLevel", difficultyLevel);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var practice = new Practice(
                            reader.GetInt32("practice_id"),
                            reader.GetInt32("student_id"),
                            reader.GetString("category"),
                            reader.GetString("difficulty_level"),
                            reader.GetInt32("num_questions"),
                            reader.GetInt32("score"),
                            reader.GetTimeSpan("time_spent")
                        );
                        practices.Add(practice);
                    }
                }
            }

            return Ok(practices);
        }
    }
}
