using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class UserCertificatesModel : BaseDefaultModel
    {
        public List<CertificateModel> SoldCertificates { get; set; }
        public List<CertificateModel> GottenCertificates { get; set; }
    }

    public class CertificateModel
    {
        public Int32 CertificateID { get; set; }

        public Int32 PacketID { get; set; }

        public String PacketName { get; set; }

        public Int32 OfferID { get; set; }

        public Int32 PacketCost { get; set; }

        public Nullable<Int32> CustomerID { get; set; }

        public String CustomerName { get; set; }

        public Nullable<Int32> SellerID { get; set; }

        public String SellerName { get; set; }

        public String CertificateCodeValue { get; set; }

        public System.DateTime CertificateCreateDate { get; set; }

        public Nullable<System.DateTime> CertificateImplementDate { get; set; }

        public Boolean CertificateIsImplement { get; set; }                
    }
}