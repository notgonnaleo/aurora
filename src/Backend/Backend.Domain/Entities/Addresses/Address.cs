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

        /// <summary>
        /// Address information
        /// </summary>
        public int? CountryId { get; set; }
        public string? CountryName { get; set; }

        public int? StateId { get; set; }
        public string? StateName { get; set; }

        public int? CityId { get; set; }
        public string? CityName { get; set; }

        public string? Reference { get; set; }
        public string? StreetName { get; set; }
        public string? StreetNumber { get; set; }

        public string? PostalCode { get; set; }

        public bool? Primary { get; set; }

        [ForeignKey("AgentId")]
        public Guid AgentId { get; set; }

        public Address() { }
        public Address(Address address, Guid userId)
        {
            AddressId = Guid.NewGuid();
            TenantId = address.TenantId;
            AddressTypeId = address.AddressTypeId;
            CountryId = address.CountryId;
            StateId = address.StateId;
            CityId = address.CityId;
            Reference = address.Reference;
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

        public void ValidateFields()
        {
            if (CountryId is null && CountryName is null)
            {
                throw new Exception("Country was not provided");
            }
        }
    }
}
