using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Article :BaseEntity
    {
        
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }

        [ForeignKey("User")]
        public string AdminId { get; set; }
        public AppUser User { get; set; }

        public ICollection<ReportArticle> ReportArticle { get; set; }

    }
}