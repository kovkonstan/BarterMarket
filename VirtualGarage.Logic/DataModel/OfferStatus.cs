//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarterMarket.Logic.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferStatus
    {
        public OfferStatus()
        {
            this.Offers = new HashSet<Offer>();
        }
    
        public int OfferStatusID { get; set; }
        public string OfferStatusName { get; set; }
    
        public virtual ICollection<Offer> Offers { get; set; }
    }
}
