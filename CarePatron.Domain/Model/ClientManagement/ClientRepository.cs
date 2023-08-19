using CarePatron.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarePatron.Domain.Model.ClientManagement
{
    public interface IClientRepository
    {

        Task<Client> GetById(string id);
        Task Create(Client client);
        Task Update(Client client);

    }

    public class ClientRepository : IClientRepository
    {
        private readonly IDataContext dataContext;

        public ClientRepository(IDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task Create(Client client)
        {
            dataContext.Clients.Add(client);
            await dataContext.SaveChangesAsync();
        }

        public Task<Client> GetById(string id)
        {
            return dataContext.Clients.FirstAsync(c => c.Id == id);
        }

        public async Task Update(Client client)
        {
            dataContext.Clients.Update(client);
            await dataContext.SaveChangesAsync();
        }

        /*
        private readonly DataContext dataContext;
        private readonly IEmailRepository emailRepository;
        private readonly IDocumentRepository documentRepository;

        public ClientRepository(DataContext dataContext, IEmailRepository emailRepository, IDocumentRepository documentRepository)
        {
            this.dataContext = dataContext;
            this.emailRepository = emailRepository;
            this.documentRepository = documentRepository;
        }

        public async Task Create(Client client)
        {
            await dataContext.AddAsync(client);
            await dataContext.SaveChangesAsync();

            await emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
            await documentRepository.SyncDocumentsFromExternalSource(client.Email);
        }

        public Task<Client[]> Get()
        {
            return dataContext.Clients.ToArrayAsync();
        }

        public async Task Update(Client client)
        {
            var existingClient = await dataContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

            if (existingClient == null)
                return;

            if (existingClient.Email != client.Email)
            {
                await emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
                await documentRepository.SyncDocumentsFromExternalSource(client.Email);
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            await dataContext.SaveChangesAsync();
        }*/
    }
}

