namespace p2_mmorpg.Input
{
    abstract public class InputMethod ()
    {
        protected uint MaxInstances = 1;
        protected uint Tanks = 1;
        protected uint Healer = 1;
        protected uint Dps = 3;
        protected uint MinTime = 1;
        protected uint MaxTime = 2;

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

            uint result = uint.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
                return;

            MaxInstances = result;
        }

        protected void ParseTank(string arg)
        {
            string name = "number of Tanks";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            Tanks = uint.Parse(arg);
        }

        protected void ParseHealer(string arg)
        {
            string name = "number of Healers";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            Healer = uint.Parse(arg);
        }
        
        protected void ParseDPS(string arg)
        {
            string name = "number of DPS's";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            Dps = uint.Parse(arg);
        }

        protected void ParseMinTime(string arg)
        {
            string name = "minimum time";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            uint result = uint.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
                return;

            MinTime = result;
        }

        protected void ParseMaxTime(string arg)
        {
            string name = "maximum time";

            if (!ValidInput.CheckIfValid(name, arg))
                return;

            uint result = uint.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
                return;

            MaxTime = result;
        }
    }
}
