namespace AuthWebApi

open System.Security.Claims
open System.Threading.Tasks
open IdentityModel
open IdentityServer4.Models
open IdentityServer4.Services

type ProfileService () =
    interface IProfileService with
    
        member __.GetProfileDataAsync (context: ProfileDataRequestContext) : Task =
            let claims = [
                Claim (ClaimTypes.DateOfBirth, "01.01.2001")
            ]

            context.IssuedClaims.AddRange claims

            Task.CompletedTask

        member __.IsActiveAsync (context: IsActiveContext): Task =
            context.IsActive <- true;
            Task.CompletedTask

