using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFormTem5.Models
{
    public class UserAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; // ASP.NET Identity User ID

        [Required]
        public string Answer { get; set; } = string.Empty;
        [Required]
        public int TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public Template Template { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // New field to store the calculated score for this answer
        public int Score { get; set; }

        //  NotMapped property to determine correctness
        [NotMapped]
        public bool IsCorrect =>
            Normalize(Answer) == Normalize(Question?.CorrectAnswer ?? "");

        private static string Normalize(string input) =>
            input.Trim().ToLowerInvariant();
    }
}
