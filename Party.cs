class Party
{
    
    public bool AddTanks(int remaining)
    {
        if (tanks == maxTanks)
            return false;

        if (remaining == 0)
        {
            CanGet = false;
            Console.WriteLine($"from AddTanks: CanGet = {CanGet}");
            return false;
        }

        tanks += 1;
        return true;
    }

    public bool AddHealer(int remaining)
    {
        if (healer == maxHealer)
            return false;

        if (remaining == 0)
        {
            CanGet = false;
            Console.WriteLine($"from AddHealer: CanGet = {CanGet}");
            return false;
        }

        healer += 1;
        return true;
    }

    public bool AddDPS(int remaining)
    {
        if (dps == maxDPS)
            return false;

        if (remaining == 0)
        {
            CanGet = false;
            Console.WriteLine($"from AddDPS: CanGet = {CanGet}");
            return false;
        }

        dps += 1;
        return true;
    }

    public bool IsFull()
    {
        if (tanks == maxTanks &&
               healer == maxHealer &&
               dps == maxDPS)
        {
            Console.WriteLine("Is full!");
            return true;
        }
        return false;
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

    public bool CanGet { get; set; } = true;

    private const int maxTanks = 1;
    private const int maxHealer = 1;
    private const int maxDPS = 3;

    private int tanks = 0;
    private int healer = 0;
    private int dps = 0;

    private static int count = 0;
    private int id = 0;
}
