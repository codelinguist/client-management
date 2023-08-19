using CarePatron.Domain.Model.ClientManagement;
using CarePatron.Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.Domain
{
    public static class DomainLayer
    {
        public static void AddDomainLayer(this IServiceCollection services)
        {
            services.AddScoped<IDataContext>(serviceProvider => serviceProvider.GetRequiredService<DataContext>());
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
