using Application.Contract;
using Application.Service.User;
using Application.Services.WorldChat;
using DTOs.ChatDTOs;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace UserPresentation.Hubs
{
    
    public class ChatHub:Hub
    {
        private readonly IWorlChatService _worldChatService;

        public ChatHub(IWorlChatService worldChatService)
        {
            _worldChatService = worldChatService;
        }

        public async Task  SendNewMessage(string message, string userName)
        {
            var res = await _worldChatService.Create(new ChatDTO() { UserName = userName, Message = message });
             Clients.All.SendAsync("ReceiveNewMessage", message, userName);
        }


    }
}
