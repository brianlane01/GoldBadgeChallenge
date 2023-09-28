using GoldBadgeChallange.Data.Entities.Enums;

namespace GoldBadgeChallange.Data.Entities
{
    public class Delivery
    {
        public Delivery()
        {

        }

        public Delivery(DateTime orderDate, DateTime deliveryDate, OrderStatus orderStatus, string itemNumber, int itemQuantity, int customerId )
        {
            DeliveryDate = deliveryDate;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            ItemNumber = itemNumber;
            ItemQuantity = itemQuantity;
            CustomerId = customerId;
        }

        public string ItemNumber { get; set; }
        public int ItemQuantity { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int DeliveryId { get; set; }
        public int CustomerId { get; set; }
        
    }
}