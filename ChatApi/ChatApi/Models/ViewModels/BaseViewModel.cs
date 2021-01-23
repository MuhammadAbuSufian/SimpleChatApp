using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models.ViewModels
{
    public class BaseViewModel<T> where T : BaseEntity
    {
        public BaseViewModel(T model)
        {
            if(model != null)
            {
                Id = model.Id;
                CreationTime = model.CreationTime;
                ModificationTime = model.ModificationTime;
                DeletionTime = model.DeletionTime;
                IsActive = model.IsActive;
            }
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationTime { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime? ModificationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsActive { get; set; }

    }
}
