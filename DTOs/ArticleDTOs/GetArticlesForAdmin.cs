﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ArticleDTOs
{
    public record GetArticlesForAdmin
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public ICollection<ReportArticle> reportArticles{ get; init; }

    }
}
