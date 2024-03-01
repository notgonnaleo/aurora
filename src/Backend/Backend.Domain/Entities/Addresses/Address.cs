using Backend.Domain.Entities.Agents;
using Backend.Domain.Entities.Base;
using Backend.Domain.Enums.AddressTypes;
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
        public int StreetNumber { get; set; }

        public string PostalCode { get; set; }

        public bool Primary { get; set; }

        /// <summary>
        /// Geocoding properties
        /// </summary>
        public Geocoding? Coordinates { get; set; }

        [ForeignKey("AgentId")]
        public Agent? Agent { get; set; }
    }
}
