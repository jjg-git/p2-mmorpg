namespace p2_mmorpg.Input
{
    abstract public class InputMethod ()
    {
        protected ushort MaxInstances = DefaultConfig.MaxInstances;
        protected ushort Tanks = DefaultConfig.Tanks;
        protected ushort Healer = DefaultConfig.Healer;
        protected ushort Dps = DefaultConfig.Dps;
        protected ushort MinTime = DefaultConfig.MaxTime;
        protected ushort MaxTime = DefaultConfig.MinTime;

        public abstract void Invoke();
        public Config Execute()
        {
            Invoke();
            ResetTime();

            Config result = new(
                MaxInstances,
                Tanks, 
                Healer, 
                Dps, 
                MinTime, 
                MaxTime
            );

            return result;
        }

        private void ResetTime()
        {
            if (!ValidInput.CheckTime(MinTime, MaxTime))
            {
                MinTime = DefaultConfig.MinTime;
                MaxTime = DefaultConfig.MaxTime;
            }
        }

        protected void ParseMaxInstances(string arg)
        {
            string name = "number of instances";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            ushort result = ushort.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
                return;

            MaxInstances = result;
        }

        protected void ParseTank(string arg)
        {
            string name = "number of Tanks";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            Tanks = ushort.Parse(arg);
        }

        protected void ParseHealer(string arg)
        {
            string name = "number of Healers";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            Healer = ushort.Parse(arg);
        }
        
        protected void ParseDPS(string arg)
        {
            string name = "number of DPS's";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            Dps = ushort.Parse(arg);
        }

        protected void ParseMinTime(string arg)
        {
            string name = "minimum time";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            ushort result = ushort.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
                return;

            MinTime = result;
        }

        protected void ParseMaxTime(string arg)
        {
            string name = "maximum time";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            ushort result = ushort.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
                return;

            MaxTime = result;
        }
    }
}
