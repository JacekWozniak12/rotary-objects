using System;

public interface IHealthStateChanged
{
    Action<int> HealthChanged { get; }
    Action Death { get; }
}