abstract public class InputMethod (
)
{
    protected uint MaxInstances = 1;
    protected uint Tanks = 1;
    protected uint Healer = 1;
    protected uint Dps = 3;
    protected uint MinTime = 1;
    protected uint MaxTime = 15;

    public abstract void Invoke();
    public Config Execute()
    {
        Invoke();
        ResetTime();

        Config result = new(
            MaxInstances,
            Tanks, 
            Healer, 
            Dps, 
            MinTime, 
            MaxTime
        );

        return result;
    }

    private void ResetTime()
    {
        if (!ValidInput.CheckTime(MinTime, MaxTime))
        {
            MinTime = DefaultConfig.MinTime;
            MaxTime = DefaultConfig.MaxTime;
        }
    }
}
