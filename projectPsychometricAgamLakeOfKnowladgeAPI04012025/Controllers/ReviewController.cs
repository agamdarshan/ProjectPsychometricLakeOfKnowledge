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
    public class ReviewController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public ReviewController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // GET: api/Review
        // Gets all reviews
        [HttpGet]
        public IActionResult GetReviews()
        {
            var reviews = new List<Review>();

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();
                string query = "SELECT review_id, student_id, review_date, content FROM review";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var review = new Review(
                        reader.GetInt32("review_id"),
                        reader.GetString("student_id"),
                        reader.GetDateTime("review_date"),
                        reader.GetString("content")
                    );
                    reviews.Add(review);
                }

                reader.Close();
            }

            return Ok(reviews);
        }

        // POST: api/Review
        // Adds a new review
        [HttpPost]
        public IActionResult AddReview([FromBody] Review newReview)
        {
            if (newReview == null)
            {
                return BadRequest("Invalid review data.");
            }

            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // Check if the student exists
                string checkQuery = "SELECT COUNT(*) FROM student WHERE id = @studentId";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@studentId", newReview.StudentId);

                int studentCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (studentCount == 0)
                {
                    return BadRequest($"Student with ID {newReview.StudentId} does not exist.");
                }

                // Insert the review
                string query = @"
                    INSERT INTO review (student_id, review_date, content)
                    VALUES (@studentId, @reviewDate, @content)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@studentId", newReview.StudentId);
                command.Parameters.AddWithValue("@reviewDate", newReview.ReviewDate);
                command.Parameters.AddWithValue("@content", newReview.Content);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Get the ID of the newly inserted review
                        int newReviewId = (int)command.LastInsertedId;
                        newReview.ReviewId = newReviewId;

                        return Ok(new { Message = "Review added successfully", Review = newReview });
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while adding the review.");
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

        // DELETE: api/Review/{id}
        // Deletes a review by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            using (MySqlConnection connection = _databaseService.GetConnection())
            {
                connection.Open();

                // Check if the review exists
                string checkQuery = "SELECT COUNT(*) FROM review WHERE review_id = @id";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@id", id);

                int reviewCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (reviewCount == 0)
                {
                    return NotFound($"Review with ID {id} not found.");
                }

                // Delete the review
                string query = "DELETE FROM review WHERE review_id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok($"Review with ID {id} deleted successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while deleting the review.");
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