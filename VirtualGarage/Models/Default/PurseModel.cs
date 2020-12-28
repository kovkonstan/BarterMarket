using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class PurseModel : BaseDefaultModel
    {
        public List<CashOperationModel> Operations { get; set; }
    }

    public class CashOperationModel
    {
        public Int32 CashOperationID { get; set; }

        public Int32 UserID { get; set; }

        public Int32 CashOperationTypeID { get; set; }

        public String CashOperationTypeName { get; set; }

        public Int32 CashAmount { get; set; }

        public Nullable<Int32> CertificateID { get; set; }

        public String PacketName { get; set; }

        public Int32 OfferID { get; set; }

        public Nullable<Int32> CertificateCount { get; set; }

        public Nullable<System.DateTime> OperationDate { get; set; }

        public Boolean IsIncrementOperation { get; set; }
    }

}