public interface IPoolingSystem
{
    int CurrentAmount { get; }
    void ReturnToPool(IPooledObject pooledObject);
}