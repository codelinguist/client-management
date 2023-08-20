[TOC]

# Run the Application
In new terminal Go to /CarePatron.Api/
- dotnet restore
- dotnet build
- dotnet run

### Test the API
1. Find the file "CarePatron API Tests.postman_collection" in the root of the repository.
2. Open Postman and import the collection
2. Run the requests

# Responses

### How long did it take? 
I was only able to work on this on Monday and Saturday midnights because of my current working schedule, the PowerBI classes I'm taking and other committments I had during the week. It's approximately 12hrs which include the architecture review, coding and this writeup.

### What could have I done further if I had more time?

If I had more time on this, I could have:
1. Implemented thorough unit and integration tests
2. API Test automations in Postman
3. Imlemented more scenarios to further demonstrate the pattern.
4. Provided diagrams and documentations to illustrate the architecture I applied. I should be able to provide these on the next (technical) interview.
5. Implemented better validation patterns, Envelope pattern and proper HTTP response codes according to the result.

### Quality and best practices
Patterns and Practices Applied
1. [Vertical Slice Architecture](https://www.jimmybogard.com/vertical-slice-architecture/)
2. [Clean Architecture Layers](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
3. [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)

### Can this architecture easily scale to a codebase with 20 Developers?
Absolutely. With the Vertical Slice Architecture, the things that are used together, are placed together in one place. For instance, if one developer updates the EditClient functionality, he/she can ensure that other functionalities are NOT using or reusing any classes or methods that are in that use case. It's also very easy to find related things because you can find them just within the same file or namespace.

Therefore, there is high cohesion between use case implementations, which also translates to high cohesion between developers working on this project.

We can imagine 30 or more additional use cases to be implemented in this application, and the codebase should still hold. This could already be an architectural or product *design  smell* but that's a different discussion for another day.


### How can you ensure data integrity in case of failures?
This architecture achieves data integrity in the following aspects:

1. **Domain/Business Integrity**: The *Aggregate* and *Entity* patterns are great patterns for implementing business rules and assertions for ensuring the integrity of an entity. In my example, the rule I imposed is that a client has to have an email address before he/she can be set as VIP. Because of the encapsulation in place, particularly the SetAsVIP, the Client can be set with IsVIP=true only if it has an EmailAddress, otherwise the operation will fail.

2. **Transactional Integrity**: Because we're using entity framework core, the SaveChangesAsync generates SQL commands and runs them in one transaction, ensuring ACID operation. The changes to an entity and its related entities should successfully persist, otherwise partially successful changes will be discarded.

### How can you ensure the API behaves as you intend it to?
I would write Unit/Integration and API Tests and setup a CI/CD pipeline to run the tests, prevent deployment in case of failure and report the test results.

# Architecture

### The Narrative
- We developers prioritize reusability and smaller lines of code at the expense of readability, traceability and long-term maintainability. We fail to recognize that there are certain layers in our application where the latter three qualities are paramount.
- When developing APIs, we sometimes fail to recognize that there should be a clear distinction between APIs built for frontend and APIs built for public consumption. This results into API endpoints that have multiple conflicting objectives and exposing unnecessary data points.

### Architectural Vision
- A highly functionally cohesive application where areas in the application have minimal to no direct or indirect dependencies from each other.
- An architecture with *well-managed complexity* such that we recognize the *bounded contexts* wherein capabilities and its boundaries are well-defined.
- An API with a clear and specific purpose which is to serve the frontend functionalities.

## The layers
[Clean Architecture screenshot here]

### Domain Layer

[Screenshot here]

- This is where domain logic lives.
- Where logic reusability is paramount.
- Has the least dependencies to third-party libraries.
- Has no dependencies to other projects
- Tactical Patterns Applied
    - **Entities and Aggregate Entities** - uniquely identifiable objects.
    - **Value Objects** - used for its values, not for its uniqueness; disposable and replaceable; no sense of continuity, unlike entities.
    - **Domain Events** - 
    - **Repositories** - an abstraction of entity's persistence and retrieval.
- Strategic Pattern Applied
    - Bounded Context


### Infrastructure Layer
- This is where external service interfaces, helpers, subscribers and publishers are defined.
- Is aware of domain layer

### Application Layer
- Is aware of both Domain and Infrastructure Layers
- Manages the application logic
    - invokes the creation, persistence and/or retrieval of entities
    - invokes entity operation (EditContactInfo, SetAsVIP, etc)
    - invokes publishing of domain events resulting from entity operation.
    - handles logging
    - resolves needed request context data.
    - returns the expected response
- This is where **readability**, **traceability** and long-term **maintainability** are more important than reusability, hear me out:
    - Between the use cases, you may see DTOs or ViewModels that have common properties. This does not mean you need to create a base class that will define all of the common properties for them to inherit. Unless there is a business value in doing so, keep those use cases separate.
    - Another example, you should not reuse an api to get clients for Clients grid in a Clients dropdown functionality.
    - I can explain how this would lead to bad development experience sooner than we think.
    - **Basically, not all things that look similar are the same.**
- With [Vertical Slice Architecture](https://www.jimmybogard.com/vertical-slice-architecture/), the application operations are organized by FRONTEND use cases. In this sample app,
    - Clients Page:
        -  SearchClients
        -  AddClient
        -  EditClient
        -  SetAsVIP
    -  Contacts Page
        - SearchContacts
        - AddContact
        - EditContact
        - DeleteContact
    - Your Team Page
        - SearchMembers
        - AddMember
        - RemoveMember
