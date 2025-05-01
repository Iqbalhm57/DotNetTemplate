using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFormTem5.Models
{
    public class Template
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [StringLength(50)]
        public string? Topic { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsPublic { get; set; } = true;

        public int UserId { get; set; } 
        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        public ICollection<TemplateTag> TemplateTags { get; set; } = new List<TemplateTag>();
    }
}
