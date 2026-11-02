# Future Roadmap

## Phase 2 – Workflow Reliability

Additional integrations:

- GitHub Actions
- Azure DevOps Pipelines

New insights:

- Failure rate trends
- Build success rate
- Build duration analysis
- Mean recovery time

---

## Phase 3 – Dependency Analysis

Introduce relationships between:

Team
↓
Repository
↓
Workflow
↓
System

Possible technologies:

- Neo4j
- Azure Cosmos DB (Graph API)

New insights:

- Impact analysis
- Dependency visualization
- Change propagation analysis

---

## Phase 4 – Security Governance

Add governance features:

- Repository permissions analysis
- Service connection governance
- Credential governance
- Secrets detection

New insights:

- Excessive permissions
- Ownership gaps
- Security risks

---

## Phase 5 – Event-Driven Collection

Replace scheduled synchronization with:

GitHub Webhooks
↓
Azure Functions
↓
Event Processing

Benefits:

- Near real-time updates
- Reduced API usage
- Improved scalability
