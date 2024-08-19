using Project.Branef.Application.Commands.v1.Customer.Create;
using Project.Branef.Application.Commands.v1.Customer.Update;

namespace Project.Branef.Application.Builders.v1.Customer
{
    public static class CustomerBulider
    {
        public static Domain.Entities.v1.Customer CreateEntityCustomerForCreate(CreateCustomerCommand command)
        {
            return new Domain.Entities.v1.Customer( command.CompanyName, command.CompanyType.ToString());
        }

        public static Domain.Entities.v1.Customer CreateEntityCustomerForUpdate(UpdateCustomerCommand command, long id)
        {
            return new Domain.Entities.v1.Customer(id, command.CompanyName, command.CompanyType.ToString());
        }
    }
}
