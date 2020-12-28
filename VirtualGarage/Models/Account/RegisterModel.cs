using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BarterMarket.Models.Attributes;
using System.Web.Mvc;

namespace BarterMarket.Models
{
    public class RegisterModel : BaseDefaultModel, IDataErrorInfo
    {
		BaseAccountModel BaseModel { get; set; }

        [Required]
        [DisplayName("Имя пользователя")]        
        public String UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("E-mail")]
        [Email(ErrorMessage = "Введите корректный E-mail")]
        public String UserEmail { get; set; }


        [Required]      
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть больше 6 и меньше 50 знаков")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public String Password { get; set; }

        [Required]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Пароль не совпадает с подтверждением")]
        [DataType(DataType.Password)]
        [DisplayName("Подтверждение пароля")]
        public String ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Я прочитал и обязуюсь соблюдать ")]
        public Boolean IsReadRules { get; set; }

        [DisplayName("Название Вашей компании (или Instagram)")]
        public String CompanyName { get; set; }

        [DisplayName("Описание компании (чем занимается компания)")]
        public String CompanyDetails { get; set; }

        [DisplayName("Телефон")]
        public String UserPhone { get; set; }

        public String Network { get; set; }
        public String Uid { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Sex { get; set; }

        public string Error
        {
            get 
            {
                return null; 
            }
        }

        public string this[string columnName]
        {
            get 
            {
                if (columnName == "IsReadRules" &&
                    !IsReadRules)
                {
                    return "При несогласии с Правилами сайта регистрация невозможна";
                }

                return null; 
            }
        }
    }
}