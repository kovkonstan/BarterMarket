using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class ListUserOffersModel : BaseDefaultModel
    {
        public BaseDefaultModel BaseModel { get; set; }

        public List<OfferModel> OfferModels { get; set; }

        public Boolean IsMyOffers { get; set; }
    }
}