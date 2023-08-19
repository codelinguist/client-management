using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarePatron.Domain.Model.ClientManagement
{
    //This is a Value Object. Ref: https://deviq.com/domain-driven-design/value-object
    public class ContactInformation
    {
        public string? Email { get; private set; }
        public string? PhoneNumber { get; private set; }
        public ContactInformation(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
