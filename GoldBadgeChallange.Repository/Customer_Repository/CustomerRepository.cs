using GoldBadgeChallange.Data.Entities;

namespace GoldBadgeChallange.Repository.Customer_Repository
{
    public class CustomerRepository
    {
        public CustomerRepository()
        {
            SeedCustomer();
        }

        private readonly List<Customer> _customerDbContext = new List<Customer>();

        

        private int newCustomerId = 1000;

        private int maxNewCustomerId = 100000; 


        public bool AddCustomer(Customer customer)
        {
            if (customer is null)
            {
                return false;
            }
            else
            {
                newCustomerId++; 
                customer.CustomerId = newCustomerId;
                _customerDbContext.Add(customer);
                return true;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerDbContext;
        }

        public Customer GetCustomerById(int id)
        {
            return _customerDbContext.SingleOrDefault(x => x.CustomerId == id)!;
        }

        public bool UpdateCustomer(int customerId, Customer newCustomerData)
        {
            Customer customerInDb = GetCustomerById(customerId);
            if (customerInDb is not null)
            {
                customerInDb!.Name = newCustomerData.Name;
                customerInDb.CustomerOrders = (newCustomerData.CustomerOrders.Count() > 0) ?
                                            newCustomerData.CustomerOrders :
                                            customerInDb.CustomerOrders;

                return true;
            }

            return false;
        }

        public bool DeleteCustomer(Customer customer)
        {
            return _customerDbContext.Remove(customer);
        }

        public bool AddDeliveryToCustomer(int customerId, Delivery delivery)
        {
            if (customerId > 0 && delivery != null)
            {
                Customer customer = GetCustomerById(customerId);
                if (customer != null)
                {
                    delivery.CustomerId = customer.CustomerId;
                    customer.CustomerOrders.Add(delivery);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveDeliveryFromCustomer(int customerId, Delivery delivery)
        {
            if (customerId > 0 && delivery != null)
            {
                Customer customer = GetCustomerById(customerId);
                if (customer != null)
                    return customer.CustomerOrders.Remove(delivery);

            }
            return false;
        }

        private void SeedCustomer()
        {
            Customer cutomerA = new Customer("Wayne Enterprises");
            Customer cutomerB = new Customer("LexCorp");
            Customer cutomerC = new Customer("Queen Consolidated");
            Customer cutomerD = new Customer("Ace Chemicals");
            Customer cutomerE = new Customer("Palmer Technologies");
            Customer cutomerF = new Customer("CheckMate");

            AddCustomer(cutomerF);
            AddCustomer(cutomerA);
            AddCustomer(cutomerB);
            AddCustomer(cutomerC);
            AddCustomer(cutomerD);
            AddCustomer(cutomerE);
        }

    }
}