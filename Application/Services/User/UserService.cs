
using Application.Contract;
using AutoMapper;
using DTO_s.ViewResult;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.User
{
    public class UserService : IuserService
    {

        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _SignInManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

      

        public UserService( UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task CheckOrCreateRole(string role)
        {
            bool ExistsRole = await _roleManager.RoleExistsAsync(role.ToLower());

            if (!ExistsRole)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

        }
        public async Task<ResultView<UserRegisterDTO>> Registration(UserRegisterDTO account, string? RoleName = "admin")
        {

            var existUserEmail = await _UserManager.FindByEmailAsync(account.Email);
            var existUserName = await _UserManager.FindByNameAsync(account.UserName);

            if (existUserName != null || existUserEmail != null)
            {
                return new ResultView<UserRegisterDTO>()
                {
                    Entity = account,
                    IsSuccess = false,
                    Message = " Already Exist"
                };
            }
            var userModel = _mapper.Map<AppUser>(account);
         await CheckOrCreateRole(RoleName);

            var result = _UserManager.CreateAsync(userModel, account.password);
            if (result.Result.Succeeded == false)
            {
                return new ResultView<UserRegisterDTO>()
                {
                    Entity = account,
                    IsSuccess = false,


                    Message = result.Result.ToString()
                };
            }



            if (RoleName.ToLower() == "user")
            {
                await _UserManager.AddToRoleAsync(userModel, "admin");
            }
           

            return new ResultView<UserRegisterDTO>()
            {
                Entity = account,
                IsSuccess = true,
                Message = " Successfully create Account "

            };



        }

        public async Task<ResultView<GetAllUserDTO>> LoginAsync(UserLoginDTO userDto)
        {
            var oldUser = await _UserManager.FindByEmailAsync(userDto.Email);

            if (oldUser == null)
            {
                return new ResultView<GetAllUserDTO> { Entity = null, Message = "Email not found", IsSuccess = false };
            }
            if (oldUser.IsBlocked == true)
            {
                return new ResultView<GetAllUserDTO> { Entity = null, Message = "Blocked User", IsSuccess = false };
            }

            var result = await _SignInManager.CheckPasswordSignInAsync(oldUser, userDto.password, lockoutOnFailure: false);
         
            if (result.Succeeded)
            {
                
                GetAllUserDTO userObj = new GetAllUserDTO()
                {
                    Email = userDto.Email,
                    IsBlocked = oldUser.IsBlocked,
                    UserName = oldUser.UserName
                };
                await _SignInManager.SignInAsync(oldUser, false);
                return new ResultView<GetAllUserDTO> { Entity = userObj, Message = "Login Successfully", IsSuccess = true };
            }

            return new ResultView<GetAllUserDTO> { Entity = null, Message = "Invalid password", IsSuccess = false };
        }
        public async Task<bool> LogoutUser()
        {
            await _SignInManager.SignOutAsync();
            return true;
        }
        public async Task<ResultView<BlockUserDTO>> BlockOrUnBlockUser(BlockUserDTO blockUserDTO)
        {
            var user = await _UserManager.FindByIdAsync(blockUserDTO.Id);

            if (user is null)
            {
                return new ResultView<BlockUserDTO> { Entity = null, IsSuccess = false, Message = "Unable to find the user." };
            }

            if (user.IsBlocked == blockUserDTO.IsBlocked)
            {
                return new ResultView<BlockUserDTO>
                {
                    Entity = blockUserDTO, IsSuccess = false, Message = user.IsBlocked ? "The user is already blocked." : "The user is already unblocked."
                };
            }

            user.IsBlocked = blockUserDTO.IsBlocked;

            var result = await _UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ResultView<BlockUserDTO>
                {
                    Entity = blockUserDTO, IsSuccess = true, Message = user.IsBlocked ? "User blocked successfully." : "User unblocked successfully."
                };
            }
            return new ResultView<BlockUserDTO> { Entity = null, IsSuccess = false, Message = "Failed to update user." };

        }

        public async Task<ResultView<List<GetAllUserDTO>>> GetAllUsers()
        {
            var users = _UserManager.Users;


            if (users == null)
            {
                return new ResultView<List<GetAllUserDTO>>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "No users found."
                };
            }
            var userlist =  users.ToList();
            var userDTOs = _mapper.Map<List<GetAllUserDTO>>(userlist);

            return new ResultView<List<GetAllUserDTO>>
            {
                Entity = userDTOs,
                IsSuccess = true,
                Message = "Successfully retrieved all users."
            };
        }

        public async Task<ResultView<List<GetAllUserDTO>>> GetAllUsersPaging(int Count, int pagenumber) 
        {
            var AlldAta = (_UserManager.Users);
            if (AlldAta == null)
            {
                return new ResultView<List<GetAllUserDTO>>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "No users found."
                };
            }
            var userlist =  AlldAta.Skip(Count * (pagenumber - 1)).Take(Count).ToList();
            var userDTOs = _mapper.Map<List<GetAllUserDTO>>(userlist);

            return new ResultView<List<GetAllUserDTO>>
            {
                Entity = userDTOs,
                IsSuccess = true,
                Message = "Successfully retrieved all users."
            };


        }
       
    }
} 

