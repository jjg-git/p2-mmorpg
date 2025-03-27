namespace Players
{
    abstract class Player 
    {
        public Player()
        {
            PrintField1();
        }
        private int _field1 = 10;
        public int Field1
        { 
            get => _field1;
            set 
            {
                _field1 = value;
                PrintField1();
            }        
        }

        public void PrintField1()
        {
            Console.WriteLine("Player's _field1 is now {0}.", _field1);
        }
    }

    class Tank : Player {}
    class Dps : Player {}
    class Healer : Player {}
}