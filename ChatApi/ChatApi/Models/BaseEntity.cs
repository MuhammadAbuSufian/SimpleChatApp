using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreApiStarter.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        public Guid? CreatedUserId { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now.ToUniversalTime();


        public Guid? ModifiedUserId { get; set; }
        public DateTime? ModificationTime { get; set; }


        public bool IsDeleted { get; set; } = false;
        public Guid? DeletedUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
