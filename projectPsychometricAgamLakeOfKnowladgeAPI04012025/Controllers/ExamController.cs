using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
using System.Collections.Generic;

namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public ExamController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all exams
        // GET: api/Exam
        [HttpGet]
        public IActionResult GetExams()
        {
            var exams = new List<Exam>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT exam_id, name, date FROM exam";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var exam = new Exam(
                        reader.GetInt32(0),  // exam_id
                        reader.GetString(1), // name
                        reader.GetDateTime(2) // date
                    );
                    exams.Add(exam);
                }

                reader.Close();
            }

            return Ok(exams);
        }

        // ✅ 2. Get exams conducted on a specific date
        // GET: api/Exam/ByDate/{date}
        [HttpGet("ByDate/{date}")]
        public IActionResult GetExamsByDate(DateTime date)
        {
            var exams = new List<Exam>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT exam_id, name, date FROM exam WHERE date = @date";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var exam = new Exam(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDateTime(2)
                    );
                    exams.Add(exam);
                }

                reader.Close();
            }

            return Ok(exams);
        }

        // ✅ 3. Get a specific exam by ID
        // GET: api/Exam/{id}
        [HttpGet("{id}")]
        public IActionResult GetExamById(int id)
        {
            Exam exam = null;

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT exam_id, name, date FROM exam WHERE exam_id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    exam = new Exam(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDateTime(2)
                    );
                }

                reader.Close();
            }

            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        // ✅ 4. Get exams scheduled after a specific date
        // GET: api/Exam/AfterDate/{date}
        [HttpGet("AfterDate/{date}")]
        public IActionResult GetExamsAfterDate(DateTime date)
        {
            var exams = new List<Exam>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT exam_id, name, date FROM exam WHERE date > @date";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var exam = new Exam(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDateTime(2)
                    );
                    exams.Add(exam);
                }

                reader.Close();
            }

            return Ok(exams);
        }
    }
}
