using CarePatron.Domain.Model.ClientManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.ClientManagement.Application
{
    public static class AddClient
    {
        public record Command:IRequest<Response>
        {
            public required string FirstName { get; init; }
            public required string LastName { get; init; }
            public required string Email { get; init; }
            public required string PhoneNumber { get; set; }
            public required bool IsVIP { get; set; }
        }

        public record Response
        {
            public string? Id { get; init; }
            public bool Success { get; set; }
            //Simple validation messages for now. 
            public string[] ValidationErrors { get; set; } = new string[0];
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IClientRepository repository;

            public Handler(IClientRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                Client client;
                if (request.IsVIP)
                {
                    //Validate the data. Otherwise, domain assertions are going to throw any invalid data.

                    client = ClientFactory.CreateNewVIPClient(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

                }
                else
                {
                    //Validate the data. Otherwise, domain assertions are going to throw any invalid data.

                    client = ClientFactory.CreateNewClient(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
                }

                await repository.Create(client);
                return new Response { Id = client.Id, Success = true };
            }
        }
    }
}
