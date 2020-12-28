using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class OfferRequestModel : BaseDefaultModel
    {
        public BaseDefaultModel BaseModel { get; set; }


        public Int32 OfferRequestID { get; set; }

        [Required]
        [DisplayName("Название компании или ниши")]
        public String CompanyName { get; set; }

        [Required]
        [DisplayName("Адрес компании")]
        public String CompanyAddress { get; set; }

        [Required]
        [DisplayName("Регион регистрации")]
        public String CompanyRegistrationRegion { get; set; }

        [Required]
        [DisplayName("В каких регионах работаете (города, области страны)")]
        public String CompanyRegions { get; set; }

        [Required]
        [DisplayName("Сайт компании или инстаграм")]
        public String CompanySite { get; set; }

        [Required]
        [DisplayName("Описание компании")]
        public String CompanyDetails { get; set; }
        
        [Required]
        [DisplayName("Как Вас зовут?")]
        public String UserName { get; set; }

        [Required]
        [DisplayName("Номер телефона")]
        public String TelephoneNumber { get; set; }

        [Required]
        [DisplayName("От кого узнали о нас?")]
        public String SourceAboutUs { get; set; }

        [Required]
        [DisplayName("Что хотите рекламировать (услуга, комплекс услуг, товар)")]
        public String AdvertisingTarget { get; set; }
        
    }
}