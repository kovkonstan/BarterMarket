using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BarterMarket.Models.Attributes;

namespace BarterMarket.Models
{
    public class ForgotPasswordModel
    {
		BaseAccountModel BaseModel { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Email(ErrorMessage = "Введите корректный E-mail")]
        public String Email { get; set; }
    }
}