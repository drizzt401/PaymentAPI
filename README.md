<h1 align="center">Process Payment API</h1>

## Description
Process Payment API using .NET Core Web API and Clean Architecture

## Bonus Requirements/Implementation
- Use repository/unit of work patterns

**Implementation**
<p>I created a Generic Repository/Unit of Work to keep track of changes and commit them.</p>

**Repository**

<img src="https://res.cloudinary.com/r3dmau5/image/upload/v1615643484/Repository.png"/>

**Unit of Work**

<img src="https://res.cloudinary.com/r3dmau5/image/upload/v1615643484/Repository.png"/>

- Use eager loading for all entities

**Implementation**
<p>I created an <code>Include()</code> method in my Repository that loads an entity with it's other related entities.</p>
<img src="https://res.cloudinary.com/r3dmau5/image/upload/v1615643925/IncludeRepo.png"/>
<img src="https://res.cloudinary.com/r3dmau5/image/upload/v1615643925/Includesnippet.png"/>

## Technologies used
<ul>
  <li>.NET Core Web API</li>
  <li>Polly</li>
  <li>MediatR ( for Command Query Responsibility Segregation (CQRS))</li>
  <li>AutoMapper</li>
  <li>Fluent Validation API</li>
  <li>Swagger UI</li>
</ul>

## Return Types
- A Cheap Payment returns a Payment Reference code that starts with **"CHEAP"**

- An Expensive Payment returns a Payment Reference code that starts with **"EXP"**
**<p><i> Expensive Payments can also return Payment References that start with "CHEAP" when the fallback policy is initiated as illustrated below</i></p>**
<p><i>Random Exception thrown</i></p>
<img src="https://res.cloudinary.com/r3dmau5/image/upload/v1615651818/exception.png"/>

<p><i>Request/Response</i></p>
<img src="https://res.cloudinary.com/r3dmau5/image/upload/v1615651819/Response.png"/>

- A Premium Payment returns a Payment Reference code that starts with **"PREM"**
## Setup
<ul>
<li>Uses Sql Server ( Install using [this link](https://go.microsoft.com/fwlink/?linkid=866662) ).</li>
  
<li>
Apply database migrations to create the db. From a command line within the PaymentAPI.Infrastructure project folder use the dotnet CLI to run : <code>PaymentAPI.Infrastructure > **dotnet ef database update**</code>
</li>
</ul>

# Visual Studio
- Simply open the solution file <code>PaymentAPI.sln</code> and build/run.
- Using the <code>https://</code> applicationUrl is recommended when appending the <code>/swagger</code> suffix to avoid CORS issues
