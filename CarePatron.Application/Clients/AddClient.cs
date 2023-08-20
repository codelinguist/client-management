using CarePatron.ClientManagement.Infrastructure;
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
    public static class AddClient
    {
        public record Command:IRequest<Response>
        {
            public required string FirstName { get; init; }
            public required string LastName { get; init; }
            public required ContactInformation ContactInformation { get; set; }
            public required bool IsVIP { get; set; }
        }

        public record ContactInformationDto
        {
            public string? Email { get; init; }
            public string? PhoneNumber { get; set; }
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
            private readonly IEmailService emailService;
            private readonly IDocumentService documentService;

            public Handler(IClientRepository repository, IEmailService emailService, IDocumentService documentService)
            {
                this.repository = repository;
                this.emailService = emailService;
                this.documentService = documentService;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                Client client;
                if (request.IsVIP)
                {
                    //Validate the data. Otherwise, domain assertions are going to throw any invalid data.

                    client = ClientFactory.CreateNewVIPClient(request.FirstName, request.LastName, request.ContactInformation);

                }
                else
                {
                    //Validate the data. Otherwise, domain assertions are going to throw any invalid data.

                    client = ClientFactory.CreateNewClient(request.FirstName, request.LastName, request.ContactInformation);
                }

                await repository.Create(client);

                //We may consider wrapping this in a reusable method since EditClient also needs to do this and this logic has business value.
                if (client.ContactInformation.HasEmail)
                {
                    await emailService.Send(client.ContactInformation.Email!, "Hi there - welcome to my Carepatron portal.");
                    await documentService.SyncDocumentsFromExternalSource(client.ContactInformation.Email!);
                }
                return new Response { Id = client.Id, Success = true };
            }
        }
    }
}
