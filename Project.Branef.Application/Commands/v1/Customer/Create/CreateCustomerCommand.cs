using MediatR;
using Project.Branef.Application.Core;
using Project.Branef.Domain.Enums.Customer;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Project.Branef.Application.Commands.v1.Customer.Create
{
    public class CreateCustomerCommand : RequestBase, IRequest<ResponseBase>
    {
        public string CompanyName { get; set; }
        public CompanyType CompanyType { get; set; }
        

        public bool Validate()
        {
            List<string> notifications = new ();

            if (string.IsNullOrEmpty(CompanyName))
                notifications.Add("CompanyName Required");

            if (!Enum.IsDefined(typeof(CompanyType), CompanyType))
                notifications.Add("CompanyType Invalid");

            Notifications = notifications;

            return !Notifications.Any();
        }
    }
}
