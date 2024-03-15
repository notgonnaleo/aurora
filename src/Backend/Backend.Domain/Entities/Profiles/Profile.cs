using Backend.Domain.Entities.Authentication.Users;
using Backend.Domain.Entities.Base;
using Backend.Infrastructure.Enums.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Profiles
{
    public class Profile : Model
    {
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Default profile information
        /// </summary>
        [Key]
        public Guid ProfileId { get; set; }
        public Guid? AgentId { get; set; }

        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        /// <summary>
        /// Brazil profile commercial and public information
        /// </summary>
        public string? CNAE { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }

        public Profile() { }
        public Profile(Profile profile, Guid userId)
        {
            ProfileId = Guid.NewGuid();
            AgentId = profile.AgentId;
            TenantId = profile.TenantId;
            DisplayName = profile.DisplayName;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            CNAE = profile.CNAE;
            CNPJ = profile.CNPJ;
            CPF = profile.CPF;

            CreatedBy = userId;
            Created = DateTime.UtcNow;
            Updated = null;
            UpdatedBy = null;
            Active = true;
        }

        public void ValidateFields(LanguagesEnum languagesEnum)
        {
            // Implement validation logic as needed for Profile entity
            // Example: Check if DisplayName or FirstName is valid
            if (string.IsNullOrWhiteSpace(DisplayName) && string.IsNullOrWhiteSpace(FirstName))
            {
                throw new ValidationException("Either DisplayName or FirstName should be provided");
            }

            // Add more validation logic for other fields as needed
        }
    }
}
