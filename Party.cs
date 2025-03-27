class Party
{

    public bool AddTanks()
    {
        if (tanks == maxTanks)
            return false;

        tanks += 1;
        return true;
    }

    public bool AddHealer()
    {
        if (healer == maxHealer)
            return false;

        healer += 1;
        return true;
    }

    public bool AddDPS()
    {
        if (dps == maxDPS)
            return false;
        dps += 1;
        return true;
    }

    public bool IsFull()
    {
        return tanks == maxTanks &&
               healer == maxHealer &&
               dps == maxDPS;
    }

    public void ShowInfo()
    {
        Console.WriteLine(
            $"Party ID {id} {{tanks: {tanks}, healer: {healer}, dps: {dps}}}"
        );
    }

    public Party()
    {
        id = count;
        // Console.WriteLine($"Party {id} is created!");

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
