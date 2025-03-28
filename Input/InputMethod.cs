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

        protected ushort validFlags = 0b111111;

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

        protected bool ParseMaxInstances(string arg)
        {
            string name = "number of instances";

            if (!ValidInput.CheckIfValid(name, arg))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.MaxInstances);
                return false;
            }

            ushort result = ushort.Parse(arg);
            // if (!ValidInput.CheckIfNotZero(name, result))
            // {
            //     validFlags = (ushort)(validFlags & ~ValidFlags.MaxInstances);
            //     return false;
            // }

            MaxInstances = result;
            return true;
        }

        protected bool ParseTank(string arg)
        {
            string name = "number of Tanks";

            if (!ValidInput.CheckIfValid(name, arg))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.Tanks);
                return false;
            }

            Tanks = ushort.Parse(arg);
            return true;
        }

        protected bool ParseHealer(string arg)
        {
            string name = "number of Healers";

            if (!ValidInput.CheckIfValid(name, arg))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.Healers);
                return false;
            }

            Healer = ushort.Parse(arg);
            return true;
        }
        
        protected bool ParseDPS(string arg)
        {
            string name = "number of DPS's";

            if (!ValidInput.CheckIfValid(name, arg))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.DPS);
                return false;
            }

            Dps = ushort.Parse(arg);
            return true;
        }

        protected bool ParseMinTime(string arg)
        {
            string name = "minimum time";

            if (!ValidInput.CheckIfValid(name, arg))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.MinTime);
                return false;
            }

            ushort result = ushort.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.MinTime);
                return false;
            }

            MinTime = result;
            return true;
        }

        protected bool ParseMaxTime(string arg)
        {
            string name = "maximum time";

            if (!ValidInput.CheckIfValid(name, arg))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.MaxTime);
                return false;
            }

            ushort result = ushort.Parse(arg);
            if (!ValidInput.CheckIfNotZero(name, result))
            {
                validFlags = (ushort)(validFlags & ~ValidFlags.MaxTime);
                return false;
            }

            MaxTime = result;
            return true;
        }

        public void ChangeValidFlags(ushort flags)
        {
            validFlags = flags;
        }
    }
}
