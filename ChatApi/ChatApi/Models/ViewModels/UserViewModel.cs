using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models.ViewModels
{
    public class UserViewModel : BaseViewModel<User>
    {
        public UserViewModel(User model) : base(model)
        {
            if(model != null)
            {
                this.Email = model.Email;
                this.FirstName = model.FirstName;
                this.LastName = model.LastName;
                this.IsOnline = model.IsOnline;
            }
        }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsOnline { get; set; }
    }
}
