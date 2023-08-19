using CarePatron.Domain.Model.ClientManagement;
using CarePatron.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.ClientManagement.Application
{
    public static class SetAsVIP
    {
        public record Command : IRequest<Response>
        {
            public required string Id { get; set; }
        }

        public record Response
        {
            public bool Success { get; set; }
            //Simple validation messages for now. 
            public string[] ValidationErrors { get; set; } = new string[0];
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IClientRepository repository;
            private readonly IDomainEventPublisher eventPublisher;

            public Handler(IClientRepository repository, IDomainEventPublisher eventPublisher)
            {
                this.repository = repository;
                this.eventPublisher = eventPublisher;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                //Validate the data. Otherwise, domain assertions are going to throw any invalid data.
                Client client = await repository.GetById(request.Id);

                var evt = client.SetAsVIP();

                await repository.Update(client);
                
                //publish the event
                await eventPublisher.Publish(evt);

                return new Response { Success = true };
            }
        }
    }
}
