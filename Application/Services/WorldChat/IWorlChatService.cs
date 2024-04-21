using DTO_s.ViewResult;
using DTOs.ChatDTOs;

namespace Application.Services.WorldChat
{
    public interface IWorlChatService
    {
        Task<ResultView<ChatDTO>> Create(ChatDTO ChatDTO);
        Task<List<ChatDTO>> GetAll();
    }
}