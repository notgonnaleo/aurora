using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Base;
using Backend.Domain.Enums.AddressTypes;
using Backend.Infrastructure.Enums.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Addresses
{
    public class Address : Model
    {
        [Key]
        public Guid AddressId { get; set; }
        public int AddressTypeId { get; set; }

        public Guid TenantId { get; set; }
        public Guid AgentId { get; set; }

        /// <summary>
        /// Address information
        /// </summary>
        public string CountryName { get; set; }
        public string CountryAlias { get; set; }

        public string StateAlias { get; set; }
        public string StateName { get; set; }

        public string City { get; set; }
        public string Region { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }

        public string PostalCode { get; set; }

        public bool Primary { get; set; }

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }

        public Address(Address address, Guid userId)
        {
            AddressId = Guid.NewGuid();
            TenantId = address.TenantId;
            AddressTypeId = address.AddressTypeId;
            CountryName = address.CountryName;
            CountryAlias = address.CountryAlias;
            StateAlias = address.StateAlias;
            StateName = address.StateName;
            City = address.City;
            Region = address.Region;
            StreetName = address.StreetName;
            StreetNumber = address.StreetNumber;
            PostalCode = address.PostalCode;
            Primary = address.Primary;
            AgentId = address.AgentId;

            CreatedBy = userId;
            Created = DateTime.UtcNow;
            Updated = null;
            UpdatedBy = null;
            Active = true;
        }

        public void ValidateFields(LanguagesEnum language)
        {
            throw new NotImplementedException();
        }
    }
}
