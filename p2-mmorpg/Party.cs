using Players;

namespace Mechanics
{
    class Party
    {
        public int NoOfDps { get; } = 0;
        public int NoOfTank { get; } = 0;
        public int NoOfHealer { get; } = 0;

        public List<Dps> DpsList { get; }
        public List<Tank> TankList { get; }
        public List<Healer> HealerList { get; }


        public Party()
        {
            DpsList = new(3);
            TankList = new(1);
            HealerList = new(1);
        }

        public void AddPartyOut(Dps newDps)
        {
            DpsList.Add(newDps);
            newDps.Field1 = 20;
        }
    }
}
