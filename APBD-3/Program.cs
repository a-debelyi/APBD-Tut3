using APBD_3;

try
{
    Console.WriteLine(">>>> Creating containers");
    var liquidContainer1 = new LiquidContainer(
        height: 250, tareWeight: 150, depth: 250, maxPayload: 1000, isHazardous: false);

    var liquidContainer2 = new LiquidContainer(
        height: 250, tareWeight: 150, depth: 250, maxPayload: 800, isHazardous: true);

    var gasContainer = new GasContainer(
        height: 200, tareWeight: 120, depth: 200, maxPayload: 600, pressure: 15);

    var refrigeratedContainer = new RefrigeratedContainer(
        height: 300, tareWeight: 200, depth: 300, maxPayload: 700, productType: "Cheese", maintainedTemperature: 10);

    Console.WriteLine(liquidContainer1);
    Console.WriteLine(liquidContainer2);
    Console.WriteLine(gasContainer);
    Console.WriteLine(refrigeratedContainer);

    Console.WriteLine("\n>>>>  Loading cargo into containers");
    liquidContainer1.LoadCargo(500);
    Console.WriteLine($"Loaded 500 kg into container {liquidContainer1.SerialNumber}");

    liquidContainer2.LoadCargo(300);
    Console.WriteLine($"Loaded 300 kg into container {liquidContainer2.SerialNumber}");

    gasContainer.LoadCargo(400);
    Console.WriteLine($"Loaded 400 kg into container {gasContainer.SerialNumber}");

    refrigeratedContainer.LoadCargo(350);
    Console.WriteLine($"Loaded 350 kg into container {refrigeratedContainer.SerialNumber}");

    Console.WriteLine("\n>>>> Creating a container ship and loading containers");
    var ship1 = new ContainerShip(maxSpeed: 30, maxContainersCount: 5, maxContainersWeightInTons: 10);
    ship1.LoadContainer(liquidContainer1);
    ship1.LoadContainer(liquidContainer2);
    ship1.LoadContainer(gasContainer);
    ship1.PrintInfo();

    Console.WriteLine("\n>>>> Loading multiple containers onto the ship");
    var extraContainers = new List<Container>
    {
        refrigeratedContainer,
        new LiquidContainer(height: 250, tareWeight: 150, depth: 250, maxPayload: 1000, isHazardous: false)
    };
    ship1.LoadContainers(extraContainers);
    ship1.PrintInfo();

    Console.WriteLine("\n>>>> Removing a container from the ship");
    ship1.RemoveContainer(gasContainer.SerialNumber);
    ship1.PrintInfo();

    Console.WriteLine("\n>>>> Unloading a container ===");
    ship1.UnloadContainer(liquidContainer2.SerialNumber);
    Console.WriteLine($"After unloading, container {liquidContainer2.SerialNumber} info:");
    Console.WriteLine(liquidContainer2);

    Console.WriteLine("\n>>>> Replacing a container on the ship");
    var replacementContainer = new GasContainer(
        height: 200, tareWeight: 120, depth: 200, maxPayload: 600, pressure: 20);
    ship1.ReplaceContainer(liquidContainer1.SerialNumber, replacementContainer);
    ship1.PrintInfo();

    Console.WriteLine("\n>>>> Transferring a container between ships");
    var ship2 = new ContainerShip(maxSpeed: 25, maxContainersCount: 3, maxContainersWeightInTons: 15);
    ContainerShip.TransferContainer(ship1, ship2, replacementContainer.SerialNumber);
    Console.WriteLine("Ship1 after transfer:");
    ship1.PrintInfo();
    Console.WriteLine("Ship2 after transfer:");
    ship2.PrintInfo();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}