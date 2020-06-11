namespace AuthWebApi

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Identity
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection

open AuthWebApi.Data
open AuthWebApi.Configuration

module Program =
    let configureServices (services: IServiceCollection): unit =
        services.AddIdentityServer()
            .AddInMemoryClients(getClients ())
            .AddInMemoryApiResources(getApiResources ()) 
            .AddInMemoryIdentityResources(getIdentityResources ())
            .AddDeveloperSigningCredential() |> ignore
            
        services.AddControllers () |> ignore

    let configureApp (context: WebHostBuilderContext) (app: IApplicationBuilder): unit =
        if context.HostingEnvironment.IsDevelopment ()
        then app.UseDeveloperExceptionPage () |> ignore

        app.UseRouting() |> ignore

        app.UseIdentityServer() |> ignore

        app.UseEndpoints(fun endpoints -> 
            endpoints.MapControllers () |> ignore
        ) |> ignore

    [<EntryPoint>]
    let main args =
        Host.CreateDefaultBuilder(args)        
            .ConfigureWebHostDefaults(fun webBuilder -> 
                webBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices) |> ignore)
            .Build()
            .Run()

        0
