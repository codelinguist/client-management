using CarePatron.Domain.Model.ClientManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.Infrastructure
{
    public interface IDomainEventPublisher
    {
        Task Publish(ClientVIPStatusChanged evt);
    }
    public class DomainEventPublisher : IDomainEventPublisher
    {
        public async Task Publish(ClientVIPStatusChanged evt)
        {
            //We may use any message broker here.
            //Other services/microservices may subscribe to this event.
            //Example: A service for processing client requests may expedite requests of a particular client that has set as VIP.
            await Task.Delay(2000);
        }
    }
}
