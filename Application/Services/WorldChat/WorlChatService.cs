
using Application.Contract;
using AutoMapper;
using DTO_s.ViewResult;
using DTOs.ArticleDTOs;
using DTOs.ChatDTOs;
using DTOs.Paginated;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorldChat
{
    public class WorlChatService : IWorlChatService
    {
        private readonly IWorldChatRepository _worldChatRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public WorlChatService(IWorldChatRepository worldChatRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _worldChatRepository = worldChatRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResultView<ChatDTO>> Create(ChatDTO ChatDTO)
        {
            if (ChatDTO == null)
            {
                return new ResultView<ChatDTO>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Message NOT Saved"
                };
            }
            var user = await _userManager.FindByNameAsync(ChatDTO.UserName);
            Model.WorldChat Chat = new Model.WorldChat()
            {
                User = user,
                Message = ChatDTO.Message,
            };

            var res = await _worldChatRepository.CreateAsync(Chat);
            await _worldChatRepository.SaveChangesAsync();
            return new ResultView<ChatDTO>
            {
                Entity = ChatDTO,
                IsSuccess = true,
                Message = "Message Saved"
            };
        }
        public async Task<List<ChatDTO>> GetAll()
        {
            var worldChats = await _worldChatRepository.GetAllAsync();
            var res = await worldChats.Select(c=>new ChatDTO { Message =c.Message,UserName = c.User.UserName}).ToListAsync();
            return res;
        }





    }
}

