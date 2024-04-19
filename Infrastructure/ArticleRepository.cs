using Application.Contract;
using Context;
using DTOs.ArticleDTOs;
using DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ArticleRepository :Repository<Article, int>, IArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
        public async Task<List<GetArticlesForAdmin>> GetAllArticles()
        {
            var res = await _context.Articles.AsNoTracking().ToListAsync();
            List<GetArticlesForAdmin> result = new List<GetArticlesForAdmin>();
            foreach (var article in res)
            {
                var articl = new GetArticlesForAdmin () {
                    Id = article.Id,
                    Title = article.Title
                };
                result.Add(articl);
            }
            return result;
        }
        public async Task DeleteBy(int id)
        {
            var article = await _context.Articles.FindAsync(id);
   
        }
        public async Task< IQueryable<GetArticlesForUser>> GetAllArticlesForUsers()
        {
            var articles =  _context.Articles
                                   .Select(article => new GetArticlesForUser
                                   {
                                       Description = article.Description,
                                       Image = article.Image,
                                       Link = article.Link,
                                       Title = article.Title
                                   });

            return  articles;
        }

    }
}
