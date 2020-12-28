using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class OfferInfoModel : BaseDefaultModel
    {
        public OfferModel OfferModel { get; set; }

        public String CategoryName { get; set; }

        public Boolean IsExistPackets = false;
    }
}