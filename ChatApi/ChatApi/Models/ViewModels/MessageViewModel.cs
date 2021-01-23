using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models.ViewModels
{
    public class MessageViewModel : BaseViewModel<Message>
    {
        public MessageViewModel(Message model) : base(model)
        {
            this.Sender = model.Sender;
            this.Receiver = model.Receiver;
            this.MessageDate = model.MessageDate;
            this.Content = model.Content;
            this.IsNew = model.IsNew;
            this.IsDeletedForMe = model.IsDeletedForMe;
        }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime MessageDate { get; set; }
        public string Content { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeletedForMe { get; set; }
    }
}
