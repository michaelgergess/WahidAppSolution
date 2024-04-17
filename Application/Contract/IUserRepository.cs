using Application.Contracts;
using DTO_s.ViewResult;
using DTOs.UserDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IUserRepository 
    {
    
        Task<ResultView<UserRegisterDTO>> UserUpdate(UserRegisterDTO user);
        Task<ResultView<UserRegisterDTO>> softDelete(string userID);
     
       



    }
}
