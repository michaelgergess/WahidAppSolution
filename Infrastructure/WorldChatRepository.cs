using Application.Contract;
using Application.Services.Article;
using Context;
using DTOs.ArticleDTOs;
using DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class WorldChatRepository :Repository<WorldChat, int>, IWorldChatRepository
    {
        private readonly ApplicationDbContext _context;

        public WorldChatRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
        public async Task<List<GetArticlesForAdmin>> GetAllArticles()
        {
            var res = await _context.Articles.Include(i => i.ReportArticle).AsNoTracking().ToListAsync();
            List<GetArticlesForAdmin> result = new List<GetArticlesForAdmin>();
            foreach (var article in res)
            {
                var articl = new GetArticlesForAdmin()
                {
                    Id = article.Id,
                    Title = article.Title,
                    reportArticles = article.ReportArticle
                };
                result.Add(articl);
            }
            return result;
        }


    }
}
