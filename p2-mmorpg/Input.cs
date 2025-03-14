namespace Input
{
    abstract class IntInput
    {
        public abstract uint Value { get; set; }
        public virtual uint ParseValue(string arg);
    }

    class MaxInstances : IntInput
    {
        override public uint Value { get; set; } = 5;
        override public uint ParseValue(string arg)
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
        }
    }
}
