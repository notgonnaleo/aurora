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
    public class Email : Model
    {
        [Key]
        public Guid EmailAddressId { get; set; }
        public Guid TenantId { get; set; }
        public Guid AgentId { get; set; }

        public string? EmailAddress { get; set; }
        public bool? Primary { get; set; }

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }

        public Email(Email email, Guid userId)
        {
            EmailAddressId = Guid.NewGuid();
            TenantId = email.TenantId;
            AgentId = email.AgentId;
            EmailAddress = email.EmailAddress;
            Primary = email.Primary;

            CreatedBy = userId;
            Created = DateTime.UtcNow;
            Updated = null;
            UpdatedBy = null;
            Active = true;
        }

        public void ValidateFields(LanguagesEnum language)
        {
            // Implement validation logic as needed for Email entity
            // Example: Check if EmailAddress is valid
            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                throw new ValidationException("EmailAddress cannot be empty");
            }
        }
    }
}
