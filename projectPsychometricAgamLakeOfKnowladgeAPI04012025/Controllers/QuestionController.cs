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
    public class QuestionController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public QuestionController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // ✅ 1. Get all questions
        // GET: api/Question
        [HttpGet]
        public IActionResult GetQuestions()
        {
            var questions = new List<Question>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT question_id, questionnaire_id, guide_id, text, difficulty_level, option_a, option_b, option_c, option_d, correct_option FROM question";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var question = new Question(
                        reader.GetInt32(0),  // question_id
                        reader.GetInt32(1),  // questionnaire_id
                        reader.GetString(2), // guide_id
                        reader.GetString(3), // text
                        reader.GetString(4), // difficulty_level
                        reader.GetString(5), // option_a
                        reader.GetString(6), // option_b
                        reader.GetString(7), // option_c
                        reader.GetString(8), // option_d
                        reader.GetString(9)  // correct_option
                    );
                    questions.Add(question);
                }

                reader.Close();
            }

            return Ok(questions);
        }

        // ✅ 2. Get questions by questionnaire ID
        // GET: api/Question/ByQuestionnaire/{questionnaireId}
        [HttpGet("ByQuestionnaire/{questionnaireId}")]
        public IActionResult GetQuestionsByQuestionnaire(int questionnaireId)
        {
            var questions = new List<Question>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM question WHERE questionnaire_id = @questionnaireId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@questionnaireId", questionnaireId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var question = new Question(
                            reader.GetInt32("question_id"),
                            reader.GetInt32("questionnaire_id"),
                            reader.GetString("guide_id"),
                            reader.GetString("text"),
                            reader.GetString("difficulty_level"),
                            reader.GetString("option_a"),
                            reader.GetString("option_b"),
                            reader.GetString("option_c"),
                            reader.GetString("option_d"),
                            reader.GetString("correct_option")
                        );
                        questions.Add(question);
                    }
                }
            }

            return Ok(questions);
        }

        // ✅ 3. Get questions by difficulty level
        // GET: api/Question/ByDifficulty/{difficultyLevel}
        [HttpGet("ByDifficulty/{difficultyLevel}")]
        public IActionResult GetQuestionsByDifficulty(string difficultyLevel)
        {
            var questions = new List<Question>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM question WHERE difficulty_level = @difficultyLevel";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@difficultyLevel", difficultyLevel);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var question = new Question(
                            reader.GetInt32("question_id"),
                            reader.GetInt32("questionnaire_id"),
                            reader.GetString("guide_id"),
                            reader.GetString("text"),
                            reader.GetString("difficulty_level"),
                            reader.GetString("option_a"),
                            reader.GetString("option_b"),
                            reader.GetString("option_c"),
                            reader.GetString("option_d"),
                            reader.GetString("correct_option")
                        );
                        questions.Add(question);
                    }
                }
            }

            return Ok(questions);
        }

        // ✅ 4. Get a specific question by its ID
        // GET: api/Question/{id}
        [HttpGet("{id}")]
        public IActionResult GetQuestionById(int id)
        {
            Question question = null;

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM question WHERE question_id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        question = new Question(
                            reader.GetInt32("question_id"),
                            reader.GetInt32("questionnaire_id"),
                            reader.GetString("guide_id"),
                            reader.GetString("text"),
                            reader.GetString("difficulty_level"),
                            reader.GetString("option_a"),
                            reader.GetString("option_b"),
                            reader.GetString("option_c"),
                            reader.GetString("option_d"),
                            reader.GetString("correct_option")
                        );
                    }
                }
            }

            if (question == null)
            {
                return NotFound("Question not found.");
            }

            return Ok(question);
        }

        // ✅ 5. Update an existing question
        // PUT: api/Question/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateQuestion(int id, [FromBody] Question updatedQuestion)
        {
            // Validate input
            if (updatedQuestion == null || id != updatedQuestion.QuestionId)
            {
                return BadRequest("Invalid question data or mismatched ID.");
            }

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // First, check if the question exists
                string checkQuery = "SELECT COUNT(*) FROM question WHERE question_id = @id";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);

                int questionCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (questionCount == 0)
                {
                    return NotFound($"Question with ID {id} not found.");
                }

                // Prepare update query
                string updateQuery = @"
                    UPDATE question 
                    SET 
                        questionnaire_id = @questionnaireId, 
                        guide_id = @guideId, 
                        text = @text, 
                        difficulty_level = @difficultyLevel, 
                        option_a = @optionA, 
                        option_b = @optionB, 
                        option_c = @optionC, 
                        option_d = @optionD, 
                        correct_option = @correctOption
                    WHERE question_id = @id";

                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);

                // Add parameters
                updateCommand.Parameters.AddWithValue("@id", id);
                updateCommand.Parameters.AddWithValue("@questionnaireId", updatedQuestion.QuestionnaireId);
                updateCommand.Parameters.AddWithValue("@guideId", updatedQuestion.GuideId);
                updateCommand.Parameters.AddWithValue("@text", updatedQuestion.Text);
                updateCommand.Parameters.AddWithValue("@difficultyLevel", updatedQuestion.DifficultyLevel);
                updateCommand.Parameters.AddWithValue("@optionA", updatedQuestion.OptionA);
                updateCommand.Parameters.AddWithValue("@optionB", updatedQuestion.OptionB);
                updateCommand.Parameters.AddWithValue("@optionC", updatedQuestion.OptionC);
                updateCommand.Parameters.AddWithValue("@optionD", updatedQuestion.OptionD);
                updateCommand.Parameters.AddWithValue("@correctOption", updatedQuestion.CorrectOption);

                try
                {
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(new
                        {
                            Message = "Question updated successfully",
                            Question = updatedQuestion
                        });
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while updating the question.");
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