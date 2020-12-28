using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BarterMarket.Models
{
    public class UserInfoModel : BaseDefaultModel
    {
		public BaseDefaultModel BaseModel { get; set; }

        //public override Int32 UserID { get; set; }

        [Required]
        [DisplayName("Имя пользователя")]
        public String UserName { get; set; }
    }
}