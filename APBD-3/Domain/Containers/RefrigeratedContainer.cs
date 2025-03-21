namespace APBD_3;

public class RefrigeratedContainer : Container
{
    private static Dictionary<string, double> _productTemperatures = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };

    public string ProductType { get; set; }
    public double MaintainedTemperature { get; set; }

    public RefrigeratedContainer(int height, double tareWeight, int depth, double maxPayload, string productType,
        double maintainedTemperature)
        : base('C', height, tareWeight, depth, maxPayload)
    {
        if (!_productTemperatures.ContainsKey(productType))
        {
            throw new ProductNotFoundException($"There is no {productType} product");
        }

        var requiredProductTemperature = _productTemperatures[productType];

        if (maintainedTemperature < requiredProductTemperature)
        {
            throw new TemperatureNotAllowedException(
                $"Container {SerialNumber} cannot store {productType} because its temperature is lower than {requiredProductTemperature}");
        }

        ProductType = productType;
        MaintainedTemperature = maintainedTemperature;
    }
}