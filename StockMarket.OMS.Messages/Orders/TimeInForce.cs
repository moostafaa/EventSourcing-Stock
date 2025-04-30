namespace StockMarket.OMS.Messages.Orders
{
    public enum TimeInForce
    {
        GTC,    // Good Till Cancelled
        DAY,    // Day Order
        IOC,    // Immediate or Cancel
        FOK     // Fill or Kill
    }
} 