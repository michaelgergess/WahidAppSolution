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
    public class ReportArticleRepository : Repository<ReportArticle, int>, IReportArticleRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportArticleRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
       

    }
}
