public record Config(
    ushort MaxInstances = DefaultConfig.MaxInstances,
    ushort Tanks = DefaultConfig.Tanks,
    ushort Healer = DefaultConfig.Healer,
    ushort Dps = DefaultConfig.Dps,
    ushort MinTime = DefaultConfig.MinTime,
    ushort MaxTime = DefaultConfig.MaxTime
)
{}
