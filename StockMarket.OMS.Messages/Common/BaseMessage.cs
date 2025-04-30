using System;

namespace StockMarket.OMS.Messages.Common
{
    public abstract class BaseMessage
    {
        public Guid MessageId { get; }
        public DateTime Timestamp { get; }
        public string MessageType { get; }

        protected BaseMessage(string messageType)
        {
            MessageId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            MessageType = messageType;
        }
    }
} 