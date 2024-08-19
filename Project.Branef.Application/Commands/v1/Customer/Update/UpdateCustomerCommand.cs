using MediatR;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Enums.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Branef.Application.Commands.v1.Customer.Update
{
    public class UpdateCustomerCommand : RequestBase, IRequest<ResponseBase>
    {
        public long Id { get; set; }
        public string CompanyName { get; set; }
        public CompanyType CompanyType { get; set; }

        public bool Validate()
        {
            List<string> notifications = new();

            if (Id == 0)
                notifications.Add("Customer Id Required");

            if (string.IsNullOrEmpty(CompanyName))
                notifications.Add("CompanyName Required");

            if (!Enum.IsDefined(typeof(CompanyType), CompanyType))
                notifications.Add("CompanyType Invalid");

            Notifications = notifications;

            return !Notifications.Any();
        }
    }
}
