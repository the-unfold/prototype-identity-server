namespace AuthWebApi.Data

open System
open Microsoft.AspNetCore.Identity.EntityFrameworkCore
open Microsoft.EntityFrameworkCore
open System.IdentityModel
open Microsoft.AspNetCore.Identity
open Microsoft.Extensions.DependencyInjection
open System.Security.Claims


type ApplicationDbContext (options: DbContextOptions<ApplicationDbContext>) =
    inherit IdentityDbContext(options)

module DatabaseInitializer =
    let InitDatabase (scopeServiceProvider: IServiceProvider): unit =
        let userManager = scopeServiceProvider.GetService<UserManager<IdentityUser>>()

        let user = IdentityUser (UserName = "User")

        let result = userManager.CreateAsync( user, "user123").GetAwaiter().GetResult()
        ()

        