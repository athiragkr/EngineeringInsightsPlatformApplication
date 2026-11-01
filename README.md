**Engineering Insights Platform**

**Overview**

The Engineering Insights Platform provides visibility into repository ownership, maintenance status, and operational risk across an engineering organization.

The application collects repository metadata from GitHub, enriches it with ownership information, calculates repository health risks, and presents actionable insights through a dashboard.

**Problem Interpretation**

Engineering organizations often manage hundreds of repositories distributed across teams.

While source control platforms provide repository data, engineering leaders still struggle to answer questions such as:

- Which repositories are actively maintained?
- Which repositories have no clear owner?
- Which repositories are becoming operational risks?
- Which repositories require intervention?

This MVP addresses repository ownership and maintenance visibility by transforming repository metadata into actionable insights.

**Selected Scope**

**Repository Ownership & Health Analysis**

The MVP focuses on:

- Repository inventory
- Ownership visibility
- Maintenance activity
- Risk identification
- Repository health reporting

**Why this scope?**

The assignment encourages selecting a single meaningful slice and exploring it in depth.

Repository ownership and health provide immediate engineering governance value and can be implemented using real GitHub data while remaining achievable within the project timeframe.

**Architecture**

See:docs/Architecture.png

**High-Level Architecture**

GitHub API

|

v

GitHub Service

|

v

Repository Import Service

|

v

SQL Server

|

v

Risk Assessment Engine

|

v

ASP.NET Core API

|

v

Blazor Dashboard

**Data Flow**

See:docs/DataFlow.png

**Flow**

GitHub Repository Data

|

v

Repository Import

|

v

Ownership Enrichment

|

v

Database Storage

|

v

Risk Assessment

|

v

API Layer

|

v

Dashboard

**Technology Stack**

**Backend**

- ASP.NET Core Web API
- Entity Framework Core

**Frontend**

- Blazor Server

**Database**

- SQL Server

**External Integration**

- GitHub REST API

**Testing**

- xUnit

**Data Sources**

**Real Data**

The following information is collected directly from GitHub:

- Repository Name
- Repository Metadata
- Last Activity Date

**Mocked Data**

For the MVP, repository ownership information is mocked using a local ownership mapping file.

Example:

JSON

{

"repositoryName": "aspnetcore",

"ownerTeam": "Web Team"

}

In a production implementation, ownership information would originate from an internal engineering catalog, CMDB, Backstage, Azure DevOps, or platform governance system.

**Insight Generation**

Repository risk is not provided by GitHub.

The platform derives risk by analysing repository activity and ownership information.

**Risk Rules**

| **Condition**                     | **Score** |
| --------------------------------- | --------- |
| No activity for more than 90 days | +3        |
| Less than 2 contributors          | +2        |
| No owner assigned                 | +2        |

**Risk Levels**

| **Score** | **Risk Level** |
| --------- | -------------- |
| 0 - 2     | Low            |
| 3 - 4     | Medium         |
| 5+        | High           |

**Example**

Repository: legacy-api

&nbsp;

No activity for 180 days

Only one contributor

No owner assigned

&nbsp;

Risk Score: 7

Risk Level: High

**Features**

**Repository Inventory**

Provides visibility into all repositories.

Displays:

- Repository Name
- Owner Team
- Last Activity Date
- Risk Score
- Risk Level

**Repository Insights**

Provides detailed analysis for a repository.

Displays:

- Risk Score
- Risk Reasons
- Recommendations

Example:Assign an owner team

Increase contributor coverage

Review repository activity

**Dashboard**

Provides:

- Total Repositories
- High Risk Repositories
- Medium Risk Repositories
- Low Risk Repositories
- Unowned Repositories

**API Endpoints**

**Import Repositories**

HTTP

POST /api/import

Imports repository information from GitHub.

**Dashboard Summary**

HTTP

GET /api/dashboard

**Repository Inventory**

HTTP

GET /api/repositories

**High Risk Repositories**

HTTP

GET /api/repositories/high-risk

**Unowned Repositories**

HTTP

GET /api/repositories/unowned

**Repository Insights**

HTTP

GET /api/repositories/{id}/insights

**Testing**

Unit tests have been implemented for the Risk Assessment Engine.

Scenarios covered:

- High Risk repository
- Medium Risk repository
- Low Risk repository
- Missing ownership
- Contributor coverage

**Trade-Offs**

**SQL Server vs Graph Database**

**Chosen**

SQL Server

**Reason**

The selected scope focuses on repository ownership and maintenance data, which is highly relational.

**Alternative**

Neo4j Graph Database

**Trade-Off**

Neo4j would become more appropriate if repository dependencies and workflow relationships became part of the solution scope.

**GitHub vs Azure DevOps**

**Chosen**

GitHub

**Reason**

Public APIs allowed rapid access to real repository information and enabled focus on insight generation rather than authentication and enterprise setup.

**Rule-Based Risk Scoring**

**Chosen**

Rule-based classification

**Reason**

Simple, explainable, and transparent.

**Alternative**

Machine learning based classification.

**Future Enhancements**

**Phase 2 - Workflow Reliability**

- GitHub Actions integration
- Azure DevOps Pipeline integration
- Build success rate analysis
- Failure trend analysis

**Phase 3 - Dependency Analysis**

Model relationships between:

Team

↓

Repository

↓

Workflow

↓

System

Potential technologies:

- Neo4j
- Azure Cosmos DB (Gremlin)

**Phase 4 - Security Governance**

- Repository permissions
- Service connections
- Credential governance
- Secrets detection
- Access reviews

**Screenshots**

**Dashboard**

See:docs/Screenshots/Dashboard.png

**Repository Inventory**

See:docs/Screenshots/Repositories.png

**Swagger APIs**

See:docs/Screenshots/Swagger.png

**Running the Solution**

**API**

Shell

dotnet run --project EngineeringInsights

**UI**

Shell

dotnet run --project EngineeringInsights.UI

**Import GitHub Repository Data**

HTTP

POST /api/import

**Open Dashboard**

<https://localhost:&lt;ui-port>&gt;
