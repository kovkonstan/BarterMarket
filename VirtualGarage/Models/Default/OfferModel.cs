using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BarterMarket.Models
{
    public class OfferModel : BaseDefaultModel
    {
        public Int32 OfferID { get; set; }

        [Required]
        [DisplayName("Название предложения")]
        public String OfferName { get; set; }

        [Required]
        [DisplayName("Категория предложения")]
        public Int32 OfferCategoryID { get; set; }
                    
        [DisplayName("Адрес компании")]
        public String OfferCompanyAddress { get; set; }

        [Required]
        [DisplayName("Имя менеджера")]
        public String OfferManagerName { get; set; }

        [Required]
        [DisplayName("Телефон менеджера")]
        public String OfferManagerPhone { get; set; }

        [DisplayName("Адрес электронной почты")]
        public String OfferManagerEmail { get; set; }

        [Required]
        [DisplayName("Описание предложения")]
        public String OfferDetails { get; set; }

        [DisplayName("Ссылка на Инстаграм")]
        public String OfferInstagramLink { get; set; }

        [DisplayName("Ссылка на VK")]
        public String OfferVKLink { get; set; }

        [DisplayName("Ссылка на Одноклассники")]
        public String OfferOdnoklassnikiLink { get; set; }

        [DisplayName("Ссылка на Facebook")]
        public String OfferFacebookLink { get; set; }

        [DisplayName("Ссылка на Twitter")]
        public String OfferTwitterLink { get; set; }

        [DisplayName("Ссылка на WhatsApp")]
        public String OfferWhatsappLink { get; set; }

        [DisplayName("Ссылка на Telegram")]
        public String OfferTelegramLink { get; set; }

        [DisplayName("Ссылка на Viber")]
        public String OfferViberLink { get; set; }

        [Required]
        [DisplayName("Статус предложения")]
        public Int32 OfferStatusID { get; set; }

        [DisplayName("Фото")]
        public Byte[] OfferPhoto { get; set; }
        public String ImageType { get; set; }
        public DateTime OfferDate { get; set; }
        public String OfferCategoryName { get; set; }
        public String OfferStatusName { get; set; }
        public Int32 OfferPacketsCount { get; set; }
        //public Int32 UserID { get; set; }
        public String UserName { get; set; }

        public IEnumerable<SelectListItem> OfferCategories { get; set; }

        public IEnumerable<PacketModel> Packets { get; set; }
    }
}