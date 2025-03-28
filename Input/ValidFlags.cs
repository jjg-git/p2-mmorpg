namespace p2_mmorpg.Input
{
    public class ValidFlags
    {
        public const ushort MaxInstances = 0b1;
        public const ushort Tanks = 0b10;
        public const ushort Healers = 0b100;
        public const ushort DPS = 0b1000;
        public const ushort MinTime = 0b10000;
        public const ushort MaxTime = 0b100000;
        public const ushort AllValid = 0b111111;

        public static bool CheckFlag(ushort whichFlag, ushort source)
        {
            return (source & whichFlag) > 0;
        }
    }
}
