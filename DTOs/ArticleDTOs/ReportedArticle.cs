using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ArticleDTOs
{
    public class ReportedArticle
    {
        public List<UserNameAndEmailOfUserDto> UserNameAndEmailOfUserDto { get; init; }
        public string Title { get; init; }
    }
}
