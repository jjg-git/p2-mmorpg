// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using p2_mmorpg.Input;

const uint msPerSec = 1000;
long programStartTimelapse;
long programEndTimelapse;

long queuePartyStartTimeLapse;
long queuePartyEndTimeLapse;

long instanceStartTimeLapse;
long instanceEndTimeLapse;

programStartTimelapse = Stopwatch.GetTimestamp();

// Statistics
List<string> instanceLogs;

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

ushort maxInstances = config.MaxInstances; // the "n" instances
ushort tanks = config.Tanks;
ushort healer = config.Healer;
ushort dps = config.Dps;
ushort minTime = config.MinTime;
ushort maxTime = config.MaxTime;


Queue<Party> partyQueue = new(); // because my LSP
                                 // complained, so i
                                 // casted it from uint
                                 // to int
List<Thread> threadlist = new(Convert.ToInt32(maxInstances));
instanceLogs = CreateListOfEmptyStrs(Convert.ToInt32(maxInstances));

Console.WriteLine("Program starts!");

object mutual_lock = new();
object datetime_lock = new();
object stats_lock = new();

queuePartyStartTimeLapse = Stopwatch.GetTimestamp();
QueueParty();
queuePartyEndTimeLapse = Stopwatch.GetTimestamp();

for (ushort i = 0; i < maxInstances; i++)
{
    ushort id = i;
    threadlist.Add(new(() => { InstanceFunction(id); }));
    threadlist.Last().Start();

    // Console.WriteLine($"Instance {i} is created!");
}

instanceStartTimeLapse = Stopwatch.GetTimestamp();
foreach (var thread in threadlist)
{
    thread.Join();
}
instanceEndTimeLapse = Stopwatch.GetTimestamp();

Console.Write("\n");
foreach (string? log in instanceLogs)
{
    Console.WriteLine(log);
}

programEndTimelapse = Stopwatch.GetTimestamp();

TimeSpan programTimeSpan = Stopwatch.GetElapsedTime(
                                programStartTimelapse, 
                                programEndTimelapse
                           );
TimeSpan queuePartyTimeSpan = Stopwatch.GetElapsedTime(
                                queuePartyStartTimeLapse, 
                                queuePartyEndTimeLapse
                              );
TimeSpan instanceTimeSpan = Stopwatch.GetElapsedTime(
                                instanceStartTimeLapse, 
                                instanceEndTimeLapse
                            );

ShowRemaining();
Console.WriteLine(
    "The program has been running for " + 
        $"{programTimeSpan.TotalSeconds:N4} seconds."
);
Console.WriteLine(
    "The party queueing was running for "+
        $"{queuePartyTimeSpan.TotalSeconds:N4} seconds."
);
Console.WriteLine(
    "The instances had been running for "+
        $"{instanceTimeSpan.TotalSeconds:N4} seconds."
);

void QueueParty()
{
    Party? newParty = null;

    while (!seeIfAnyZero())
    {
        // Console.WriteLine(
        //     $"Making a party..."
        // );

        newParty ??= new();

        // Console.WriteLine(
        //     $"A party is created."
        // );

        while (!newParty.IsFull() && !seeIfAnyZero())
        {
            // if (tanks != 0)
            // {
            //     if (newParty.AddTanks())
            //         tanks--;
            // }
            // 
            // if (healer != 0)
            // {
            //     if (newParty.AddHealer())
            //         healer--;
            // }

            // if (dps != 0)
            // {
            //     if (newParty.AddDPS())
            //         dps--;
            // }

            if (newParty.AddTanks())
                tanks--;
        
            if (newParty.AddHealer())
                healer--;

            if (newParty.AddDPS())
                dps--;
        }

        if (newParty.IsFull())
        {
            // newParty.ShowInfo();
            partyQueue.Enqueue(newParty);
        }

        newParty = null;

        ShowRemaining();
    }
}

