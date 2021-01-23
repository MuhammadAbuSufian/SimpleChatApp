using ChatApi.Models;
using DotNetCoreApiStarter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Data.Repositories
{
    public interface IMessageRepository : IBaseRepository<Message>
    {

    }

    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(BusinessDbContext db) : base(db)
        {
        }
    }
}
