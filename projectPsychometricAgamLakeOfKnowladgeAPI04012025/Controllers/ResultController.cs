//using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
//using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
//using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
//using System.Collections.Generic;

//namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ResultController : ControllerBase
//    {
//        private readonly DatabaseService _databaseService;

//        public ResultController(DatabaseService databaseService)
//        {
//            _databaseService = databaseService;
//        }

//        // GET: api/Result
//        [HttpGet]
//        public IActionResult GetResults()
//        {
//            var results = new List<Result>();

//            using (MySqlConnection connection = _databaseService.GetConnection())
//            {
//                connection.Open();
//                string query = "SELECT result_id, student_id, exam_id, score_english, score_hebrew, score_quantity, completion_date FROM result";
//                MySqlCommand command = new MySqlCommand(query, connection);
//                MySqlDataReader reader = command.ExecuteReader();

//                while (reader.Read())
//                {
//                    var result = new Result(
//                        reader.GetInt32(0),  // result_id
//                        reader.GetInt32(1),  // student_id
//                        reader.GetInt32(2),  // exam_id
//                        reader.GetInt32(3),  // score_english
//                        reader.GetInt32(4),  // score_hebrew
//                        reader.GetInt32(5),  // score_quantity
//                        reader.GetDateTime(6) // completion_date
//                    );
//                    results.Add(result);
//                }

//                reader.Close();
//            }

//            return Ok(results);
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
using System.Collections.Generic;

namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public ResultController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all results
        // GET: api/Result
        [HttpGet]
        public IActionResult GetResults()
        {
            var results = new List<Result>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT result_id, student_id, exam_id, score_english, score_hebrew, score_quantity, completion_date FROM result";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var result = new Result(
                        reader.GetInt32(0),  // result_id
                        reader.GetInt32(1),  // student_id
                        reader.GetInt32(2),  // exam_id
                        reader.GetInt32(3),  // score_english
                        reader.GetInt32(4),  // score_hebrew
                        reader.GetInt32(5),  // score_quantity
                        reader.GetDateTime(6) // completion_date
                    );
                    results.Add(result);
                }

                reader.Close();
            }

            return Ok(results);
        }

        // ✅ 2. Get results by student ID
        // GET: api/Result/ByStudent/{studentId}
        [HttpGet("ByStudent/{studentId}")]
        public IActionResult GetResultsByStudent(int studentId)
        {
            var results = new List<Result>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM result WHERE student_id = @studentId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentId", studentId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new Result(
                            reader.GetInt32("result_id"),
                            reader.GetInt32("student_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetInt32("score_english"),
                            reader.GetInt32("score_hebrew"),
                            reader.GetInt32("score_quantity"),
                            reader.GetDateTime("completion_date")
                        );
                        results.Add(result);
                    }
                }
            }

            return Ok(results);
        }

        // ✅ 3. Get results by exam ID
        // GET: api/Result/ByExam/{examId}
        [HttpGet("ByExam/{examId}")]
        public IActionResult GetResultsByExam(int examId)
        {
            var results = new List<Result>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM result WHERE exam_id = @examId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@examId", examId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new Result(
                            reader.GetInt32("result_id"),
                            reader.GetInt32("student_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetInt32("score_english"),
                            reader.GetInt32("score_hebrew"),
                            reader.GetInt32("score_quantity"),
                            reader.GetDateTime("completion_date")
                        );
                        results.Add(result);
                    }
                }
            }

            return Ok(results);
        }

        // ✅ 4. Get results with score above a certain threshold
        // GET: api/Result/AboveScore/{score}
        [HttpGet("AboveScore/{score}")]
        public IActionResult GetResultsAboveScore(int score)
        {
            var results = new List<Result>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM result WHERE (score_english + score_hebrew + score_quantity) > @score";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@score", score);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new Result(
                            reader.GetInt32("result_id"),
                            reader.GetInt32("student_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetInt32("score_english"),
                            reader.GetInt32("score_hebrew"),
                            reader.GetInt32("score_quantity"),
                            reader.GetDateTime("completion_date")
                        );
                        results.Add(result);
                    }
                }
            }

            return Ok(results);
        }

        // ✅ 5. Get results by completion date
        // GET: api/Result/ByDate/{date}
        [HttpGet("ByDate/{date}")]
        public IActionResult GetResultsByDate(DateTime date)
        {
            var results = new List<Result>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM result WHERE DATE(completion_date) = @date";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@date", date);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new Result(
                            reader.GetInt32("result_id"),
                            reader.GetInt32("student_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetInt32("score_english"),
                            reader.GetInt32("score_hebrew"),
                            reader.GetInt32("score_quantity"),
                            reader.GetDateTime("completion_date")
                        );
                        results.Add(result);
                    }
                }
            }

            return Ok(results);
        }
    }
}

