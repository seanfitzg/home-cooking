# Home Cooking

A full-stack React -> ASP.Net Core demo application.

The front end React app is here: https://github.com/seanfitzg/home-cooking-react

Deployed to: https://flux-home-cooking.herokuapp.com/

This app uses:

- .Net Core 5.0
- Dapr - [Distributed Application Runtime](https://dapr.io/)
- Docker
- Entity Framework (for SqlLite).
- [Auth0](https://auth0.com/) to enable username-based authentication. This provides authentication & authorisation on our endpoint via Json Web Tokens.
- SqlLite - (very) simple filed-based SQL DB.
- [MediatR](https://github.com/jbogard/MediatR) - in-process messaging service.
