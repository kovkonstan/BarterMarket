using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BarterMarket.Models.Attributes;
using System.ComponentModel;

namespace BarterMarket.Models
{
    public class ChangeEmailModel : BaseDefaultModel
    {
		public BaseDefaultModel BaseModel { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Email(ErrorMessage = "Введите корректный E-mail")]  
        public String NewEmail { get; set;}
    }
}