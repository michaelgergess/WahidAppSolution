using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class ReportArticle :BaseEntity
    {

        [ForeignKey("User")]
        public string UserName { get; set; }
        public AppUser User { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        public Article Article { get; set; }

       

    }
}