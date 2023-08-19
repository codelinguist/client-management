using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.Domain.Model.ClientManagement
{
    public record ClientVIPStatusChanged
    {
        public required string Id { get; init; }
        public bool IsVIP { get; init; }
    }
}
