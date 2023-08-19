## How to run the application
In new terminal Go to /CarePatron.Api/
- dotnet restore
- dotnet build
- dotnet run

### Test the API
1. Find the file "CarePatron API Tests.postman_collection" in the root of the repository.
2. Open Postman and import the collection
2. Run the requests

## What could have I done further if I had more time?
First of all, I was only able to work on this on Monday and Saturday midnights because of my current working schedule, the PowerBI classes I'm taking and other committments I had during the week. If I had more time on this, I could have:
1. Implemented thorough unit and integration tests
2. API Test automations in Postman
3. Imlemented more scenarios to further demonstrate the pattern.
4. Provided diagrams and documentations to illustrate the architecture I applied. I should be able to provide these on the next (technical) interview.
5. Implemented better validation patterns, Envelope pattern and proper HTTP response codes according to the result.

# Answers to Extras
## Quality and best practices
Patterns and Practices Applied
1. [Vertical Slice Architecture](https://www.jimmybogard.com/vertical-slice-architecture/)
2. [Clean Architecture Layers](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
3. [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)

## Can this architecture easily scale to a codebase with 20 Developers?
Absolutely. With the Vertical Slice Architecture, the things that are used together, are placed together in one place. For instance, if one developer updates the EditClient functionality, he/she can ensure that other functionalities are NOT using or reusing any classes or methods that are in that use case. It's also very easy to find related things because you can find them just within the same file or namespace.

Therefore, there is high cohesion between use case implementations, which also translates to high cohesion between developers working on this project.

We can imagine 30 or more additional use cases to be implemented in this application, and the codebase should still hold. This could already be an *architecture smell* but that's a different discussion for another day.


# How can you ensure data integrity in case of failures?
This architecture achieves data integrity in the following aspects:

1. **Domain/Business Integrity**: The *Aggregate* and *Entity* patterns are great way for implementing rules and assertions for ensuring the integrity of an entity. In my example, the rule I imposed is that a client has to have an email address before he/she can be set as VIP. Because of the encapsulation in place, particularly the SetAsVIP, the Client can be set with IsVIP=true only if it has an EmailAddress, otherwise the operation will fail.

2. **Transactional Integrity**: Because we're using entity framework core, the SaveChangesAsync generates SQL commands and runs them in one transaction, ensuring ACID operation. The changes to an entity and its related entities should successfully persist, otherwise partially committed changes will be discarded.

## How can you ensure the API behaves as you intend it to?
I would write Unit/Integration and API Tests and setup a CI/CD pipeline to run the tests, prevent deployment in case of failure and report the test results.
