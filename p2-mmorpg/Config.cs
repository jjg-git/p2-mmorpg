public record Config(
    uint MaxInstances = DefaultConfig.MaxInstances,
    uint Tanks = DefaultConfig.Tanks,
    uint Healer = DefaultConfig.Healer,
    uint Dps = DefaultConfig.Dps,
    uint MinTime = DefaultConfig.MinTime,
    uint MaxTime = DefaultConfig.MaxTime,
);
