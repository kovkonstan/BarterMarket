using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BarterMarket.Models
{
    public class AddPacketModel : BaseDefaultModel
    {
        public PacketModel PacketModel { get; set; }

        public String OfferName { get; set; }
    }

    public class EditPacketModel : AddPacketModel
    {

    }

    public class PacketModel
    {
        public Int32 PacketID { get; set; }
        public Int32 OfferID { get; set; }

        [Required]
        [DisplayName("Название пакета")]
        public String PacketName { get; set; }

        [Required]
        [DisplayName("Цена пакета (в пиарах)")]
        public Int32 PacketCost { get; set; }

        [Required]
        [DisplayName("Количество пакетов")]
        public Int32 PacketsCount { get; set; }

        [DisplayName("Оставшееся количество пакетов")]
        public Int32 AvailAblePacketsCount { get; set; }

        [DisplayName("Статус пакета")]
        public Int32 PacketStatusID { get; set; }

        public String PacketStatusName { get; set; }

        public DateTime PacketDate { get; set; }        
    }
}