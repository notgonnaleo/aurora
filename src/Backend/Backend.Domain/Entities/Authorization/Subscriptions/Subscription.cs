using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Authorization.Subscriptions
{
    [Table("Subscription")]
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
