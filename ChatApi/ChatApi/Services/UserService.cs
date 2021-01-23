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
    public interface IUserService : IBaseService<User, UserViewModel, UserRequestModel>
    {
        Task<UserViewModel> GetByEmail(string email);
    }
    public class UserService : BaseService<User, UserViewModel, UserRequestModel>, IUserService
    {
        private readonly IUserRepository _repositoy;
        public UserService(IUserRepository repository) : base(repository)
        {
            _repositoy = repository;
        }
        
        public async Task<UserViewModel> GetByEmail(string email)
        {
            var user = await _repositoy.GetAll().Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user != null) return new UserViewModel(user);
            return null;
        }
    }
}
