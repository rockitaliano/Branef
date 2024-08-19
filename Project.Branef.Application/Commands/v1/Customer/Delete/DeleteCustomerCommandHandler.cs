using MediatR;
using Microsoft.Extensions.Logging;
using Project.Branef.Application.Builders.v1.Customer;
using Project.Branef.Application.Commands.v1.Customer.Create;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Interfaces.Repositories.v1.Customer;

namespace Project.Branef.Application.Commands.v1.Customer.Delete
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ResponseBase>
    {
        private readonly ILogger _logger;
        private readonly ICustomerSqlRepository _customerRepository;
        private readonly ICustomerMongoRepository _customerMongoRepository;

        public DeleteCustomerCommandHandler(ILoggerFactory loggerFactory,
            ICustomerSqlRepository customerRepository,
            ICustomerMongoRepository customerMongoRepository)
        {
            _logger = loggerFactory.CreateLogger<CreateCustomerCommandHandler>();
            _customerRepository = customerRepository;
            _customerMongoRepository = customerMongoRepository;
        }

        public async Task<ResponseBase> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var requestValid = request.Validate();
                if (!requestValid)
                    return new ResponseBase(request.Notifications, false, "Id Customer Invalid");

                var customerDb = await _customerRepository.GetByIdAsync(request.Id);
                if (customerDb is null)
                    return new ResponseBase(null, false, "Customer Not Found");

                await _customerRepository.DeleteAsync(request.Id);
                await _customerMongoRepository.DeleteAsync(request.Id);

                return await Task.FromResult(new ResponseBase(null, true, "Customer deleted success"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteCustomerCommandHandler of the Customer {request.Id} | CorrelationId: {request.CorrelationId}");
                throw;
            }
        }
    }
}
