using MediatR;
using Microsoft.Extensions.Logging;
using Project.Branef.Application.Builders.v1.Customer;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;

namespace Project.Branef.Application.Commands.v1.Customer.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseBase>
    {
        private readonly ILogger _logger;
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly ICustomerMongoRepository _customerMongoRepository;

        public CreateCustomerCommandHandler(ILoggerFactory loggerFactory, 
            ICustomerSqlRepository customerRepository, 
            ICustomerMongoRepository customerMongoRepository)
        {
            _logger = loggerFactory.CreateLogger<CreateCustomerCommandHandler>();
            _customerRepository = customerRepository;
            _customerMongoRepository = customerMongoRepository;
        }

        public async Task<ResponseBase> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var requestValid = request.Validate();
                if (!requestValid)
                    return new ResponseBase(request.Notifications, false, "Customer Invalid");

                if (await _customerRepository.ExistsAsync(request.CompanyName))
                    return new ResponseBase(null, false, "CompanyName Exists");

                var entity = CustomerBulider.CreateEntityCustomerForCreate(request);

                var customer = await _customerRepository.AddAsync(entity);
                await _customerMongoRepository.AddAsync(customer);

                return await Task.FromResult(new ResponseBase(customer, true, "Customer created success"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in CreateCustomerCommandHandler of the Customer {request.CompanyName} | CorrelationId: {request.CorrelationId}");
                throw;
            }
        }
    }
}
