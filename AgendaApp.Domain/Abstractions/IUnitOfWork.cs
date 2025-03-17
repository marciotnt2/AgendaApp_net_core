namespace AgendaApp.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepository { get; }
        Task CommitAsync();
    }
}
