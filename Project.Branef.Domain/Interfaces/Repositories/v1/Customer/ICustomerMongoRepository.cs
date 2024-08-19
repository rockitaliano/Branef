namespace Project.Branef.Domain.Interfaces.Repositories.v1.Customer
{
    public interface ICustomerMongoRepository
    {
        Task AddAsync(Entities.v1.Customer customer);
        Task<IEnumerable<Entities.v1.Customer>> GetAllAsync();
        Task UpdateAsync(Entities.v1.Customer customer);
        Task DeleteAsync(long id);
        Task<Entities.v1.Customer> GetByIdAsync(long id);
    }
}
