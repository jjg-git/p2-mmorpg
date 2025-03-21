// See https://aka.ms/new-console-template for more information
class RuntimeInput : InputMethod
{
    public override void Invoke()
    {
        Console.Write($"Number of instances? ");
        string? input = Console.ReadLine();

        Console.WriteLine(input == "" ? "im empty" : (input ?? "im null"));
    }
}

