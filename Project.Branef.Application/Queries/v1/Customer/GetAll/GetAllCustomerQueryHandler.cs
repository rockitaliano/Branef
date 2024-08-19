using MediatR;
using Microsoft.Extensions.Logging;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;

namespace Project.Branef.Application.Queries.v1.Customer.GetAll
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, ResponseBase>
    {

        private readonly ILogger _logger;
        private readonly ICustomerMongoRepository _customerMongoRepository;

        public GetAllCustomerQueryHandler(ILoggerFactory loggerFactory, ICustomerMongoRepository customerMongoRepository)
        {
            _logger = loggerFactory.CreateLogger<GetAllCustomerQuery>();
            _customerMongoRepository = customerMongoRepository;
        }

        public async Task<ResponseBase> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerMongoRepository.GetAllAsync();
                
                return await Task.FromResult(new ResponseBase(customers, true, "Customer list"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when listing Customer | CorrelationId: {request.CorrelationId}");
                throw;
            }
        }
    }
}
