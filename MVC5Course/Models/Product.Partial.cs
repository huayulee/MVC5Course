namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Price>=1000 && this.Stock > 100)
            {
                yield return new ValidationResult("價格大於1000的商品不可以超過100個!", new string[] { "Stock" });
            }

            if (this.ProductName.Contains("Fuck"))
            {
                yield return new ValidationResult("不可以罵髒話!", new string[] { "ProductName" });
            }

            yield break;
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("產品名稱")]
        [StringLength(80, ErrorMessage="{0}長度不得大於 {1} 個字元")]
        public string ProductName { get; set; }
        [Required]
        [DisplayName("價格")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        [Required]
        [DisplayName("庫存")]
        [Range(0,9999)]
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
