namespace APBD_3;

public abstract class Container
{
    private static int _id = 1;

    public double CargoMass { get; set; }
    public int Height { get; set; }
    public double TareWeight { get; set; }
    public int Depth { get; set; }
    public string SerialNumber { get; }
    public double MaxPayload { get; set; }

    protected Container(char type, int height, double tareWeight, int depth, double maxPayload)
    {
        Height = height;
        TareWeight = tareWeight;
        Depth = depth;
        MaxPayload = maxPayload;
        SerialNumber = $"KON-{type}-{_id++}";
    }

    public virtual void EmptyCargo()
    {
        CargoMass = 0;
    }

    public virtual void LoadCargo(double cargoMass)
    {
        var newCargoMass = cargoMass + CargoMass;
        if (newCargoMass > MaxPayload)
        {
            throw new OverfillException(
                $"Attempt to load {cargoMass}kg for container {SerialNumber} exceeds container's maximum payload of {MaxPayload}");
        }

        CargoMass = newCargoMass;
    }

    public override string ToString()
    {
        return $"[Serial number: {SerialNumber}, Tare weight: {TareWeight} kg, Height: {Height} cm," +
               $"Depth: {Depth} cm, Maximum payload: {MaxPayload} kg, Cargo mass: {CargoMass} kg]";
    }
}