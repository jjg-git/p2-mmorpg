// See https://aka.ms/new-console-template for more information
uint maxInstances = 1;
uint tanks = 1;
uint healer = 1;
uint dps = 1;

void showRemaining()
{
    Console.WriteLine("---Remaining---");
    Console.WriteLine($"tank: {tanks}");
    Console.WriteLine($"healer: {healer}");
    Console.WriteLine($"dps: {dps}");
}

void addParty()
{
    party++;
}

void assignPartyToInstance()
{
    instanceRunning++;
}

bool seeIfAnyEmpty()
{
    return (tanks == 0) || (healer == 0) || (dps == 0);
}

class Party
{

    public bool addTanks()
    {
        if (tanks == maxTanks)
            return false;

        tanks += 1;
        return true;
    }

    public bool addHealer()
    {
        if (healer == maxHealer)
            return false;

        healer += 1;
        return true;
    }

    public bool addDPS()
    {
        if (dps == maxDPS)
            return false;
        dps += 1;
        return true;
    }

    public bool isFull()
    {
        return tanks == maxTanks &&
               healer == maxHealer &&
               dps == maxDPS;
    }

    public void showInfo()
    {
        Console.WriteLine(
            $"Party ID {id} {{tanks: {tanks}, healer: {healer}, dps: {dps}}}"
        );
    }

    public Party()
    {
        id = count;
        count++;
    }

    private const int maxTanks = 1;
    private const int maxHealer = 1;
    private const int maxDPS = 3;

    private int tanks = 0;
    private int healer = 0;
    private int dps = 0;

    private static int count = 0;
    private int id = 0;
}
