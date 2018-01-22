namespace TAFC
{
    // MACD formula:
    // shortMA = (0.15 * price) + (0.85 * shortMA[-1])
    // longMA  = (0.075 * price) + (0.925 * longMA[-1])
    // MACD = shortMA - longMA

    public static class MACD
    {
        public static double GetFinalPriceCondition(double[] input, double desiredMACD)
        {
            // run MACD, then solve for desiredMACD with equations

            double shortTimeExpo = 0.15;
            double longTimeExpo = 0.075;
            
            double shortTimeExpoMA = 1 - shortTimeExpo;
            double longTimeExpoMA = 1 - longTimeExpo;
            
            double shortMA = 0;
            double longMA = 0;

            for(int priceIndex = 0; priceIndex < input.Length; priceIndex++)
            {
                shortMA = (shortTimeExpo * input[priceIndex]) + (shortTimeExpoMA * shortMA);
                longMA = (longTimeExpo * input[priceIndex]) + (longTimeExpoMA * longMA);
            }

            // predict price with following formula:
            // desiredMACD = ((0.15 * price) + (0.85 * shortMA[-1])) - ((0.075 * price) + (0.925 * longMA[-1]))
            // price?
            // desiredMACD = (0.15 * price) + (0.85 * shortMA[-1]) - (0.075 * price) - (0.925 * longMA[-1])
            // (0.075 * price) - (0.15 * price)  = (0.85 * shortMA[-1]) - (0.925 * longMA[-1]) - desiredMACD
            // (0.075 - 0.15) * price  = (0.85 * shortMA[-1]) - (0.925 * longMA[-1]) - desiredMACD
            // price  = ((0.85 * shortMA[-1]) - (0.925 * longMA[-1]) - desiredMACD) / (0.075 - 0.15)

            return ((shortTimeExpoMA * shortMA) - (longTimeExpoMA * longMA) - desiredMACD) / (longTimeExpo - shortTimeExpo);
        }
    }
}
