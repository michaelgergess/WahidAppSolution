
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 

    public class AppUser : IdentityUser
    {
        public bool IsBlocked { get; set; } =false;

        public ICollection<Article> Article { get; set; }

        public ICollection<ReportArticle> ReportArticle { get; set; }

        public AppUser()
        {
            ReportArticle = new List<ReportArticle>();
            Article = new List<Article>();
        }


    }
}
