namespace LoaTool.Service;
public class AuctionService
{
    private readonly double TAX = 0.05;

    public int Calculate(int price, double per)
    {
        double result = Math.Ceiling(price * ( 1 - TAX ) * ( ( per - 1 ) / per ));
        System.Diagnostics.Trace.WriteLine("Auction: " + result);
        return Convert.ToInt32(result);
    }
}
