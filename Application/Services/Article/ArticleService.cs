
using Application.Contract;
using DTO_s.ViewResult;
using DTOs.ArticleDTOs;
using Microsoft.AspNetCore.Identity;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly UserManager<AppUser> _userManager;

        public ArticleService(IArticleRepository articleRepository, UserManager<AppUser> userManager)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
        }

        public async Task<ResultView<ArticleDTO>> Create(ArticleDTO articleDTO)
        {
            if (articleDTO == null)
            {
                return new ResultView<ArticleDTO> { 
                    Entity = null, IsSuccess = false, Message = "Article NOT Saved" 
                };
            }
            using var stream = new MemoryStream();
            await articleDTO.Image.CopyToAsync(stream);
            var user = await _userManager.FindByNameAsync(articleDTO.AdminName);
            Model.Article article = new Model.Article()
            {
                AdminId = user.Id,
                Description = articleDTO.Description,
                Image = stream.ToArray(),
                Link = articleDTO.Link,
                Title = articleDTO.Title,
            };
            
            var res = await _articleRepository.CreateAsync(article);
            await _articleRepository.SaveChangesAsync();
            return new ResultView<ArticleDTO>
            {
                Entity = articleDTO,
                IsSuccess = true,
                Message = "Article Saved"
            };
        }
        

        public async Task<bool> TitleIsExist(string Title)
        {
            var articles = await _articleRepository.GetAllAsync();
           var res = articles.Any(a => a.Title == Title);
            return res;
        }
        public async Task<List<GetArticlesForAdmin>> GetAllArticlesForAdmin()
        {
            var articles = await _articleRepository.GetAllArticles();
            return articles;
        }
        public async Task<bool> DeleteArticleAsync(int id)
        {
            
            var article = await _articleRepository.GetByIdAsync(id);
            if (article is not null)
            {
                await _articleRepository.DeleteAsync(article);
                await _articleRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<GetArticlesForAdmin> ConfrimationForDeleteAsync(int id)
        {

            var article = await _articleRepository.GetByIdAsync(id);
          if(article is not null)
            {
                return new GetArticlesForAdmin { Id =  article.Id ,Title = article.Title};
            }
          return null;
        }




    }
}
