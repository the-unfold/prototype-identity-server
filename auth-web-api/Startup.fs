namespace AuthWebApi

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Identity
open Microsoft.AspNetCore.Identity.EntityFrameworkCore
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.EntityFrameworkCore

open AuthWebApi.Data
open AuthWebApi.Configuration

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        services.AddDbContext<ApplicationDbContext>(fun config -> 
            config.UseInMemoryDatabase("MEMORY")|> ignore
        )
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            |> ignore

        
        services.ConfigureApplicationCookie(fun config ->
            config.LoginPath <- PathString "/auth/login"
            config.LogoutPath <- PathString "/auth/logout"
            config.Cookie.Name <- "authcookie") |> ignore

        // Add framework services.
        services.AddIdentityServer()
            .AddAspNetIdentity<IdentityUser>()
            .AddInMemoryClients(GetClients())
            .AddInMemoryApiResources(GetApiResources())
            //.AddPersistedGrantStore()
            .AddInMemoryIdentityResources(GetIdentityResources())
            .AddProfileService<ProfileService>()
            .AddDeveloperSigningCredential() |> ignore

        services.AddControllers();

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseRouting () |> ignore

        app.UseIdentityServer () |> ignore

        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
