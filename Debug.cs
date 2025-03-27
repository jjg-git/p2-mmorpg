class Debug
{
    public static string NameThread(int threadHashCode, string nameInstance)
    {
        return $"Thread {nameInstance} {threadHashCode}: ";
    }

    public static void ConsoleWriteLineThread(
            int threadHashCode,
            string nameInstance,
            string caption
            )
    {
        Console.WriteLine(
            NameThread(threadHashCode, nameInstance) + caption
        );
    }

    public static void WriteThreadStatus(
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

    public static void WriteInstanceStatus(
            uint id,
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

        Console.WriteLine(
            $"Instance {id}'s status: {statusCaption}."
        );
    }
}
