namespace LoaTool.Service;
public class AuctionService
{
    private readonly double TAX = 0.05; 

    public int Calculate(int price, int per)
    {
        return Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * ( ( per - 1 ) / per )));
    }
}
