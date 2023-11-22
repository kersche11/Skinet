using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems,string byerEmail, Address shipToAdress, DeliveryMethod deliveryMethod,  decimal subtotal)
        {
            ByerEmail = byerEmail;
            ShipToAdress = shipToAdress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;         
        }

        public string ByerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Address ShipToAdress {get;set;}
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus Status  { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }

    }
}