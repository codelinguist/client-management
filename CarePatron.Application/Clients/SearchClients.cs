using CarePatron.Domain.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.ClientManagement.Application
{
    public static class SearchClients
    {
        public record Query : IRequest<Response>
        {
            public string? SearchTerm { get; init; }
            //TODO: Implement PageSize and PageIndex Parameters.
        }

        public record Response
        {
            public required List<ClientViewModel> Clients { get; init; }
            //TODO: Implement pagination. We can create reusable helper classes and methods for producing this response structure
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IDataContext dataContext;

            public Handler(IDataContext dataContext)
            {
                this.dataContext = dataContext;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var searchTermNormalized = request.SearchTerm?.ToLower()??string.Empty;
                var clientsVm = await dataContext.Clients.Where(i=>
                        i.FirstName.ToLower().Contains(searchTermNormalized)
                        || i.LastName.ToLower().Contains(searchTermNormalized)
                    ).Select(i =>
                    new ClientViewModel
                    {
                        Id = i.Id,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        Email = i.Email,
                        PhoneNumber = i.PhoneNumber,
                        IsVIP=i.IsVIP
                    }).ToListAsync();

                return new Response { Clients = clientsVm };
            }
        }

        public class ClientViewModel
        {
            public required string Id { get; init; }
            public required string FirstName { get; init; }
            public required string LastName { get; init; }
            public string? Email { get; init; }
            public string? PhoneNumber { get; init; }
            public bool IsVIP { get; init; }
        }
    }
}