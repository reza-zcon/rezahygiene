//This project uses Duende Software and parts of their templates (no affiliation with Wrapt or Craftsman).
//Please see the Duende Licensing information at https://duendesoftware.com/
namespace Reza.AuthServer;

using Duende.IdentityServer.Models;
using System.Collections.Generic;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    // Api Resource is your Api as a whole. 
    // Api with multiple endpoints has a name that clients can ask for
    // we can have an api with various resources
    // have multiple scopes for the api (e.g. full_access, readr_only
    // when someone asks for a token for one of these scopes, which claims should be included in the scope
    // You can add these `UserClaims` to the Api level so that, regardless of which scope you are asking for, they will be included (e.g. name, email)
    // You can then add specific claims that will be included when requesting that particular scope
    // ***Be aware, that scopes are purely for authorizing clients, not users.**
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("recipe_management", "Recipe Management")
            {
                Scopes = { "countries.readonly", "countries.fullaccess", "currencies.readonly", "currencies.fullaccess" },
                ApiSecrets = { new Secret("4653f605-2b36-43eb-bbef-a93480079f20".Sha256()) },
                UserClaims = { "openid", "profile" },
            },
        };
    
    // allow access to identity information. client level rules of who can access what (e.g. read:sample, read:order, create:order, read:report)
    // this will be in the audience claim and will be checked by the jwt middleware to grant access or not
    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("countries.readonly", "Countries - Read Only", new[] { "recipes.read" }),
            new ApiScope("countries.fullaccess", "Countries - Full Access"),
            new ApiScope("currencies.readonly", "Currencies - Read Only", new[] { "recipes.read" }),
            new ApiScope("currencies.fullaccess", "Currencies - Full Access"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
                new Client
            {
                ClientId = "ops.swagger",
                ClientName = "RM Swagger",
                ClientSecrets = { new Secret("974d6f71-d41b-4601-9a7a-a33081f80687".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:5375/swagger/oauth2-redirect.html"},
                PostLogoutRedirectUris = {"http://localhost:5375/"},
                FrontChannelLogoutUri = "http://localhost:5375/signout-oidc",
                AllowedCorsOrigins = {"https://localhost:5375"},

                AllowOfflineAccess = true,
                RequirePkce = true,
                RequireClientSecret = true,

                AllowedScopes = { "countries.readonly", "countries.fullaccess", "currencies.readonly", "currencies.fullaccess", "openid", "profile" }
            },
        };
}