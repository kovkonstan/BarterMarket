using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BarterMarket.Models.Attributes;

namespace BarterMarket.Models
{
    public class    UserModel : BaseDefaultModel
    {
        [Required]
        [DisplayName("Имя пользователя")]
        public String UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Email(ErrorMessage = "Введите корректный E-mail")]
        public String UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public String Password { get; set; }

        [DisplayName("Роль")]
        public String UserRole { get; private set; }

        [Required]
        [DisplayName("Название компании")]
        public String CompanyName { get; set; }

        [Required]
        [DisplayName("Описание компании")]
        public String CompanyDetails { get; set; }

        [DisplayName("Телефон")]
        public String UserPhone { get; set; }

        public DateTime? RegistrationDate { get; set; }
    }
}