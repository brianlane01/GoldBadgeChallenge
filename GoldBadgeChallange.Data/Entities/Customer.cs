namespace GoldBadgeChallange.Data.Entities
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(string name)
        {
            Name = name;
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }

         public List<Delivery> CustomerOrders { get; set; } = new List<Delivery>();
    }
}