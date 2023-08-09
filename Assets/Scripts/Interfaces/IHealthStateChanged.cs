using System;

public interface IHealthStateChanged
{
    event Action<int> HealthChanged { get; }
    event Action Death { get; }
}