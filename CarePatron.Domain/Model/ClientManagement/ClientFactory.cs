using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.Domain.Model.ClientManagement
{
    public static class ClientFactory
    {
        public static Client CreateNewClient(string firstName, string lastName, string email, string phoneNumber)
        {
            //Domain Assertions: https://opus.ch/ddd-concepts-and-patterns-supple-design/
            ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));
            //TODO: Assert valid email

            var id = Guid.NewGuid().ToString();
            return new Client(id, firstName, lastName, email, phoneNumber, false);
        }


        public static Client CreateNewVIPClient(string firstName, string lastName, string email, string phoneNumber)
        {
            //Domain Assertions: https://opus.ch/ddd-concepts-and-patterns-supple-design/
            ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));
            //Domain requirement: Let's say we require email address for VIP clients
            //TODO: Assert valid email
            ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

            var id = Guid.NewGuid().ToString();
            return new Client(id, firstName, lastName, email, phoneNumber, true);
        }
    }
}
