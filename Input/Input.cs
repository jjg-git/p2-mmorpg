namespace p2_mmorpg.Input
{
    abstract class IntInput
    {
        public abstract uint Value { get; set; }
        public abstract uint ParseValue(string arg);
        }

        class MaxInstances : IntInput
        {
        override public uint Value { get; set; } = 5;
        public override uint ParseValue(string arg)
        {
            try
            {
                Value = uint.Parse(arg);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error! ${e.Message}");
                Console.WriteLine($"Setting max number of instances to {Value}");
            }
            return Value;
        }
    }
}
