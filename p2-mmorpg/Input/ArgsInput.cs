namespace p2_mmorpg.Input
{
    class ArgsInput : InputMethod
    {
        public ArgsInput(string[] args)
        {
            this.args = args;
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
}
