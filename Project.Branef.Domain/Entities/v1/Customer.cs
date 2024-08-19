namespace Project.Branef.Domain.Entities.v1
{
    public class Customer
    {
        public long? Id { get; private set; }
        public string CompanyName { get; private set; }
        public string CompanyType { get; private set; }

        public Customer(long? id, string companyName, string companyType)
        {
            Id = id;
            CompanyName = companyName;
            CompanyType = companyType;
        }

        public Customer(string companyName, string companyType)
        {
            CompanyName = companyName;
            CompanyType = companyType;
        }

        public void SetIdentifier(long id)
        {
            Id = id;
        }
    }
}
