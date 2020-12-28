using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarterMarket.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace BarterMarket.Models
{
    public class ChangePasswordModel : BaseDefaultModel
    {
		public BaseDefaultModel BaseModel { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Старый пароль")]
        public String OldPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть больше 6 и меньше 50 знаков")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public String NewPassword { get; set; }

        [Required]
        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Пароль не совпадает с подтверждением")]
        [DataType(DataType.Password)]
        [DisplayName("Подтверждение пароля")]
        public String ConfirmPassword { get; set; }
    }
}