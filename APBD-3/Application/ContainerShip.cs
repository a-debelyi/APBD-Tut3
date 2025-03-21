namespace APBD_3;

public class ContainerShip
{
    private readonly List<Container> _containers;
    public double MaxSpeed { get; set; }
    public int MaxContainersCount { get; set; }
    public double MaxContainersWeightInTons { get; set; }

    public ContainerShip(double maxSpeed, int maxContainersCount, double maxContainersWeightInTons)
    {
        MaxSpeed = maxSpeed;
        MaxContainersCount = maxContainersCount;
        MaxContainersWeightInTons = maxContainersWeightInTons;
        _containers = new List<Container>();
    }

    private bool IsAllowedToLoad(Container container)
    {
        var containersWeightTons = _containers.Sum(cont => cont.GetTotalWeight()) / 1000.0;
        var newContainerWeightTons = container.GetTotalWeight() / 1000.0;
        return containersWeightTons + newContainerWeightTons <= MaxContainersWeightInTons;
    }

    public void LoadContainer(Container container)
    {
        if (_containers.Count >= MaxContainersCount)
        {
            throw new OverfillException(
                $"Attempt to load container {container.SerialNumber} exceeds ship's maximum container count of {MaxContainersCount}");
        }

        var newContainerWeightTons = container.GetTotalWeight() / 1000.0;
        if (!IsAllowedToLoad(container))
        {
            throw new OverfillException(
                $"Attempt to load additional {newContainerWeightTons} tons to ship exceeds ship's maximum weight of {MaxContainersWeightInTons} tons");
        }

        _containers.Add(container);
    }

    public void LoadContainers(List<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    private Container GetContainer(string serialNumber)
    {
        var container = _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            throw new ResourceNotFoundException($"There is no container with the serial number {serialNumber}");
        }

        return container;
    }

    public void RemoveContainer(string serialNumber)
    {
        var container = GetContainer(serialNumber);
        _containers.Remove(container);
    }

    public void UnloadContainer(string serialNumber)
    {
        var container = GetContainer(serialNumber);
        container.EmptyCargo();
    }

    public void ReplaceContainer(string serialNumber, Container newContainer)
    {
        var oldContainer = GetContainer(serialNumber);
        RemoveContainer(serialNumber);
        if (!IsAllowedToLoad(newContainer))
        {
            LoadContainer(oldContainer);
            var newContainerWeightTons = newContainer.GetTotalWeight() / 1000.0;
            throw new OverfillException(
                $"Attempt to load additional {newContainerWeightTons} tons to ship exceeds ship's maximum weight of {MaxContainersWeightInTons} tons");
        }

        LoadContainer(newContainer);
    }

    public static void TransferContainer(ContainerShip shipFrom, ContainerShip shipTo, string serialNumber)
    {
        var container = shipFrom.GetContainer(serialNumber);
        shipFrom._containers.Remove(container);
        shipTo.LoadContainer(container);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"ℹ️ Ship information:");
        Console.WriteLine($"Max speed: {MaxSpeed} knots");
        Console.WriteLine($"Max containers count: {MaxContainersCount}");
        Console.WriteLine($"Max containers weight (tons): {MaxContainersWeightInTons}");
        Console.WriteLine($"Currently has {_containers.Count} container(s)");

        var containersWeightTons = _containers.Sum(cont => cont.GetTotalWeight()) / 1000.0;
        Console.WriteLine($"Total weight of containers on the ship: {containersWeightTons} tons");

        Console.WriteLine("Containers:");
        foreach (var container in _containers)
        {
            Console.WriteLine("   " + container);
        }

        Console.WriteLine();
    }
}