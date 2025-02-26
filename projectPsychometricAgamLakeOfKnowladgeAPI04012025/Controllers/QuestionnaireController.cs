using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Services;
using projectPsychometricAgamLakeOfKnowladgeAPI04012025.Classes;
using System.Collections.Generic;

namespace projectPsychometricAgamLakeOfKnowladgeAPI04012025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public QuestionnaireController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all questionnaires
        // GET: api/Questionnaire
        [HttpGet]
        public IActionResult GetQuestionnaires()
        {
            var questionnaires = new List<Questionnaire>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT questionnaire_id, exam_id, category, title FROM questionnaire";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var questionnaire = new Questionnaire(
                        reader.GetInt32(0),  // questionnaire_id
                        reader.GetInt32(1),  // exam_id
                        reader.GetString(2), // category
                        reader.GetString(3)  // title
                    );
                    questionnaires.Add(questionnaire);
                }

                reader.Close();
            }

            return Ok(questionnaires);
        }

        // ✅ 2. Get questionnaires by exam ID
        // GET: api/Questionnaire/ByExam/{examId}
        [HttpGet("ByExam/{examId}")]
        public IActionResult GetQuestionnairesByExam(int examId)
        {
            var questionnaires = new List<Questionnaire>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM questionnaire WHERE exam_id = @examId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@examId", examId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var questionnaire = new Questionnaire(
                            reader.GetInt32("questionnaire_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetString("category"),
                            reader.GetString("title")
                        );
                        questionnaires.Add(questionnaire);
                    }
                }
            }

            return Ok(questionnaires);
        }

        // ✅ 3. Get questionnaires by category
        // GET: api/Questionnaire/ByCategory/{category}
        [HttpGet("ByCategory/{category}")]
        public IActionResult GetQuestionnairesByCategory(string category)
        {
            var questionnaires = new List<Questionnaire>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM questionnaire WHERE category = @category";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@category", category);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var questionnaire = new Questionnaire(
                            reader.GetInt32("questionnaire_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetString("category"),
                            reader.GetString("title")
                        );
                        questionnaires.Add(questionnaire);
                    }
                }
            }

            return Ok(questionnaires);
        }

        // ✅ 4. Get a questionnaire by its ID
        // GET: api/Questionnaire/{id}
        [HttpGet("{id}")]
        public IActionResult GetQuestionnaireById(int id)
        {
            Questionnaire questionnaire = null;

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM questionnaire WHERE questionnaire_id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        questionnaire = new Questionnaire(
                            reader.GetInt32("questionnaire_id"),
                            reader.GetInt32("exam_id"),
                            reader.GetString("category"),
                            reader.GetString("title")
                        );
                    }
                }
            }

            if (questionnaire == null)
            {
                return NotFound("Questionnaire not found.");
            }

            return Ok(questionnaire);
        }
    }
}
