// See https://aka.ms/new-console-template for more information

uint maxInstances = 10; // the "n" instances
uint instanceRunning = 0;
uint party = 0;
uint tanks = 33;
uint healer = 33;
uint dps = 100;
uint minTime = 200;
uint maxTime = 1000;

// Inputting time
Config? config;

if (args.Length == 6)
{

}



Queue<Party> partyQueue = new(); // because my LSP
                                 // complained, so i
                                 // casted it from uint
                                 // to int
List<Thread> threadlist = new((int)maxInstances);
Console.WriteLine("Program starts!");

object mutual_lock = new();
object datetime_lock = new();
object stats_lock = new();

QueueParty();

for (int i = 0; i < maxInstances; i++)
{
    threadlist.Add(new(InstanceFunction));
    threadlist.Last().Start();
}

foreach (var thread in threadlist)
{
    thread.Join();
}


Console.Write("\n");
Console.WriteLine($"Number of parties created: {party}");

void QueueParty()
{
    Party? newParty = null;

    while (!seeIfAnyEmpty())
    {
        Console.WriteLine(
            $"Making a party..."
        );

        newParty ??= new();

        Console.WriteLine(
            $"A party is created."
        );

        while (!newParty.IsFull() && !seeIfAnyEmpty())
        {
            if (newParty.AddTanks())
                tanks--;
            if (newParty.AddHealer())
                healer--;
            if (newParty.AddDPS())
                dps--;
        }

        if (newParty.IsFull())
        {
            newParty.ShowInfo();
            AddParty();
            partyQueue.Enqueue(newParty);
        }

        newParty = null;

        showRemaining();
    }
}

void InstanceFunction()
{
    int threadHashCode = Thread.CurrentThread.GetHashCode();
    string nameInstance = "newInstance";
    uint sleepTime = GetRandomTime();

    bool lookingForParty = true;

    WriteInstanceStatus(threadHashCode, nameInstance, false);
    while (true)
    {
        // Monitor.Enter(mutual_lock);
        lock (mutual_lock)
        {
            lookingForParty = partyQueue.Count != 0;
        }

        if (!lookingForParty)
        {
            break;
        }

        Party? queuedParty;

        lock (mutual_lock)
        {
            queuedParty = partyQueue.Dequeue();
        }

        if (queuedParty == null)
        {
            ConsoleWriteLineThread(
                threadHashCode,
                nameInstance,
                "Dequeueing failed!!"
            );

            return;
        }

        WriteInstanceStatus(threadHashCode, nameInstance, true);

        // Monitor.Exit(mutual_lock);

        ConsoleWriteLineThread(
            threadHashCode,
            nameInstance,
            $"Thread sleeps for {sleepTime}."
        );
        Thread.Sleep((int)sleepTime);

        WriteInstanceStatus(threadHashCode, nameInstance, false);
    }

    ConsoleWriteLineThread(
        threadHashCode,
        nameInstance,
        "Done!"
    );
}

uint GetRandomTime()
{
    uint time = 0;
    Random random = Random.Shared;
    time = (uint)random.Next() % (maxTime - minTime) + minTime;

    return time;
}

/*
while (!seeIfAnyEmpty())
{
    // Starting steps
    if (listOfParty.Count == 0)
    {
        Console.Write("\n");
        listOfParty.Add(new Party());
    }

    Party newParty = listOfParty.Last();

    // Party member assigning
    if (newParty.AddTanks())
        tanks--;

    if (newParty.AddHealer())
        healer--;

    if (newParty.AddDPS())
        dps--;

    newParty.ShowInfo();
    showRemaining();


    // End condition
    if (newParty.IsFull())
    {
        addParty();

        assignPartyToInstance();

        // Either wait for the instance list to run out
        if (isInstanceQueueFull())
        {
            Console.Write("\n");
            Console.WriteLine("The parties are about to do their mission...");

            while (instanceRunning > 0)
            {
                partyCompletesMission();
            }
            
            Console.WriteLine("All parties have completed the mission.");
        }

        // or make the party do the mission immediately
        // by the time they get a mission.
        // partyCompletesMission();

        if (!seeIfAnyEmpty())
        {
            Console.Write("\n");
            listOfParty.Add(new Party());
        }
    }
}

*/

bool isInstanceQueueFull()
{
    return instanceRunning == maxInstances;
}

