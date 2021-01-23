using ChatApi.Models;
using DotNetCoreApiStarter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Data.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {

    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BusinessDbContext db) : base(db)
        {
        }
    }
}
