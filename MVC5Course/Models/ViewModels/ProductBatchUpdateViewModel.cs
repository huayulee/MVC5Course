using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductBatchUpdateViewModel: IValidatableObject
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int max = 7878;
            if (this.Price == max)
            {
                yield return new ValidationResult("ViewModel驗證沒通過，原因: 價格不能等於" + max);
            }
        }
    }
}