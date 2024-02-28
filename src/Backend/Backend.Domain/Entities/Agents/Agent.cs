using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Base;
using Backend.Domain.Enums.Colors;
using Backend.Domain.Enums.MetricUnits;
using Backend.Infrastructure.Enums.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Agents
{
    [Table("Agent")]
    public class Agent : Model
    {
        [Key]
        public Guid AgentId { get; set; }
        [Required]
        public Guid TenantId { get; set; }
        public Guid? UserId { get; set; }
        [Required]
        public int AgentTypeId { get; set; }
        public string? Name { get; set; }

        /// <summary>
        /// This property is going to tell if this agent is supposed to be the main agent
        /// for the tenant in use at the moment.
        /// </summary>
        public bool IsPrimary { get; set; }

        [ForeignKey("AgentTypeId")]
        public virtual AgentType? AgentType { get; set; }
    }
}
