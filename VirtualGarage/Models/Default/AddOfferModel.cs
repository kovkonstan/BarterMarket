using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class AddOfferModel : BaseDefaultModel
    {
        public BaseDefaultModel BaseModel { get; set; }

        public OfferModel OfferModel { get; set; }
    }

    public class EditOfferModel : AddOfferModel
    {

    }
}