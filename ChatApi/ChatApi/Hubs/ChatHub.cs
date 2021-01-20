using ChatApi.Models;
using ChatApi.Models.Common;
using ChatApi.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IMessageService _service;
        static IList<UserConnection> Users = new List<UserConnection>();
        public ChatHub(IMessageService service)
        {
            _service = service;
        }
        public async Task OnConnect(string id, string fullname, string username)
        {
            var existingUser = Users.FirstOrDefault(x => x.Username == username);
            var indexExistingUser = Users.IndexOf(existingUser);
            UserConnection user = new UserConnection
            {
                UserId = id,
                ConnectionId = Context.ConnectionId,
                FullName = fullname,
                Username = username
            };

            if (!Users.Contains(existingUser))
            {
                Users.Add(user);
            }
            else
            {
                Users[indexExistingUser] = user;
            }
            await Clients.All.SendAsync("OnConnect", Users);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public Task SendMessageToUser(Message message)
        {
            var reciever = Users.FirstOrDefault(x => x.UserId == message.Receiver);
            var connectionId = reciever == null ? "no one recive this message" : reciever.ConnectionId;
            _service.Save(message);
            return Clients.Client(connectionId).SendAsync("ReceiveDM", Context.ConnectionId, message);
        }
        public void RemoveOnlineUser(string userID)
        {
            var user = Users.Where(x => x.UserId == userID).ToList();
            foreach (UserConnection i in user)
                Users.Remove(i);

            Clients.All.SendAsync("OnDisconnect", Users);
        }

    }
}
