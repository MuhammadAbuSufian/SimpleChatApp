using ChatApi.Data.Repositories;
using ChatApi.Models;
using ChatApi.Models.RequestModels;
using ChatApi.Models.ViewModels;
using DotNetCoreApiStarter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IMessageService : IBaseService<Message, MessageViewModel, MessageRequestModel>
    {
        Task<List<Message>> GetMessagesByUserId(string id);
        Task<List<Message>> GetAllOrderByMessage();
        public Message GetMessage(Message msg);
    }
    public class MessageService : BaseService<Message, MessageViewModel, MessageRequestModel>, IMessageService
    {
        private readonly IMessageRepository _repository;
        public MessageService(IMessageRepository repository): base(repository)
        {
            _repository = repository;
        }

        public async Task<List<Message>> GetMessagesByUserId(string id)
        {
            return await _repository.GetAll().Where(x => x.Receiver == id || x.Sender == id).OrderBy(x => x.MessageDate).ToListAsync(); ;
        }
        public async Task<List<Message>> GetAllOrderByMessage()
        {
            var result = await _repository.GetAll().OrderBy(x => x.MessageDate).ToListAsync();
            return result;
        }

        public Message GetMessage(Message msg)
        {
            var message = _repository.GetAll().Where(x => x.Content == msg.Content && x.Sender == msg.Sender &&
            x.Receiver == msg.Receiver && x.MessageDate == msg.MessageDate).FirstOrDefault();
            return message;
        }
    }
}
