using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Domain.ValueObjects;

namespace StockMarket.Domain.ReadModels
{
    public class OrderBookProjection
    {
        public Guid InstrumentId { get; }
        public List<OrderBookEntry> BuyOrders { get; }
        public List<OrderBookEntry> SellOrders { get; }

        public OrderBookProjection(Guid instrumentId)
        {
            InstrumentId = instrumentId;
            BuyOrders = new List<OrderBookEntry>();
            SellOrders = new List<OrderBookEntry>();
        }

        public void AddBuyOrder(Guid orderId, Money price, Quantity quantity)
        {
            BuyOrders.Add(new OrderBookEntry(orderId, price, quantity));
            BuyOrders.Sort((a, b) => b.Price.CompareTo(a.Price)); // Sort by price descending
        }

        public void AddSellOrder(Guid orderId, Money price, Quantity quantity)
        {
            SellOrders.Add(new OrderBookEntry(orderId, price, quantity));
            SellOrders.Sort((a, b) => a.Price.CompareTo(b.Price)); // Sort by price ascending
        }

        public void RemoveOrder(Guid orderId)
        {
            BuyOrders.RemoveAll(o => o.OrderId == orderId);
            SellOrders.RemoveAll(o => o.OrderId == orderId);
        }

        public void UpdateOrder(Guid orderId, Money newPrice, Quantity newQuantity)
        {
            var buyOrder = BuyOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (buyOrder != null)
            {
                BuyOrders.Remove(buyOrder);
                AddBuyOrder(orderId, newPrice, newQuantity);
                return;
            }

            var sellOrder = SellOrders.FirstOrDefault(o => o.OrderId == orderId);
            if (sellOrder != null)
            {
                SellOrders.Remove(sellOrder);
                AddSellOrder(orderId, newPrice, newQuantity);
            }
        }

        public class OrderBookEntry
        {
            public Guid OrderId { get; }
            public Money Price { get; }
            public Quantity Quantity { get; }

            public OrderBookEntry(Guid orderId, Money price, Quantity quantity)
            {
                OrderId = orderId;
                Price = price;
                Quantity = quantity;
            }
        }
    }
} 