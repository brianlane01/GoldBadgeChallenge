using GoldBadgeChallange.Data.Entities;
using GoldBadgeChallange.Repository.Customer_Repository;
using GoldBadgeChallange.Data.Entities.Enums;

namespace GoldBadgeChallange.Repository.Delivery_Repository
{
    public class DeliveryRepository
    {
        private readonly List<Delivery> _deliveryDbContext = new List<Delivery>();

        private readonly List<Customer> _customerDbContext = new List<Customer>();

        private readonly CustomerRepository _customerRepo = new CustomerRepository();

        private Random random = new Random();

        private int _count = 0;

        public DeliveryRepository()
        {
            SeedDelivery();
        }

        private void AssignId(Delivery delivery)
        {
            _count++;
            delivery.DeliveryId = _count;
        }

        public bool AddDelivery(Delivery delivery)
        {
            if (delivery is null)
            {
                return false;
            }
            else
            {
                SaveDeliveryToDatabase(delivery);
                return true;
            }
        }

        private bool SaveDeliveryToDatabase(Delivery delivery)
        {
            AssignId(delivery);
            _deliveryDbContext.Add(delivery);
            return true;
        }

        public bool DeleteADelivery(Delivery delivery)
        {
            bool deleteResult = _deliveryDbContext.Remove(delivery);
            return deleteResult;
        }

        public Delivery GetDeliveryById(int id)
        {
            return _deliveryDbContext.SingleOrDefault(x => x.DeliveryId == id)!;
        }

        public List<Delivery> GetAllDeliveries()
        {
            return _deliveryDbContext;
        }

        public bool UpdateADelivery(int deliveryId, Delivery newDelivery)
        {
            Delivery deliveryInDatabase = GetDeliveryById(deliveryId);
            if (deliveryInDatabase is not null)
            {
                deliveryInDatabase.DeliveryDate = newDelivery.DeliveryDate;
                deliveryInDatabase.OrderDate = newDelivery.OrderDate;
                deliveryInDatabase.OrderStatus = newDelivery.OrderStatus;
                deliveryInDatabase.ItemNumber = newDelivery.ItemNumber;
                deliveryInDatabase.ItemQuantity = newDelivery.ItemQuantity;
                return true;

            }
            return false;
        }

        public Delivery GetDeliveriesByCustomerId(int customerId)
        {
            return _deliveryDbContext.FirstOrDefault(order => order.CustomerId == customerId);
        }

        public List<Delivery> GetDeliveriesByOrderSatus(OrderStatus orderStatus)
        {
            return _deliveryDbContext.Where(order => order.OrderStatus == orderStatus).ToList();
        }

        private void SeedDelivery()
        {
            var delivery1 = new Delivery
            {
                OrderDate = new DateTime(2023, 05, 31, 5, 10, 20),
                DeliveryDate = new DateTime(2023, 06, 03, 12, 0, 0),
                ItemNumber = "BKL34567",
                ItemQuantity = 55,
                OrderStatus = OrderStatus.Canceled,
                CustomerId = 1001
            };

            var delivery2 = new Delivery
            {
                OrderDate = new DateTime(2023, 06, 01, 5, 10, 20),
                DeliveryDate = new DateTime(2023, 06, 03, 12, 0, 0),
                ItemNumber = "BKL34897",
                ItemQuantity = 78,
                OrderStatus = OrderStatus.Complete,
                CustomerId = 1002
            };

            var delivery3 = new Delivery
            {
                OrderDate = new DateTime(2023, 06, 02, 15, 25, 20),
                DeliveryDate = new DateTime(2023, 06, 04, 20, 0, 0),
                ItemNumber = "BKL37617",
                ItemQuantity = 15,
                OrderStatus = OrderStatus.Complete,
                CustomerId = 1003
            };

            var delivery4 = new Delivery
            {
                OrderDate = new DateTime(2023, 09, 25, 14, 10, 20),
                DeliveryDate = new DateTime(2023, 09, 27, 20, 0, 0),
                ItemNumber = "BKL37347",
                ItemQuantity = 15,
                OrderStatus = OrderStatus.Complete,
                CustomerId = 1003
            };

            var delivery5 = new Delivery
            {
                OrderDate = new DateTime(2023, 09, 25, 14, 10, 20),
                DeliveryDate = new DateTime(2023, 09, 27, 20, 0, 0),
                ItemNumber = "BKL37347",
                ItemQuantity = 38,
                OrderStatus = OrderStatus.Scheduled,
                CustomerId = 1004
            };

            var delivery6 = new Delivery
            {
                OrderDate = new DateTime(2023, 09, 25, 14, 14, 20),
                DeliveryDate = new DateTime(2023, 09, 27, 20, 0, 0),
                ItemNumber = "BKL30007",
                ItemQuantity = 49,
                OrderStatus = OrderStatus.EnRoute,
                CustomerId = 1005
            };

            var delivery7 = new Delivery
            {
                OrderDate = new DateTime(2023, 09, 25, 14, 20, 20),
                DeliveryDate = new DateTime(2023, 09, 27, 20, 0, 0),
                ItemNumber = "BKL30907",
                ItemQuantity = 49,
                OrderStatus = OrderStatus.EnRoute,
                CustomerId = 1003
            };

            var delivery8 = new Delivery
            {
                OrderDate = new DateTime(2023, 09, 23, 14, 20, 20),
                DeliveryDate = new DateTime(2023, 09, 26, 20, 0, 0),
                ItemNumber = "BKL31007",
                ItemQuantity = 49,
                OrderStatus = OrderStatus.Canceled,
                CustomerId = 1001
            };

            AddDelivery(delivery1);
            AddDelivery(delivery2);
            AddDelivery(delivery3);
            AddDelivery(delivery4);
            AddDelivery(delivery5);
            AddDelivery(delivery6);
            AddDelivery(delivery7);
            AddDelivery(delivery8);
        }
    }
}