void showRemaining()
{
    Console.WriteLine("---Remaining---");
    Console.WriteLine($"tank: {tanks}");
    Console.WriteLine($"healer: {healer}");
    Console.WriteLine($"dps: {dps}");
}

void AddParty()
{
    party++;
}

void assignPartyToInstance()
{
    Console.WriteLine("The party is assigned to a mission!");
    instanceRunning++;

    Console.WriteLine($"Instances: {instanceRunning}/{maxInstances}");
}

void partyCompletesMission()
{
    Console.WriteLine("The party completed the mission.");
    instanceRunning--;
}

bool seeIfAnyEmpty()
{
    return (tanks == 0) || (healer == 0) || (dps == 0);
}

void DrawLine()
{
    Console.WriteLine("--------------------------------");
}

string NameThread(int threadHashCode, string nameInstance)
{
    return $"Thread {nameInstance} {threadHashCode}: ";
}

void ConsoleWriteLineThread(
        int threadHashCode,
        string nameInstance,
        string caption
        )
{
    Console.WriteLine(
        NameThread(threadHashCode, nameInstance) + caption
    );
}

void WriteInstanceStatus(
        int threadHashCode,
        string nameInstance,
        bool status
)
{
    string statusCaption = "";
    switch (status)
    {
        case true:
            statusCaption = "active";
            break;

        case false:
            statusCaption = "empty";
            break;
    }
    ConsoleWriteLineThread(
        threadHashCode,
        nameInstance,
        $"Status: {statusCaption}."
    );
}

void WriteTimeStamp(
        int threadHashCode,
        string nameInstance,
        bool status
)
{
    string timeStampCaption = "";
    ConsoleWriteLineThread(
        threadHashCode,
        nameInstance,
        $"Status: {timeStampCaption}."
    );
}

class Instance
{
    private string nameInstance;
    private int hashCode;

    Instance(string nameInstance, int hashCode)
    {
        this.nameInstance = nameInstance;
        this.hashCode = hashCode;
    }
}

record Config(
    uint MaxInstances = 1,
    uint Tanks = 1,
    uint Healer = 1,
    uint Dps = 3,
    uint MinTime = 1,
    uint MaxTime = 15
);

class ArgsInput
{
    private uint maxInstances = 1;
    private uint tanks = 1;
    private uint healers = 1;
    private uint dps = 3;
    private uint minTime = 1;
    private uint maxTime = 15;

    private void ParseMaxInstances(string arg)
    {
        string name = "number of instances";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        maxInstances = uint.Parse(arg);
    }

    private void ParseTank(string arg)
    {
        string name = "number of Tanks";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        tanks = uint.Parse(arg);
    }

    private void ParseHealer(string arg)
    {
        string name = "number of Healers";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        healers = uint.Parse(arg);
    }
    
    private void ParseDPS(string arg)
    {
        string name = "number of DPS's";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        dps = uint.Parse(arg);
    }

    private void ParseMinTime(string arg)
    {
        string name = "minimum time";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        minTime = uint.Parse(arg);
    }

    private void ParseMaxTime(string arg)
    {
        string name = "maximum time";

        if (!ValidInput.CheckIfValid(name, arg))
            return;

        maxTime = uint.Parse(arg);
    }

    public void ParseArgs(string[] args)
    {
        ParseMaxInstances(args[0]);
        ParseTank(args[1]);
        ParseHealer(args[2]);
        ParseDPS(args[3]);
        ParseMinTime(args[4]);
        ParseMaxTime(args[5]);
    }

}

class RuntimeInput
{
    private uint maxInstances = 1;
    private uint tanks = 1;
    private uint healers = 1;
    private uint dps = 3;
    private uint minTime = 1;
    private uint maxTime = 15;

    public Config InvokeInput()
    {
        Console.Write("Number of Instances: ");
        Console.ReadLine();
    }
}

class Input
{
    
}

class ValidInput
{
    private static void ShowErrorParsing(string field)
    {
        Console.WriteLine(
            $"ERROR! "
            + "{field} must be a positive number. "
            + "No letters or symbols are allowed."
        );
    }

    private static void ShowErrorNegative(string field)
    {
        Console.WriteLine(
            $"ERROR! "
            + "{field} must be a positive number. "
        );
    }

    public static bool CheckIfValid(string name, string arg)
    {
        if (!int.TryParse(arg, out int result))
        {
            ShowErrorParsing(name);
            return false;
        }

        if (result < 0)
        {
            ShowErrorNegative(name);
            return false;
        }

        return true;
    }
}
