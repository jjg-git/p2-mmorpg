// See https://aka.ms/new-console-template for more information

// mfw input validation
using Input;

uint maxInstances = 1;
uint tanks = 1;
uint healer = 1;
uint dps = 1;

maxInstances = (new Input.MaxInstances()).Parse(args[0]);

