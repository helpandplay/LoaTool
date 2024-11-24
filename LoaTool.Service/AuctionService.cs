namespace LoaTool.Service;
public class AuctionService
{
    private readonly double TAX = 0.05;
    private readonly double weight_4 = 0.75;
    private readonly double weight_8 = 0.875;
    private readonly double weight_16 = 0.9375;
    private readonly double recommandWeight_4 = 0.71;
    private readonly double recommandWeight_8 = 0.84;
    private readonly double recommandWeight_16 = 0.89;

    public int[] Calculate(int price, double per)
    {
        if(per == 4)
        {
            return new int[]
            {
                 Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * weight_4)),
                 Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * recommandWeight_4))
            };
        }
        else if(per == 8)
        {
            return new int[]
            {
                 Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * weight_8)),
                 Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * recommandWeight_8))
            };
        }

        return new int[]
        {
                 Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * weight_16)),
                 Convert.ToInt32(Math.Ceiling(price * ( 1 - TAX ) * recommandWeight_16))
        };
    }
}
