using ChatApi.Models;
using DotNetCoreApiStarter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IMessageService
    {
        void Save(Message model);
        Task<List<Message>> GetMessagesByUserId(string id);
        Task<List<Message>> GetAll();
    }
    public class MessageService : IMessageService
    {
        private readonly BusinessDbContext _context;
        public MessageService(BusinessDbContext context)
        {
            _context = context;
        }
        public void Save(Message model)
        {
            _context.Messages.Add(model);
            _context.SaveChanges();
        }
        public async Task<List<Message>> GetMessagesByUserId(string id)
        {
            var result = await _context.Messages.AsQueryable().Where(x => x.Receiver == id).OrderBy(x=>x.MessageDate).ToListAsync();
            return result;
        }
        public async Task<List<Message>> GetAll()
        {
            var result = await _context.Messages.OrderBy(x => x.MessageDate).ToListAsync();
            return result;
        }
    }
}
