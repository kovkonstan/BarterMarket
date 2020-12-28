using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class AddPiarsModel
    {
        public Int32 UserID { get; set; }

        public String UserName { get; set; }

        public Int32 UserPiarsCounts { get; set; }

        public Int32 PiarsCountsToIncrement { get; set; }
    }
}