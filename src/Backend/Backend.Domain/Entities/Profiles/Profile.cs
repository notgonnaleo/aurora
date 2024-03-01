using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Entities.Profiles
{
    public class Profile
    {
        /// <summary>
        /// Default profile information
        /// </summary>
        public Guid ProfileId { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Brazil profile commercial and public information
        /// </summary>
        public string CNAE { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
    }
}
