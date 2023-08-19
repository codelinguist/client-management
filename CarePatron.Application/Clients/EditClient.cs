using CarePatron.Domain.Model.ClientManagement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.ClientManagement.Application
{
    public static class EditClient
    {
        public record Command : IRequest<Response>
        {
            public required string Id { get; set; }
            public required string FirstName { get; init; }
            public required string LastName { get; init; }
            public required ContactInformation ContactInformation { get; set; }
        }

        //TODO: Since this is also defined in AddClient, we may move this to a 'Common' namespace to be reusable. 
        public record ContactInformationDto
        {
            public string? Email { get; init; }
            public string? PhoneNumber { get; set; }
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

            public Handler(IClientRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                //Validate the data. Otherwise, domain assertions are going to throw any invalid data.

                Client client = await repository.GetById(request.Id);

                client.EditInformation(request.FirstName, request.LastName, request.ContactInformation);

                await repository.Update(client);
                return new Response { Success = true };
            }
        }
    }
}
