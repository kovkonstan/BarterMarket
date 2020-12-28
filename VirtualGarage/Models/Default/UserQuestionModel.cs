using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class UserQuestionModel : BaseDefaultModel
    {
        public BaseDefaultModel BaseModel { get; set; }

        public Int32 UserQuestionID { get; set; }

        [Required]
        [DisplayName("Как Вас зовут?")]
        public String UserName { get; set; }

        [Required]
        [DisplayName("Адрес электронной почты")]
        public String UserEmail { get; set; }

        [Required]
        [DisplayName("Ваш вопрос")]
        public String UserMessage { get; set; }

    }
}