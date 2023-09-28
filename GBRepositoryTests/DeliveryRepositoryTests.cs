using Xunit;
using GoldBadgeChallange.Data.Entities;
using GoldBadgeChallange.Data.Entities.Enums;
using GoldBadgeChallange.Repository.Delivery_Repository;

namespace GBRepositoryTests
{
    public class DeliveryRepositoryTests
    {
        private DeliveryRepository _deliveryTestRepo;
        private readonly List<Delivery> _deliveryDbContext = new List<Delivery>();


        public DeliveryRepositoryTests()
        {
           _deliveryTestRepo = new DeliveryRepository(); 
        }
        [Fact]
        public void AddToDatabase_ShouldGetCorrectBoolean()
        {
            Delivery delivery = new Delivery();
            bool addResult = _deliveryTestRepo.AddDelivery(delivery);
            Assert.True(addResult);
        }

        [Fact]
        public void GetAllDeliveries_ShouldReturnAListOfDeliveries()
        {
        
            List<Delivery> retrievedDelivery = _deliveryTestRepo.GetAllDeliveries();

            int expectedCount = 8;
            int actual = retrievedDelivery.Count;

        
            Assert.Equal(expectedCount,actual);
        }

        [Fact]
        public void GetDeliveryByID_ShouldReturnADelivery()
        {
            
            Delivery returnedDelivery = _deliveryTestRepo.GetDeliveryById(2);
            Delivery expected = _deliveryTestRepo.GetAllDeliveries()[1];
            Delivery actual = returnedDelivery;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDeliveryByCustomerId_ShouldReturnADelivery()
        {
            Delivery newReturnedDelivery = _deliveryTestRepo.GetDeliveriesByCustomerId(1005);
            Delivery expected = _deliveryTestRepo.GetAllDeliveries()[5];
            Delivery actual = newReturnedDelivery;
            Assert.Equal(expected, actual);
        }

    }
}

