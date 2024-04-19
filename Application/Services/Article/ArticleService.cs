
using Application.Contract;
using AutoMapper;
using DTO_s.ViewResult;
using DTOs.ArticleDTOs;
using DTOs.Paginated;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;
        private readonly IReportArticleRepository _reportArticleRepository;

        public ArticleService(IArticleRepository articleRepository, UserManager<AppUser> userManager, IMapper mapper, IReportArticleRepository reportArticleRepository)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
            _mapper = mapper;
            _reportArticleRepository = reportArticleRepository;
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
        public async Task<PaginatedList<GetArticlesForUser>> GetArticlesForUser(int PageNumber, int PageSize)
        {
            var articles = await _articleRepository.GetAllArticlesForUsers();

            var paginatedArticles = await PaginatedList<GetArticlesForUser>.CreateAsync(articles, PageNumber, PageSize);

            return paginatedArticles;
        }
        public async Task<string> CreateReport(string articleTitle,string userName)
        {
            var articles = await _articleRepository.GetAllAsync();
            var article = await articles.FirstOrDefaultAsync(a=>a.Title== articleTitle);
            if (article is null)
            {
                return "Not Found!!";
            }
            var user = await _userManager.FindByNameAsync(userName);
           var reports =  await _reportArticleRepository.GetAllAsync();
            var res = reports.Where(r => r.Article.Id == article.Id && r.User.UserName == userName);
            if (!res.Any())
            {
                ReportArticle reportArticle = new ReportArticle()
                {
                    Article = article,
                    User = user
                };

                await _reportArticleRepository.CreateAsync(reportArticle);
                await _reportArticleRepository.SaveChangesAsync();
                return "Reported successfully";
            }
            return "It has been reported before!!";
        }
        public async Task<ReportedArticle> GetAllReportBy(int articleId)
        {
            var article = await _articleRepository.GetByIdAsync(articleId);
            if (article is not null)
            {
                var reported = await _reportArticleRepository.GetAllAsync();
                var reportedByArticle = await reported.Where(r => r.ArticleId == articleId).ToListAsync();
                List<UserNameAndEmailOfUserDto> res = new List<UserNameAndEmailOfUserDto>();
                foreach (var report in reportedByArticle)
                {
                    var userlop = await _userManager.FindByIdAsync(report.UserName);
                  
                    var x = new UserNameAndEmailOfUserDto()
                    {
                        UserName = userlop.UserName,
                        Email = userlop.Email
                    };
                   
                    res.Add(x);
                }
               
                var r = new ReportedArticle()
                {
                    Title = article.Title,
                    UserNameAndEmailOfUserDto = res
                };
                return r;
            }
            return null;


        }




    }
}
