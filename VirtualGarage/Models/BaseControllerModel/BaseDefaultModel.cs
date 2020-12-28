using BarterMarket.Helpers;
using KK.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class BaseDefaultModel
    {

        public Int32 UserID { get; set; }

        public String UserNick { get; set; }

        public Int32 UserPiarsCount { get; set; }

        public String PageName { get; set; }

        public List<SimpleUserOffer>  UserOffers { get; set; }

        public void FillBaseDefaultModel(IUnitOfWork unitOfWork, String userEmail, String pageName)
        {
            var m = DataHelper.FillBaseDefaultModel(unitOfWork, userEmail);

            if (m != null)
            {
                this.UserID = m.UserID;
                this.UserNick = m.UserNick;
                this.UserPiarsCount = m.UserPiarsCount;
                this.UserOffers = m.UserOffers;
            }

            this.PageName = pageName;
        }

        public void FillBaseDefaultModel(IUnitOfWork unitOfWork, String userEmail)
        {
            FillBaseDefaultModel(unitOfWork, userEmail, String.Empty);
        }
               
    }

    public class SimpleUserOffer
    {
        public Int32 OfferID { get; set; }

        public String OfferName { get; set; }
    }
}