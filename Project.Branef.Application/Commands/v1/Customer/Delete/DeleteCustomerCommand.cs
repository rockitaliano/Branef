using MediatR;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Enums.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Branef.Application.Commands.v1.Customer.Delete
{
    public class DeleteCustomerCommand : RequestBase, IRequest<ResponseBase>
    {
        public long Id { get; set; }

        public bool Validate()
        {
            List<string> notifications = new();

            if (Id == 0)
                notifications.Add("Id Customer Required");

            Notifications = notifications;

            return !Notifications.Any();
        }
    }
}
