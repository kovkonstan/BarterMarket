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
    
    public partial class OfferCategory
    {
        public OfferCategory()
        {
            this.Offers = new HashSet<Offer>();
        }
    
        public int OfferCategoryID { get; set; }
        public string OfferCategoryName { get; set; }
    
        public virtual ICollection<Offer> Offers { get; set; }
    }
}