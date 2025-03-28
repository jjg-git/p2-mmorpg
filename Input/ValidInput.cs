
namespace p2_mmorpg.Input
{
    public class ValidInput
    {
        private static void ShowErrorParsing(string field)
        {
            Error.ShowMessage(
                $"{field} must be a positive number. " +
                "No letters or symbols are allowed."
            );
        }

        private static void ShowErrorNegative(string field)
        {
            Error.ShowMessage($"{field} must be a positive nonzero number.");
        }

        public static bool CheckTime(uint minTime, uint maxTime)
        {
            bool check = maxTime > minTime;
            if (!check)
            {
                Error.ShowMessage(
                    "Max time and minimum time must not be the same! " +
                    "Setting both to default value " +
                    $"({DefaultConfig.MinTime} for minimum time, " + 
                    $"{DefaultConfig.MaxTime} for maximum time.)"
                );
            }
            return check;
        }

        public static bool CheckIfValid(string name, string arg)
        {
            if (!uint.TryParse(arg, out uint result))
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

        public static bool CheckIfNotZero(string name, uint input)
        {
            if (input == 0)
                ShowErrorZero(name);
            return input != 0;
        }

        private static void ShowErrorZero(string field)
        {
            Error.ShowMessage($"{field} must not be zero. ");
        }
    }
}
