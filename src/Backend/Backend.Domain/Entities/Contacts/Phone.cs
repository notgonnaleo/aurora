using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Base;
using Backend.Infrastructure.Enums.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Contacts
{
    public class Phone : Model
    {
        [Key]
        public Guid PhoneId { get; set; }
        public Guid TenantId { get; set; }
        public Guid AgentId { get; set; }

        public string? AreaCode { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Primary { get; set; }

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }

        public Phone() { }

        public Phone(Phone phone, Guid userId)
        {
            PhoneId = Guid.NewGuid();
            TenantId = phone.TenantId;
            AgentId = phone.AgentId;
            AreaCode = phone.AreaCode;
            PhoneNumber = phone.PhoneNumber;
            Primary = phone.Primary;

            CreatedBy = userId;
            Created = DateTime.UtcNow;
            Updated = null;
            UpdatedBy = null;
            Active = true;
        }

        public void ValidateFields(LanguagesEnum language)
        {
            // Implement validation logic as needed for Phone entity
            // Example: Check if PhoneNumber is valid
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                throw new ValidationException("PhoneNumber cannot be empty");
            }
        }
    }
}
