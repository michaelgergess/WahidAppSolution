using DTO_s.ViewResult;
using DTOs.ArticleDTOs;
using DTOs.Paginated;
using Model;

namespace Application.Services.Article
{
    public interface IArticleService
    {
        Task<ResultView<ArticleDTO>> Create(ArticleDTO articleDTO);
        Task<bool> TitleIsExist(string Title);
        Task<List<GetArticlesForAdmin>> GetAllArticlesForAdmin();
        Task<bool> DeleteArticleAsync(int id);
        Task<GetArticlesForAdmin> ConfrimationForDeleteAsync(int id);
        Task<PaginatedList<GetArticlesForUser>> GetArticlesForUser(int PageNumber, int PageSize);
    }
}