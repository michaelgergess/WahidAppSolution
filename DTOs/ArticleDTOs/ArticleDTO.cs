using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ArticleDTOs
{
    public class ArticleDTO
    {
        [RegularExpression(@"^(https?|ftp):\/\/[^\s\/$.?#].[^\s]*$", ErrorMessage = "Invalid URL")]
        public string? Link { get; set; }
        [Required]
        [Remote(action: "CheckTitle", controller: "Article", ErrorMessage = "Title Already Exist")]

        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public string AdminName{ get; set; }
    }
}
