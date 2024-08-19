using MongoDB.Driver;
using Project.Branef.Domain.Entities.v1;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;

namespace Project.Branef.Infrastructure.Data.Repositories.v1.Custormer.MongoRepositorie
{
    public class CustomerMongoRepository : ICustomerMongoRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerMongoRepository(IMongoDatabase database)
        {
            _customers = database.GetCollection<Customer>("Customers");
        }

        public async Task AddAsync(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customers.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _customers.ReplaceOneAsync(Builders<Customer>.Filter.Eq("_id", customer.Id), customer);
        }

        public async Task DeleteAsync(long id)
        {
            await _customers.DeleteOneAsync(Builders<Customer>.Filter.Eq("_id", id));
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            return await _customers.Find(Builders<Customer>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
        }
    }
}
