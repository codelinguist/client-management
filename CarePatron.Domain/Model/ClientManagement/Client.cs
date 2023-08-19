﻿using System;

namespace CarePatron.Domain.Model.ClientManagement
{
    //This is an Aggregate Entity: Ref: https://www.martinfowler.com/bliki/DDD_Aggregate.html
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

        public void EditInformation(string firstName, string lastName, string email, string phoneNumber)
        {
            //TODO: We can have reusable assertion methods for Client Class so we don't have to rewrite.
            //Domain Assertions: https://opus.ch/ddd-concepts-and-patterns-supple-design/
            ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));

            if (IsVIP)
            {
                //Domain requirement: Let's say we require email address for VIP clients
                //TODO: Assert valid email
                ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));
            }

            //TODO: Detect changes by comparing the params against current values.
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            //TODO: return DomainEvent ClientInformationChanged to be published if needed. Ref: https://www.martinfowler.com/eaaDev/DomainEvent.html
        }

        public ClientVIPStatusChanged SetAsVIP()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidOperationException("Cannot set VIP. Client has no email address.");   
            }
            if (IsVIP)
            {
                return null;
            }

            IsVIP = true;
            return new ClientVIPStatusChanged { Id = Id, IsVIP = true };
        }

        public ClientVIPStatusChanged RemoveVIP()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new InvalidOperationException("Cannot set VIP. Client has no email address.");
            }
            if (!IsVIP)
            {
                return null!;
            }

            IsVIP = false;
            return new ClientVIPStatusChanged { Id = Id, IsVIP = true };
        }


        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool IsVIP { get; private set; }
    }
}

