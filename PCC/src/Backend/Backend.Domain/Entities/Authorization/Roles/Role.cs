using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authorization.Roles
{
    public class Role
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
