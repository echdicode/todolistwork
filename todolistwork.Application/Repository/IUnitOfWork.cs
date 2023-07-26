namespace todolistwork.Application.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        IContactRepository Contacts { get; }
        IAdminUserRepository AdminUsers { get; }
        ITaskUserRepository TaskUsers { get; }
        IUserAccountsRepository UserAccounts { get; }
        IUserRepository UserAccountsRepository { get; }


    }
}
