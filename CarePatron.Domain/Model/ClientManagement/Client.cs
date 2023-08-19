using System;

namespace CarePatron.Domain.Model.ClientManagement
{
    public class Client
    {
        internal Client(string id, string firstName, string lastName, string email, string phoneNumber, bool isVIP)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            IsVIP = isVIP;
        }

        public string Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }
        public bool IsVIP { get; private set; }
    }
}

