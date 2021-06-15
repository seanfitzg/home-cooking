# home-cooking
This is an ongoing, albeit fairly basic, demo application I'm building to learn/investigate some of the skills and technologies required to build a full stack React -> ASP.Net Core application deployed to Azure.

So far, this app uses:

- .Net Core 3.1
- Dapr - [Distributed Application Runtime](https://dapr.io/)
- Entity Framework (for SqlLite).
- [Auth0](https://auth0.com/) to enable username-based authentication.  This provides authentication & authorisation on our endpoint via Json Web Tokens.
- SqlLite - (very) simple filed-based SQL DB.

Coming soon hopefully:
- [MediatR](https://github.com/jbogard/MediatR) - in-process messaging service from Jimmy Bogard.


