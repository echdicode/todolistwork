namespace todolistwork.Application.Repository
{
    public interface IUnitOfWork
    {
        ITaskUserRepository TaskUsers { get; }
        IUserRepository UserRepository { get; }
     

    }
}
