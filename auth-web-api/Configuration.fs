namespace AuthWebApi

open IdentityModel
open IdentityServer4
open IdentityServer4.Models
open System

module Configuration =

    let getClients () =
        Console.WriteLine("Get Clients") 
        [ 
            Client(
                
                ClientId = "client_id",
                ClientName = "test client",
                AccessTokenType = AccessTokenType.Jwt,
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