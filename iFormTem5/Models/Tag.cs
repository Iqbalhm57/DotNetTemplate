using System.ComponentModel.DataAnnotations;

namespace iFormTem5.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = string.Empty;

        public ICollection<TemplateTag> TemplateTags { get; set; } = new List<TemplateTag>();
    }
}
