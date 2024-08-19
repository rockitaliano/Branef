namespace Project.Branef.Domain.Interfaces.Repositories.v1.Customer
{
    public interface ICustomerSqlRepository
    {
        Task<Entities.v1.Customer> AddAsync(Entities.v1.Customer customer);
        Task<bool> ExistsAsync(string companyName);
        Task UpdateAsync(Entities.v1.Customer customer);
        Task<Entities.v1.Customer> GetByIdAsync(long id);
        Task<IEnumerable<Entities.v1.Customer>> GetAllAsync();
        Task DeleteAsync(long id);
    }
}
