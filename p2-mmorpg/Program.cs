// See https://aka.ms/new-console-template for more information

Lock _lock = new();

new Thread( () => 
    {
        lock(_lock) 
        {
            var id = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine($"Thread {id}: I sleep."); 
            Thread.Sleep(1000); 
            Console.WriteLine($"Thread {id}: REAL SHIT?"); 
        }
    }).Start();
Console.WriteLine("Hello, World!");
