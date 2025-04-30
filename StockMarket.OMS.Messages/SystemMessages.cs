using System;

namespace StockMarket.OMS.Messages
{
    public class SystemStatusMessage : BaseMessage
    {
        public string Status { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        public SystemStatusMessage(
            string exchangeId,
            string status,
            string message)
            : base(exchangeId, "SystemStatus")
        {
            Status = status;
            Message = message;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class ErrorMessage : BaseMessage
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public string Details { get; }
        public DateTime Timestamp { get; }

        public ErrorMessage(
            string exchangeId,
            string errorCode,
            string errorMessage,
            string details = null)
            : base(exchangeId, "Error")
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Details = details;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class HeartbeatMessage : BaseMessage
    {
        public DateTime Timestamp { get; }

        public HeartbeatMessage(string exchangeId)
            : base(exchangeId, "Heartbeat")
        {
            Timestamp = DateTime.UtcNow;
        }
    }

    public class MarketStatusMessage : BaseMessage
    {
        public string Symbol { get; }
        public string Status { get; }
        public string Reason { get; }
        public DateTime Timestamp { get; }

        public MarketStatusMessage(
            string exchangeId,
            string symbol,
            string status,
            string reason = null)
            : base(exchangeId, "MarketStatus")
        {
            Symbol = symbol;
            Status = status;
            Reason = reason;
            Timestamp = DateTime.UtcNow;
        }
    }
} 