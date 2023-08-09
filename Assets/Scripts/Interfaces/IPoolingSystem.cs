public interface IPoolingSystem
{
    int GetCurrentAmount();
    int GetAmount();
    void ReturnToPool(IPooledObject pooledObject);
}