namespace p2_mmorpg.Input
{
    class ArgsInput(string[] args) : InputMethod
    {
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
                // validFlags = (ushort)(validFlags & ~ValidFlags.MinTime);
                // validFlags = (ushort)(validFlags & ~ValidFlags.MaxTime);
            }

            // if (validFlags != ValidFlags.AllValid)
            // {
            //     RuntimeInput runtimeInput = new();
            //     Config correctConfig;

            //     runtimeInput.ChangeValidFlags(validFlags);
            //     correctConfig = new PrepareConfig().Execute(runtimeInput);
            //     config = correctConfig;
            // }
        }

        private string[] args = args;
    }
}
