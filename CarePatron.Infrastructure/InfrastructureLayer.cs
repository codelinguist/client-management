using CarePatron.ClientManagement.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.Infrastructure
{
    public static class InfrastructureLayer
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
        }
    }
}
