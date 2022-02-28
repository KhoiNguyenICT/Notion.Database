namespace NotionCore.Infrastructure;

public interface IInfrastructure<out T>
{
    T Instance { get; }
}