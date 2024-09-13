namespace Assembly.Horizon.Domain.Interface;

public interface IEntity<T>
{
    public T Id { get; set; }
}
