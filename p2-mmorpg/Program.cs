// See https://aka.ms/new-console-template for more information

// Inputting time
Config config = new();
PrepareConfig prepareConfig = new();

// Console.WriteLine($"args.Length = {args.Length}");

if (args.Length != 6)
{
    if (args.Length > 0)
    {
        Error.ShowMessage("""
                Wrong syntax of the arguments.
                Syntax: instances tanks healers dps min max
                where
                    instances - number of instances
                    tanks - number of tanks in the Queue
                    healers - number of healers in the Queue
                    dps - number of dps in the Queue
                    min - minimum time for the instance to wait
                    max - maximum time for the instance to wait
                """
        ); 
    }
    config = prepareConfig.Execute(new RuntimeInput());
}
else
{
    config = prepareConfig.Execute(new ArgsInput(args));
}

ConfigInfo(config);

uint maxInstances = config.MaxInstances; // the "n" instances
uint tanks = config.Tanks;
uint healer = config.Healer;
uint dps = config.Dps;
uint minTime = config.MinTime;
uint maxTime = config.MaxTime;

uint instanceRunning = 0;

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
    int id = i;
    threadlist.Add(new(() => { InstanceFunction(id); }));
    threadlist.Last().Start();

    // Console.WriteLine($"Instance {i} is created!");
}

foreach (var thread in threadlist)
{
    thread.Join();
}


Console.Write("\n");
foreach (string? log in instanceLogs)
{
    Console.WriteLine(log);
}

void QueueParty()
{
    Party? newParty = null;

    while (!seeIfAllEmpty())
    {
        // Console.WriteLine(
        //     $"Making a party..."
        // );

        newParty ??= new();

        // Console.WriteLine(
        //     $"A party is created."
        // );

        while (!newParty.IsFull() && !seeIfAllEmpty())
        {
            if (tanks != 0)
            {
                if (newParty.AddTanks())
                    tanks--;
            }
            
            if (healer != 0)
            {
                if (newParty.AddHealer())
                    healer--;
            }

            if (dps != 0)
            {
                if (newParty.AddDPS())
                    dps--;
            }
        }

        if (newParty.IsFull())
        {
            newParty.ShowInfo();
            partyQueue.Enqueue(newParty);
        }

        newParty = null;

        showRemaining();
    }
}

void InstanceFunction(int id)
{
    uint sleepTime = GetRandomTime();

    bool lookingForParty = true;

    while (true)
    {
        Debug.WriteInstanceStatus(id, false);
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

        Debug.WriteInstanceStatus(id, true);

        // Monitor.Exit(mutual_lock);

        ConsoleWriteLineThread(
            threadHashCode,
            nameInstance,
            $"Thread sleeps for {sleepTime}."
        );
        Thread.Sleep((int)sleepTime);

        Debug.WriteInstanceStatus(id, false);
    }

    // Debug.WriteInstanceStatus(id, false);
    // ConsoleWriteLineThread(
    //     threadHashCode,
    //     nameInstance,
    //     "Done!"
    // );
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

bool seeIfAllEmpty()
{
    return (tanks == 0) && (healer == 0) && (dps == 0);
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

void WriteTimeStamp(
        int threadHashCode,
        string nameInstance,
        bool status
)
{
    string timeStampCaption = "";
    Debug.ConsoleWriteLineThread(
        threadHashCode,
        nameInstance,
        $"Status: {timeStampCaption}."
    );
}

void ConfigInfo(Config config)
{
    Console.WriteLine(
        $"""
        MaxInstances = {config.MaxInstances}
        Tanks = {config.Tanks}
        Healer = {config.Healer}
        DPS = {config.Dps}
        MinTime = {config.MinTime}
        MaxTime = {config.MaxTime}
        """
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

