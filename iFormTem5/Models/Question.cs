using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFormTem5.Models
{
    public enum QuestionType
    {
        SingleLine,
        MultiLine,
        Integer,
        Checkbox
    }

    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        public Template? Template { get; set; }

        [Required]
        public QuestionType Type { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; } = string.Empty;

        public bool ShowInTable { get; set; } = false;

        public int Order { get; set; } = 0;
        public int DisplayOrder { get; set; }

        [StringLength(500)]
        public string? CorrectAnswer { get; set; }

   
        [Range(0, 100)]
        public int PointValue { get; set; } = 1; // Default 1 point per question

    }
}
