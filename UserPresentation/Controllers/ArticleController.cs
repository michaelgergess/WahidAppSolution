using Application.Service.User;
using Application.Services.Article;
using DTOs.ArticleDTOs;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace UserPresentation.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<AppUser> _userManager;

        public ArticleController(IArticleService articleService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

    
        public async Task< ActionResult> Index()
        {
            var res = await _articleService.GetAllArticlesForAdmin();
            return View(res);
        }
        public async Task<ActionResult> IndexUser(int PageNumber=1)
        {
            const int pageSize = 9;
            var res = await _articleService.GetArticlesForUser(PageNumber, pageSize);
            return View(res);
        }
        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(ArticleDTO article)
        {
            if (ModelState.IsValid)
            {
                var result = await _articleService.Create(article);
                if (result.IsSuccess == true)
                {
                    ModelState.AddModelError("", result.Message);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Message);
                return View(article);
            }
            else
            {
                return View(article);
            }
        }

      
      

        // GET: ArticleController/Delete/5
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (id > 0)
            {
             var res =  await _articleService.ConfrimationForDeleteAsync(id);
                return View(res);
            }

            return NotFound();
        }

        // POST: Article/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _articleService.DeleteArticleAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    
    public async Task<IActionResult> CheckTitle(string title)
        {
            bool exist = await _articleService.TitleIsExist(title);
            return Json(!exist);

        }
    }
}
