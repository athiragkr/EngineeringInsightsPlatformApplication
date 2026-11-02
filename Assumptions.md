# Assumptions

## Data Sources

- Repository metadata is collected from the GitHub REST API.
- Repository ownership information is mocked for the MVP using a local mapping file.
- Repository ownership would typically come from an internal engineering catalog, CMDB, Backstage catalog, or Azure DevOps metadata service.

## Risk Assessment

- Risk thresholds are implemented as configurable business rules.
- Repository inactivity is determined using the last activity date.
- Contributor coverage is simplified for the MVP.

## Scope

- The MVP focuses on repository ownership and repository health.
- Workflow reliability, dependency analysis, security governance, and access management are outside the MVP scope.

## Environment

- SQL Server is used for persistence.
- Blazor is used for the dashboard.
- Entity Framework Core is used for data access.
