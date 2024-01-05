using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domain.Entities.Categorys;


namespace Backend.Domain.Entities.SubCategory
{
    [Table("SubCategorys")]
    public class SubCategory
    {
        public Guid SubCategoryId { get; set; }
        public Guid TenantId { get; set; }
        public string? SubCategoryName { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? Created { get; set; }  
        public DateTime? Updated { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid CategoryId { get; set; }
        // obs: mudei a chave FK string pra Guid 


    }
}
