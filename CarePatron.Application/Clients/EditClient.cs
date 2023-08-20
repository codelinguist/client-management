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
                //Validate the data. Otherwise, domain assertions are going to throw any invalid data.

                Client client = await repository.GetById(request.Id);

                if (client == null)
                {
                    return new Response { Success = false, ValidationErrors = new string[] { "Client does not exist." } };
                }

                var previousContactInfo = client.ContactInformation;
                client.EditInformation(request.FirstName, request.LastName, request.ContactInformation);

                await repository.Update(client);

                if (previousContactInfo.Email != client.ContactInformation.Email && client.ContactInformation.HasEmail)
                {
                    await emailService.Send(client.ContactInformation.Email!, "Hi there - welcome to my Carepatron portal.");
                    await documentService.SyncDocumentsFromExternalSource(client.ContactInformation.Email!);
                }
                return new Response { Success = true };
            }
        }
    }
}
