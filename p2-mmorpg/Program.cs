// See https://aka.ms/new-console-template for more information

// mfw input validation
if (args.Length != 0) {
    Console.WriteLine("IM NOT EMPTY");
    
    uint output = 69;
    try
    {
        output = uint.Parse(args[0]);
    }
    catch (Exception _) 
    {
        Console.WriteLine("BAD FORMATTING! Defaulting...")
    }

    Console.WriteLine($"output = {output}");
}
