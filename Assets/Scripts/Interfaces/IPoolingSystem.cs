public interface IPoolingSystem
{
    int CurrentAmount { get; }
    int AllAmount { get; }
    void ReturnToPool(IPooledObject pooledObject);
}