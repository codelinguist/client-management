using CarePatron.Domain.Model.ClientManagement;
using CarePatron.Domain.Persistence;

namespace CarePatron.Data
{
    public class DataSeeder
    {
        private readonly DataContext dataContext;

        public DataSeeder(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void Seed()
        {
            var clients = new List<Client>() {
                ClientFactory.CreateNewClient("John", "Smith", "john@gmail.com", "+18202820232"),
                ClientFactory.CreateNewVIPClient("John", "Stevens", "vip@gmail.com", "+18222920236"),
                ClientFactory.CreateNewClient("Steven", "Smith", "vip@gmail.com", "+18222920236"),
                ClientFactory.CreateNewVIPClient("Ziggy", "Hedgehog", "thehedgehog@gmail.com", "+18222426236"),
            };
            dataContext.AddRange(clients);

            dataContext.SaveChanges();
        }
    }
}

