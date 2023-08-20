using CarePatron.Domain.Model.ClientManagement;

namespace CarePatron.Domain.Unit.Tests
{
    public class ClientTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateNewClient_Should_MapCorrectly()
        {
            var firstName = "Ralph Jourdan";
            var lastName = "Barro";
            var email = "codelinguist@gmail.com";
            var phone = "+639566826368";
            var client = ClientFactory.CreateNewClient(firstName, lastName, new ContactInformation(email, phone));
            Assert.IsFalse(string.IsNullOrEmpty(client.Id));
            Assert.AreEqual(firstName, client.FirstName);
            Assert.AreEqual(lastName, client.LastName);
            Assert.AreEqual(email, client.ContactInformation.Email);
            Assert.AreEqual(phone, client.ContactInformation.PhoneNumber);
            Assert.IsFalse(client.IsVIP);
        }


        [Test]
        public void CreateNewVIPClient_Should_MapCorrectly()
        {
            var firstName = "Ralph Jourdan";
            var lastName = "Barro";
            var email = "codelinguist@gmail.com";
            var phone = "+639566826368";
            var client = ClientFactory.CreateNewVIPClient(firstName, lastName, new ContactInformation(email, phone));
            Assert.IsFalse(string.IsNullOrEmpty(client.Id));
            Assert.AreEqual(firstName, client.FirstName);
            Assert.AreEqual(lastName, client.LastName);
            Assert.AreEqual(email, client.ContactInformation.Email);
            Assert.AreEqual(phone, client.ContactInformation.PhoneNumber);
            Assert.IsTrue(client.IsVIP);
        }
    }
}