// See https://aka.ms/new-console-template for more information
namespace p2_mmorpg.Input
{
    class RuntimeInput : InputMethod
    {
        private delegate void ParseFunc(string input);

        private void AskInput(string caption, ParseFunc parseFunc)
        {
            Console.Write(caption);
            string input = Util.ReadLine();
            parseFunc(input);
        }
        
        public override void Invoke()
        {
            AskInput("Number of instances? ", ParseMaxInstances);
            AskInput("Number of tanks? ", ParseTank);
            AskInput("Number of healers? ", ParseHealer);
            AskInput("Number of DPS? ", ParseDPS);
            AskInput("Minimum time in seconds for the instance to run? ", ParseMinTime);
            AskInput("Maximum time in seconds for the instance to run? ", ParseMaxTime);
        }
    }
}
