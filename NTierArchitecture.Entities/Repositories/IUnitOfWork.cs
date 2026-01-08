namespace NTierArchitecture.Entities.Repositories
{
    //Unit of Work Pattern mantığı ile kullanım (Transaction yönetimi için)

    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