void InstanceFunction(ushort id)
{
    // Console.WriteLine(
    //     $"Instance {id} ({Thread.CurrentThread.GetHashCode()}) starts!"
    // );

    // int threadHashCode = Thread.CurrentThread.GetHashCode();
    // string nameInstance = "newInstance";
    ushort sleepTime = GetRandomTime();
    ushort partyCreated = 0;

    // For timelapse
    string timeLog = string.Empty;
    string partyLog = string.Empty;

    bool lookingForParty = true;

    long previous;
    long current;

    // Console.WriteLine($"previous (id {id}) = {previous.TotalMilliseconds}");
    previous = Stopwatch.GetTimestamp();

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
            // Console.WriteLine(
            //     $"Instance {id} cannot find any more parties."
            // );
            break;
        }

        Party? queuedParty;

        lock (mutual_lock)
        {
            queuedParty = partyQueue.Dequeue();
        }

        if (queuedParty == null)
        {
            /*
            ConsoleWriteLineThread(
                threadHashCode,
                nameInstance,
                "Dequeueing failed!!"
            );
            */

            break;
        }
        assignPartyToInstance(id);
        partyCreated++;

        Debug.WriteInstanceStatus(id, true);

        // Monitor.Exit(mutual_lock);

        // ConsoleWriteLineThread(
        //     threadHashCode,
        //     nameInstance,
        //     $"Thread sleeps for {sleepTime}."
        // );
        int timeSeconds = Convert.ToInt32(sleepTime * msPerSec);
        Console.WriteLine(
            $"Instance {id} is " +
            string.Format("waiting for {0,2} ms...", timeSeconds)
        );

        // previous = TimeSpan.FromTicks(Stopwatch.GetTimestamp());
        Thread.Sleep(timeSeconds);
        // Thread.Sleep(Convert.ToInt32(sleepTime));
        // current = TimeSpan.FromTicks(Stopwatch.GetTimestamp());

        // ShowDiffTimeSpan(previous, current);
        partyCompletesMission(id);
        Debug.WriteInstanceStatus(id, false);
        sleepTime = GetRandomTime();
    }

    current = Stopwatch.GetTimestamp();
    // Debug.WriteInstanceStatus(id, false);
    // ConsoleWriteLineThread(
    //     threadHashCode,
    //     nameInstance,
    //     "Done!"
    // );

    // Console.WriteLine($"previous (id {id}) = {current.TotalMilliseconds}");
    timeLog = WriteTimeStamp(current, previous);

    // Console.WriteLine($"Instance {id} locks stats_lock!");
    lock(stats_lock)
    {
        instanceLogs[Convert.ToInt32(id)] = 
            $"Instance {id}:\n"
            + $"\tTime served: {timeLog}\n"
            + $"\tNumber of parties served: {partyCreated}";
    }
    // Console.WriteLine($"Instance {id} unlocks stats_lock!");
}

ushort GetRandomTime()
{
    ushort time = 0;
    Random random = Random.Shared;
    time = (ushort)(random.Next() % (maxTime - minTime + 1) + minTime);

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

void ShowRemaining()
{
    Console.WriteLine("---Remaining---");
    Console.WriteLine($"tank: {tanks}");
    Console.WriteLine($"healer: {healer}");
    Console.WriteLine($"dps: {dps}");
}

void assignPartyToInstance(uint id)
{
    Console.WriteLine($"The party is assigned to instance {id}!");

    // Console.WriteLine($"Instances: {instanceRunning}/{maxInstances}");
}

void partyCompletesMission(uint id)
{
    Console.WriteLine($"The party completed instance {id}.");
}

bool seeIfAnyZero()
{
    // return ((tanks - 1) == 0) || 
    //     ((healer - 1) == 0) || 
    //     ((dps - 1) == 0);

    return (tanks == 0) || 
        (healer == 0) || 
        (dps == 0);
}

string WriteTimeStamp(long currentTick, long previousTick)
{
    return string.Format("{0:N2} ms", 
        Stopwatch.GetElapsedTime(previousTick, currentTick).TotalMilliseconds
    );
}

void ConfigInfo(Config config)
{
    Console.Write("\n");
    Console.WriteLine("Configuration summary:");
    Console.WriteLine(
        $"""
        Number of instances = {config.MaxInstances}
        Number of tanks = {config.Tanks}
        Number of healers = {config.Healer}
        Number of DPS's = {config.Dps}
        Minimum time in seconds for the instance to run = {config.MinTime}
        Maximum time in seconds for the instance to run = {config.MaxTime}
        """
    );
    Console.Write("\n");
}

List<string> CreateListOfEmptyStrs(int capacity)
{
    List<string> result = new(capacity);

    for (int i = 0; i < capacity; i++)
    {
        result.Add(String.Empty);
    }
    
    return result;
}
