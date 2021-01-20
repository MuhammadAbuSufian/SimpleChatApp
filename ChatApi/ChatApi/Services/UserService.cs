using ChatApi.Models.RequestModels;
using DotNetCoreApiStarter.Data;
using DotNetCoreApiStarter.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public interface IUserService
    {
        Task<int> Save(ApplicationUser model);
        Task<List<ApplicationUser>> GetAll();
        Task<ApplicationUser> GetByEmail(string email);
    }
    public class UserService: IUserService
    {
        private readonly BusinessDbContext _context;
        public UserService(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<int> Save(ApplicationUser model)
        {
            _context.ApplicationUsers.Add(model);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            var result = await _context.ApplicationUsers.ToListAsync();
            return result;
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            var result = await _context.ApplicationUsers.AsQueryable().Where(x => x.Email == email).FirstOrDefaultAsync();
            return result;
        }
    }
}
