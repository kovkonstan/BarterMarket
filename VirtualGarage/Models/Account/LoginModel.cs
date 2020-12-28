using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BarterMarket.Models.Attributes;

namespace BarterMarket.Models
{
    public class LoginModel : BaseDefaultModel
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Email(ErrorMessage = "Введите корректный E-mail")]
        public String UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public String Password { get; set; }

        public Boolean IsRememberMe { get; set; }
    }
}