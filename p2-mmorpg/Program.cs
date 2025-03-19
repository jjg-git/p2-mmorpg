// See https://aka.ms/new-console-template for more information


uint maxInstances = 2; // the "n" instances
uint instanceRunning = 0;

uint party = 0;

uint tanks = 10;
uint healer = 10;
uint dps = 10;

uint minTime = 200;
uint maxTime = 1000;

Queue<Party> partyQueue = new(); // because my LSP
                                 // complained, so i
                                 // casted it from uint
                                 // to int
List<Thread> threadlist = new((int)maxInstances);
Console.WriteLine("Program starts!");

int data = 0;
object mutual_lock = new();

for (int i = 0; i < maxInstances; i++)
{
    threadlist.Add(new(InstanceFunction));
    threadlist.Last().Start();
}

foreach (var thread in threadlist)
{
    thread.Join();
}

void QueueParty()
{
    Party? newParty = null;
    while (!seeIfAnyEmpty())
    {
        if (partyQueue.Count != 0)
            continue;

        newParty ??= new();
        partyQueue.Enqueue(newParty);
        uint acquiredTanks = 0;
        uint acquiredHealer = 0;
        uint acquiredDPS = 0;

        while (true)
        {
            if (acquiredTanks == 1 &&
                    acquiredHealer == 1 &&
                    acquiredDPS == 3)
            {
                break;
            }
            lock (mutual_lock)
            {
                if (acquiredTanks != 1)
                {
                    tanks--;
                    acquiredTanks++;
                }
                if (acquiredHealer != 1)
                {
                    healer--;
                    acquiredHealer++;
                }
                if (acquiredDPS != 3)
                {
                    dps--;
                    acquiredDPS++;
                }
            }
        }
        Monitor.Exit(mutual_lock);
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
Console.Write("\n");
Console.WriteLine($"Number of parties created: {party}");

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

void addParty()
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

