namespace AuthWebApi

open IdentityModel
open IdentityServer4
open IdentityServer4.Models

module Configuration =

    let GetClients () =
        [ 
            Client(
                ClientName = "client_name",
                ClientId = "client_id",
                ClientSecrets = [| Secret("client_secret".ToSha256())|],
                Enabled = true,
                AccessTokenType = AccessTokenType.Jwt,
                AllowedScopes = [|
                    IdentityServerConstants.StandardScopes.OpenId
                    IdentityServerConstants.StandardScopes.Profile
                |],
                AllowedGrantTypes = GrantTypes.ClientCredentials
            )
        ]

    let GetApiResources() =
        [
            ApiResource("api")
        ]

    let GetIdentityResources() =
        [
            IdentityResources.OpenId () :> IdentityResource
            IdentityResources.Profile () :> IdentityResource
        ]