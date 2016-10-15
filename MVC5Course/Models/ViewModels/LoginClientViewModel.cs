using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ViewModels
{
    public class LoginClientViewModel
    {
        [Required]
        [MaxLength(10, ErrorMessage = "{0}長度不得超過{1}個字元")]
        [DisplayName("名")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "{0}長度不得超過{1}個字元")]
        [DisplayName("中間名")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "{0}長度不得超過{1}個字元")]
        [DisplayName("姓")]
        public string LastName { get; set; }

        [DisplayName("出生日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
    }
}