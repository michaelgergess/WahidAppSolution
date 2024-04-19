using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ArticleDTOs
{
    public class GetArticlesForUser
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string Link { get; set; }
    }
}
