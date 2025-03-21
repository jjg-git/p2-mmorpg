class ArgsInput : InputMethod
{
    public ArgsInput(string[] args)
    {
        this.args = args;
    }

    private void ParseMaxInstances(string arg)
    {
        string name = "number of instances";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        MaxInstances = uint.Parse(arg);
    }

    private void ParseTank(string arg)
    {
        string name = "number of Tanks";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        Tanks = uint.Parse(arg);
    }

    private void ParseHealer(string arg)
    {
        string name = "number of Healers";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        Healer = uint.Parse(arg);
    }
    
    private void ParseDPS(string arg)
    {
        string name = "number of DPS's";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        Dps = uint.Parse(arg);
    }

    private void ParseMinTime(string arg)
    {
        string name = "minimum time";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        MinTime = uint.Parse(arg);
    }

    private void ParseMaxTime(string arg)
    {
        string name = "maximum time";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        MaxTime = uint.Parse(arg);
    }

    private void ParseArgs(string[] args)
    {
        ParseMaxInstances(args[0]);
        ParseTank(args[1]);
        ParseHealer(args[2]);
        ParseDPS(args[3]);
        ParseMinTime(args[4]);
        ParseMaxTime(args[5]);
    }

    public override void Invoke()
    {
        ParseArgs(args);

        if (!ValidInput.CheckTime(MinTime, MaxTime))
        {
            MinTime = DefaultConfig.MinTime;
            MaxTime = DefaultConfig.MaxTime;
        }
    }

    private string[] args;
}
