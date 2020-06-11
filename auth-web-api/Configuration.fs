namespace AuthWebApi

open IdentityModel
open IdentityServer4
open IdentityServer4.Models

module Configuration =

    let getClients () =
        [ 
            Client(
                ClientId = "client_id",
                ClientSecrets = [|Secret ("client_secret".ToSha256()) |],
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = [| "api" |]
            )
        ]

    let getApiResources() =
        [
            ApiResource("api")
        ]

    let getIdentityResources() =
        [
            IdentityResources.OpenId () :> IdentityResource
            IdentityResources.Profile () :> IdentityResource
        ]