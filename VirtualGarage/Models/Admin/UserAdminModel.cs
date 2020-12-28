using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class UserAdminModel
    {
        public Int32 UserID { get; set; }
        public String UserNick { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }
        public Int32 UserRoleID { get; set; }
        public Nullable<Boolean> IsCorporativeAccount { get; set; }
        public String UserCompanyName { get; set; }
        public String UserCompanyDetails { get; set; }
        public Nullable<System.DateTime> UserRegisrationDate { get; set; }
        public Nullable<Int32> UserPiarsCount { get; set; }

    }
}