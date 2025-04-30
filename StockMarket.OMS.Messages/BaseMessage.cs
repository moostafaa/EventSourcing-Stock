using System;

namespace StockMarket.OMS.Messages
{
    public abstract class BaseMessage
    {
        public Guid MessageId { get; }
        public DateTime Timestamp { get; }
        public string ExchangeId { get; }
        public string MessageType { get; }

        protected BaseMessage(string exchangeId, string messageType)
        {
            MessageId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            ExchangeId = exchangeId;
            MessageType = messageType;
        }
    }
} 