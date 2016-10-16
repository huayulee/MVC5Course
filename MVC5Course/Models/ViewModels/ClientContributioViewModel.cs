using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ClientContributioViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("客戶貢獻$$")]
        [DisplayFormat(DataFormatString ="{0:###,##}")]
        public decimal? OrderTotal { get; set; }
    }
}