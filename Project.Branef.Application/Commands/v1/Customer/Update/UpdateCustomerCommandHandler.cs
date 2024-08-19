using MediatR;
using Microsoft.Extensions.Logging;
using Project.Branef.Application.Builders.v1.Customer;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;

namespace Project.Branef.Application.Commands.v1.Customer.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ResponseBase>
    {
        private readonly ILogger _logger;
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly ICustomerMongoRepository _customerMongoRepository;

        public UpdateCustomerCommandHandler(ILoggerFactory loggerFactory, 
            ICustomerSqlRepository customerRepository,
            ICustomerMongoRepository customerMongoRepository)
        {
            _logger = loggerFactory.CreateLogger<UpdateCustomerCommandHandler>();
            _customerRepository = customerRepository;
            _customerMongoRepository = customerMongoRepository;
        }

        public async Task<ResponseBase> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var requestValid = request.Validate();
                if (!requestValid)
                    return new ResponseBase(request.Notifications, false, "Customer Invalid");

                if (await _customerRepository.ExistsAsync(request.CompanyName))
                    return new ResponseBase(null, false, "CompanyName Exists");

                var customerDb = await _customerRepository.GetByIdAsync(request.Id);
                if (customerDb is null)
                    return new ResponseBase(null, false, "Customer Not Found");

                var entity = CustomerBulider.CreateEntityCustomerForUpdate(request, customerDb.Id.Value);

                await _customerRepository.UpdateAsync(entity);
                await _customerMongoRepository.UpdateAsync(entity);

                return await Task.FromResult(new ResponseBase(null, true, "Customer, updated success"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in UpdateCustomerCommandHandler of the Customer {request.CompanyName} | CorrelationId: {request.CorrelationId}");
                throw;
            }
        }
    }
}
