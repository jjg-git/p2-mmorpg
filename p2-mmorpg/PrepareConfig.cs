
class PrepareConfig
{
    
    // Choose between args input and input prompt
    public Config Execute(InputMethod method)
    {
        return method.Execute();
    }
}
