using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models.Common
{
    public class UserConnection
    {
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
    }
}
