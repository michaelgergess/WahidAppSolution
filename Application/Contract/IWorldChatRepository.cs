using Application.Contracts;
using DTO_s.ViewResult;
using DTOs.ArticleDTOs;
using DTOs.UserDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IWorldChatRepository  : IRepository<WorldChat, int>
    {

    }
}
