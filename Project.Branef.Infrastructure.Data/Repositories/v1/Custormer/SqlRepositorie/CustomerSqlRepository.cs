using Dapper;
using Project.Branef.Domain.Entities.v1;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;

namespace Project.Branef.Infrastructure.Data.Repositories.v1.Custormer.SqlRepositorie
{
    public class CustomerSqlRepository : ICustomerSqlRepository
    {
        private readonly SQLDataContext _context;

        public CustomerSqlRepository(SQLDataContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            using (var db = _context.OpenConn())
            {
                var id = await db.ExecuteScalarAsync<long>(
                    @"INSERT INTO [tb_customer] (CompanyName, CompanyType) VALUES (@companyName, @companyType);
                    SELECT CAST(scope_identity() AS INT)",
                        new { @companyName = customer.CompanyName, @companyType = customer.CompanyType });

                customer.SetIdentifier(id);
                return customer;
            }
        }

        public async Task<bool> ExistsAsync(string companyName)
        {
            using (var db = _context.OpenConn())
            {
                var result = await db.ExecuteScalarAsync<bool>(
                    @"SELECT 1 FROM tb_customer WHERE @companyName = companyName",
                        new { @companyName = companyName });

                return await Task.FromResult(result);
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            using (var sqlConn = _context.OpenConn())
            {
                await sqlConn.ExecuteAsync(
                @"  UPDATE tb_customer
	                    SET CompanyName = @companyName,
                            CompanyType = @companyType
                    WHERE Id = @id",
                    new { @id = customer.Id, @companyName = customer.CompanyName, @companyType = customer.CompanyType });
            }
        }

        public async Task<Customer> GetByIdAsync(long id)
        {
            using (var sqlConn = _context.OpenConn())
            {
                var result = await sqlConn.QueryFirstOrDefaultAsync<Customer>(
                @"  SELECT 
	                    Id,
	                    CompanyName,
	                    CompanyType
                    FROM tb_customer 
                    WHERE Id = @id",
                    new { @id = id });

                return await Task.FromResult(result);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var sqlConn = _context.OpenConn())
            {
                var result = await sqlConn.QueryAsync<Customer>(
                @"SELECT 
                    Id
                    CompanyName, 
                    CompanyType
                 FROM tb_customer");
                return await Task.FromResult(result);
            }
        }

        public async Task DeleteAsync(long id)
        {
            using (var sqlConn = _context.OpenConn())
            {
                await sqlConn.ExecuteScalarAsync(
                @"DELETE tb_customer WHERE Id = @id",
                    new { @id = id });
            }
        }
    }
}
