namespace APBD_3;

public class GasContainer : Container, IHazardNotifier
{
    public int Pressure { get; set; }

    public GasContainer(int height, double tareWeight, int depth, double maxPayload, int pressure)
        : base('G', height, tareWeight, depth, maxPayload)
    {
        Pressure = pressure;
    }

    public override void EmptyCargo()
    {
        CargoMass = CargoMass * 0.05;
    }

    public override void LoadCargo(double cargoMass)
    {
        var newCargoMass = CargoMass + cargoMass;
        if (newCargoMass > MaxPayload)
        {
            NotifyHazardSituation(
                $"Attempted to load cargo with mass of {newCargoMass} kg, but it exceeds its maximum loading payload of {MaxPayload} kg");
            throw new OverfillException(
                $"Cargo mass for gas container {SerialNumber} exceeds maximum loading payload");
        }

        base.LoadCargo(cargoMass);
    }

    public void NotifyHazardSituation(string message)
    {
        Console.WriteLine($"!! <Hazardous situation - Gas container {SerialNumber}>: {message}");
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Pressure: {Pressure} atm";
    }
}