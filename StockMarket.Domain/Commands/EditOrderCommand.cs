using System;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.Commands
{
    public class EditOrderCommand
    {
        public Guid OrderId { get; }
        public Money NewPrice { get; }
        public Quantity NewQuantity { get; }

        public EditOrderCommand(
            Guid orderId,
            Money newPrice,
            Quantity newQuantity)
        {
            OrderId = orderId;
            NewPrice = newPrice;
            NewQuantity = newQuantity;
        }
    }
} 