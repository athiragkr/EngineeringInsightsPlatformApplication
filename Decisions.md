# Architectural Decisions

## Why GitHub?

GitHub was chosen because it provides public APIs and real repository data, allowing focus on insight generation rather than authentication and enterprise integration.

### Alternative
- Azure DevOps

### Trade-Off
GitHub provides easier access to repository data, while Azure DevOps provides deeper CI/CD integration.

---

## Why SQL Server?

Repository, ownership and risk information are highly relational.

### Benefits

- Simple querying
- Entity Framework Core integration
- Familiar enterprise database platform

### Alternatives

- Azure Cosmos DB
- Neo4j

### Trade-Off

SQL Server is simpler for the MVP, while graph databases become valuable when modelling complex dependencies.

---

## Why Entity Framework Core?

Entity Framework Core was selected to reduce development effort and improve maintainability.

### Alternative

- Raw SQL / ADO.NET

### Trade-Off

EF Core simplifies development while slightly reducing control over generated SQL.

---

## Why Blazor?

Blazor enables a single .NET technology stack across backend and frontend.

### Alternatives

- React
- Angular

### Trade-Off

Blazor simplifies development while React offers a larger frontend ecosystem.

---

## Why Rule-Based Risk Scoring?

The MVP uses explainable business rules.

### Benefits

- Easy to understand
- Easy to justify
- Transparent results


### Trade-Off

Rule-based scoring is simpler and more transparent but less adaptive.
