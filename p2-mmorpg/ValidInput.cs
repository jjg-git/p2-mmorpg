public class ValidInput
{
    private static void ShowErrorParsing(string field)
    {
        Console.WriteLine(
            $"ERROR! "
            + $"{field} must be a positive number. "
            + "No letters or symbols are allowed."
        );
    }

    private static void ShowErrorNegative(string field)
    {
        Console.WriteLine(
            $"ERROR! "
            + $"{field} must be a positive number. "
        );
    }

    public static bool CheckTime(uint minTime, uint maxTime)
    {
        bool check = maxTime > minTime;
        if (!check)
        {
            Console.WriteLine(
                $"ERROR! "
                + "Max time and minimum time must not be the same! "
                + "Setting both to default value "
                + "(1 for minimum time, 15 for maximum time.)"
            );
        }
        return maxTime > minTime;
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
