namespace APBD_3;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; set; }

    public LiquidContainer(int height, double tareWeight, int depth, double maxPayload, bool isHazardous)
        : base('L', height, tareWeight, depth, maxPayload)
    {
        IsHazardous = isHazardous;
    }

    public override void LoadCargo(double cargoMass)
    {
        var maxLoadingPayload = MaxPayload * (IsHazardous ? 0.5 : 0.9);
        var newCargoMass = CargoMass + cargoMass;
        if (newCargoMass > maxLoadingPayload)
        {
            NotifyHazardSituation(
                $"Attempted to load cargo with mass of {newCargoMass} kg, but it exceeds its maximum loading payload of {maxLoadingPayload} kg");
            throw new OverfillException(
                $"Cargo mass for liquid container {SerialNumber} exceeds maximum loading payload");
        }

        base.LoadCargo(cargoMass);
    }

    public void NotifyHazardSituation(string message)
    {
        Console.WriteLine($"!! <Hazardous situation - Liquid container {SerialNumber}>: {message}");
    }
    
    public override string ToString()
    {
        return base.ToString() + $", IsHazardous: {IsHazardous}";
    }
}